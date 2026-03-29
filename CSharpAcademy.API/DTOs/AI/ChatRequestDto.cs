namespace CSharpAcademy.API.DTOs.AI;

public class ChatRequestDto
{
    public string Pergunta { get; set; } = string.Empty;
    public int LicaoId { get; set; }
    public int? ExercicioId { get; set; }
    public int? ModuloId { get; set; }
    public string Idioma { get; set; } = "pt-BR";
}
