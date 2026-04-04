using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IRespostaRepository
{
    Task AdicionarAsync(RespostaUsuario resposta);
    Task<IEnumerable<(int ModuloId, bool Correta)>> ObterComModuloAsync(int usuarioId);
    Task<bool> SalvarAsync();
}
