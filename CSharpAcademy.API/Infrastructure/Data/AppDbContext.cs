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

        // Seed Módulos
        modelBuilder.Entity<Modulo>().HasData(
            new Modulo { Id = 1, Titulo = "Fundamentos C#", Descricao = "Variáveis, tipos, operadores e controle de fluxo", Ordem = 1, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true },
            new Modulo { Id = 2, Titulo = "Orientação a Objetos", Descricao = "Classes, herança, polimorfismo e encapsulamento", Ordem = 2, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true },
            new Modulo { Id = 3, Titulo = "Coleções e LINQ", Descricao = "List, Dictionary, Array e consultas LINQ", Ordem = 3, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true },
            new Modulo { Id = 4, Titulo = "Async/Await e Concorrência", Descricao = "Programação assíncrona, Tasks e threading", Ordem = 4, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true },
            new Modulo { Id = 5, Titulo = "Design Patterns", Descricao = "Repository, Factory, Singleton e outros padrões", Ordem = 5, NivelMinimo = NivelDificuldade.Avancado, Ativo = true }
        );

        // Seed Lições
        modelBuilder.Entity<Licao>().HasData(
            new Licao { Id = 1, ModuloId = 1, Titulo = "Variáveis e Tipos", Descricao = "int, string, bool, double e inferência de tipo", ConteudoTeoricoMarkdown = "## Variáveis em C#\n\nC# é uma linguagem fortemente tipada. Cada variável possui um tipo definido em tempo de compilação.\n\n### Tipos primitivos\n```csharp\nint idade = 25;\ndouble salario = 4500.50;\nbool ativo = true;\nstring nome = \"João\";\n```\n\n### Inferência de tipo com var\nA palavra-chave `var` permite que o compilador infira o tipo:\n```csharp\nvar cidade = \"São Paulo\"; // string\nvar populacao = 12_000_000; // int\n```\n\n### Constantes\n```csharp\nconst double PI = 3.14159;\n```", Ordem = 1, XPRecompensa = 10, Ativo = true },
            new Licao { Id = 2, ModuloId = 1, Titulo = "Controle de Fluxo", Descricao = "if/else, switch, loops for e foreach", ConteudoTeoricoMarkdown = "## Controle de Fluxo\n\n### if/else\n```csharp\nif (nota >= 7)\n    Console.WriteLine(\"Aprovado\");\nelse\n    Console.WriteLine(\"Reprovado\");\n```\n\n### switch expression (C# moderno)\n```csharp\nvar resultado = nota switch\n{\n    >= 9 => \"A\",\n    >= 7 => \"B\",\n    >= 5 => \"C\",\n    _ => \"D\"\n};\n```\n\n### foreach\n```csharp\nvar nomes = new[] { \"Ana\", \"Bruno\", \"Carlos\" };\nforeach (var nome in nomes)\n    Console.WriteLine(nome);\n```", Ordem = 2, XPRecompensa = 10, Ativo = true },
            new Licao { Id = 3, ModuloId = 1, Titulo = "Métodos e Funções", Descricao = "Declaração, parâmetros, retorno e sobrecarga", ConteudoTeoricoMarkdown = "## Métodos em C#\n\n### Declaração básica\n```csharp\npublic int Somar(int a, int b) => a + b;\n```\n\n### Parâmetros opcionais\n```csharp\npublic string Saudar(string nome, string prefixo = \"Olá\")\n    => $\"{prefixo}, {nome}!\";\n```\n\n### Sobrecarga\nDois métodos com o mesmo nome mas parâmetros diferentes:\n```csharp\npublic double Calcular(double x) => x * x;\npublic double Calcular(double x, double y) => x * y;\n```", Ordem = 3, XPRecompensa = 15, Ativo = true },
            new Licao { Id = 4, ModuloId = 2, Titulo = "Classes e Objetos", Descricao = "Propriedades, construtores e instanciação", ConteudoTeoricoMarkdown = "## Classes em C#\n\n```csharp\npublic class Produto\n{\n    public int Id { get; set; }\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n\n    public Produto(string nome, decimal preco)\n    {\n        Nome = nome;\n        Preco = preco;\n    }\n\n    public string Descricao() => $\"{Nome} - R$ {Preco:F2}\";\n}\n\n// Uso:\nvar produto = new Produto(\"Notebook\", 3500m);\nConsole.WriteLine(produto.Descricao());\n```", Ordem = 1, XPRecompensa = 15, Ativo = true },
            new Licao { Id = 5, ModuloId = 2, Titulo = "Herança e Polimorfismo", Descricao = "Herança, override e interfaces", ConteudoTeoricoMarkdown = "## Herança\n\n```csharp\npublic class Animal\n{\n    public string Nome { get; set; } = string.Empty;\n    public virtual string EmitirSom() => \"...\";\n}\n\npublic class Cachorro : Animal\n{\n    public override string EmitirSom() => \"Au au!\";\n}\n```\n\n## Interfaces\n```csharp\npublic interface IVoador\n{\n    void Voar();\n}\n\npublic class Passaro : Animal, IVoador\n{\n    public override string EmitirSom() => \"Piu piu!\";\n    public void Voar() => Console.WriteLine(\"Voando...\");\n}\n```", Ordem = 2, XPRecompensa = 20, Ativo = true },
            new Licao { Id = 6, ModuloId = 3, Titulo = "Listas e Arrays", Descricao = "List<T>, Array e operações comuns", ConteudoTeoricoMarkdown = "## Coleções em C#\n\n### Array (tamanho fixo)\n```csharp\nint[] numeros = { 1, 2, 3, 4, 5 };\nConsole.WriteLine(numeros.Length); // 5\n```\n\n### List<T> (tamanho dinâmico)\n```csharp\nvar nomes = new List<string> { \"Ana\", \"Bruno\" };\nnomes.Add(\"Carlos\");\nnomes.Remove(\"Ana\");\nConsole.WriteLine(nomes.Count); // 2\n```\n\n### Dictionary<TKey, TValue>\n```csharp\nvar capitais = new Dictionary<string, string>\n{\n    [\"SP\"] = \"São Paulo\",\n    [\"RJ\"] = \"Rio de Janeiro\"\n};\n```", Ordem = 1, XPRecompensa = 20, Ativo = true },
            new Licao { Id = 7, ModuloId = 3, Titulo = "LINQ Básico", Descricao = "Where, Select, OrderBy e First", ConteudoTeoricoMarkdown = "## LINQ — Language Integrated Query\n\n```csharp\nvar numeros = new List<int> { 5, 3, 8, 1, 9, 2 };\n\n// Filtrar\nvar pares = numeros.Where(n => n % 2 == 0).ToList();\n\n// Transformar\nvar dobrados = numeros.Select(n => n * 2).ToList();\n\n// Ordenar\nvar ordenados = numeros.OrderBy(n => n).ToList();\n\n// Primeiro elemento\nvar maior = numeros.Max();\nvar primeiro = numeros.First(n => n > 5);\n\n// Encadeamento\nvar resultado = numeros\n    .Where(n => n > 3)\n    .OrderByDescending(n => n)\n    .Select(n => $\"Número: {n}\")\n    .ToList();\n```", Ordem = 2, XPRecompensa = 25, Ativo = true },
            new Licao { Id = 8, ModuloId = 4, Titulo = "async/await", Descricao = "Tarefas assíncronas com Task e async/await", ConteudoTeoricoMarkdown = "## Programação Assíncrona\n\nasync/await permite executar operações sem bloquear a thread principal.\n\n```csharp\npublic async Task<string> BuscarDadosAsync(int id)\n{\n    // Simula chamada a banco/API\n    await Task.Delay(100);\n    return $\"Dados do id {id}\";\n}\n\n// Chamando:\nvar dados = await BuscarDadosAsync(42);\n```\n\n### Múltiplas tasks em paralelo\n```csharp\nvar task1 = BuscarDadosAsync(1);\nvar task2 = BuscarDadosAsync(2);\n\nvar resultados = await Task.WhenAll(task1, task2);\n```\n\n### Método sem retorno\n```csharp\npublic async Task SalvarAsync(string dados)\n{\n    await File.WriteAllTextAsync(\"arquivo.txt\", dados);\n}\n```", Ordem = 1, XPRecompensa = 30, Ativo = true },
            new Licao { Id = 9, ModuloId = 5, Titulo = "Repository Pattern", Descricao = "Abstraindo o acesso a dados com Repository", ConteudoTeoricoMarkdown = "## Repository Pattern\n\nIsola a lógica de acesso a dados da lógica de negócio.\n\n```csharp\n// 1. Interface — define o contrato\npublic interface IProdutoRepository\n{\n    Task<Produto?> ObterPorIdAsync(int id);\n    Task<IEnumerable<Produto>> ObterTodosAsync();\n    Task AdicionarAsync(Produto produto);\n    Task<bool> SalvarAsync();\n}\n\n// 2. Implementação — detalhe de infraestrutura\npublic class ProdutoRepository(AppDbContext ctx) : IProdutoRepository\n{\n    public async Task<Produto?> ObterPorIdAsync(int id)\n        => await ctx.Produtos.FindAsync(id);\n\n    public async Task AdicionarAsync(Produto produto)\n        => await ctx.Produtos.AddAsync(produto);\n\n    public async Task<bool> SalvarAsync()\n        => await ctx.SaveChangesAsync() > 0;\n}\n\n// 3. Controller — depende da interface, nunca da implementação\npublic class ProdutoController(IProdutoRepository repo) : ControllerBase { }\n```", Ordem = 1, XPRecompensa = 35, Ativo = true }
        );

        // Seed Exercícios — 3 por lição
        modelBuilder.Entity<Exercicio>().HasData(
            // Lição 1 — Variáveis e Tipos
            new Exercicio { Id = 1, LicaoId = 1, Enunciado = "Qual palavra-chave permite que o compilador C# infira o tipo de uma variável automaticamente?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"var\",\"dynamic\",\"object\",\"auto\"]", RespostaCorreta = "var", Explicacao = "'var' é uma palavra-chave de inferência de tipo em tempo de compilação. O tipo é definido pelo compilador com base no valor atribuído.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 2, LicaoId = 1, Enunciado = "Em C#, 'string' e 'String' representam o mesmo tipo.", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Verdadeiro", Explicacao = "'string' é um alias da linguagem para 'System.String'. Ambos são idênticos e intercambiáveis.", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 3, LicaoId = 1, Enunciado = "Para declarar um valor que não pode ser alterado em C#, usa-se a palavra-chave: ____", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "const", Explicacao = "'const' declara uma constante em tempo de compilação. Diferente de 'readonly', que é resolvida em tempo de execução.", Ordem = 3, XPRecompensa = 5 },

            // Lição 2 — Controle de Fluxo
            new Exercicio { Id = 4, LicaoId = 2, Enunciado = "Qual estrutura é mais adequada em C# para comparar uma variável contra múltiplos valores constantes?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"switch\",\"if/else\",\"while\",\"do/while\"]", RespostaCorreta = "switch", Explicacao = "'switch' é otimizado para comparação de um valor contra constantes. A switch expression do C# 8+ torna ainda mais conciso.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 5, LicaoId = 2, Enunciado = "É possível modificar os elementos de uma coleção durante uma iteração com foreach.", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Falso", Explicacao = "foreach não permite modificar a coleção durante a iteração. Para isso, use um loop for convencional ou crie uma nova lista.", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 6, LicaoId = 2, Enunciado = "O operador ternário em C# segue a sintaxe: condição ____ valorTrue : valorFalse", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "?", Explicacao = "O operador ternário usa '?' para separar a condição do valor verdadeiro, e ':' para separar os dois valores.", Ordem = 3, XPRecompensa = 5 },

            // Lição 3 — Métodos
            new Exercicio { Id = 7, LicaoId = 3, Enunciado = "Qual modificador de acesso restringe um método ao escopo da própria classe?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"private\",\"public\",\"protected\",\"internal\"]", RespostaCorreta = "private", Explicacao = "'private' limita o acesso ao membro apenas dentro da classe que o declara. É o modificador mais restritivo.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 8, LicaoId = 3, Enunciado = "Em C# é possível ter dois métodos com o mesmo nome desde que tenham assinaturas diferentes (sobrecarga).", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Verdadeiro", Explicacao = "Sobrecarga (overloading) permite múltiplos métodos com o mesmo nome, diferenciados pelo número ou tipo de parâmetros.", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 9, LicaoId = 3, Enunciado = "Um método que não retorna nenhum valor usa o tipo de retorno: ____", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "void", Explicacao = "'void' indica ausência de retorno. Para métodos assíncronos sem retorno, usa-se 'Task' no lugar de 'async void'.", Ordem = 3, XPRecompensa = 5 },

            // Lição 4 — Classes e Objetos
            new Exercicio { Id = 10, LicaoId = 4, Enunciado = "O que é um construtor em C#?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"Método especial que inicializa uma instância da classe\",\"Uma propriedade obrigatória\",\"Um tipo de herança\",\"Uma interface implícita\"]", RespostaCorreta = "Método especial que inicializa uma instância da classe", Explicacao = "O construtor tem o mesmo nome da classe, não tem tipo de retorno e é chamado automaticamente ao instanciar o objeto com 'new'.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 11, LicaoId = 4, Enunciado = "Uma classe pode ter múltiplos construtores com parâmetros diferentes.", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Verdadeiro", Explicacao = "Isso é sobrecarga de construtor. É comum ter um construtor padrão (sem parâmetros) e outros construtores com parâmetros.", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 12, LicaoId = 4, Enunciado = "Para criar uma nova instância de uma classe chamada Carro, usamos: var carro = ____ Carro();", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "new", Explicacao = "'new' aloca memória e chama o construtor da classe para inicializar o objeto.", Ordem = 3, XPRecompensa = 5 },

            // Lição 5 — Herança e Polimorfismo
            new Exercicio { Id = 13, LicaoId = 5, Enunciado = "Qual símbolo é usado em C# para indicar que uma classe herda de outra?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\":\",\"extends\",\"inherits\",\"->\"]", RespostaCorreta = ":", Explicacao = "Em C# usamos ':' para herança (e também para implementar interfaces). Ex: 'public class Cachorro : Animal'.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 14, LicaoId = 5, Enunciado = "Em C#, uma classe pode herdar de múltiplas classes ao mesmo tempo.", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Falso", Explicacao = "C# não suporta herança múltipla de classes. Para comportamento múltiplo, usa-se interfaces, que podem ser implementadas em quantidade ilimitada.", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 15, LicaoId = 5, Enunciado = "Para sobrescrever um método virtual na subclasse, usa-se a palavra-chave: ____", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "override", Explicacao = "'override' indica que o método substitui a implementação da classe base marcada com 'virtual' ou 'abstract'.", Ordem = 3, XPRecompensa = 5 },

            // Lição 6 — Listas e Arrays
            new Exercicio { Id = 16, LicaoId = 6, Enunciado = "Qual é a principal diferença entre Array e List<T> em C#?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"List<T> tem tamanho dinâmico, Array tem tamanho fixo\",\"Array é mais seguro que List<T>\",\"List<T> não aceita tipos genéricos\",\"São equivalentes em todos os aspectos\"]", RespostaCorreta = "List<T> tem tamanho dinâmico, Array tem tamanho fixo", Explicacao = "Arrays têm tamanho definido na criação. List<T> cresce dinamicamente com Add() e encolhe com Remove().", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 17, LicaoId = 6, Enunciado = "O método List<T>.Add() adiciona elementos ao início da lista.", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Falso", Explicacao = "Add() adiciona ao FINAL da lista. Para adicionar no início ou em posição específica, use Insert(index, item).", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 18, LicaoId = 6, Enunciado = "Para obter a quantidade de elementos em uma List<T>, usa-se a propriedade: ____", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "Count", Explicacao = "List<T>.Count retorna o número de elementos. Arrays usam .Length. LINQ usa .Count() (método de extensão).", Ordem = 3, XPRecompensa = 5 },

            // Lição 7 — LINQ
            new Exercicio { Id = 19, LicaoId = 7, Enunciado = "Qual método LINQ retorna o primeiro elemento que satisfaz uma condição e lança exceção se não encontrar?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"First()\",\"Select()\",\"Where()\",\"Take()\"]", RespostaCorreta = "First()", Explicacao = "First() lança InvalidOperationException se não houver elemento. Use FirstOrDefault() para retornar null/default quando não encontrar.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 20, LicaoId = 7, Enunciado = "LINQ só funciona com coleções em memória (LINQ to Objects).", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Falso", Explicacao = "LINQ funciona com qualquer IQueryable<T> ou IEnumerable<T>: banco de dados via EF Core (LINQ to SQL), XML, arquivos, etc.", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 21, LicaoId = 7, Enunciado = "Para filtrar elementos em LINQ com uma condição, usa-se o método: ____", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "Where", Explicacao = "Where() filtra elementos que satisfazem um predicado. Retorna IEnumerable<T> com os elementos que passaram no filtro.", Ordem = 3, XPRecompensa = 5 },

            // Lição 8 — async/await
            new Exercicio { Id = 22, LicaoId = 8, Enunciado = "O que acontece quando a runtime encontra um 'await' em um método assíncrono?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"A thread é liberada para outras tarefas enquanto aguarda\",\"O programa é pausado completamente\",\"Uma nova thread é criada automaticamente\",\"O método é cancelado\"]", RespostaCorreta = "A thread é liberada para outras tarefas enquanto aguarda", Explicacao = "await suspende o método atual sem bloquear a thread. A thread retorna ao pool e pode processar outras requisições, melhorando a escalabilidade.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 23, LicaoId = 8, Enunciado = "Todo método que usa 'await' deve ter 'async' em sua declaração.", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Verdadeiro", Explicacao = "'async' e 'await' são inseparáveis. 'async' marca o método como assíncrono e habilita o uso de 'await' dentro dele.", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 24, LicaoId = 8, Enunciado = "Um método assíncrono que não retorna valor deve ter tipo de retorno: ____", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "Task", Explicacao = "Use 'Task' (não 'async void') para métodos assíncronos sem retorno. 'async void' não pode ser aguardado e dificulta o tratamento de exceções.", Ordem = 3, XPRecompensa = 5 },

            // Lição 9 — Repository Pattern
            new Exercicio { Id = 25, LicaoId = 9, Enunciado = "Qual é o principal benefício do Repository Pattern?", Tipo = TipoExercicio.MultiplaEscolha, OpcoesJson = "[\"Desacoplar a lógica de negócio do acesso a dados\",\"Aumentar a velocidade das queries\",\"Reduzir a quantidade de código\",\"Substituir completamente o ORM\"]", RespostaCorreta = "Desacoplar a lógica de negócio do acesso a dados", Explicacao = "O Repository isola 'como' os dados são obtidos (SQL, EF, API externa) da lógica que decide 'o que' fazer com eles. Facilita testes e troca de infraestrutura.", Ordem = 1, XPRecompensa = 5 },
            new Exercicio { Id = 26, LicaoId = 9, Enunciado = "No Repository Pattern, um Controller pode acessar o DbContext diretamente quando precisar de performance.", Tipo = TipoExercicio.VerdadeiroFalso, OpcoesJson = "[\"Verdadeiro\",\"Falso\"]", RespostaCorreta = "Falso", Explicacao = "Controllers NUNCA devem acessar infraestrutura diretamente. Se precisar de performance, otimize o Repository (projeções, AsNoTracking, paginação).", Ordem = 2, XPRecompensa = 5 },
            new Exercicio { Id = 27, LicaoId = 9, Enunciado = "Em Clean Architecture, o Controller deve depender da ____ do Repository, não da implementação concreta.", Tipo = TipoExercicio.PreencherEspacos, OpcoesJson = "[]", RespostaCorreta = "interface", Explicacao = "Dependência de abstrações (interfaces) é o Dependency Inversion Principle (D do SOLID). Permite trocar SQLite por SQL Server sem alterar o Controller.", Ordem = 3, XPRecompensa = 5 }
        );
    }
}
