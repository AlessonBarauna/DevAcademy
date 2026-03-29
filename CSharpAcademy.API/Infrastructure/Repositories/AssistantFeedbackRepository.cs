using CSharpAcademy.API.Domain.AI;
using CSharpAcademy.API.Infrastructure.Data;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class AssistantFeedbackRepository(AppDbContext ctx) : IAssistantFeedbackRepository
{
    public async Task AddAsync(AssistantFeedback feedback)
        => await ctx.AssistantFeedbacks.AddAsync(feedback);

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
