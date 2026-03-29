using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class UsuarioRepository(AppDbContext ctx) : IUsuarioRepository
{
    public async Task<Usuario?> ObterPorIdAsync(int id)
        => await ctx.Usuarios.Include(u => u.Progressos).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<Usuario?> ObterPorEmailAsync(string email)
        => await ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<bool> ExisteEmailAsync(string email)
        => await ctx.Usuarios.AnyAsync(u => u.Email == email);

    public async Task AdicionarAsync(Usuario usuario)
        => await ctx.Usuarios.AddAsync(usuario);

    public Task AtualizarAsync(Usuario usuario)
    {
        ctx.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
