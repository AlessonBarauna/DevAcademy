using System.Security.Claims;
using CSharpAcademy.API.Application.Services;
using CSharpAcademy.API.Domain;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/licao/{licaoId:int}/exercicios")]
[Authorize]
public class ExercicioController(
    IExercicioRepository exercicioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Lista todos os exercícios de uma lição</summary>
    [HttpGet]
    public async Task<IActionResult> ObterPorLicao(int licaoId)
    {
        var exercicios = await exercicioRepo.ObterPorLicaoAsync(licaoId);
        if (!exercicios.Any()) return NotFound();

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

        return Ok(resultado);
    }

    /// <summary>Obtém um exercício pelo Id (sem revelar a resposta)</summary>
    [HttpGet("{exercicioId:int}")]
    public async Task<IActionResult> ObterPorId(int exercicioId)
    {
        var exercicio = await exercicioRepo.ObterPorIdAsync(exercicioId);
        if (exercicio == null) return NotFound();

        return Ok(new ExercicioDto
        {
            Id = exercicio.Id,
            LicaoId = exercicio.LicaoId,
            Enunciado = exercicio.Enunciado,
            Tipo = exercicio.Tipo.ToString(),
            OpcoesJson = exercicio.OpcoesJson,
            Ordem = exercicio.Ordem,
            XpRecompensa = exercicio.XPRecompensa
        });
    }

    /// <summary>Verifica a resposta do usuário para um exercício</summary>
    [HttpPost("{exercicioId:int}/responder")]
    public async Task<IActionResult> Responder(
        int exercicioId,
        [FromBody] ResponderExercicioDto dto,
        [FromServices] IRespostaRepository respostaRepo,
        [FromServices] IUsuarioRepository usuarioRepo)
    {
        var exercicio = await exercicioRepo.ObterPorIdAsync(exercicioId);
        if (exercicio == null) return NotFound();

        var correta = string.Equals(
            dto.Resposta.Trim(),
            exercicio.RespostaCorreta.Trim(),
            StringComparison.OrdinalIgnoreCase);

        var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
        if (usuario != null)
        {
            VidasHelper.AplicarRecarga(usuario);
            if (!correta) VidasHelper.Deduzir(usuario);
            await usuarioRepo.AtualizarAsync(usuario);
        }

        await respostaRepo.AdicionarAsync(new RespostaUsuario
        {
            UsuarioId = UsuarioId,
            ExercicioId = exercicioId,
            Resposta = dto.Resposta,
            Correta = correta,
            DataResposta = DateTime.UtcNow
        });
        await respostaRepo.SalvarAsync();

        return Ok(new
        {
            correta,
            explicacao = correta ? null : exercicio.Explicacao,
            respostaCorreta = correta ? exercicio.RespostaCorreta : null,
            vidasRestantes = usuario?.Vidas ?? VidasHelper.MaxVidas,
            minutosParaRecarga = usuario != null ? VidasHelper.MinutosParaProximaRecarga(usuario) : 0
        });
    }
}
