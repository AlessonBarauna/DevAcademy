using System.Security.Claims;
using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/nota")]
[Authorize]
public class NotaController(INotaLicaoRepository repo) : ControllerBase
{
    private int UsuarioId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("{licaoId:int}")]
    public async Task<IActionResult> Obter(int licaoId)
    {
        var nota = await repo.ObterPorUsuarioELicaoAsync(UsuarioId, licaoId);
        return Ok(new { conteudo = nota?.Conteudo ?? string.Empty });
    }

    [HttpPost("{licaoId:int}")]
    public async Task<IActionResult> Salvar(int licaoId, [FromBody] SalvarNotaDto dto)
    {
        var nota = await repo.ObterPorUsuarioELicaoAsync(UsuarioId, licaoId);

        if (nota is null)
        {
            nota = new NotaLicao
            {
                UsuarioId = UsuarioId,
                LicaoId = licaoId,
                Conteudo = dto.Conteudo
            };
        }
        else
        {
            nota.Conteudo = dto.Conteudo;
            nota.AtualizadoEm = DateTime.UtcNow;
        }

        await repo.SalvarAsync(nota);
        return Ok();
    }
}

public record SalvarNotaDto(string Conteudo);
