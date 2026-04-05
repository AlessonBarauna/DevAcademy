using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LigaController(
    IUsuarioRepository usuarioRepo,
    IProgressoRepository progressoRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private static (string Nome, string Icone) ObterLiga(int nivel) => nivel switch
    {
        1 => ("Bronze", "🥉"),
        2 => ("Prata", "🥈"),
        3 => ("Ouro", "🥇"),
        _ => ("Diamante", "💎")
    };

    /// <summary>Retorna o leaderboard semanal da liga do usuário autenticado (mesmo NivelAtual)</summary>
    [HttpGet("semana")]
    public async Task<IActionResult> ObterLigaSemana()
    {
        var eu = await usuarioRepo.ObterPorIdAsync(UsuarioId);
        if (eu is null) return NotFound();

        var (ligaNome, ligaIcone) = ObterLiga(eu.NivelAtual);

        // XP ganho esta semana por todos os usuários
        var xpSemanal = (await progressoRepo.ObterXpSemanalAsync()).ToDictionary(x => x.UsuarioId, x => x.XpSemanal);

        // Todos os usuários da mesma liga
        var todosUsuarios = (await usuarioRepo.ObterRankingAsync(9999)).ToList();
        var mesmaTier = todosUsuarios.Where(u => u.NivelAtual == eu.NivelAtual).ToList();

        // Ordena por XP semanal desc, depois por XP total como desempate
        var ordenados = mesmaTier
            .Select(u => new
            {
                u.Id,
                u.Nome,
                u.StreakAtual,
                XpSemana = xpSemanal.TryGetValue(u.Id, out var xp) ? xp : 0
            })
            .OrderByDescending(u => u.XpSemana)
            .ThenByDescending(u => u.Id)
            .ToList();

        var participantes = ordenados
            .Take(15)
            .Select((u, idx) => new LigaItemDto
            {
                Posicao = idx + 1,
                Id = u.Id,
                Nome = u.Nome,
                XpSemana = u.XpSemana,
                StreakAtual = u.StreakAtual,
                EuMesmo = u.Id == UsuarioId
            }).ToList();

        var euNoTop = participantes.Any(p => p.EuMesmo);
        if (!euNoTop)
        {
            var minhaPos = ordenados.FindIndex(u => u.Id == UsuarioId) + 1;
            var meuItem = ordenados.FirstOrDefault(u => u.Id == UsuarioId);
            if (meuItem is not null)
                participantes.Add(new LigaItemDto
                {
                    Posicao = minhaPos,
                    Id = eu.Id,
                    Nome = eu.Nome,
                    XpSemana = meuItem.XpSemana,
                    StreakAtual = eu.StreakAtual,
                    EuMesmo = true
                });
        }

        // Calcula semana (segunda → domingo)
        var hoje = DateTime.UtcNow.Date;
        var diasDesdeSegunda = ((int)hoje.DayOfWeek + 6) % 7;
        var inicioSemana = hoje.AddDays(-diasDesdeSegunda);
        var fimSemana = inicioSemana.AddDays(6);

        var meuXp = xpSemanal.TryGetValue(UsuarioId, out var xpMeu) ? xpMeu : 0;
        var minhaPosicao = ordenados.FindIndex(u => u.Id == UsuarioId) + 1;

        return Ok(new LigaSemanaDto
        {
            Liga = ligaNome,
            LigaIcone = ligaIcone,
            MeuXpSemana = meuXp,
            MinhaPosicao = minhaPosicao > 0 ? minhaPosicao : ordenados.Count + 1,
            TotalParticipantes = mesmaTier.Count,
            SemanaInicio = inicioSemana.ToString("dd/MM"),
            SemanaFim = fimSemana.ToString("dd/MM"),
            Participantes = participantes
        });
    }
}
