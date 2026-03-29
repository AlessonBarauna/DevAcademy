using System.Diagnostics;
using System.Text.Json;
using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Domain.AI;
using CSharpAcademy.API.DTOs.AI;
using CSharpAcademy.API.Infrastructure.Repositories;

namespace CSharpAcademy.API.Application.Services.AI;

public class AssistantService(
    IMistralService mistral,
    IPromptBuilder promptBuilder,
    IChatMessageRepository chatRepo,
    IAssistantFAQRepository faqRepo,
    IAssistantFeedbackRepository feedbackRepo,
    IUsuarioRepository usuarioRepo,
    ILicaoRepository licaoRepo,
    ILogger<AssistantService> logger) : IAssistantService
{
    public async Task<ChatResponseDto> ResponderPerguntaAsync(int usuarioId, ChatRequestDto request)
    {
        try
        {
            // 1. Validar segurança
            var perguntaValida = await mistral.ValidarRespostaSeguraAsync(request.Pergunta);
            if (perguntaValida == null)
                return new ChatResponseDto { Sucesso = false, Mensagem = "Desculpe, não posso responder essa pergunta.", Tipo = "erro_seguranca" };

            // 2. Carregar contexto via repositories (sem AppDbContext direto)
            var usuario = await usuarioRepo.ObterPorIdAsync(usuarioId);
            if (usuario == null)
                return new ChatResponseDto { Sucesso = false, Mensagem = "Usuário não encontrado.", Tipo = "erro_interno" };

            var licao = await licaoRepo.ObterPorIdAsync(request.LicaoId);
            if (licao == null)
                return new ChatResponseDto { Sucesso = false, Mensagem = "Lição não encontrada.", Tipo = "erro_interno" };

            // 3. Histórico das últimas 10 mensagens
            var historico = (await chatRepo.GetPorLicaoAsync(usuarioId, request.LicaoId, ultimasN: 10)).ToList();

            // 4. Verificar FAQ cache
            var faqMatch = await faqRepo.BuscarPorPerguntaSimilarAsync(
                request.Pergunta, request.LicaoId, usuario.NivelAtual, request.Idioma);

            var usouCache = false;
            string resposta;
            int tempoMs;

            if (faqMatch != null)
            {
                resposta = faqMatch.Resposta;
                usouCache = true;
                tempoMs = 5;
                faqMatch.TotalUsos++;
                await faqRepo.UpdateAsync(faqMatch);
                await faqRepo.SalvarAsync();
            }
            else
            {
                // 5. Chamar Groq/Mistral com prompt dinâmico
                var prompt = promptBuilder.ConstruirPromptAssistente(usuario, licao, request.Pergunta, historico);

                var sw = Stopwatch.StartNew();
                resposta = await mistral.ExecutarPromptAsync(prompt);
                sw.Stop();
                tempoMs = (int)sw.ElapsedMilliseconds;

                // 6. Promover para FAQ se pergunta recorrente (>= 2x)
                var vezesRepetida = historico.Count(h =>
                    h.PerguntaUsuario.Equals(request.Pergunta, StringComparison.OrdinalIgnoreCase));

                if (vezesRepetida >= 2)
                {
                    await faqRepo.AddAsync(new AssistantFAQ
                    {
                        ModuloId = licao.ModuloId,
                        LicaoId = request.LicaoId,
                        Pergunta = request.Pergunta,
                        Resposta = resposta,
                        Idioma = request.Idioma,
                        NivelMinimo = (NivelDificuldade)usuario.NivelAtual,
                        TotalUsos = 1,
                        DataCriacao = DateTime.UtcNow,
                        Ativa = true
                    });
                    await faqRepo.SalvarAsync();
                }
            }

            // 7. Salvar mensagem no histórico
            var novaMensagem = new ChatMessage
            {
                UsuarioId = usuarioId,
                ModuloId = licao.ModuloId,
                LicaoId = request.LicaoId,
                ExercicioId = request.ExercicioId,
                PerguntaUsuario = request.Pergunta,
                RespostaAssistente = resposta,
                IdiomaUsado = request.Idioma,
                NivelUsuarioNaMomentoPergunta = usuario.NivelAtual,
                DataCriacao = DateTime.UtcNow,
                UsouFAQCache = usouCache,
                TempoRespostaMs = tempoMs
            };

            await chatRepo.AddAsync(novaMensagem);
            await chatRepo.SalvarAsync();

            return new ChatResponseDto
            {
                IdMensagem = novaMensagem.Id,
                Resposta = resposta,
                Sucesso = true,
                UsouCache = usouCache,
                Idioma = request.Idioma,
                SugerirExercicio = request.Pergunta.Contains("exemplo", StringComparison.OrdinalIgnoreCase)
                                || request.Pergunta.Contains("exerc", StringComparison.OrdinalIgnoreCase)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao processar pergunta do usuário {UserId}", usuarioId);
            return new ChatResponseDto { Sucesso = false, Mensagem = "Erro ao processar pergunta.", Tipo = "erro_interno" };
        }
    }

    public async Task<List<ChatMessageDto>> GetHistoricoAsync(int usuarioId, int licaoId, int pagina = 1)
    {
        var mensagens = await chatRepo.GetPorLicaoAsync(usuarioId, licaoId, skip: (pagina - 1) * 20, take: 20);

        return mensagens.Select(m => new ChatMessageDto
        {
            Id = m.Id,
            Pergunta = m.PerguntaUsuario,
            Resposta = m.RespostaAssistente,
            Estrelas = m.AvaliacaoEstrelas,
            Data = m.DataCriacao,
            UsouCache = m.UsouFAQCache
        }).ToList();
    }

    public async Task<bool> AvaliarRespostaAsync(int idMensagem, FeedbackDto feedback)
    {
        var mensagem = await chatRepo.GetByIdAsync(idMensagem);
        if (mensagem == null) return false;

        mensagem.AvaliacaoEstrelas = feedback.Estrelas;
        mensagem.ComentarioFeedback = feedback.Comentario;
        await chatRepo.UpdateAsync(mensagem);

        await feedbackRepo.AddAsync(new AssistantFeedback
        {
            ChatMessageId = idMensagem,
            UsuarioId = feedback.UsuarioId,
            Estrelas = feedback.Estrelas,
            Comentario = feedback.Comentario,
            RespostaAjudou = feedback.RespostaAjudou,
            RespostaClara = feedback.RespostaClara,
            RespostaCompleta = feedback.RespostaCompleta,
            DataAvaliacao = DateTime.UtcNow
        });

        await feedbackRepo.SalvarAsync();
        return true;
    }

    public async Task<CustomExerciseDto> GerarExercicioCustomizadoAsync(
        int usuarioId, int licaoId, string topicoPergunta)
    {
        var usuario = await usuarioRepo.ObterPorIdAsync(usuarioId);
        var nivel = (NivelDificuldade)(usuario?.NivelAtual ?? 1);

        var jsonResposta = await mistral.GerarExercicioAsync(topicoPergunta, nivel, "pt-BR");

        try
        {
            var doc = JsonDocument.Parse(jsonResposta);
            var root = doc.RootElement;

            var opcoes = root.TryGetProperty("opcoes", out var opcoesEl)
                ? opcoesEl.EnumerateArray().Select(o => o.GetString() ?? "").ToList()
                : new List<string>();

            return new CustomExerciseDto
            {
                Enunciado = root.TryGetProperty("enunciado", out var e) ? e.GetString() ?? "" : "",
                Tipo = root.TryGetProperty("tipo", out var tp) ? tp.GetString() ?? "MultiplaEscolha" : "MultiplaEscolha",
                Opcoes = opcoes,
                RespostaCorreta = root.TryGetProperty("resposta_correta", out var rc) ? rc.GetString() ?? "" : "",
                Explicacao = root.TryGetProperty("explicacao", out var ex) ? ex.GetString() ?? "" : ""
            };
        }
        catch
        {
            return new CustomExerciseDto
            {
                Enunciado = "Não foi possível gerar o exercício. Tente novamente.",
                Tipo = "MultiplaEscolha",
                Opcoes = [],
                RespostaCorreta = "",
                Explicacao = jsonResposta
            };
        }
    }
}
