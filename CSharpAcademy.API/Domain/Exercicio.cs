namespace CSharpAcademy.API.Domain;

public class Exercicio
{
    public int Id { get; set; }
    public int LicaoId { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public string RespostaCorreta { get; set; } = string.Empty;
    public string Explicacao { get; set; } = string.Empty;
    public TipoExercicio Tipo { get; set; } = TipoExercicio.MultiplaEscolha;
    public string OpcoesJson { get; set; } = "[]"; // JSON array
    public string? DicaTexto { get; set; }          // dica exibida após resposta errada
    public int Ordem { get; set; }
    public int XPRecompensa { get; set; } = 5;

    public Licao Licao { get; set; } = null!;
    public ICollection<RespostaUsuario> RespostasUsuarios { get; set; } = [];
}
