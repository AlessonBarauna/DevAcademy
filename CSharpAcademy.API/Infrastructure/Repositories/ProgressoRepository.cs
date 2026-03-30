using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class ProgressoRepository(AppDbContext ctx) : IProgressoRepository
{
    public async Task<IEnumerable<int>> ObterLicoesConcluidasAsync(int usuarioId)
        => await ctx.Progressos
            .Where(p => p.UsuarioId == usuarioId && p.Completada)
            .Select(p => p.LicaoId)
            .ToListAsync();

    public async Task<IEnumerable<(DateOnly Data, int Contagem)>> ObterAtividadePorDiaAsync(int usuarioId, DateTime desde)
    {
        var lista = await ctx.Progressos
            .Where(p => p.UsuarioId == usuarioId && p.Completada && p.DataConclusao >= desde)
            .Select(p => p.DataConclusao!.Value.Date)
            .ToListAsync();

        return lista
            .GroupBy(d => DateOnly.FromDateTime(d))
            .Select(g => (g.Key, g.Count()));
    }

    public async Task<Progresso?> ObterAsync(int usuarioId, int licaoId)
        => await ctx.Progressos.FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.LicaoId == licaoId);

    public async Task AdicionarAsync(Progresso progresso)
        => await ctx.Progressos.AddAsync(progresso);

    public Task AtualizarAsync(Progresso progresso)
    {
        ctx.Progressos.Update(progresso);
        return Task.CompletedTask;
    }

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
