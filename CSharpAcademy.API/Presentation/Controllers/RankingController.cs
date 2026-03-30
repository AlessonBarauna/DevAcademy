using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RankingController(IUsuarioRepository usuarioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Retorna o top 10 de usuários por XP</summary>
    [HttpGet]
    public async Task<IActionResult> ObterRanking()
    {
        var usuarios = (await usuarioRepo.ObterRankingAsync(10)).ToList();

        var resultado = usuarios.Select((u, idx) => new RankingItemDto
        {
            Posicao = idx + 1,
            Id = u.Id,
            Nome = u.Nome,
            Xp = u.XP,
            NivelAtual = u.NivelAtual,
            StreakAtual = u.StreakAtual,
            EuMesmo = u.Id == UsuarioId
        });

        return Ok(resultado);
    }
}
