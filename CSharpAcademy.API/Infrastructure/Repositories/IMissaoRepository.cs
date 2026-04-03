using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IMissaoRepository
{
    Task<IEnumerable<MissaoDiaria>> ObterHojeAsync(int usuarioId);
    Task AdicionarRangeAsync(IEnumerable<MissaoDiaria> missoes);
    Task AtualizarAsync(MissaoDiaria missao);
    Task<int> ContarExerciciosCorretosHojeAsync(int usuarioId);
    Task<int> ContarLicoesConcluidasHojeAsync(int usuarioId);
    Task<int> SomarXpGanhoHojeAsync(int usuarioId);
    Task<bool> SalvarAsync();
}
