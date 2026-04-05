using System.Security.Claims;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/metas")]
[Authorize]
public class MetasController(IMissaoRepository missaoRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Retorna o progresso real da semana atual (seg-dom)</summary>
    [HttpGet("semana")]
    public async Task<IActionResult> ObterSemana()
    {
        var licoes     = await missaoRepo.ContarLicoesConcluidasNaSemanaAsync(UsuarioId);
        var xp         = await missaoRepo.SomarXpGanhoNaSemanaAsync(UsuarioId);
        var exercicios = await missaoRepo.ContarExerciciosCorretosNaSemanaAsync(UsuarioId);

        var hoje = DateTime.UtcNow.Date;
        var diasDesdeSegunda = ((int)hoje.DayOfWeek + 6) % 7;
        var inicioSemana = hoje.AddDays(-diasDesdeSegunda);
        var fimSemana    = inicioSemana.AddDays(6);

        return Ok(new
        {
            licoesNaSemana     = licoes,
            xpNaSemana         = xp,
            exerciciosNaSemana = exercicios,
            inicioSemana       = inicioSemana.ToString("yyyy-MM-dd"),
            fimSemana          = fimSemana.ToString("yyyy-MM-dd"),
        });
    }
}
