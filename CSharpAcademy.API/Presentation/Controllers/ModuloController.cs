using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ModuloController(AppDbContext ctx) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var progressos = await ctx.Progressos
            .Where(p => p.UsuarioId == UsuarioId && p.Completada)
            .Select(p => p.LicaoId)
            .ToListAsync();

        var modulos = await ctx.Modulos
            .Include(m => m.Licoes)
            .Where(m => m.Ativo)
            .OrderBy(m => m.Ordem)
            .ToListAsync();

        var result = modulos.Select(m => new ModuloDto
        {
            Id = m.Id,
            Titulo = m.Titulo,
            Descricao = m.Descricao,
            Ordem = m.Ordem,
            NivelMinimo = m.NivelMinimo.ToString(),
            TotalLicoes = m.Licoes.Count,
            LicoesCompletadas = m.Licoes.Count(l => progressos.Contains(l.Id))
        });

        return Ok(result);
    }

    [HttpGet("{id:int}/licoes")]
    public async Task<IActionResult> GetLicoes(int id)
    {
        var progressos = await ctx.Progressos
            .Where(p => p.UsuarioId == UsuarioId && p.Completada)
            .Select(p => p.LicaoId)
            .ToListAsync();

        var licoes = await ctx.Licoes
            .Where(l => l.ModuloId == id && l.Ativo)
            .OrderBy(l => l.Ordem)
            .ToListAsync();

        if (licoes.Count == 0) return NotFound();

        var result = licoes.Select(l => new LicaoDto
        {
            Id = l.Id,
            ModuloId = l.ModuloId,
            Titulo = l.Titulo,
            Descricao = l.Descricao,
            ConteudoTeoricoMarkdown = l.ConteudoTeoricoMarkdown,
            Ordem = l.Ordem,
            XPRecompensa = l.XPRecompensa,
            Completada = progressos.Contains(l.Id)
        });

        return Ok(result);
    }
}
