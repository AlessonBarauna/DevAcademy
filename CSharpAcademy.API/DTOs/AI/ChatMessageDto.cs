namespace CSharpAcademy.API.DTOs.AI;

public class ChatMessageDto
{
    public int Id { get; set; }
    public string Pergunta { get; set; } = string.Empty;
    public string Resposta { get; set; } = string.Empty;
    public int? Estrelas { get; set; }
    public DateTime Data { get; set; }
    public bool UsouCache { get; set; }
}
