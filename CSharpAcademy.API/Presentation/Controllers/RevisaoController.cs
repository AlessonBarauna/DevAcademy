using System.Security.Claims;
using CSharpAcademy.API.Application.Services;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/revisao")]
[Authorize]
public class RevisaoController(
    IProgressoRepository progressoRepo,
    IUsuarioRepository usuarioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Retorna as lições com revisão pendente para hoje</summary>
    [HttpGet("pendentes")]
    public async Task<IActionResult> ObterPendentes()
    {
        var revisoes = await progressoRepo.ObterRevisoesHojeAsync(UsuarioId);
        var resultado = revisoes.Select(p => new RevisaoPendenteDto
        {
            LicaoId = p.LicaoId,
            LicaoTitulo = p.Licao.Titulo,
            ModuloId = p.Licao.ModuloId,
            NivelRetencao = p.NivelRetencao,
            TotalRevisoes = p.TotalRevisoes,
            ProximaRevisao = p.ProximaRevisao!.Value
        });

        return Ok(resultado);
    }

    /// <summary>Registra o resultado de uma revisão (acertou ou errou)</summary>
    [HttpPost("{licaoId:int}/registrar")]
    public async Task<IActionResult> Registrar(int licaoId, [FromBody] RegistrarRevisaoDto dto)
    {
        var progresso = await progressoRepo.ObterAsync(UsuarioId, licaoId);
        if (progresso == null || !progresso.Completada)
            return NotFound();

        var (novoNivel, proxima) = dto.Acertou
            ? SrsHelper.Acertou(progresso.NivelRetencao)
            : SrsHelper.Errou(progresso.NivelRetencao);

        progresso.NivelRetencao = novoNivel;
        progresso.ProximaRevisao = proxima;
        progresso.TotalRevisoes++;
        await progressoRepo.AtualizarAsync(progresso);

        // XP simbólico por revisar (metade da lição original)
        var xpGanho = 0;
        if (dto.Acertou)
        {
            var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
            if (usuario != null)
            {
                xpGanho = 5;
                usuario.XP += xpGanho;
                await usuarioRepo.AtualizarAsync(usuario);
            }
        }

        await progressoRepo.SalvarAsync();

        var intervaloDias = SrsHelper.IntervaloDias(novoNivel);
        var mensagem = dto.Acertou
            ? $"Ótimo! Próxima revisão em {intervaloDias} dia(s). Nível de retenção: {novoNivel}/5"
            : $"Continue praticando! Próxima revisão em {intervaloDias} dia(s).";

        return Ok(new RevisaoResultadoDto
        {
            NovoNivelRetencao = novoNivel,
            ProximaRevisao = proxima,
            XpGanho = xpGanho,
            Mensagem = mensagem
        });
    }
}
