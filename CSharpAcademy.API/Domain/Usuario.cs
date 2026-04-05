namespace CSharpAcademy.API.Domain;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public int NivelAtual { get; set; } = 1; // 1-4
    public int XP { get; set; } = 0;
    public string Idioma { get; set; } = "pt-BR";
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public int StreakAtual { get; set; } = 0;
    public int StreakMaximo { get; set; } = 0;
    public DateTime? UltimoEstudo { get; set; }

    // Hearts System
    public int Vidas { get; set; } = 5;
    public DateTime? UltimoRecargaVida { get; set; }

    // Streak Freeze
    public int StreakFreeze { get; set; } = 1;

    public ICollection<Progresso> Progressos { get; set; } = [];
    public ICollection<Conquista> Conquistas { get; set; } = [];
    public ICollection<AI.ChatMessage> ChatMessages { get; set; } = [];
    public ICollection<AI.ChatSession> ChatSessions { get; set; } = [];
}
