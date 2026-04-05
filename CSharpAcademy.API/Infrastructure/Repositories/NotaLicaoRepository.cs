using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class NotaLicaoRepository(AppDbContext db) : INotaLicaoRepository
{
    public async Task<NotaLicao?> ObterPorUsuarioELicaoAsync(int usuarioId, int licaoId) =>
        await db.NotasLicao
            .FirstOrDefaultAsync(n => n.UsuarioId == usuarioId && n.LicaoId == licaoId);

    public async Task SalvarAsync(NotaLicao nota)
    {
        if (nota.Id == 0)
            db.NotasLicao.Add(nota);
        else
            db.NotasLicao.Update(nota);

        await db.SaveChangesAsync();
    }
}
