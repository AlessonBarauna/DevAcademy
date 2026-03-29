using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class RespostaRepository(AppDbContext ctx) : IRespostaRepository
{
    public async Task AdicionarAsync(RespostaUsuario resposta)
        => await ctx.RespostasUsuarios.AddAsync(resposta);

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
