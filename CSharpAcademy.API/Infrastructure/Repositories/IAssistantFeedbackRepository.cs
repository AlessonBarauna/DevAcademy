using CSharpAcademy.API.Domain.AI;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public interface IAssistantFeedbackRepository
{
    Task AddAsync(AssistantFeedback feedback);
    Task<bool> SalvarAsync();
}
