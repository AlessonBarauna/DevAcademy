using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Domain.AI;

namespace CSharpAcademy.API.Application.Services.AI;

public class PromptBuilder : IPromptBuilder
{
    public string ConstruirPromptAssistente(
        Usuario usuario,
        Licao licaoAtual,
        string perguntaUsuario,
        List<ChatMessage> historico)
    {
        var nivelTexto = usuario.NivelAtual switch
        {
            1 => "Iniciante — use exemplos simples, evite jargões, explique termos técnicos",
            2 => "Intermediário — aprofunde conceitos, mencione boas práticas e padrões",
            3 => "Avançado — discuta arquitetura, SOLID, patterns avançados",
            4 => "Especialista — DDD, CQRS, microserviços, performance e trade-offs",
            _ => "Intermediário"
        };

        var tamanhoResposta = usuario.NivelAtual switch
        {
            1 => "Resposta curta (até 200 palavras)",
            2 => "Resposta média (200–400 palavras)",
            3 => "Resposta detalhada (400–600 palavras)",
            4 => "Resposta muito detalhada (600+ palavras com código)",
            _ => "Resposta normal"
        };

        var historicoTxt = historico.Count == 0
            ? "(sem histórico prévio)"
            : string.Join("\n", historico.TakeLast(5).Select(m =>
                $"Aluno: {m.PerguntaUsuario}\nProfessor: {m.RespostaAssistente}"));

        var moduloTitulo = licaoAtual.Modulo?.Titulo ?? "Módulo desconhecido";

        return $"""
            Você é um professor especialista em C# e .NET.

            === PERFIL DO ALUNO ===
            Nome: {usuario.Nome}
            Nível: {usuario.NivelAtual} ({nivelTexto})
            XP Total: {usuario.XP}

            === LIÇÃO ATUAL ===
            Módulo: {moduloTitulo}
            Lição: {licaoAtual.Titulo}
            Descrição: {licaoAtual.Descricao}
            Conteúdo:
            {licaoAtual.ConteudoTeoricoMarkdown}

            === REGRAS ===
            1. Responda APENAS perguntas sobre C# e .NET
            2. NÃO forneça respostas prontas de exercícios
            3. NÃO produza código malicioso ou para fins ilícitos
            4. {tamanhoResposta}
            5. Use exemplos práticos quando possível
            6. Formate o código em blocos markdown ```csharp

            === HISTÓRICO DA CONVERSA ===
            {historicoTxt}

            === PERGUNTA DO ALUNO ===
            {perguntaUsuario}

            === SUA RESPOSTA ===
            """;
    }
}
