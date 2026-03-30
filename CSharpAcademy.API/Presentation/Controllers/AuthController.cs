using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpAcademy.API.Domain;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using AtividadeDiaDto = CSharpAcademy.API.DTOs.AtividadeDiaDto;
using ConquistaDto = CSharpAcademy.API.DTOs.ConquistaDto;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUsuarioRepository usuarioRepo, IConfiguration config) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Retorna os dados atualizados do usuário autenticado (XP, nível)</summary>
    [HttpGet("perfil")]
    [Authorize]
    public async Task<IActionResult> Perfil()
    {
        var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
        if (usuario == null) return NotFound();
        return Ok(new { usuario.Id, usuario.Nome, usuario.Email, usuario.NivelAtual, Xp = usuario.XP, usuario.StreakAtual, usuario.StreakMaximo, UltimoEstudo = usuario.UltimoEstudo?.ToString("yyyy-MM-dd") });
    }

    [HttpGet("atividade")]
    [Authorize]
    public async Task<IActionResult> Atividade([FromServices] IProgressoRepository progressoRepo)
    {
        var desde = DateTime.UtcNow.AddMonths(-6);
        var atividade = await progressoRepo.ObterAtividadePorDiaAsync(UsuarioId, desde);
        return Ok(atividade.Select(a => new AtividadeDiaDto(a.Data.ToString("yyyy-MM-dd"), a.Contagem)));
    }

    [HttpGet("conquistas")]
    [Authorize]
    public async Task<IActionResult> Conquistas([FromServices] IConquistaRepository conquistaRepo)
    {
        var conquistas = await conquistaRepo.ObterPorUsuarioAsync(UsuarioId);
        return Ok(conquistas.Select(c => new ConquistaDto
        {
            Codigo = c.Codigo,
            Titulo = c.Titulo,
            Descricao = c.Descricao,
            Icone = c.Icone,
            DataConquista = c.DataConquista.ToString("dd/MM/yyyy")
        }));
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto dto)
    {
        if (await usuarioRepo.ExisteEmailAsync(dto.Email))
            return BadRequest(new { mensagem = "E-mail já cadastrado." });

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            NivelAtual = 1,
            XP = 0,
            DataCadastro = DateTime.UtcNow
        };

        await usuarioRepo.AdicionarAsync(usuario);
        await usuarioRepo.SalvarAsync();

        return Ok(MapParaDto(usuario));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var usuario = await usuarioRepo.ObterPorEmailAsync(dto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

        return Ok(MapParaDto(usuario));
    }

    private UsuarioResponseDto MapParaDto(Usuario usuario) => new()
    {
        Id = usuario.Id,
        Nome = usuario.Nome,
        Email = usuario.Email,
        NivelAtual = usuario.NivelAtual,
        Xp = usuario.XP,
        Token = GerarToken(usuario)
    };

    private string GerarToken(Usuario usuario)
    {
        var jwtKey = config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key não configurada.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome)
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
