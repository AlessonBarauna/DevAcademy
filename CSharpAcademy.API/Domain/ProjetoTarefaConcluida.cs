namespace CSharpAcademy.API.Domain;

public class ProjetoTarefaConcluida
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int ProjetoId { get; set; }
    public int TarefaId { get; set; }
    public DateTime DataConclusao { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; } = null!;
}
