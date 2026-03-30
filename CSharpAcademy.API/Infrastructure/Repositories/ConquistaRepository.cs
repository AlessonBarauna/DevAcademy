using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class ConquistaRepository(AppDbContext ctx) : IConquistaRepository
{
    public async Task<IEnumerable<Conquista>> ObterPorUsuarioAsync(int usuarioId)
        => await ctx.Conquistas
            .Where(c => c.UsuarioId == usuarioId)
            .OrderBy(c => c.DataConquista)
            .ToListAsync();

    public async Task<bool> JaPossuiAsync(int usuarioId, string codigo)
        => await ctx.Conquistas.AnyAsync(c => c.UsuarioId == usuarioId && c.Codigo == codigo);

    public async Task AdicionarAsync(Conquista conquista)
        => await ctx.Conquistas.AddAsync(conquista);

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
