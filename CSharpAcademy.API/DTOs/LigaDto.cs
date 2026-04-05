namespace CSharpAcademy.API.DTOs;

public class LigaItemDto
{
    public int Posicao { get; set; }
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int XpSemana { get; set; }
    public int StreakAtual { get; set; }
    public bool EuMesmo { get; set; }
}

public class LigaSemanaDto
{
    public string Liga { get; set; } = string.Empty;        // "Bronze", "Prata", "Ouro", "Diamante"
    public string LigaIcone { get; set; } = string.Empty;
    public int MeuXpSemana { get; set; }
    public int MinhaPosicao { get; set; }
    public int TotalParticipantes { get; set; }
    public string SemanaInicio { get; set; } = string.Empty;
    public string SemanaFim { get; set; } = string.Empty;
    public List<LigaItemDto> Participantes { get; set; } = [];
}
