using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Domain.AI;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IAssistantFAQRepository
{
    Task<AssistantFAQ?> BuscarPorPerguntaSimilarAsync(string pergunta, int? licaoId, int nivel, string idioma);
    Task AddAsync(AssistantFAQ faq);
    Task UpdateAsync(AssistantFAQ faq);
    Task<bool> SalvarAsync();
}
