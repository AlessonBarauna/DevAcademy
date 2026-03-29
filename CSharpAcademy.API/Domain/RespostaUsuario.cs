namespace CSharpAcademy.API.Domain;

public class RespostaUsuario
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int ExercicioId { get; set; }
    public string Resposta { get; set; } = string.Empty;
    public bool Correta { get; set; }
    public DateTime DataResposta { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; } = null!;
    public Exercicio Exercicio { get; set; } = null!;
}
