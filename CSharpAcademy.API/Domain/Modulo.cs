namespace CSharpAcademy.API.Domain;

public class Modulo
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public NivelDificuldade NivelMinimo { get; set; } = NivelDificuldade.Iniciante;
    public bool Ativo { get; set; } = true;
    public int? PreRequisitoId { get; set; }

    public ICollection<Licao> Licoes { get; set; } = [];
}
