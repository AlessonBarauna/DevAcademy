namespace CSharpAcademy.API.Domain.AI;

public class ChatSession
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int LicaoId { get; set; }

    public string Titulo { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataFim { get; set; }
    public bool Ativa { get; set; } = true;

    public int TotalMensagens { get; set; }
    public decimal MediaAvaliacoes { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public Licao Licao { get; set; } = null!;
    public ICollection<ChatMessage> Mensagens { get; set; } = [];
}
