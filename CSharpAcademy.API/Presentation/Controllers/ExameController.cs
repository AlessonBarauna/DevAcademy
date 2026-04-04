using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/exame")]
[Authorize]
public class ExameController(
    IExercicioRepository exercicioRepo,
    IModuloRepository moduloRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Inicia um exame do módulo: retorna até 10 exercícios aleatórios</summary>
    [HttpGet("{moduloId:int}/iniciar")]
    public async Task<IActionResult> Iniciar(int moduloId)
    {
        var modulo = await moduloRepo.ObterPorIdAsync(moduloId);
        if (modulo == null) return NotFound();

        var exercicios = await exercicioRepo.ObterAleatoriosPorModuloAsync(moduloId, 10);
        var lista = exercicios.ToList();
        if (lista.Count == 0) return NotFound(new { mensagem = "Nenhum exercício encontrado para este módulo." });

        var dto = lista.Select(e => new ExercicioDto
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

        return Ok(new
        {
            moduloId,
            titulo = modulo.Titulo,
            totalQuestoes = lista.Count,
            duracaoSegundos = 300,
            exercicios = dto
        });
    }
}
