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

    public Usuario Usuario { get; set; } = null!;
    public Licao Licao { get; set; } = null!;
}
