using CSharpAcademy.API.Domain.AI;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IChatMessageRepository
{
    Task<ChatMessage?> GetByIdAsync(int id);
    Task<IEnumerable<ChatMessage>> GetPorLicaoAsync(int usuarioId, int licaoId, int ultimasN = 0, int skip = 0, int take = 20);
    Task AddAsync(ChatMessage message);
    Task UpdateAsync(ChatMessage message);
    Task<bool> SalvarAsync();
}
