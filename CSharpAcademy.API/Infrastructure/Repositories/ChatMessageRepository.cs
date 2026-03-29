using CSharpAcademy.API.Domain.AI;
using CSharpAcademy.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Repositories;

public class ChatMessageRepository(AppDbContext ctx) : IChatMessageRepository
{
    public async Task<ChatMessage?> GetByIdAsync(int id)
        => await ctx.ChatMessages.FindAsync(id);

    public async Task<IEnumerable<ChatMessage>> GetPorLicaoAsync(
        int usuarioId, int licaoId, int ultimasN = 0, int skip = 0, int take = 20)
    {
        var query = ctx.ChatMessages
            .Where(m => m.UsuarioId == usuarioId && m.LicaoId == licaoId)
            .OrderByDescending(m => m.DataCriacao);

        if (ultimasN > 0)
            return await query.Take(ultimasN).OrderBy(m => m.DataCriacao).ToListAsync();

        return await query.Skip(skip).Take(take).OrderBy(m => m.DataCriacao).ToListAsync();
    }

    public async Task AddAsync(ChatMessage message)
        => await ctx.ChatMessages.AddAsync(message);

    public Task UpdateAsync(ChatMessage message)
    {
        ctx.ChatMessages.Update(message);
        return Task.CompletedTask;
    }

    public async Task<bool> SalvarAsync()
        => await ctx.SaveChangesAsync() > 0;
}
