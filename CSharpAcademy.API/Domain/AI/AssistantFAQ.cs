namespace CSharpAcademy.API.Domain.AI;

public class AssistantFAQ
{
    public int Id { get; set; }
    public int ModuloId { get; set; }
    public int? LicaoId { get; set; }

    public string Pergunta { get; set; } = string.Empty;
    public string Resposta { get; set; } = string.Empty;
    public string Idioma { get; set; } = "pt-BR";
    public NivelDificuldade NivelMinimo { get; set; } = NivelDificuldade.Iniciante;

    public int TotalUsos { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataUltimaAtualizacao { get; set; }
    public bool Ativa { get; set; } = true;

    public Modulo Modulo { get; set; } = null!;
    public Licao? Licao { get; set; }
}
