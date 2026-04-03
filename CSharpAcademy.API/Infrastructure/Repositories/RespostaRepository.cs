using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class RespostaRepository(AppDbContext ctx) : IRespostaRepository
{
    public async Task AdicionarAsync(RespostaUsuario resposta)
        => await ctx.RespostasUsuarios.AddAsync(resposta);

    public async Task<IEnumerable<(int ModuloId, bool Correta)>> ObterComModuloAsync(int usuarioId)
    {
        var lista = await ctx.RespostasUsuarios
            .Where(r => r.UsuarioId == usuarioId)
            .Join(ctx.Exercicios, r => r.ExercicioId, e => e.Id,
                (r, e) => new { e.LicaoId, r.Correta })
            .Join(ctx.Licoes, x => x.LicaoId, l => l.Id,
                (x, l) => new { l.ModuloId, x.Correta })
            .Select(x => new { x.ModuloId, x.Correta })
            .ToListAsync();

        return lista.Select(x => (x.ModuloId, x.Correta));
    }

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
