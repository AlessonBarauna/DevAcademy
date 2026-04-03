namespace CSharpAcademy.API.Domain;

public class Progresso
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int LicaoId { get; set; }
    public bool Completada { get; set; }
    public int XPGanho { get; set; }
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataConclusao { get; set; }

    // SRS — Spaced Repetition System
    public int NivelRetencao { get; set; } = 1;        // 1-5, sobe ao acertar, cai ao errar
    public int TotalRevisoes { get; set; } = 0;
    public DateTime? ProximaRevisao { get; set; }      // null = nunca revisou ainda

    public Usuario Usuario { get; set; } = null!;
    public Licao Licao { get; set; } = null!;
}
