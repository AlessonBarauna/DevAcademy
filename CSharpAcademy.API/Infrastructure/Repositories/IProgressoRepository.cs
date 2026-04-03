using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IProgressoRepository
{
    Task<IEnumerable<int>> ObterLicoesConcluidasAsync(int usuarioId);
    Task<IEnumerable<(DateOnly Data, int Contagem)>> ObterAtividadePorDiaAsync(int usuarioId, DateTime desde);
    Task<Progresso?> ObterAsync(int usuarioId, int licaoId);
    Task<IEnumerable<Progresso>> ObterRevisoesHojeAsync(int usuarioId);
    Task AdicionarAsync(Progresso progresso);
    Task AtualizarAsync(Progresso progresso);
    Task<bool> SalvarAsync();
}
