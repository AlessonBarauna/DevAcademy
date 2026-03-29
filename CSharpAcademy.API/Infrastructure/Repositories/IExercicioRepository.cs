using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IExercicioRepository
{
    Task<IEnumerable<Exercicio>> ObterPorLicaoAsync(int licaoId);
    Task<Exercicio?> ObterPorIdAsync(int id);
    Task<bool> SalvarAsync();
}
