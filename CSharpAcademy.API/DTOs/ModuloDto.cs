namespace CSharpAcademy.API.DTOs;

public class ModuloDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public string NivelMinimo { get; set; } = string.Empty;
    public int TotalLicoes { get; set; }
    public int LicoesCompletadas { get; set; }
}

public class LicaoDto
{
    public int Id { get; set; }
    public int ModuloId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string ConteudoTeoricoMarkdown { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public int XpRecompensa { get; set; }
    public bool Completada { get; set; }
}

public class ConcluirLicaoResponseDto
{
    public int XpGanho { get; set; }
    public int NovoNivel { get; set; }
    public int XpTotal { get; set; }
    public bool JaConcluidaAntes { get; set; }
    public int StreakAtual { get; set; }
    public List<ConquistaDto> NovasConquistas { get; set; } = [];
}

public class ConquistaDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;
}
