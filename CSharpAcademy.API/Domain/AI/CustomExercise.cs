namespace CSharpAcademy.API.Domain.AI;

public class CustomExercise
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int LicaoId { get; set; }
    public int ChatMessageId { get; set; }

    public string Enunciado { get; set; } = string.Empty;
    public string RespostaCorreta { get; set; } = string.Empty;
    public string Explicacao { get; set; } = string.Empty;
    public TipoExercicio Tipo { get; set; } = TipoExercicio.MultiplaEscolha;

    public bool Completado { get; set; }
    public DateTime DataGeracao { get; set; } = DateTime.UtcNow;
    public DateTime? DataCompletacao { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public Licao Licao { get; set; } = null!;
}
