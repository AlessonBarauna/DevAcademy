namespace CSharpAcademy.API.DTOs.AI;

public class ChatResponseDto
{
    public int IdMensagem { get; set; }
    public string? Resposta { get; set; }
    public bool Sucesso { get; set; }
    public string? Mensagem { get; set; }
    public string? Tipo { get; set; }
    public bool UsouCache { get; set; }
    public string Idioma { get; set; } = "pt-BR";
    public bool SugerirExercicio { get; set; }
}
