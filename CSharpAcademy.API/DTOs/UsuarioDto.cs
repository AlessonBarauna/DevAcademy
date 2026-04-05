using System.ComponentModel.DataAnnotations;

namespace CSharpAcademy.API.DTOs;

public class RegistrarUsuarioDto
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MinLength(2, ErrorMessage = "Nome deve ter ao menos 2 caracteres.")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "Senha deve ter ao menos 6 caracteres.")]
    [MaxLength(100)]
    public string Senha { get; set; } = string.Empty;
}

public class LoginDto
{
    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória.")]
    public string Senha { get; set; } = string.Empty;
}

public record AtividadeDiaDto(string Data, int Contagem);

public class UsuarioResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int NivelAtual { get; set; }
    public int Xp { get; set; }
    public string Token { get; set; } = string.Empty;
    public int StreakFreeze { get; set; }
}
