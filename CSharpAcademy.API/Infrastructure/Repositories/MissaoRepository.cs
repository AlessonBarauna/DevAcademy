using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class MissaoRepository(AppDbContext ctx) : IMissaoRepository
{
    public async Task<IEnumerable<MissaoDiaria>> ObterHojeAsync(int usuarioId)
    {
        var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
        return await ctx.MissoesDiarias
            .Where(m => m.UsuarioId == usuarioId && m.Data == hoje)
            .ToListAsync();
    }

    public async Task AdicionarRangeAsync(IEnumerable<MissaoDiaria> missoes)
        => await ctx.MissoesDiarias.AddRangeAsync(missoes);

    public Task AtualizarAsync(MissaoDiaria missao)
    {
        ctx.MissoesDiarias.Update(missao);
        return Task.CompletedTask;
    }

    public async Task<int> ContarExerciciosCorretosHojeAsync(int usuarioId)
    {
        var hoje = DateTime.UtcNow.Date;
        return await ctx.RespostasUsuarios
            .Where(r => r.UsuarioId == usuarioId && r.Correta && r.DataResposta >= hoje)
            .CountAsync();
    }

    public async Task<int> ContarLicoesConcluidasHojeAsync(int usuarioId)
    {
        var hoje = DateTime.UtcNow.Date;
        return await ctx.Progressos
            .Where(p => p.UsuarioId == usuarioId && p.Completada && p.DataConclusao >= hoje)
            .CountAsync();
    }

    public async Task<int> SomarXpGanhoHojeAsync(int usuarioId)
    {
        var hoje = DateTime.UtcNow.Date;
        return await ctx.Progressos
            .Where(p => p.UsuarioId == usuarioId && p.Completada && p.DataConclusao >= hoje)
            .SumAsync(p => p.XPGanho);
    }

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
