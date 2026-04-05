using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IModuloRepository
{
    Task<IEnumerable<Modulo>> ObterTodosAsync();
    Task<Modulo?> ObterPorIdAsync(int id);
    Task<IEnumerable<Modulo>> BuscarAsync(string termo);
    Task<bool> SalvarAsync();
}
