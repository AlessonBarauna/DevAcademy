namespace CSharpAcademy.API.DTOs;

public class RevisaoPendenteDto
{
    public int LicaoId { get; set; }
    public string LicaoTitulo { get; set; } = string.Empty;
    public int ModuloId { get; set; }
    public int NivelRetencao { get; set; }
    public int TotalRevisoes { get; set; }
    public DateTime ProximaRevisao { get; set; }
}

public class RegistrarRevisaoDto
{
    public bool Acertou { get; set; }
}

public class RevisaoResultadoDto
{
    public int NovoNivelRetencao { get; set; }
    public DateTime ProximaRevisao { get; set; }
    public int XpGanho { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}
