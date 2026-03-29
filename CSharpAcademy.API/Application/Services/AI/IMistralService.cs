using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Application.Services.AI;

public interface IMistralService
{
    Task<string> ExecutarPromptAsync(string prompt, string? modelo = null);
    Task<string> GerarExercicioAsync(string topico, NivelDificuldade nivel, string idioma);
    Task<string?> ValidarRespostaSeguraAsync(string pergunta);
}
