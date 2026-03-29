using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IProgressoRepository
{
    Task<IEnumerable<int>> ObterLicoesConcluidasAsync(int usuarioId);
    Task<Progresso?> ObterAsync(int usuarioId, int licaoId);
    Task AdicionarAsync(Progresso progresso);
    Task AtualizarAsync(Progresso progresso);
    Task<bool> SalvarAsync();
}
