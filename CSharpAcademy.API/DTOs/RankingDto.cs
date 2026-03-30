namespace CSharpAcademy.API.DTOs;

public class RankingItemDto
{
    public int Posicao { get; set; }
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Xp { get; set; }
    public int NivelAtual { get; set; }
    public int StreakAtual { get; set; }
    public bool EuMesmo { get; set; }
}
