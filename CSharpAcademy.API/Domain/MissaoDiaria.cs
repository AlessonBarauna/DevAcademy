namespace CSharpAcademy.API.Domain;

public enum TipoMissao
{
    GanharXP = 1,
    ConcluirLicoes = 2,
    ExerciciosCorretos = 3
}

public class MissaoDiaria
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateOnly Data { get; set; }
    public TipoMissao Tipo { get; set; }
    public int Meta { get; set; }
    public int XpBonus { get; set; } = 15;
    public bool Concluida { get; set; }
    public DateTime? DataConclusao { get; set; }

    public Usuario Usuario { get; set; } = null!;
}
