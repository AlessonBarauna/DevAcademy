using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/desafio")]
[Authorize]
public class DesafioController(IExercicioRepository exercicioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Retorna exercícios aleatórios de lições já concluídas pelo usuário</summary>
    [HttpGet("rapido")]
    public async Task<IActionResult> Rapido([FromQuery] int quantidade = 5)
    {
        quantidade = Math.Clamp(quantidade, 1, 10);
        var exercicios = await exercicioRepo.ObterAleatoriosDeConcluidasAsync(UsuarioId, quantidade);

        if (!exercicios.Any())
            return Ok(new { semExercicios = true, exercicios = Array.Empty<ExercicioDto>() });

        var resultado = exercicios.Select(e => new ExercicioDto
        {
            Id = e.Id,
            LicaoId = e.LicaoId,
            Enunciado = e.Enunciado,
            Tipo = e.Tipo.ToString(),
            OpcoesJson = e.OpcoesJson,
            DicaTexto = e.DicaTexto,
            Ordem = e.Ordem,
            XpRecompensa = e.XPRecompensa
        });

        return Ok(new { semExercicios = false, exercicios = resultado });
    }
}
