using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Repositories;

namespace CSharpAcademy.API.Application.Services;

/// <summary>Avalia e concede conquistas ao usuário após cada lição concluída.</summary>
public class ConquistaService(
    IConquistaRepository conquistaRepo,
    IProgressoRepository progressoRepo)
{
    private static readonly List<(string Codigo, string Titulo, string Descricao, string Icone, Func<Usuario, int, bool> Condicao)> Definicoes =
    [
        ("PRIMEIRA_LICAO",   "Primeiro Passo",     "Concluiu sua primeira lição",          "🌱", (u, total) => total >= 1),
        ("LICOES_5",         "Em Ritmo",           "Concluiu 5 lições",                    "🚀", (u, total) => total >= 5),
        ("LICOES_10",        "Dedicado",           "Concluiu 10 lições",                   "🎯", (u, total) => total >= 10),
        ("LICOES_27",        "Mestre do Curso",    "Concluiu todas as lições",             "🏅", (u, total) => total >= 27),
        ("XP_100",           "Centenário",         "Acumulou 100 XP",                      "⚡", (u, _) => u.XP >= 100),
        ("XP_500",           "Veterano",           "Acumulou 500 XP",                      "💎", (u, _) => u.XP >= 500),
        ("NIVEL_2",          "Subiu de Nível",     "Alcançou o nível Intermediário",       "📈", (u, _) => u.NivelAtual >= 2),
        ("NIVEL_4",          "Especialista",       "Alcançou o nível máximo",              "👑", (u, _) => u.NivelAtual >= 4),
        ("STREAK_3",         "Sequência de 3",     "3 dias consecutivos de estudo",        "🔥", (u, _) => u.StreakAtual >= 3),
        ("STREAK_7",         "Semana Perfeita",    "7 dias consecutivos de estudo",        "🌟", (u, _) => u.StreakAtual >= 7),
    ];

    public async Task<List<Conquista>> AvaliarAsync(Usuario usuario)
    {
        var totalLicoes = (await progressoRepo.ObterLicoesConcluidasAsync(usuario.Id)).Count();
        var novas = new List<Conquista>();

        foreach (var (codigo, titulo, descricao, icone, condicao) in Definicoes)
        {
            if (!condicao(usuario, totalLicoes)) continue;
            if (await conquistaRepo.JaPossuiAsync(usuario.Id, codigo)) continue;

            var conquista = new Conquista
            {
                UsuarioId = usuario.Id,
                Codigo = codigo,
                Titulo = titulo,
                Descricao = descricao,
                Icone = icone,
                DataConquista = DateTime.UtcNow
            };
            await conquistaRepo.AdicionarAsync(conquista);
            novas.Add(conquista);
        }

        if (novas.Count > 0)
            await conquistaRepo.SalvarAsync();

        return novas;
    }
}
