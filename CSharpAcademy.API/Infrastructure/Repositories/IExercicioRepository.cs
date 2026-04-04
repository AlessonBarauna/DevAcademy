using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IExercicioRepository
{
    Task<IEnumerable<Exercicio>> ObterPorLicaoAsync(int licaoId);
    Task<Exercicio?> ObterPorIdAsync(int id);
    Task<IEnumerable<Exercicio>> ObterAleatoriosPorModuloAsync(int moduloId, int quantidade);
    Task<IEnumerable<Exercicio>> ObterAleatoriosDeConcluidasAsync(int usuarioId, int quantidade);
    Task<bool> SalvarAsync();
}
