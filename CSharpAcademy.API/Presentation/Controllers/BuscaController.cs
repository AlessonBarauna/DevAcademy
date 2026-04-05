using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/busca")]
[Authorize]
public class BuscaController(
    IModuloRepository moduloRepo,
    ILicaoRepository licaoRepo) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Buscar([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Trim().Length < 2)
            return Ok(new { modulos = Array.Empty<object>(), licoes = Array.Empty<object>() });

        var termo = q.Trim();
        var modulosTask = moduloRepo.BuscarAsync(termo);
        var licoesTask = licaoRepo.BuscarAsync(termo);
        await Task.WhenAll(modulosTask, licoesTask);

        var modulos = modulosTask.Result.Select(m => new
        {
            id = m.Id,
            titulo = m.Titulo,
            descricao = m.Descricao,
            nivelMinimo = m.NivelMinimo
        });

        var licoes = licoesTask.Result.Select(l => new
        {
            id = l.Id,
            moduloId = l.ModuloId,
            moduloTitulo = l.Modulo?.Titulo ?? "",
            titulo = l.Titulo,
            descricao = l.Descricao
        });

        return Ok(new { modulos, licoes });
    }
}
