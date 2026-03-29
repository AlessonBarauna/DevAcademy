using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IRespostaRepository
{
    Task AdicionarAsync(RespostaUsuario resposta);
    Task<bool> SalvarAsync();
}
