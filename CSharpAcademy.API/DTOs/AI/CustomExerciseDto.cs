namespace CSharpAcademy.API.DTOs.AI;

public class CustomExerciseDto
{
    public string Enunciado { get; set; } = string.Empty;
    public string Tipo { get; set; } = "MultiplaEscolha";
    public List<string> Opcoes { get; set; } = [];
    public string RespostaCorreta { get; set; } = string.Empty;
    public string Explicacao { get; set; } = string.Empty;
}

public class GerarExercicioRequestDto
{
    public int LicaoId { get; set; }
    public string TopicoPergunta { get; set; } = string.Empty;
}
