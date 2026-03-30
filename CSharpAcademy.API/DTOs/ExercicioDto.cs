namespace CSharpAcademy.API.DTOs;

public class ExercicioDto
{
    public int Id { get; set; }
    public int LicaoId { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string OpcoesJson { get; set; } = "[]";
    public int Ordem { get; set; }
    public int XpRecompensa { get; set; }
}

public class ResponderExercicioDto
{
    public string Resposta { get; set; } = string.Empty;
}
