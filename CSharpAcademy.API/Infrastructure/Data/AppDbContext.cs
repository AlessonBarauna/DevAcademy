using CSharpAcademy.API.Domain;
using CSharpAcademy.API.Domain.AI;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Modulo> Modulos => Set<Modulo>();
    public DbSet<Licao> Licoes => Set<Licao>();
    public DbSet<Exercicio> Exercicios => Set<Exercicio>();
    public DbSet<RespostaUsuario> RespostasUsuarios => Set<RespostaUsuario>();
    public DbSet<Progresso> Progressos => Set<Progresso>();

    // AI
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<ChatSession> ChatSessions => Set<ChatSession>();
    public DbSet<AssistantFAQ> AssistantFAQs => Set<AssistantFAQ>();
    public DbSet<AssistantFeedback> AssistantFeedbacks => Set<AssistantFeedback>();
    public DbSet<CustomExercise> CustomExercises => Set<CustomExercise>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChatMessage>().HasOne(c => c.Usuario)
            .WithMany(u => u.ChatMessages).HasForeignKey(c => c.UsuarioId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChatSession>().HasOne(s => s.Usuario)
            .WithMany(u => u.ChatSessions).HasForeignKey(s => s.UsuarioId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssistantFeedback>().HasOne(f => f.ChatMessage)
            .WithMany().HasForeignKey(f => f.ChatMessageId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssistantFeedback>().HasOne(f => f.Usuario)
            .WithMany().HasForeignKey(f => f.UsuarioId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Progresso>()
            .HasIndex(p => new { p.UsuarioId, p.LicaoId }).IsUnique();

        // Seed modules
        modelBuilder.Entity<Modulo>().HasData(
            new Modulo { Id = 1, Titulo = "Fundamentos C#", Descricao = "Variáveis, tipos, operadores e controle de fluxo", Ordem = 1, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true },
            new Modulo { Id = 2, Titulo = "Orientação a Objetos", Descricao = "Classes, herança, polimorfismo e encapsulamento", Ordem = 2, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true },
            new Modulo { Id = 3, Titulo = "Coleções e LINQ", Descricao = "List, Dictionary, Array e consultas LINQ", Ordem = 3, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true },
            new Modulo { Id = 4, Titulo = "Async/Await e Concorrência", Descricao = "Programação assíncrona, Tasks e threading", Ordem = 4, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true },
            new Modulo { Id = 5, Titulo = "Design Patterns", Descricao = "Repository, Factory, Singleton e outros padrões", Ordem = 5, NivelMinimo = NivelDificuldade.Avancado, Ativo = true }
        );

        modelBuilder.Entity<Licao>().HasData(
            // Módulo 1
            new Licao { Id = 1, ModuloId = 1, Titulo = "Variáveis e Tipos", Descricao = "int, string, bool, double e inferência de tipo", ConteudoTeoricoMarkdown = "## Variáveis em C#\n\nC# é uma linguagem fortemente tipada...", Ordem = 1, XPRecompensa = 10 },
            new Licao { Id = 2, ModuloId = 1, Titulo = "Controle de Fluxo", Descricao = "if/else, switch, loops for e foreach", ConteudoTeoricoMarkdown = "## Controle de Fluxo\n\nDecisões e repetições em C#...", Ordem = 2, XPRecompensa = 10 },
            new Licao { Id = 3, ModuloId = 1, Titulo = "Métodos e Funções", Descricao = "Declaração, parâmetros, retorno e sobrecarga", ConteudoTeoricoMarkdown = "## Métodos\n\nMétodos encapsulam comportamento...", Ordem = 3, XPRecompensa = 15 },
            // Módulo 2
            new Licao { Id = 4, ModuloId = 2, Titulo = "Classes e Objetos", Descricao = "Propriedades, construtores e instanciação", ConteudoTeoricoMarkdown = "## Classes em C#\n\nClasses são moldes para objetos...", Ordem = 1, XPRecompensa = 15 },
            new Licao { Id = 5, ModuloId = 2, Titulo = "Herança e Polimorfismo", Descricao = "Herança, override e interfaces", ConteudoTeoricoMarkdown = "## Herança\n\nReutilização de código através de herança...", Ordem = 2, XPRecompensa = 20 },
            // Módulo 3
            new Licao { Id = 6, ModuloId = 3, Titulo = "Listas e Arrays", Descricao = "List<T>, Array e operações comuns", ConteudoTeoricoMarkdown = "## Coleções\n\nListas são coleções dinâmicas...", Ordem = 1, XPRecompensa = 20 },
            new Licao { Id = 7, ModuloId = 3, Titulo = "LINQ Básico", Descricao = "Where, Select, OrderBy e First", ConteudoTeoricoMarkdown = "## LINQ\n\nLanguage Integrated Query permite consultas...", Ordem = 2, XPRecompensa = 25 },
            // Módulo 4
            new Licao { Id = 8, ModuloId = 4, Titulo = "async/await", Descricao = "Tarefas assíncronas com Task e async/await", ConteudoTeoricoMarkdown = "## Programação Assíncrona\n\nasync/await simplifica código assíncrono...", Ordem = 1, XPRecompensa = 30 },
            // Módulo 5
            new Licao { Id = 9, ModuloId = 5, Titulo = "Repository Pattern", Descricao = "Abstraindo o acesso a dados com Repository", ConteudoTeoricoMarkdown = "## Repository Pattern\n\nO padrão Repository isola a lógica de acesso a dados...", Ordem = 1, XPRecompensa = 35 }
        );
    }
}
