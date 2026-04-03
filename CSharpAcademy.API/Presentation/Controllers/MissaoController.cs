using System.Security.Claims;
using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/missoes")]
[Authorize]
public class MissaoController(
    IMissaoRepository missaoRepo,
    IUsuarioRepository usuarioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private static readonly (TipoMissao Tipo, int Meta, int XpBonus, string Descricao)[] Templates =
    [
        (TipoMissao.GanharXP,            30,  15, "Ganhe 30 XP hoje"),
        (TipoMissao.GanharXP,            60,  20, "Ganhe 60 XP hoje"),
        (TipoMissao.ConcluirLicoes,       1,  15, "Conclua 1 lição"),
        (TipoMissao.ConcluirLicoes,       2,  25, "Conclua 2 lições"),
        (TipoMissao.ExerciciosCorretos,   5,  15, "Acerte 5 exercícios"),
        (TipoMissao.ExerciciosCorretos,  10,  25, "Acerte 10 exercícios"),
    ];

    /// <summary>Retorna (ou gera) as missões do dia com progresso atual</summary>
    [HttpGet("hoje")]
    public async Task<IActionResult> ObterHoje()
    {
        var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
        var missoes = (await missaoRepo.ObterHojeAsync(UsuarioId)).ToList();

        // Geração lazy — cria 3 missões determinísticas baseadas no userId+data
        if (missoes.Count == 0)
        {
            var seed = UsuarioId * 1000 + hoje.DayOfYear + hoje.Year;
            var rng = new Random(seed);
            var selecionados = Templates.OrderBy(_ => rng.Next()).Take(3).ToList();

            missoes = selecionados.Select(t => new MissaoDiaria
            {
                UsuarioId = UsuarioId,
                Data = hoje,
                Tipo = t.Tipo,
                Meta = t.Meta,
                XpBonus = t.XpBonus,
                Concluida = false
            }).ToList();

            await missaoRepo.AdicionarRangeAsync(missoes);
            await missaoRepo.SalvarAsync();
        }

        // Calcula progresso atual uma vez
        var xpHoje = await missaoRepo.SomarXpGanhoHojeAsync(UsuarioId);
        var licoesHoje = await missaoRepo.ContarLicoesConcluidasHojeAsync(UsuarioId);
        var exerciciosHoje = await missaoRepo.ContarExerciciosCorretosHojeAsync(UsuarioId);

        var resultado = new List<object>();
        var xpBonusTotal = 0;

        foreach (var m in missoes)
        {
            var progresso = m.Tipo switch
            {
                TipoMissao.GanharXP => xpHoje,
                TipoMissao.ConcluirLicoes => licoesHoje,
                TipoMissao.ExerciciosCorretos => exerciciosHoje,
                _ => 0
            };

            // Marca como concluída automaticamente
            if (!m.Concluida && progresso >= m.Meta)
            {
                m.Concluida = true;
                m.DataConclusao = DateTime.UtcNow;
                xpBonusTotal += m.XpBonus;
                await missaoRepo.AtualizarAsync(m);
            }

            resultado.Add(new
            {
                id = m.Id,
                tipo = m.Tipo.ToString(),
                descricao = DescricaoMissao(m),
                meta = m.Meta,
                progresso = Math.Min(progresso, m.Meta),
                percentual = (int)Math.Min(100, Math.Round((double)progresso / m.Meta * 100)),
                concluida = m.Concluida,
                xpBonus = m.XpBonus
            });
        }

        if (xpBonusTotal > 0)
        {
            var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
            if (usuario != null)
            {
                usuario.XP += xpBonusTotal;
                await usuarioRepo.AtualizarAsync(usuario);
            }
        }

        await missaoRepo.SalvarAsync();
        return Ok(resultado);
    }

    private static string DescricaoMissao(MissaoDiaria m) => m.Tipo switch
    {
        TipoMissao.GanharXP => $"Ganhe {m.Meta} XP hoje",
        TipoMissao.ConcluirLicoes => $"Conclua {m.Meta} lição(ões)",
        TipoMissao.ExerciciosCorretos => $"Acerte {m.Meta} exercício(s)",
        _ => string.Empty
    };
}
