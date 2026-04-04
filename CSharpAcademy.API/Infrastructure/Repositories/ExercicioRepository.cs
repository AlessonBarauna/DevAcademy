using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class ExercicioRepository(AppDbContext ctx) : IExercicioRepository
{
    public async Task<IEnumerable<Exercicio>> ObterPorLicaoAsync(int licaoId)
        => await ctx.Exercicios
            .Where(e => e.LicaoId == licaoId)
            .OrderBy(e => e.Ordem)
            .ToListAsync();

    public async Task<Exercicio?> ObterPorIdAsync(int id)
        => await ctx.Exercicios.FindAsync(id);

    public async Task<IEnumerable<Exercicio>> ObterAleatoriosPorModuloAsync(int moduloId, int quantidade)
        => await ctx.Exercicios
            .Where(e => ctx.Licoes.Any(l => l.Id == e.LicaoId && l.ModuloId == moduloId))
            .OrderBy(_ => Guid.NewGuid())
            .Take(quantidade)
            .ToListAsync();

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
