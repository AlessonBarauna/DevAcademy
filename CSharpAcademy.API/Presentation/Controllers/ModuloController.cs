using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ModuloController(
    IModuloRepository moduloRepo,
    IProgressoRepository progressoRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Lista todos os módulos com progresso do usuário</summary>
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var licoesConcluidas = (await progressoRepo.ObterLicoesConcluidasAsync(UsuarioId)).ToList();
        var modulos = await moduloRepo.ObterTodosAsync();

        var resultado = modulos.Select(m => new ModuloDto
        {
            Id = m.Id,
            Titulo = m.Titulo,
            Descricao = m.Descricao,
            Ordem = m.Ordem,
            NivelMinimo = m.NivelMinimo.ToString(),
            TotalLicoes = m.Licoes.Count,
            LicoesCompletadas = m.Licoes.Count(l => licoesConcluidas.Contains(l.Id))
        });

        return Ok(resultado);
    }

    /// <summary>Obtém um módulo pelo Id</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var modulo = await moduloRepo.ObterPorIdAsync(id);
        if (modulo == null) return NotFound();

        var licoesConcluidas = (await progressoRepo.ObterLicoesConcluidasAsync(UsuarioId)).ToList();

        return Ok(new ModuloDto
        {
            Id = modulo.Id,
            Titulo = modulo.Titulo,
            Descricao = modulo.Descricao,
            Ordem = modulo.Ordem,
            NivelMinimo = modulo.NivelMinimo.ToString(),
            TotalLicoes = modulo.Licoes.Count,
            LicoesCompletadas = modulo.Licoes.Count(l => licoesConcluidas.Contains(l.Id))
        });
    }
}
