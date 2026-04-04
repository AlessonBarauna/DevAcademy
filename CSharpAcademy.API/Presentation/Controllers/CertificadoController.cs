using System.Security.Claims;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/certificado")]
[Authorize]
public class CertificadoController(
    IModuloRepository moduloRepo,
    IProgressoRepository progressoRepo,
    IUsuarioRepository usuarioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Retorna dados do certificado se o módulo estiver 100% concluído</summary>
    [HttpGet("{moduloId:int}")]
    public async Task<IActionResult> ObterCertificado(int moduloId)
    {
        var modulo = await moduloRepo.ObterPorIdAsync(moduloId);
        if (modulo == null) return NotFound();

        var licoesConcluidas = (await progressoRepo.ObterLicoesConcluidasAsync(UsuarioId)).ToList();
        var totalLicoes = modulo.Licoes.Count;
        var concluidas = modulo.Licoes.Count(l => licoesConcluidas.Contains(l.Id));

        if (concluidas < totalLicoes || totalLicoes == 0)
            return BadRequest(new { mensagem = "Módulo não concluído.", percentual = totalLicoes > 0 ? concluidas * 100 / totalLicoes : 0 });

        var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
        var xpTotal = modulo.Licoes.Sum(l => l.XPRecompensa);

        // Data da última lição concluída como data do certificado
        var progressos = await Task.WhenAll(modulo.Licoes.Select(l => progressoRepo.ObterAsync(UsuarioId, l.Id)));
        var dataConclusao = progressos
            .Where(p => p?.DataConclusao != null)
            .Max(p => p!.DataConclusao)
            ?? DateTime.UtcNow;

        return Ok(new
        {
            nomeAluno = usuario?.Nome ?? string.Empty,
            moduloTitulo = modulo.Titulo,
            moduloDescricao = modulo.Descricao,
            nivel = modulo.NivelMinimo.ToString(),
            totalLicoes,
            xpGanho = xpTotal,
            dataConclusao = dataConclusao.ToString("dd/MM/yyyy"),
            emitidoEm = DateTime.UtcNow.ToString("dd/MM/yyyy")
        });
    }
}
