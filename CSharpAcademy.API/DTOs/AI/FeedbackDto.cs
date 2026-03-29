namespace CSharpAcademy.API.DTOs.AI;

public class FeedbackDto
{
    public int UsuarioId { get; set; }
    public int Estrelas { get; set; } // 1-5
    public string? Comentario { get; set; }
    public bool RespostaAjudou { get; set; }
    public bool RespostaClara { get; set; }
    public bool RespostaCompleta { get; set; }
}
