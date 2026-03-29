using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpAcademy.API.Domain;
using CSharpAcademy.API.DTOs;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AppDbContext ctx, IConfiguration config) : ControllerBase
{
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto dto)
    {
        if (await ctx.Usuarios.AnyAsync(u => u.Email == dto.Email))
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

        ctx.Usuarios.Add(usuario);
        await ctx.SaveChangesAsync();

        return Ok(new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            NivelAtual = usuario.NivelAtual,
            XP = usuario.XP,
            Token = GerarToken(usuario)
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var usuario = await ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

        return Ok(new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            NivelAtual = usuario.NivelAtual,
            XP = usuario.XP,
            Token = GerarToken(usuario)
        });
    }

    private string GerarToken(Usuario usuario)
    {
        var jwtKey = config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.");
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
