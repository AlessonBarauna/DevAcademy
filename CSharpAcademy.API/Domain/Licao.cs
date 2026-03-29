namespace CSharpAcademy.API.Domain;

public class Licao
{
    public int Id { get; set; }
    public int ModuloId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string ConteudoTeoricoMarkdown { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public int XPRecompensa { get; set; } = 10;
    public bool Ativo { get; set; } = true;

    public Modulo Modulo { get; set; } = null!;
    public ICollection<Exercicio> Exercicios { get; set; } = [];
    public ICollection<Progresso> Progressos { get; set; } = [];
}
