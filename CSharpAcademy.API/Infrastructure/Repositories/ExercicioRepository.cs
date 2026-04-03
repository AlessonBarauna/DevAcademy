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

    public async Task<IEnumerable<Exercicio>> ObterAleatoriosDeConcluidasAsync(int usuarioId, int quantidade)
    {
        var licoesConcluidas = await ctx.Progressos
            .Where(p => p.UsuarioId == usuarioId && p.Completada)
            .Select(p => p.LicaoId)
            .ToListAsync();

        if (licoesConcluidas.Count == 0) return [];

        return await ctx.Exercicios
            .Where(e => licoesConcluidas.Contains(e.LicaoId))
            .OrderBy(_ => EF.Functions.Random())
            .Take(quantidade)
            .ToListAsync();
    }

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
