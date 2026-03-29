namespace CSharpAcademy.API.Domain.AI;

public class AssistantFeedback
{
    public int Id { get; set; }
    public int ChatMessageId { get; set; }
    public int UsuarioId { get; set; }

    public int Estrelas { get; set; }
    public string? Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;

    public bool RespostaAjudou { get; set; }
    public bool RespostaClara { get; set; }
    public bool RespostaCompleta { get; set; }

    public ChatMessage ChatMessage { get; set; } = null!;
    public Usuario Usuario { get; set; } = null!;
}
