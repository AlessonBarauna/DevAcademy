using CSharpAcademy.API.DTOs.AI;

namespace CSharpAcademy.API.Application.Services.AI;

public interface IAssistantService
{
    Task<ChatResponseDto> ResponderPerguntaAsync(int usuarioId, ChatRequestDto request);
    Task<List<ChatMessageDto>> GetHistoricoAsync(int usuarioId, int licaoId, int pagina = 1);
    Task<bool> AvaliarRespostaAsync(int idMensagem, FeedbackDto feedback);
    Task<CustomExerciseDto> GerarExercicioCustomizadoAsync(int usuarioId, int licaoId, string topicoPergunta);
}
