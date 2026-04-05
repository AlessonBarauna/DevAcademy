using System.Security.Claims;
using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Infrastructure.Data;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjetoController(AppDbContext db, IUsuarioRepository usuarioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private static readonly List<ProjetoDefinicao> Projetos =
    [
        new(1, "🧮 Calculadora", "Implemente uma calculadora com as quatro operações básicas e tratamento de erros.",
            "Iniciante", 10,
            [
                new(1, "Criar método Somar(double a, double b)"),
                new(2, "Criar método Subtrair(double a, double b)"),
                new(3, "Criar método Multiplicar(double a, double b)"),
                new(4, "Criar método Dividir(double a, double b) com verificação de divisão por zero"),
                new(5, "Testar todas as operações com Console.WriteLine"),
            ]),
        new(2, "📝 Gerenciador de Tarefas", "Crie um sistema de tarefas em memória com CRUD completo usando coleções.",
            "Iniciante", 15,
            [
                new(1, "Criar classe Tarefa com Id, Titulo, Concluida"),
                new(2, "Implementar método AdicionarTarefa(string titulo)"),
                new(3, "Implementar método ListarTarefas() com foreach"),
                new(4, "Implementar método ConcluirTarefa(int id)"),
                new(5, "Implementar método RemoverTarefa(int id)"),
                new(6, "Mostrar contagem de tarefas pendentes e concluídas"),
            ]),
        new(3, "📊 Análise de Notas", "Use LINQ para analisar notas de alunos: média, aprovados, reprovados e ranking.",
            "Intermediário", 20,
            [
                new(1, "Criar classe Aluno com Nome e Nota"),
                new(2, "Criar lista com 10 alunos e notas aleatórias"),
                new(3, "Calcular média da turma com LINQ (.Average)"),
                new(4, "Listar alunos aprovados (nota >= 7) ordenados por nota decrescente"),
                new(5, "Listar alunos reprovados com nota abaixo da média"),
                new(6, "Exibir o top 3 alunos com as melhores notas"),
                new(7, "Calcular desvio padrão das notas"),
            ]),
        new(4, "🏦 Banco Simples", "Crie um sistema bancário com conta corrente, depósito, saque e extrato.",
            "Intermediário", 25,
            [
                new(1, "Criar classe ContaBancaria com Numero, Titular e Saldo"),
                new(2, "Implementar Depositar(decimal valor) com validação de valor positivo"),
                new(3, "Implementar Sacar(decimal valor) com validação de saldo suficiente"),
                new(4, "Implementar Transferir(ContaBancaria destino, decimal valor)"),
                new(5, "Criar List<string> de histórico de transações"),
                new(6, "Implementar ExibirExtrato() formatado com data e valor"),
                new(7, "Testar cenário completo: depósito, transferência e saque"),
            ]),
        new(5, "🎮 Jogo da Forca", "Implemente a lógica do clássico jogo da forca com lista de palavras temáticas.",
            "Avançado", 30,
            [
                new(1, "Criar lista de palavras de C# (ex: 'variavel', 'interface', 'delegate')"),
                new(2, "Sortear palavra aleatória e criar array de '_' para exibição"),
                new(3, "Implementar loop do jogo com leitura de letras"),
                new(4, "Verificar letra na palavra e revelar posições corretas"),
                new(5, "Controlar erros e exibir as tentativas restantes (máx 6)"),
                new(6, "Detectar vitória (todas as letras reveladas)"),
                new(7, "Detectar derrota e revelar a palavra"),
                new(8, "Adicionar opção de jogar novamente"),
            ]),
    ];

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var concluidas = await db.ProjetoTarefasConcluidas
            .Where(p => p.UsuarioId == UsuarioId)
            .ToListAsync();

        var resultado = Projetos.Select(p =>
        {
            var tarefasConcluidas = concluidas
                .Where(c => c.ProjetoId == p.Id)
                .Select(c => c.TarefaId)
                .ToHashSet();

            return new
            {
                p.Id,
                p.Titulo,
                p.Descricao,
                p.Dificuldade,
                p.XpRecompensa,
                totalTarefas = p.Tarefas.Count,
                tarefasConcluidas = tarefasConcluidas.Count,
                percentual = p.Tarefas.Count > 0
                    ? (int)Math.Round((double)tarefasConcluidas.Count / p.Tarefas.Count * 100)
                    : 0,
                concluido = tarefasConcluidas.Count == p.Tarefas.Count,
                tarefas = p.Tarefas.Select(t => new
                {
                    t.Id,
                    t.Descricao,
                    concluida = tarefasConcluidas.Contains(t.Id)
                })
            };
        });

        return Ok(resultado);
    }

    [HttpPost("{projetoId}/tarefa/{tarefaId}/concluir")]
    public async Task<IActionResult> ConcluirTarefa(int projetoId, int tarefaId)
    {
        var projeto = Projetos.FirstOrDefault(p => p.Id == projetoId);
        if (projeto == null) return NotFound();

        var tarefa = projeto.Tarefas.FirstOrDefault(t => t.Id == tarefaId);
        if (tarefa == null) return NotFound();

        var jaExiste = await db.ProjetoTarefasConcluidas
            .AnyAsync(p => p.UsuarioId == UsuarioId && p.ProjetoId == projetoId && p.TarefaId == tarefaId);

        if (!jaExiste)
        {
            db.ProjetoTarefasConcluidas.Add(new ProjetoTarefaConcluida
            {
                UsuarioId = UsuarioId,
                ProjetoId = projetoId,
                TarefaId = tarefaId,
                DataConclusao = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }

        // Verifica se o projeto foi concluído agora
        var totalConcluidas = await db.ProjetoTarefasConcluidas
            .CountAsync(p => p.UsuarioId == UsuarioId && p.ProjetoId == projetoId);

        var projetoConcluido = totalConcluidas == projeto.Tarefas.Count;
        var xpGanho = 0;

        if (projetoConcluido)
        {
            var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
            if (usuario != null)
            {
                // Dar XP apenas uma vez (verificar se já recebeu)
                var primeiraConclusao = !jaExiste && totalConcluidas == projeto.Tarefas.Count;
                if (primeiraConclusao)
                {
                    usuario.XP += projeto.XpRecompensa;
                    await usuarioRepo.AtualizarAsync(usuario);
                    await usuarioRepo.SalvarAsync();
                    xpGanho = projeto.XpRecompensa;
                }
            }
        }

        return Ok(new { projetoConcluido, xpGanho, totalConcluidas, total = projeto.Tarefas.Count });
    }
}

public record ProjetoDefinicao(
    int Id,
    string Titulo,
    string Descricao,
    string Dificuldade,
    int XpRecompensa,
    List<TarefaDefinicao> Tarefas);

public record TarefaDefinicao(int Id, string Descricao);
