using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ModuloController(
    IModuloRepository moduloRepo,
    IProgressoRepository progressoRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Lista todos os módulos com progresso do usuário</summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObterTodos()
    {
        var licoesConcluidas = (await progressoRepo.ObterLicoesConcluidasAsync(UsuarioId)).ToList();
        var modulos = await moduloRepo.ObterTodosAsync();

        // Pré-computa quantas lições cada módulo tem concluídas (para calcular desbloqueio)
        var concluidasPorModulo = modulos.ToDictionary(
            m => m.Id,
            m => m.Licoes.Count(l => licoesConcluidas.Contains(l.Id)));

        var resultado = modulos.Select(m =>
        {
            var licoesConcluídasNesteModulo = concluidasPorModulo[m.Id];
            var desbloqueado = m.PreRequisitoId == null ||
                (modulos.FirstOrDefault(r => r.Id == m.PreRequisitoId) is { } req
                 && req.Licoes.Count > 0
                 && concluidasPorModulo[req.Id] >= req.Licoes.Count);

            return new ModuloDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Descricao = m.Descricao,
                Ordem = m.Ordem,
                NivelMinimo = m.NivelMinimo.ToString(),
                TotalLicoes = m.Licoes.Count,
                LicoesCompletadas = licoesConcluídasNesteModulo,
                PreRequisitoId = m.PreRequisitoId,
                Desbloqueado = desbloqueado
            };
        });

        return Ok(resultado);
    }

    /// <summary>Obtém um módulo pelo Id</summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous] 
    public async Task<IActionResult> ObterPorId(int id)
    {
        var modulo = await moduloRepo.ObterPorIdAsync(id);
        if (modulo == null) return NotFound();

        var licoesConcluidas = (await progressoRepo.ObterLicoesConcluidasAsync(UsuarioId)).ToList();
        var licoesConcluidasNeste = modulo.Licoes.Count(l => licoesConcluidas.Contains(l.Id));

        bool desbloqueado = true;
        if (modulo.PreRequisitoId != null)
        {
            var prereq = await moduloRepo.ObterPorIdAsync(modulo.PreRequisitoId.Value);
            desbloqueado = prereq != null && prereq.Licoes.Count > 0
                && prereq.Licoes.Count(l => licoesConcluidas.Contains(l.Id)) >= prereq.Licoes.Count;
        }

        return Ok(new ModuloDto
        {
            Id = modulo.Id,
            Titulo = modulo.Titulo,
            Descricao = modulo.Descricao,
            Ordem = modulo.Ordem,
            NivelMinimo = modulo.NivelMinimo.ToString(),
            TotalLicoes = modulo.Licoes.Count,
            LicoesCompletadas = licoesConcluidasNeste,
            PreRequisitoId = modulo.PreRequisitoId,
            Desbloqueado = desbloqueado
        });
    }
}
