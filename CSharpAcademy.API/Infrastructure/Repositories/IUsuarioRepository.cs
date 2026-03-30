using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(int id);
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<bool> ExisteEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task<bool> SalvarAsync();
    Task<IEnumerable<Usuario>> ObterRankingAsync(int top = 10);
    Task<int> ObterPosicaoRankingAsync(int usuarioId);
}
