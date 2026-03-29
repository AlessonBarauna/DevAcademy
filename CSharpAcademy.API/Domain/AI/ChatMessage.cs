namespace CSharpAcademy.API.Domain.AI;

public class ChatMessage
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int? ChatSessionId { get; set; }

    public int? ModuloId { get; set; }
    public int? LicaoId { get; set; }
    public int? ExercicioId { get; set; }

    public string PerguntaUsuario { get; set; } = string.Empty;
    public string RespostaAssistente { get; set; } = string.Empty;

    public string IdiomaUsado { get; set; } = "pt-BR";
    public int NivelUsuarioNaMomentoPergunta { get; set; } = 1;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public int? AvaliacaoEstrelas { get; set; }
    public string? ComentarioFeedback { get; set; }

    public int TempoRespostaMs { get; set; }
    public bool UsouFAQCache { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public ChatSession? ChatSession { get; set; }
}
