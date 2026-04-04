using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RankingController(
    IUsuarioRepository usuarioRepo,
    IProgressoRepository progressoRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Retorna o top 10 de usuários por XP; inclui a posição do usuário autenticado se estiver fora do top</summary>
    [HttpGet]
    public async Task<IActionResult> ObterRanking()
    {
        var top10 = (await usuarioRepo.ObterRankingAsync(10)).ToList();
        var euNoTop = top10.Any(u => u.Id == UsuarioId);

        var resultado = top10.Select((u, idx) => new RankingItemDto
        {
            Posicao = idx + 1,
            Id = u.Id,
            Nome = u.Nome,
            Xp = u.XP,
            NivelAtual = u.NivelAtual,
            StreakAtual = u.StreakAtual,
            EuMesmo = u.Id == UsuarioId
        }).ToList();

        if (!euNoTop)
        {
            var posicao = await usuarioRepo.ObterPosicaoRankingAsync(UsuarioId);
            var eu = await usuarioRepo.ObterPorIdAsync(UsuarioId);
            if (eu != null && posicao > 0)
            {
                resultado.Add(new RankingItemDto
                {
                    Posicao = posicao,
                    Id = eu.Id,
                    Nome = eu.Nome,
                    Xp = eu.XP,
                    NivelAtual = eu.NivelAtual,
                    StreakAtual = eu.StreakAtual,
                    EuMesmo = true
                });
            }
        }

        return Ok(resultado);
    }

    /// <summary>Ranking por XP ganho na semana atual (segunda a domingo)</summary>
    [HttpGet("semanal")]
    public async Task<IActionResult> ObterRankingSemanal()
    {
        var xpSemanal = (await progressoRepo.ObterXpSemanalAsync())
            .OrderByDescending(x => x.XpSemanal)
            .ToList();

        if (!xpSemanal.Any()) return Ok(Array.Empty<RankingItemDto>());

        // Carrega apenas os usuários que aparecem no ranking
        var idsNoRanking = xpSemanal.Take(10).Select(x => x.UsuarioId).ToList();
        var todos = (await usuarioRepo.ObterRankingAsync(9999)).ToList();
        var mapaUsuarios = todos.ToDictionary(u => u.Id);

        var top10 = xpSemanal
            .Take(10)
            .Select((x, idx) => mapaUsuarios.TryGetValue(x.UsuarioId, out var u) ? new RankingItemDto
            {
                Posicao = idx + 1,
                Id = u.Id,
                Nome = u.Nome,
                Xp = x.XpSemanal,
                NivelAtual = u.NivelAtual,
                StreakAtual = u.StreakAtual,
                EuMesmo = u.Id == UsuarioId
            } : null)
            .Where(x => x != null)
            .ToList();

        var euNoTop = top10.Any(u => u!.EuMesmo);
        if (!euNoTop)
        {
            var meuXp = xpSemanal.FirstOrDefault(x => x.UsuarioId == UsuarioId);
            var minhaPosicao = xpSemanal.FindIndex(x => x.UsuarioId == UsuarioId) + 1;
            var eu = await usuarioRepo.ObterPorIdAsync(UsuarioId);
            if (eu != null && minhaPosicao > 0)
            {
                top10.Add(new RankingItemDto
                {
                    Posicao = minhaPosicao,
                    Id = eu.Id,
                    Nome = eu.Nome,
                    Xp = meuXp.XpSemanal,
                    NivelAtual = eu.NivelAtual,
                    StreakAtual = eu.StreakAtual,
                    EuMesmo = true
                });
            }
        }

        return Ok(top10);
    }
}
