using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class LicaoRepository(AppDbContext ctx) : ILicaoRepository
{
    public async Task<IEnumerable<Licao>> ObterPorModuloAsync(int moduloId)
        => await ctx.Licoes
            .Include(l => l.Modulo)
            .Where(l => l.ModuloId == moduloId && l.Ativo)
            .OrderBy(l => l.Ordem)
            .ToListAsync();

    public async Task<Licao?> ObterPorIdAsync(int id)
        => await ctx.Licoes
            .Include(l => l.Modulo)
            .FirstOrDefaultAsync(l => l.Id == id && l.Ativo);

    public async Task<IEnumerable<Licao>> BuscarAsync(string termo)
        => await ctx.Licoes
            .Include(l => l.Modulo)
            .Where(l => l.Ativo && (
                EF.Functions.Like(l.Titulo, $"%{termo}%") ||
                EF.Functions.Like(l.Descricao, $"%{termo}%")))
            .OrderBy(l => l.ModuloId).ThenBy(l => l.Ordem)
            .Take(8)
            .ToListAsync();

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
