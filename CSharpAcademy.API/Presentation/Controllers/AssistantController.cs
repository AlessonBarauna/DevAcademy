using System.Security.Claims;
using CSharpAcademy.API.Application.Services.AI;
using CSharpAcademy.API.DTOs.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssistantController(IAssistantService assistantService) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Faz uma pergunta ao Professor Assistente</summary>
    [HttpPost("perguntar")]
    public async Task<IActionResult> Perguntar([FromBody] ChatRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Pergunta))
            return BadRequest(new { mensagem = "A pergunta não pode ser vazia." });

        var resposta = await assistantService.ResponderPerguntaAsync(UsuarioId, request);
        return resposta.Sucesso ? Ok(resposta) : BadRequest(resposta);
    }

    /// <summary>Retorna histórico de conversa de uma lição</summary>
    [HttpGet("historico/{licaoId:int}")]
    public async Task<IActionResult> GetHistorico(int licaoId, [FromQuery] int pagina = 1)
    {
        var historico = await assistantService.GetHistoricoAsync(UsuarioId, licaoId, pagina);
        return Ok(historico);
    }

    /// <summary>Avalia uma resposta do assistente (1–5 estrelas)</summary>
    [HttpPost("{idMensagem:int}/avaliar")]
    public async Task<IActionResult> AvaliarResposta(int idMensagem, [FromBody] FeedbackDto feedback)
    {
        if (feedback.Estrelas is < 1 or > 5)
            return BadRequest(new { mensagem = "Avaliação deve ser entre 1 e 5 estrelas." });

        var resultado = await assistantService.AvaliarRespostaAsync(idMensagem, feedback);
        return resultado ? Ok() : NotFound();
    }

    /// <summary>Gera um exercício customizado por IA</summary>
    [HttpPost("gerar-exercicio")]
    public async Task<IActionResult> GerarExercicio([FromBody] GerarExercicioRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.TopicoPergunta))
            return BadRequest(new { mensagem = "Informe o tópico para gerar o exercício." });

        var exercicio = await assistantService.GerarExercicioCustomizadoAsync(
            UsuarioId, request.LicaoId, request.TopicoPergunta);

        return Ok(exercicio);
    }
}
