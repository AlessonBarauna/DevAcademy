using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface ILicaoRepository
{
    Task<IEnumerable<Licao>> ObterPorModuloAsync(int moduloId);
    Task<Licao?> ObterPorIdAsync(int id);
    Task<bool> SalvarAsync();
}
