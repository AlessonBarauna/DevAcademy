using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Domain.AI;

namespace CSharpAcademy.API.Application.Services.AI;

public interface IPromptBuilder
{
    string ConstruirPromptAssistente(
        Usuario usuario,
        Licao licaoAtual,
        string perguntaUsuario,
        List<ChatMessage> historico);
}
