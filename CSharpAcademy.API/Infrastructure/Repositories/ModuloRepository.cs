using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class ModuloRepository(AppDbContext ctx) : IModuloRepository
{
    public async Task<IEnumerable<Modulo>> ObterTodosAsync()
        => await ctx.Modulos
            .Include(m => m.Licoes)
            .Where(m => m.Ativo)
            .OrderBy(m => m.Ordem)
            .ToListAsync();

    public async Task<Modulo?> ObterPorIdAsync(int id)
        => await ctx.Modulos
            .Include(m => m.Licoes)
            .FirstOrDefaultAsync(m => m.Id == id && m.Ativo);

    public async Task<IEnumerable<Modulo>> BuscarAsync(string termo)
        => await ctx.Modulos
            .Where(m => m.Ativo && (
                EF.Functions.Like(m.Titulo, $"%{termo}%") ||
                EF.Functions.Like(m.Descricao, $"%{termo}%")))
            .OrderBy(m => m.Ordem)
            .Take(5)
            .ToListAsync();

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
