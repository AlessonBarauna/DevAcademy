namespace CSharpAcademy.API.Domain;

public class NotaLicao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int LicaoId { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; } = null!;
    public Licao Licao { get; set; } = null!;
}
