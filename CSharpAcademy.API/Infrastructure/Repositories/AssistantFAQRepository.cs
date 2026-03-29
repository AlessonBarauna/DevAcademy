using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Domain.AI;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class AssistantFAQRepository(AppDbContext ctx) : IAssistantFAQRepository
{
    public async Task<AssistantFAQ?> BuscarPorPerguntaSimilarAsync(
        string pergunta, int? licaoId, int nivel, string idioma)
    {
        var palavras = pergunta.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Where(p => p.Length > 3).ToArray();

        if (palavras.Length == 0) return null;

        var faqs = await ctx.AssistantFAQs
            .Where(f => f.Ativa
                && f.Idioma == idioma
                && (int)f.NivelMinimo <= nivel
                && (f.LicaoId == licaoId || f.LicaoId == null))
            .ToListAsync();

        // Simple similarity: most keyword overlap
        return faqs
            .Select(f => new
            {
                Faq = f,
                Score = palavras.Count(p => f.Pergunta.ToLower().Contains(p))
            })
            .Where(x => x.Score >= Math.Max(1, palavras.Length / 2))
            .OrderByDescending(x => x.Score)
            .FirstOrDefault()?.Faq;
    }

    public async Task AddAsync(AssistantFAQ faq)
        => await ctx.AssistantFAQs.AddAsync(faq);

    public Task UpdateAsync(AssistantFAQ faq)
    {
        ctx.AssistantFAQs.Update(faq);
        return Task.CompletedTask;
    }

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
