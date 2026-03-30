using System.Security.Claims;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/modulo/{moduloId:int}/licoes")]
[Authorize]
public class LicaoController(
    ILicaoRepository licaoRepo,
    IProgressoRepository progressoRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Lista todas as lições de um módulo</summary>
    [HttpGet]
    public async Task<IActionResult> ObterPorModulo(int moduloId)
    {
        var licoes = await licaoRepo.ObterPorModuloAsync(moduloId);
        if (!licoes.Any()) return NotFound();

        var licoesConcluidas = (await progressoRepo.ObterLicoesConcluidasAsync(UsuarioId)).ToList();

        var resultado = licoes.Select(l => new LicaoDto
        {
            Id = l.Id,
            ModuloId = l.ModuloId,
            Titulo = l.Titulo,
            Descricao = l.Descricao,
            ConteudoTeoricoMarkdown = l.ConteudoTeoricoMarkdown,
            Ordem = l.Ordem,
            XPRecompensa = l.XPRecompensa,
            Completada = licoesConcluidas.Contains(l.Id)
        });

        return Ok(resultado);
    }

    /// <summary>Obtém uma lição pelo Id</summary>
    [HttpGet("{licaoId:int}")]
    public async Task<IActionResult> ObterPorId(int licaoId)
    {
        var licao = await licaoRepo.ObterPorIdAsync(licaoId);
        if (licao == null) return NotFound();

        var licoesConcluidas = (await progressoRepo.ObterLicoesConcluidasAsync(UsuarioId)).ToList();

        return Ok(new LicaoDto
        {
            Id = licao.Id,
            ModuloId = licao.ModuloId,
            Titulo = licao.Titulo,
            Descricao = licao.Descricao,
            ConteudoTeoricoMarkdown = licao.ConteudoTeoricoMarkdown,
            Ordem = licao.Ordem,
            XPRecompensa = licao.XPRecompensa,
            Completada = licoesConcluidas.Contains(licao.Id)
        });
    }

    /// <summary>Marca uma lição como concluída e concede XP ao usuário</summary>
    [HttpPost("{licaoId:int}/concluir")]
    public async Task<IActionResult> Concluir(int licaoId, [FromServices] IUsuarioRepository usuarioRepo)
    {
        var licao = await licaoRepo.ObterPorIdAsync(licaoId);
        if (licao == null) return NotFound();

        var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
        if (usuario == null) return NotFound();

        var progresso = await progressoRepo.ObterAsync(UsuarioId, licaoId);

        if (progresso != null && progresso.Completada)
            return Ok(new ConcluirLicaoResponseDto
            {
                XpGanho = 0,
                NovoNivel = usuario.NivelAtual,
                XpTotal = usuario.XP,
                JaConcluidaAntes = true
            });

        if (progresso == null)
        {
            await progressoRepo.AdicionarAsync(new Domain.Progresso
            {
                UsuarioId = UsuarioId,
                LicaoId = licaoId,
                Completada = true,
                XPGanho = licao.XPRecompensa,
                DataInicio = DateTime.UtcNow,
                DataConclusao = DateTime.UtcNow
            });
        }
        else
        {
            progresso.Completada = true;
            progresso.XPGanho = licao.XPRecompensa;
            progresso.DataConclusao = DateTime.UtcNow;
            await progressoRepo.AtualizarAsync(progresso);
        }

        usuario.XP += licao.XPRecompensa;
        usuario.NivelAtual = usuario.XP switch
        {
            < 100 => 1,
            < 300 => 2,
            < 700 => 3,
            _ => 4
        };
        await usuarioRepo.AtualizarAsync(usuario);
        await progressoRepo.SalvarAsync();

        return Ok(new ConcluirLicaoResponseDto
        {
            XpGanho = licao.XPRecompensa,
            NovoNivel = usuario.NivelAtual,
            XpTotal = usuario.XP,
            JaConcluidaAntes = false
        });
    }
}
