using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface INotaLicaoRepository
{
    Task<NotaLicao?> ObterPorUsuarioELicaoAsync(int usuarioId, int licaoId);
    Task SalvarAsync(NotaLicao nota);
}
