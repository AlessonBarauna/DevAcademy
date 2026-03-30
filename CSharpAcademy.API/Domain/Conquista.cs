namespace CSharpAcademy.API.Domain;

public class Conquista
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Codigo { get; set; } = string.Empty;  // ex: "PRIMEIRA_LICAO"
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;   // emoji
    public DateTime DataConquista { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; } = null!;
}
