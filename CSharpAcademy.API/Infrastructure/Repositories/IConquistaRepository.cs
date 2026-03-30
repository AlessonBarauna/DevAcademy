using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IConquistaRepository
{
    Task<IEnumerable<Conquista>> ObterPorUsuarioAsync(int usuarioId);
    Task<bool> JaPossuiAsync(int usuarioId, string codigo);
    Task AdicionarAsync(Conquista conquista);
    Task<bool> SalvarAsync();
}
