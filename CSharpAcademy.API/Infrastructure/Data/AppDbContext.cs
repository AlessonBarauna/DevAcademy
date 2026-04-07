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
    public DbSet<Conquista> Conquistas => Set<Conquista>();
    public DbSet<MissaoDiaria> MissoesDiarias => Set<MissaoDiaria>();
    public DbSet<NotaLicao> NotasLicao => Set<NotaLicao>();
    public DbSet<ProjetoTarefaConcluida> ProjetoTarefasConcluidas => Set<ProjetoTarefaConcluida>();

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

        modelBuilder.Entity<NotaLicao>()
            .HasIndex(n => new { n.UsuarioId, n.LicaoId }).IsUnique();

        modelBuilder.Entity<ProjetoTarefaConcluida>()
            .HasIndex(p => new { p.UsuarioId, p.ProjetoId, p.TarefaId }).IsUnique();

        modelBuilder.Entity<ProjetoTarefaConcluida>()
            .HasOne(p => p.Usuario).WithMany().HasForeignKey(p => p.UsuarioId).OnDelete(DeleteBehavior.Cascade);

        // Seed Módulos
        modelBuilder.Entity<Modulo>().HasData(
            new Modulo { Id = 1, Titulo = "Fundamentos C#", Descricao = "Variáveis, tipos, operadores e controle de fluxo", Ordem = 1, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true, PreRequisitoId = null },
            new Modulo { Id = 2, Titulo = "Orientação a Objetos", Descricao = "Classes, herança, polimorfismo e encapsulamento", Ordem = 2, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true, PreRequisitoId = 1 },
            new Modulo { Id = 3, Titulo = "Coleções e LINQ", Descricao = "List, Dictionary, Array e consultas LINQ", Ordem = 3, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true, PreRequisitoId = 2 },
            new Modulo { Id = 4, Titulo = "Async/Await e Concorrência", Descricao = "Programação assíncrona, Tasks e threading", Ordem = 4, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true, PreRequisitoId = 3 },
            new Modulo { Id = 5, Titulo = "Design Patterns", Descricao = "Repository, Factory, Singleton e outros padrões", Ordem = 5, NivelMinimo = NivelDificuldade.Avancado, Ativo = true, PreRequisitoId = 4 },
            new Modulo { Id = 6, Titulo = "Entity Framework Core", Descricao = "DbContext, Migrations, consultas e operações CRUD com EF Core", Ordem = 6, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true, PreRequisitoId = 5 },
            new Modulo { Id = 7, Titulo = "CRUD com ASP.NET Web API", Descricao = "Criando APIs REST completas com controllers, verbos HTTP e status codes", Ordem = 7, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true, PreRequisitoId = 6 },
            new Modulo { Id = 8, Titulo = "Validação e DTOs", Descricao = "Data Annotations, FluentValidation e padrão DTO para transferência de dados", Ordem = 8, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true, PreRequisitoId = 7 },
            new Modulo { Id = 9, Titulo = "Autenticação JWT", Descricao = "Tokens JWT, autenticação, autorização e roles em APIs .NET", Ordem = 9, NivelMinimo = NivelDificuldade.Avancado, Ativo = true, PreRequisitoId = 8 },
            new Modulo { Id = 10, Titulo = "Middleware e Tratamento de Erros", Descricao = "Exception handling global, criação de middleware e logging com ILogger", Ordem = 10, NivelMinimo = NivelDificuldade.Avancado, Ativo = true, PreRequisitoId = 9 },
            new Modulo { Id = 11, Titulo = "Testes com xUnit", Descricao = "Testes unitários, mocking com Moq e introdução ao TDD", Ordem = 11, NivelMinimo = NivelDificuldade.Avancado, Ativo = true, PreRequisitoId = 5 },
            new Modulo { Id = 12, Titulo = "Injeção de Dependência", Descricao = "Princípios de DI, IoC Container do .NET e lifetimes de serviços", Ordem = 12, NivelMinimo = NivelDificuldade.Avancado, Ativo = true, PreRequisitoId = 5 },
            new Modulo { Id = 13, Titulo = "Clean Architecture", Descricao = "Separação de camadas, Use Cases, Entities e aplicando Clean Arch em APIs .NET", Ordem = 13, NivelMinimo = NivelDificuldade.Especialista, Ativo = true, PreRequisitoId = 12 }
        );

        // Seed Lições
        modelBuilder.Entity<Licao>().HasData(

            // ── Módulo 1: Fundamentos C# ─────────────────────────────────────────────
            new Licao
            {
                Id = 1, ModuloId = 1, Ordem = 1, XPRecompensa = 10, Ativo = true,
                Titulo = "Variáveis e Tipos",
                Descricao = "Tipos primitivos, inferência com var, const, nullable e conversão de tipos",
                ConteudoTeoricoMarkdown =
                    "## Variáveis e Tipos de Dados em C#\n\n" +
                    "> 💡 **Cenário real**: Imagine que você está construindo um sistema de e-commerce. " +
                    "Você precisa guardar o *nome* do produto (`\"Notebook\"`), o *preço* (`3499.99m`), " +
                    "a *quantidade* em estoque (`5`) e se está *disponível* para venda (`true`). " +
                    "Cada informação tem uma natureza diferente — C# exige que você declare o tipo correto " +
                    "para cada dado, e isso é o que torna seu código seguro, rápido e livre de surpresas em produção.\n\n" +
                    "C# é **fortemente tipado**: toda variável tem um tipo definido em tempo de compilação. " +
                    "O compilador valida cada operação com base nesses tipos, eliminando erros antes de rodar o programa.\n\n" +
                    "### Tipos primitivos\n\n" +
                    "```csharp\n" +
                    "// Inteiros\nbyte   b = 255;              // 0–255 (8 bits)\nshort  s = 32_767;           // –32.768 a 32.767 (16 bits)\nint    i = 2_147_483_647;    // tipo padrão para inteiros (32 bits)\nlong   l = 9_999_999_999L;   // sufixo L obrigatório (64 bits)\n\n" +
                    "// Ponto flutuante\nfloat   f = 3.14f;    // sufixo f obrigatório (~7 dígitos de precisão)\ndouble  d = 3.14159;  // padrão para decimais (~15 dígitos)\ndecimal m = 19.99m;   // sufixo m — use para DINHEIRO (sem erro de arredondamento binário)\n\n" +
                    "// Outros\nbool   ok   = true;\nchar   c    = 'A';      // aspas simples — um único caractere Unicode\nstring nome = \"Maria\"; // aspas duplas — sequência de chars (tipo referência)\n" +
                    "```\n\n" +
                    "### var — inferência de tipo\n\n" +
                    "A palavra-chave `var` instrui o compilador a deduzir o tipo pelo valor atribuído. " +
                    "O tipo é **fixo** após a declaração — `var` não é `dynamic`:\n\n" +
                    "```csharp\nvar cidade  = \"São Paulo\"; // string\nvar preco   = 9.99m;       // decimal\nvar ativo   = true;        // bool\n// cidade = 42;            // ERRO de compilação — tipo já é string\n```\n\n" +
                    "> Use `var` quando o tipo é **óbvio** pelo lado direito. Evite quando prejudica a leitura.\n\n" +
                    "### string e String são o mesmo tipo\n\n" +
                    "`string` (minúsculo) é um **alias da linguagem** para `System.String`. " +
                    "São 100% intercambiáveis — use `string` por convenção no código:\n\n" +
                    "```csharp\nstring a = \"olá\";   // alias preferido\nString b = \"olá\";   // classe do framework — mesmo tipo\nbool igual = a.GetType() == b.GetType(); // true\n```\n\n" +
                    "### const vs readonly\n\n" +
                    "```csharp\nconst double PI = 3.14159265;   // compilação — valor gravado no IL, nunca muda\nreadonly int Capacidade;         // definida no construtor — pode variar por instância\n\n" +
                    "// const exige valor em tempo de compilação:\n// const DateTime Hoje = DateTime.Now; // ERRO — DateTime.Now não é constante\n```\n\n" +
                    "### Tipos Nullable\n\n" +
                    "Tipos de valor (`int`, `bool`, etc.) não aceitam `null` por padrão. " +
                    "Adicione `?` para torná-los anuláveis:\n\n" +
                    "```csharp\nint? idade = null;\nif (idade.HasValue)\n    Console.WriteLine(idade.Value);\n\n" +
                    "// Operador de coalescência nula\nint resultado = idade ?? 0;  // usa 0 se idade for null\nidade ??= 18;               // atribui 18 apenas se ainda for null\n```\n\n" +
                    "### Conversão de tipos\n\n" +
                    "```csharp\n// Implícita — sem perda de dados\nint  x = 100;\nlong y = x;   // long é maior, conversão automática\n\n" +
                    "// Explícita (cast) — pode perder dados\ndouble d = 9.99;\nint    i = (int)d;  // i = 9, parte decimal descartada\n\n" +
                    "// Parsing seguro de strings\nbool ok = int.TryParse(\"abc\", out int val); // ok = false, val = 0 — nunca lança exceção\nint n   = int.Parse(\"42\");                  // lança FormatException se inválido\n```\n\n" +
                    "### Guia rápido: qual tipo usar?\n\n" +
                    "| Situação | Tipo recomendado | Por quê |\n|---|---|---|\n" +
                    "| Contagem, IDs, índices | `int` | Suficiente para 99% dos casos |\n" +
                    "| Valores monetários | `decimal` | Sem erro de arredondamento binário |\n" +
                    "| Cálculos científicos | `double` | Alta precisão, mais rápido que decimal |\n" +
                    "| Texto | `string` | Tipo nativo para sequências de caracteres |\n" +
                    "| Sim/Não, flags | `bool` | Expressivo e sem ambiguidade |\n" +
                    "| Datas e horas | `DateTime` | Nunca use string para armazenar datas! |"
            },

            new Licao
            {
                Id = 2, ModuloId = 1, Ordem = 2, XPRecompensa = 10, Ativo = true,
                Titulo = "Controle de Fluxo",
                Descricao = "if/else, operador ternário, switch expression, for, foreach e while",
                ConteudoTeoricoMarkdown =
                    "## Controle de Fluxo em C#\n\n" +
                    "> 💡 **Analogia**: Pense em um caixa de supermercado — *se* o cliente tem cartão fidelidade, " +
                    "aplica 10% de desconto; *se não*, cobra o preço normal. *Para cada* item no carrinho, " +
                    "registra a venda. *Enquanto* a fila não esvaziar, atende o próximo. " +
                    "Essa tomada de decisão e repetição é exatamente o que o controle de fluxo faz no código.\n\n" +
                    "Controle de fluxo determina **quais instruções são executadas** e em que ordem.\n\n" +
                    "### if / else if / else\n\n" +
                    "```csharp\nif (nota >= 9)\n    Console.WriteLine(\"Excelente\");\nelse if (nota >= 7)\n    Console.WriteLine(\"Aprovado\");\nelse\n    Console.WriteLine(\"Reprovado\");\n```\n\n" +
                    "### Operador ternário `? :`\n\n" +
                    "A forma mais concisa de um `if/else` que retorna um valor:\n\n" +
                    "```csharp\n// sintaxe: condição ? valorSeVerdadeiro : valorSeFalso\nstring resultado = nota >= 7 ? \"Aprovado\" : \"Reprovado\";\nint absoluto = x >= 0 ? x : -x;\n\n" +
                    "// Pode ser encadeado (use com moderação)\nstring nivel = nota >= 9 ? \"A\" : nota >= 7 ? \"B\" : \"C\";\n```\n\n" +
                    "### switch expression (C# 8+)\n\n" +
                    "Forma moderna e concisa de múltiplas comparações:\n\n" +
                    "```csharp\nstring nivel = nota switch\n{\n    >= 9  => \"Excelente\",\n    >= 7  => \"Aprovado\",\n    >= 5  => \"Recuperação\",\n    _     => \"Reprovado\"   // _ é o caso padrão (default)\n};\n\n" +
                    "// switch statement clássico (ainda útil para tipos exatos)\nswitch (diaSemana)\n{\n    case \"Sábado\":\n    case \"Domingo\":\n        Console.WriteLine(\"Fim de semana\"); break;\n    default:\n        Console.WriteLine(\"Dia útil\"); break;\n}\n```\n\n" +
                    "> **Quando usar switch vs if/else?** `switch` é preferível ao comparar uma variável contra **múltiplos valores constantes**.\n\n" +
                    "### for e foreach\n\n" +
                    "```csharp\n// for — use quando precisa do índice\nfor (int i = 0; i < 5; i++)\n    Console.WriteLine(i);\n\n" +
                    "// foreach — use para percorrer coleções (mais legível)\nvar nomes = new[] { \"Ana\", \"Bruno\", \"Carlos\" };\nforeach (var nome in nomes)\n    Console.WriteLine(nome);\n\n" +
                    "// ATENÇÃO: NÃO modifique a coleção dentro do foreach\n// foreach (var item in lista) { lista.Remove(item); } // InvalidOperationException!\n// Para remover durante iteração, use um for reverso ou crie uma nova lista\n```\n\n" +
                    "### while e do-while\n\n" +
                    "```csharp\n// while — verifica ANTES de executar (pode não executar nenhuma vez)\nint i = 0;\nwhile (i < 3) { Console.WriteLine(i); i++; }\n\n" +
                    "// do-while — executa pelo menos UMA vez\ndo {\n    Console.Write(\"Tente novamente: \");\n    entrada = Console.ReadLine();\n} while (entrada != \"sair\");\n```\n\n" +
                    "### break e continue\n\n" +
                    "```csharp\nforeach (var n in numeros)\n{\n    if (n == 0) continue;  // pula para a próxima iteração\n    if (n < 0)  break;     // encerra o loop completamente\n    Console.WriteLine(100 / n);\n}\n```\n\n" +
                    "### Guia: qual estrutura usar?\n\n" +
                    "| Situação | Use |\n|---|---|\n" +
                    "| Uma condição simples | `if/else` |\n" +
                    "| Expressão que retorna valor | Operador ternário `? :` |\n" +
                    "| Comparar variável contra N valores constantes | `switch expression` |\n" +
                    "| Quantidade de iterações conhecida | `for` |\n" +
                    "| Percorrer coleção sem precisar do índice | `foreach` |\n" +
                    "| Repetir enquanto condição for verdadeira | `while` |\n" +
                    "| Executar pelo menos uma vez, depois verificar | `do-while` |"
            },

            new Licao
            {
                Id = 3, ModuloId = 1, Ordem = 3, XPRecompensa = 15, Ativo = true,
                Titulo = "Métodos e Funções",
                Descricao = "Modificadores de acesso, parâmetros, sobrecarga e métodos expressão",
                ConteudoTeoricoMarkdown =
                    "## Métodos em C#\n\n" +
                    "> 💡 **Analogia**: Um método é como uma receita de cozinha — você define os ingredientes " +
                    "(parâmetros), as instruções (corpo do método) e o prato pronto (valor de retorno). " +
                    "A diferença é que no código você escreve a receita **uma vez** e pode usá-la infinitas vezes, " +
                    "de qualquer lugar da aplicação, sem repetir código.\n\n" +
                    "Métodos encapsulam um bloco de lógica reutilizável. São a unidade básica de comportamento em C#.\n\n" +
                    "### Anatomia de um método\n\n" +
                    "```csharp\n//  modificador  retorno  nome     parâmetros\n    public       int      Somar   (int a, int b)\n    {\n        return a + b;\n    }\n\n" +
                    "// Expression-bodied (=> ) — para métodos de uma linha\npublic int Somar(int a, int b) => a + b;\npublic string Saudar(string nome) => $\"Olá, {nome}!\";\n```\n\n" +
                    "### Modificadores de acesso\n\n" +
                    "Controlam de onde o método pode ser chamado:\n\n" +
                    "```csharp\npublic class Calculadora\n{\n    public  double Resultado { get; private set; }  // public: acessível em qualquer lugar\n    private double Calcular(double a, double b) => a + b; // private: só dentro desta classe\n\n    // protected: esta classe + quem herdar dela\n    // internal:  qualquer classe do mesmo projeto (assembly)\n    // protected internal: union de ambos\n\n    public void Somar(double a, double b)\n        => Resultado = Calcular(a, b); // OK — Calcular é private mas estamos na mesma classe\n}\n\n// var c = new Calculadora();\n// c.Calcular(1, 2); // ERRO — Calcular é private\n```\n\n" +
                    "> A regra é: **o mais restritivo possível**. Se algo não precisa ser público, faça `private`.\n\n" +
                    "### void — métodos sem retorno\n\n" +
                    "```csharp\npublic void Logar(string mensagem)\n{\n    Console.WriteLine($\"[{DateTime.Now:HH:mm:ss}] {mensagem}\");\n    // sem return (ou return; vazio para sair antecipadamente)\n}\n```\n\n" +
                    "### Parâmetros opcionais e nomeados\n\n" +
                    "```csharp\npublic string Formatar(string texto, bool maiusculo = false, string prefixo = \"\")\n    => prefixo + (maiusculo ? texto.ToUpper() : texto);\n\n" +
                    "// Chamadas equivalentes:\nFormatar(\"olá\");                            // usa defaults\nFormatar(\"olá\", maiusculo: true);           // parâmetro nomeado\nFormatar(\"olá\", prefixo: \">> \", maiusculo: true); // ordem livre com nomes\n```\n\n" +
                    "### Sobrecarga (Overloading)\n\n" +
                    "Dois métodos com o **mesmo nome** mas **assinaturas diferentes** (tipos ou quantidade de parâmetros):\n\n" +
                    "```csharp\npublic int    Calcular(int a, int b)       => a + b;\npublic double Calcular(double a, double b) => a + b;\npublic int    Calcular(int a, int b, int c) => a + b + c;\n\n// O compilador escolhe a versão correta pelo tipo dos argumentos\nCalcular(1, 2);        // chama versão int\nCalcular(1.5, 2.5);    // chama versão double\n```\n\n" +
                    "### params — número variável de argumentos\n\n" +
                    "```csharp\npublic int Somar(params int[] numeros) => numeros.Sum();\n\nSomar(1, 2, 3);      // 6\nSomar(10, 20);       // 30\nSomar();             // 0\n```\n\n" +
                    "### Boas práticas de nomenclatura\n\n" +
                    "```csharp\n// ✅ Verbos descritivos — métodos fazem algo\npublic void EnviarEmail(string destinatario) { }\npublic decimal CalcularDesconto(decimal preco) { }\npublic bool ValidarCpf(string cpf) { }\npublic Usuario ObterPorId(int id) { }\n\n" +
                    "// ❌ Evite nomes vagos\npublic void Fazer() { }      // fazer o quê?\npublic int Processar() { }   // processar o quê?\npublic bool Check() { }      // verificar o quê?\n```\n\n" +
                    "> **Regra**: Se você precisa comentar o que um método faz, é sinal de que o nome não está claro o suficiente. " +
                    "Um bom nome **documenta sozinho**: `CalcularImpostoNFe()` é melhor do que `Calcular()` com um comentário explicando."
            },

            // ── Módulo 2: Orientação a Objetos ───────────────────────────────────────
            new Licao
            {
                Id = 4, ModuloId = 2, Ordem = 1, XPRecompensa = 15, Ativo = true,
                Titulo = "Classes e Objetos",
                Descricao = "Propriedades, construtores múltiplos, new e inicializadores de objeto",
                ConteudoTeoricoMarkdown =
                    "## Classes e Objetos em C#\n\n" +
                    "> 💡 **Analogia**: A ficha cadastral de produto de um e-commerce é a *classe* — " +
                    "ela define que todo produto tem nome, preço e estoque. " +
                    "Cada produto específico (`\"Notebook Dell\"`, `\"Mouse Logitech\"`) é um *objeto* — " +
                    "uma instância concreta com seus próprios valores. " +
                    "Você pode criar milhares de objetos a partir de uma única classe, assim como " +
                    "uma fábrica produz milhares de carros a partir de um mesmo projeto.\n\n" +
                    "Uma **classe** é um molde (blueprint). Um **objeto** é uma instância concreta desse molde, criada com `new`.\n\n" +
                    "### Declaração de classe\n\n" +
                    "```csharp\npublic class Produto\n{\n    // Propriedades (preferidas a campos públicos)\n    public int    Id    { get; set; }\n    public string Nome  { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n\n    // Método de instância\n    public string Descricao() => $\"{Nome} — R$ {Preco:F2}\";\n}\n```\n\n" +
                    "### Construtores\n\n" +
                    "O construtor tem o **mesmo nome da classe**, não tem tipo de retorno e é chamado automaticamente pelo `new`:\n\n" +
                    "```csharp\npublic class Produto\n{\n    public string Nome  { get; }\n    public decimal Preco { get; }\n\n    // Construtor padrão (sem parâmetros)\n    public Produto() { Nome = \"Sem nome\"; Preco = 0; }\n\n    // Construtor com parâmetros\n    public Produto(string nome, decimal preco)\n    {\n        Nome  = nome;\n        Preco = preco;\n    }\n\n    // Encadeamento: chama o outro construtor com : this(...)\n    public Produto(string nome) : this(nome, 0m) { }\n}\n```\n\n" +
                    "**Uma classe pode ter múltiplos construtores** — isso se chama sobrecarga de construtores. " +
                    "O compilador escolhe qual chamar com base nos argumentos passados ao `new`.\n\n" +
                    "### Criando objetos com `new`\n\n" +
                    "`new` aloca memória no heap, chama o construtor e retorna a referência ao objeto:\n\n" +
                    "```csharp\nvar p1 = new Produto();                   // construtor padrão\nvar p2 = new Produto(\"Notebook\", 3500m);   // construtor com 2 parâmetros\nvar p3 = new Produto(\"Mouse\");             // construtor com 1 parâmetro\n\nConsole.WriteLine(p2.Descricao()); // Notebook — R$ 3500,00\n```\n\n" +
                    "### Inicializadores de objeto `{ }`\n\n" +
                    "Permite definir propriedades públicas sem precisar de um construtor específico:\n\n" +
                    "```csharp\nvar p = new Produto { Id = 1, Nome = \"Teclado\", Preco = 150m };\n\n// Equivalente a:\nvar p = new Produto();\np.Id    = 1;\np.Nome  = \"Teclado\";\np.Preco = 150m;\n```\n\n" +
                    "### Membros estáticos\n\n" +
                    "Membros estáticos pertencem à **classe**, não a instâncias individuais:\n\n" +
                    "```csharp\npublic class Contador\n{\n    public static int Total { get; private set; } = 0;\n\n    public Contador() { Total++; } // cada new incrementa o contador estático\n}\n\nvar a = new Contador();\nvar b = new Contador();\nConsole.WriteLine(Contador.Total); // 2 — acesso via nome da classe\n```\n\n" +
                    "### Classes vs Structs — uma distinção importante\n\n" +
                    "```csharp\n// Classe — TIPO DE REFERÊNCIA\n// Armazenada no heap; variáveis guardam referência (ponteiro)\nvar p1 = new Produto { Nome = \"Teclado\" };\nvar p2 = p1;          // p2 aponta para O MESMO objeto!\np2.Nome = \"Mouse\";    // p1.Nome também vira \"Mouse\"!\n\n// Struct — TIPO DE VALOR (ex: int, DateTime, decimal)\n// Armazenada no stack; variáveis guardam o valor diretamente\nint a = 5;\nint b = a;  // b é uma CÓPIA independente\nb = 10;     // a continua sendo 5\n```\n\n" +
                    "> **Regra prática**: use **classes** para entidades de negócio (Produto, Usuario, Pedido). " +
                    "Use **structs** para dados pequenos e imutáveis (coordenadas, cores, ranges). " +
                    "Na dúvida, use classe."
            },

            new Licao
            {
                Id = 5, ModuloId = 2, Ordem = 2, XPRecompensa = 20, Ativo = true,
                Titulo = "Herança e Polimorfismo",
                Descricao = "Herança com :, virtual/override, classes abstratas e interfaces",
                ConteudoTeoricoMarkdown =
                    "## Herança e Polimorfismo\n\n" +
                    "> 💡 **Cenário real**: Em um banco, toda conta tem titular, saldo e operações de " +
                    "depósito/saque. Mas `ContaCorrente` tem limite de cheque especial, e `ContaPoupanca` " +
                    "rende juros mensalmente. Com herança, ambas reutilizam a lógica comum de `Conta` " +
                    "e adicionam apenas o que é específico — sem duplicar código.\n\n" +
                    "**Herança** permite que uma classe reutilize e estenda o comportamento de outra. " +
                    "**Polimorfismo** permite tratar objetos de subclasses de forma uniforme.\n\n" +
                    "### Herança com `:`\n\n" +
                    "```csharp\npublic class Animal\n{\n    public string Nome { get; set; } = string.Empty;\n    public virtual string EmitirSom() => \"...\";\n    public void Respirar() => Console.WriteLine($\"{Nome} respira\");\n}\n\npublic class Cachorro : Animal\n{\n    public override string EmitirSom() => \"Au au!\";\n}\n\npublic class Gato : Animal\n{\n    public override string EmitirSom() => \"Miau!\";\n}\n\n// Polimorfismo: trate subclasses como o tipo base\nAnimal[] animais = { new Cachorro { Nome = \"Rex\" }, new Gato { Nome = \"Mimi\" } };\nforeach (var a in animais)\n    Console.WriteLine($\"{a.Nome}: {a.EmitirSom()}\"); // cada um responde diferente\n```\n\n" +
                    "### C# NÃO suporta herança múltipla de classes\n\n" +
                    "Uma classe só pode herdar de **uma única classe**:\n\n" +
                    "```csharp\n// ERRO de compilação — herança múltipla de classes não existe em C#\n// public class Mula : Cavalo, Burro { }\n\n// SOLUÇÃO: use interfaces para comportamento múltiplo\npublic interface ITransporte { void Transportar(); }\npublic interface IResistente { int NivelResistencia { get; } }\npublic class Mula : Animal, ITransporte, IResistente\n{\n    public void Transportar() => Console.WriteLine(\"Transportando carga\");\n    public int NivelResistencia => 8;\n}\n```\n\n" +
                    "> C# herda de **uma classe** e implementa **n interfaces** — essa é a forma correta de modelar comportamentos múltiplos.\n\n" +
                    "### virtual e override\n\n" +
                    "`virtual` marca um método como substituível. `override` na subclasse substitui a implementação:\n\n" +
                    "```csharp\npublic class Forma\n{\n    public virtual double Area() => 0;\n    public virtual string Descricao() => $\"Área: {Area():F2}\";\n}\n\npublic class Circulo : Forma\n{\n    public double Raio { get; set; }\n    public override double Area() => Math.PI * Raio * Raio;\n    // Descricao() não é sobrescrita — usa a da classe base com Area() correto\n}\n```\n\n" +
                    "### Classes abstratas\n\n" +
                    "Não podem ser instanciadas diretamente. Forçam subclasses a implementar métodos:\n\n" +
                    "```csharp\npublic abstract class Relatorio\n{\n    public abstract string GerarConteudo(); // subclasses DEVEM implementar\n    public void Imprimir() => Console.WriteLine(GerarConteudo()); // método concreto\n}\n\n// var r = new Relatorio(); // ERRO — classe abstrata não pode ser instanciada\n```\n\n" +
                    "### base — acessando a classe pai\n\n" +
                    "```csharp\npublic class ContaPremium : Conta\n{\n    public ContaPremium(string titular) : base(titular) { } // chama construtor da base\n    public override string Descricao() => base.Descricao() + \" [PREMIUM]\";\n}\n```\n\n" +
                    "### Herança vs Composição — quando usar cada uma\n\n" +
                    "```csharp\n// ✅ USE HERANÇA quando há uma relação \"É UM\":\n// Cachorro É UM Animal ✅\n// ContaCorrente É UMA Conta ✅\n\n" +
                    "// ✅ USE COMPOSIÇÃO quando há uma relação \"TEM UM\":\n// Pedido TEM UM Cliente ✅ (não: Pedido É UM Cliente ❌)\n// Carro TEM UM Motor ✅\npublic class Pedido\n{\n    public Cliente Cliente { get; set; } // composição\n    public List<Item> Itens { get; set; } = new();\n}\n```\n\n" +
                    "> **Regra de ouro**: prefira composição à herança quando em dúvida. " +
                    "Herança cria acoplamento forte entre classes. Composição é mais flexível e testável. " +
                    "Use herança somente quando a relação \"É UM\" for genuína e estável."
            },

            new Licao
            {
                Id = 10, ModuloId = 2, Ordem = 3, XPRecompensa = 20, Ativo = true,
                Titulo = "Encapsulamento",
                Descricao = "Modificadores de acesso, propriedades com validação e acessor init",
                ConteudoTeoricoMarkdown =
                    "## Encapsulamento\n\n" +
                    "> 💡 **Analogia**: Pense no painel do seu carro — você vê a velocidade, aperta o freio, " +
                    "gira o volante. Mas você **não tem acesso direto** ao motor para ajustar a injeção de combustível. " +
                    "O motor está *encapsulado*: expõe apenas o que é seguro usar e protege o que poderia ser danificado " +
                    "por uso incorreto. Seus objetos C# devem funcionar da mesma forma.\n\n" +
                    "Encapsulamento significa **esconder os detalhes internos** e expor apenas o que é necessário. " +
                    "É um dos pilares da OOP e evita que código externo coloque objetos em estados inválidos.\n\n" +
                    "### Modificadores de acesso\n\n" +
                    "| Modificador | Acessível por |\n|---|---|\n| `public` | Qualquer lugar |\n| `private` | Apenas a própria classe |\n| `protected` | Classe + subclasses |\n| `internal` | Qualquer classe do mesmo assembly (projeto) |\n| `protected internal` | Subclasses + mesmo assembly |\n| `private protected` | Subclasses dentro do mesmo assembly |\n\n" +
                    "```csharp\npublic class ContaBancaria\n{\n    private decimal _saldo;                  // campo privado — ninguém acessa diretamente\n    public  decimal Saldo => _saldo;         // propriedade somente leitura\n    protected string Titular { get; }        // visível para subclasses\n    internal int    AgenciaId { get; set; }  // visível no mesmo projeto\n\n    public void Depositar(decimal valor)\n    {\n        if (valor <= 0) throw new ArgumentException(\"Valor deve ser positivo\");\n        _saldo += valor; // único ponto de mutação\n    }\n\n    public bool Sacar(decimal valor)\n    {\n        if (valor > _saldo) return false;\n        _saldo -= valor;\n        return true;\n    }\n}\n```\n\n" +
                    "### Propriedades com get público e set privado\n\n" +
                    "O padrão mais comum: leitura pública, escrita controlada:\n\n" +
                    "```csharp\npublic class Produto\n{\n    private decimal _preco;\n\n    // Propriedade com validação no set\n    public decimal Preco\n    {\n        get => _preco;\n        set\n        {\n            if (value < 0) throw new ArgumentException(\"Preço não pode ser negativo\");\n            _preco = value;\n        }\n    }\n\n    // Alternativa concisa: set privado\n    public string Codigo { get; private set; } = Guid.NewGuid().ToString();\n}\n\n// var p = new Produto();\n// p.Preco = -10; // ArgumentException\n// p.Codigo = \"x\"; // ERRO de compilação — set é private\n```\n\n" +
                    "### init — somente no inicializador\n\n" +
                    "C# 9 introduziu `init`, que permite definir a propriedade **apenas durante a construção** do objeto:\n\n" +
                    "```csharp\npublic class Pedido\n{\n    public int      Id      { get; init; }\n    public DateTime Criado  { get; } = DateTime.UtcNow;\n    public string   Cliente { get; init; } = string.Empty;\n}\n\nvar pedido = new Pedido { Id = 42, Cliente = \"João\" }; // OK\n// pedido.Id = 1; // ERRO — init só permite atribuição na inicialização\n```\n\n" +
                    "> `init` é ideal para **objetos imutáveis após criação** — DTOs, Value Objects, records.\n\n" +
                    "### Por que encapsular?\n\n" +
                    "- **Invariantes garantidas**: o objeto nunca fica em estado inválido\n" +
                    "- **Facilidade de mudança**: a implementação interna pode mudar sem quebrar quem usa\n" +
                    "- **Testabilidade**: o comportamento é previsível e isolado"
            },

            // ── Módulo 3: Coleções e LINQ ────────────────────────────────────────────
            new Licao
            {
                Id = 6, ModuloId = 3, Ordem = 1, XPRecompensa = 20, Ativo = true,
                Titulo = "Listas e Arrays",
                Descricao = "Array (tamanho fixo), List<T> (dinâmico) e Dictionary<TKey, TValue>",
                ConteudoTeoricoMarkdown =
                    "## Coleções em C#\n\n" +
                    "> 💡 **Cenário real**: Um carrinho de compras é uma lista de itens que cresce e encolhe — " +
                    "o cliente adiciona e remove produtos livremente. Um cardápio de restaurante é um array " +
                    "fixo de pratos. Um dicionário de CEPs mapeia cada código a uma cidade — busca instantânea " +
                    "por qualquer CEP sem percorrer todos. Cada estrutura tem seu caso de uso ideal.\n\n" +
                    "C# oferece diversas estruturas de coleção. As mais usadas no dia a dia são `Array`, `List<T>` e `Dictionary<TKey, TValue>`.\n\n" +
                    "### Array — tamanho fixo\n\n" +
                    "```csharp\n// Declaração e inicialização\nint[]    numeros  = { 1, 2, 3, 4, 5 };\nstring[] nomes    = new string[3]; // array de 3 posições, todas null\n\n" +
                    "Console.WriteLine(numeros.Length); // 5 — propriedade Length para tamanho\nnumeros[0] = 99;   // acesso por índice (base 0)\nnumeros[5] = 1;    // IndexOutOfRangeException — índice inválido!\n\n" +
                    "// Arrays são de tamanho FIXO — não é possível adicionar ou remover elementos\n```\n\n" +
                    "### List<T> — tamanho dinâmico\n\n" +
                    "`List<T>` é o array dinâmico do C# — cresce e encolhe automaticamente:\n\n" +
                    "```csharp\nvar nomes = new List<string> { \"Ana\" };\n\n// Add() SEMPRE adiciona ao FINAL da lista\nnomes.Add(\"Bruno\");         // [\"Ana\", \"Bruno\"]\nnomes.Add(\"Carlos\");        // [\"Ana\", \"Bruno\", \"Carlos\"]\n\n// Insert(índice, item) — insere em posição específica\nnomes.Insert(0, \"Zara\");   // [\"Zara\", \"Ana\", \"Bruno\", \"Carlos\"]\n\n// Remove e RemoveAt\nnomes.Remove(\"Ana\");        // remove a primeira ocorrência de \"Ana\"\nnomes.RemoveAt(0);          // remove pelo índice\n\n// Tamanho e acesso\nConsole.WriteLine(nomes.Count);  // Count retorna o número de elementos\nConsole.WriteLine(nomes[0]);     // acesso por índice como array\n\n// Verificação\nbool tem = nomes.Contains(\"Bruno\"); // true\n```\n\n" +
                    "> **Array vs List<T>**: Array tem tamanho **fixo** e usa `.Length`. List<T> tem tamanho **dinâmico** e usa `.Count`.\n\n" +
                    "### Dictionary<TKey, TValue> — pares chave-valor\n\n" +
                    "Lookup em O(1) por chave — muito mais rápido do que buscar em lista:\n\n" +
                    "```csharp\nvar capitais = new Dictionary<string, string>\n{\n    [\"SP\"] = \"São Paulo\",\n    [\"RJ\"] = \"Rio de Janeiro\",\n    [\"MG\"] = \"Belo Horizonte\"\n};\n\ncapitais[\"BA\"] = \"Salvador\";   // adiciona ou atualiza\n\n// Leitura segura\nif (capitais.TryGetValue(\"PR\", out string? capital))\n    Console.WriteLine(capital);\nelse\n    Console.WriteLine(\"Estado não encontrado\");\n\nConsole.WriteLine(capitais.Count);           // 4\nbool existe = capitais.ContainsKey(\"SP\");    // true\n```\n\n" +
                    "### IEnumerable<T> — a abstração base\n\n" +
                    "Array, List, Dictionary e outras coleções implementam `IEnumerable<T>`, " +
                    "que é o tipo aceito pelo `foreach` e pelo LINQ:\n\n" +
                    "```csharp\n// Preferir IEnumerable<T> em parâmetros — mais flexível\npublic void Imprimir(IEnumerable<string> itens)\n{\n    foreach (var item in itens)\n        Console.WriteLine(item);\n}\n\nImprimir(nomes);     // List<string>\nImprimir(outroArray); // string[]\n```\n\n" +
                    "### Guia rápido: qual coleção usar?\n\n" +
                    "| Necessidade | Use |\n|---|---|\n" +
                    "| Tamanho fixo, acesso por índice | `T[]` (array) |\n" +
                    "| Lista que cresce e encolhe | `List<T>` |\n" +
                    "| Busca rápida por chave | `Dictionary<TKey, TValue>` |\n" +
                    "| Sem duplicatas, ordem não importa | `HashSet<T>` |\n" +
                    "| Fila FIFO (primeiro a entrar, primeiro a sair) | `Queue<T>` |\n" +
                    "| Pilha LIFO (último a entrar, primeiro a sair) | `Stack<T>` |\n" +
                    "| Leitura concorrente de múltiplas threads | `ConcurrentDictionary<K,V>` |"
            },

            new Licao
            {
                Id = 7, ModuloId = 3, Ordem = 2, XPRecompensa = 25, Ativo = true,
                Titulo = "LINQ Básico",
                Descricao = "Where, Select, OrderBy, First vs FirstOrDefault, Any/All e uso com EF Core",
                ConteudoTeoricoMarkdown =
                    "## LINQ — Language Integrated Query\n\n" +
                    "> 💡 **Cenário real**: Imagine um catálogo de produtos. Você quer mostrar apenas os " +
                    "produtos disponíveis, em ordem de preço, mostrando só nome e preço. " +
                    "Sem LINQ: 3 loops, 2 listas auxiliares, muito código repetitivo. " +
                    "Com LINQ: `produtos.Where(p => p.Disponivel).OrderBy(p => p.Preco).Select(p => new { p.Nome, p.Preco })` — " +
                    "uma linha legível que qualquer dev entende imediatamente.\n\n" +
                    "LINQ é um conjunto de **métodos de extensão** sobre `IEnumerable<T>` (e `IQueryable<T>`) " +
                    "que permite consultar coleções com sintaxe fluente e expressiva.\n\n" +
                    "### Filtragem com Where\n\n" +
                    "```csharp\nvar numeros = new List<int> { 5, 3, 8, 1, 9, 2, 7, 4 };\n\nvar pares   = numeros.Where(n => n % 2 == 0).ToList(); // [8, 2, 4]\nvar maiores = numeros.Where(n => n > 5).ToList();       // [8, 9, 7]\n\n// Where NÃO modifica a lista original — retorna nova sequência\n```\n\n" +
                    "### Transformação com Select\n\n" +
                    "```csharp\nvar nomes    = new[] { \"ana\", \"bruno\", \"carlos\" };\nvar maiusc   = nomes.Select(n => n.ToUpper()).ToList(); // [\"ANA\", \"BRUNO\", \"CARLOS\"]\nvar comprimentos = nomes.Select(n => n.Length).ToList(); // [3, 5, 6]\n```\n\n" +
                    "### Ordenação\n\n" +
                    "```csharp\nvar ordenados     = numeros.OrderBy(n => n).ToList();            // crescente\nvar decrescentes  = numeros.OrderByDescending(n => n).ToList();  // decrescente\nvar porNome       = pessoas.OrderBy(p => p.Sobrenome).ThenBy(p => p.Nome).ToList();\n```\n\n" +
                    "### First e FirstOrDefault\n\n" +
                    "Essa é uma diferença **crítica** no dia a dia:\n\n" +
                    "```csharp\nvar numeros = new List<int> { 1, 2, 3 };\n\n// First() lança InvalidOperationException se nenhum elemento satisfaz a condição\nvar x = numeros.First(n => n > 10); // LANÇA EXCEÇÃO — nenhum elemento > 10\n\n// FirstOrDefault() retorna o valor padrão do tipo se não encontrar\nvar y = numeros.FirstOrDefault(n => n > 10);       // retorna 0 (default de int)\nvar z = nomes.FirstOrDefault(n => n == \"Inexistente\"); // retorna null (default de string)\n\n// Boas práticas: use FirstOrDefault + verificação de null\nvar usuario = usuarios.FirstOrDefault(u => u.Id == id);\nif (usuario is null) throw new KeyNotFoundException();\n```\n\n" +
                    "### Any, All e Count\n\n" +
                    "```csharp\nbool temAdulto  = pessoas.Any(p => p.Idade >= 18);   // verdadeiro se pelo menos um\nbool todosMaior = pessoas.All(p => p.Idade >= 0);    // verdadeiro se todos\nint  qtdAtivos  = usuarios.Count(u => u.Ativo);      // conta os que satisfazem\n\nint  total = numeros.Sum();\nint  maior = numeros.Max();\ndouble media = numeros.Average();\n```\n\n" +
                    "### Encadeamento de operações\n\n" +
                    "```csharp\nvar resultado = produtos\n    .Where(p => p.Preco > 100)\n    .OrderBy(p => p.Nome)\n    .Select(p => new { p.Nome, p.Preco })\n    .ToList();\n```\n\n" +
                    "### LINQ funciona além de coleções em memória\n\n" +
                    "`IQueryable<T>` implementa LINQ com **tradução para SQL** — o banco executa o filtro, " +
                    "não o C#. Isso é o que torna o EF Core eficiente:\n\n" +
                    "```csharp\n// LINQ to Objects (IEnumerable) — filtra na memória\nvar ativos = lista.Where(u => u.Ativo).ToList();\n\n// LINQ to SQL via EF Core (IQueryable) — vira WHERE na query SQL!\nvar ativos = await ctx.Usuarios\n    .Where(u => u.Ativo)\n    .OrderBy(u => u.Nome)\n    .ToListAsync(); // SELECT * FROM Usuarios WHERE Ativo = 1 ORDER BY Nome\n```\n\n" +
                    "> **Regra de ouro**: chame `.ToList()` / `.ToListAsync()` **apenas no final** — antes disso, a query ainda não foi executada (lazy evaluation)."
            },

            new Licao
            {
                Id = 11, ModuloId = 3, Ordem = 3, XPRecompensa = 25, Ativo = true,
                Titulo = "LINQ Avançado",
                Descricao = "GroupBy, Join, Aggregate, Skip/Take e projeções complexas",
                ConteudoTeoricoMarkdown =
                    "## LINQ Avançado\n\n" +
                    "Com os operadores básicos dominados, o LINQ se torna ainda mais poderoso " +
                    "para cenários de agregação, junção e paginação.\n\n" +
                    "### GroupBy — agrupamento\n\n" +
                    "```csharp\nvar pedidos = new List<Pedido> { ... };\n\n// Agrupa pedidos por ClienteId e calcula totais\nvar resumo = pedidos\n    .GroupBy(p => p.ClienteId)\n    .Select(g => new\n    {\n        ClienteId  = g.Key,\n        Total      = g.Sum(p => p.Valor),\n        Quantidade = g.Count()\n    })\n    .OrderByDescending(r => r.Total)\n    .ToList();\n\n// g.Key é o valor da chave (ClienteId)\n// g é um IGrouping<TKey, TElement> — iterável\n```\n\n" +
                    "### Join — combinando duas coleções\n\n" +
                    "```csharp\n// Inner join: retorna apenas registros com correspondência em ambos os lados\nvar resultado = clientes.Join(\n    pedidos,\n    c => c.Id,         // chave de clientes\n    p => p.ClienteId,  // chave de pedidos\n    (c, p) => new { NomeCliente = c.Nome, Pedido = p.Numero, Valor = p.Valor }\n);\n\n// Com EF Core, prefira Include() para navigation properties\nvar clientesComPedidos = await ctx.Clientes.Include(c => c.Pedidos).ToListAsync();\n```\n\n" +
                    "### Aggregate — redução personalizada\n\n" +
                    "```csharp\nvar numeros = new[] { 1, 2, 3, 4, 5 };\n\n// Acumula um resultado percorrendo a sequência\nint produto = numeros.Aggregate(1, (acc, n) => acc * n); // 120 (1×2×3×4×5)\n\nstring frase = new[] { \"C#\", \"é\", \"incrível\" }\n    .Aggregate((a, b) => $\"{a} {b}\"); // \"C# é incrível\"\n\n// Equivalente manual:\nint resultado = 1;\nforeach (var n in numeros) resultado *= n;\n```\n\n" +
                    "### Paginação com Skip e Take\n\n" +
                    "```csharp\nint pagina  = 2;\nint tamanho = 10;\n\nvar paginado = produtos\n    .OrderBy(p => p.Nome)\n    .Skip((pagina - 1) * tamanho)  // pula os da página anterior\n    .Take(tamanho)                 // pega apenas os desta página\n    .ToList();\n\n// Página 1: Skip(0).Take(10)  → itens 0–9\n// Página 2: Skip(10).Take(10) → itens 10–19\n```\n\n" +
                    "> `Take(n)` retorna no máximo `n` elementos. Se a coleção tiver menos, retorna o que tiver (não lança exceção).\n\n" +
                    "### Any, All e Contains\n\n" +
                    "```csharp\nbool temPromocao = produtos.Any(p => p.Desconto > 0);\nbool todosAtivos = produtos.All(p => p.Ativo);\nbool temId5      = ids.Contains(5);\n```\n\n" +
                    "### Distinct, Union, Intersect, Except\n\n" +
                    "```csharp\nvar a = new[] { 1, 2, 3, 4 };\nvar b = new[] { 3, 4, 5, 6 };\n\na.Distinct()    // remove duplicatas da mesma sequência\na.Union(b)      // {1,2,3,4,5,6} — todos sem duplicatas\na.Intersect(b)  // {3,4}         — apenas os comuns\na.Except(b)     // {1,2}         — apenas os de 'a' que não estão em 'b'\n```"
            },

            // ── Módulo 4: Async/Await ─────────────────────────────────────────────────
            new Licao
            {
                Id = 8, ModuloId = 4, Ordem = 1, XPRecompensa = 30, Ativo = true,
                Titulo = "async/await",
                Descricao = "Programação assíncrona, Task<T>, thread liberada e async Task vs async void",
                ConteudoTeoricoMarkdown =
                    "## Programação Assíncrona com async/await\n\n" +
                    "> 💡 **Analogia**: Imagine um garçom em um restaurante. Ele anota o pedido da mesa 1 " +
                    "e leva para a cozinha. Se ele ficasse parado esperando o prato ficar pronto (*síncrono*), " +
                    "nenhuma outra mesa seria atendida. Em vez disso, ele vai atender a mesa 2, 3, 4... " +
                    "e quando a cozinha avisa que o prato ficou pronto, ele busca e entrega (*assíncrono*). " +
                    "Seu servidor web funciona exatamente assim com `async/await`.\n\n" +
                    "Em aplicações modernas (APIs, apps, games), bloquear uma thread esperando I/O é um desperdício. " +
                    "O modelo `async/await` permite que uma thread seja **liberada** enquanto espera por operações lentas.\n\n" +
                    "### O problema do código síncrono\n\n" +
                    "```csharp\n// BLOQUEANTE — a thread fica presa esperando o banco responder\npublic IActionResult Get(int id)\n{\n    var usuario = db.Usuarios.Find(id); // thread bloqueada por 50ms\n    return Ok(usuario);\n}\n// Em um servidor com 1000 req/s, isso pode esgotar o ThreadPool!\n```\n\n" +
                    "### async/await — a thread é liberada\n\n" +
                    "```csharp\n// ASSÍNCRONO — a thread é devolvida ao pool enquanto o banco responde\npublic async Task<IActionResult> Get(int id)\n{\n    var usuario = await db.Usuarios.FindAsync(id); // thread liberada!\n    return Ok(usuario);\n    // a thread retorna aqui, podendo atender outras requisições\n}\n```\n\n" +
                    "> Quando o código encontra um `await`, o método é **suspenso** e a thread retorna ao pool. " +
                    "Quando a operação completa, uma thread (possivelmente diferente) retoma o método de onde parou.\n\n" +
                    "### Task e Task<T>\n\n" +
                    "```csharp\n// Task representa uma operação assíncrona sem valor de retorno\npublic async Task SalvarAsync(string dados)\n{\n    await File.WriteAllTextAsync(\"arquivo.txt\", dados);\n}\n\n// Task<T> representa uma operação que retorna um valor\npublic async Task<string> BuscarAsync(int id)\n{\n    await Task.Delay(100); // simula chamada ao banco\n    return $\"Resultado do id {id}\";\n}\n\nvar resultado = await BuscarAsync(42);\n```\n\n" +
                    "### async Task vs async void\n\n" +
                    "**Nunca use `async void`** exceto em event handlers:\n\n" +
                    "```csharp\n// async void — NÃO pode ser aguardado, exceções não são propagadas\npublic async void Ruim() { await Task.Delay(100); }\n\n// async Task — pode ser aguardado, exceções propagam corretamente\npublic async Task Bom() { await Task.Delay(100); }\n\n// Problema com async void:\nasync void MetodoRuim()\n{\n    throw new Exception(\"Erro silencioso!\"); // vai crashar o app sem poder ser capturado\n}\n```\n\n" +
                    "### Tratamento de exceções\n\n" +
                    "```csharp\ntry\n{\n    var resultado = await BuscarAsync(99);\n}\ncatch (HttpRequestException ex)\n{\n    Console.WriteLine($\"Erro de rede: {ex.Message}\");\n}\ncatch (OperationCanceledException)\n{\n    Console.WriteLine(\"Operação cancelada pelo usuário\");\n}\nfinally\n{\n    // finally funciona normalmente em async\n    Console.WriteLine(\"Finalizado\");\n}\n```\n\n" +
                    "### Task.Run — trabalho CPU-bound\n\n" +
                    "```csharp\n// Use Task.Run apenas para CPU-bound (processamento pesado), NÃO para I/O\nvar resultado = await Task.Run(() => CalcularPrimosAte(1_000_000));\n\n// Para I/O (banco, HTTP, arquivo), use os métodos Async nativos — NÃO Task.Run\n// ERRADO: await Task.Run(() => db.Usuarios.Find(id));\n// CERTO:  await db.Usuarios.FindAsync(id);\n```"
            },

            new Licao
            {
                Id = 12, ModuloId = 4, Ordem = 2, XPRecompensa = 30, Ativo = true,
                Titulo = "CancellationToken",
                Descricao = "Cancelamento cooperativo com CancellationTokenSource e ThrowIfCancellationRequested",
                ConteudoTeoricoMarkdown =
                    "## CancellationToken — Cancelamento Cooperativo\n\n" +
                    "C# usa um modelo de cancelamento **cooperativo**: você pede para parar (signal), " +
                    "e o código verifica e honra esse pedido. Não existe matar uma thread à força de forma segura.\n\n" +
                    "### CancellationTokenSource e CancellationToken\n\n" +
                    "```csharp\n// CancellationTokenSource cria e controla o token\nusing var cts = new CancellationTokenSource();\n\n// O token é passado para as operações que devem respeitá-lo\nCancellationToken token = cts.Token;\n\n// Cancelar após 5 segundos (timeout)\ncts.CancelAfter(TimeSpan.FromSeconds(5));\n\n// Cancelar manualmente\ncts.Cancel();\n```\n\n" +
                    "### Usando o token em operações assíncronas\n\n" +
                    "```csharp\npublic async Task<string> BuscarDadosAsync(int id, CancellationToken ct = default)\n{\n    // Verificação manual antes de operação longa\n    ct.ThrowIfCancellationRequested(); // lança OperationCanceledException se cancelado\n\n    await Task.Delay(2000, ct); // Task.Delay respeita o token automaticamente\n\n    ct.ThrowIfCancellationRequested(); // verificar novamente após cada etapa longa\n\n    return $\"Dados do id {id}\";\n}\n```\n\n" +
                    "### Capturando o cancelamento\n\n" +
                    "```csharp\nusing var cts = new CancellationTokenSource();\ncts.CancelAfter(3000); // timeout de 3s\n\ntry\n{\n    var dados = await BuscarDadosAsync(1, cts.Token);\n    Console.WriteLine(dados);\n}\ncatch (OperationCanceledException)\n{\n    Console.WriteLine(\"Operação cancelada — timeout atingido ou usuário cancelou\");\n}\n```\n\n" +
                    "### Em ASP.NET Core — cancelamento automático\n\n" +
                    "O framework injeta o token do request automaticamente. " +
                    "Se o cliente desconectar, o token é cancelado:\n\n" +
                    "```csharp\n[HttpGet(\"{id}\")]\npublic async Task<IActionResult> Get(int id, CancellationToken ct)\n{\n    // ct é cancelado se o cliente desconectar antes de receber a resposta\n    var dados = await _repo.ObterAsync(id, ct);\n    return Ok(dados);\n}\n```\n\n" +
                    "### Verificação manual com IsCancellationRequested\n\n" +
                    "```csharp\npublic async Task ProcessarLoteAsync(IEnumerable<int> ids, CancellationToken ct)\n{\n    foreach (var id in ids)\n    {\n        // Verificação não-lançante dentro de loops\n        if (ct.IsCancellationRequested)\n        {\n            Console.WriteLine(\"Processamento interrompido pelo usuário\");\n            break;\n        }\n\n        await ProcessarItemAsync(id, ct);\n    }\n}\n```\n\n" +
                    "> `ct.ThrowIfCancellationRequested()` é equivalente a:\n" +
                    "> `if (ct.IsCancellationRequested) throw new OperationCanceledException(ct);`"
            },

            new Licao
            {
                Id = 13, ModuloId = 4, Ordem = 3, XPRecompensa = 30, Ativo = true,
                Titulo = "Task.WhenAll e Task.WhenAny",
                Descricao = "Execução paralela de tasks, WhenAll vs WaitAll e race com WhenAny",
                ConteudoTeoricoMarkdown =
                    "## Paralelismo com Task.WhenAll e Task.WhenAny\n\n" +
                    "Quando há múltiplas operações **independentes entre si**, rodá-las em paralelo " +
                    "pode reduzir drasticamente o tempo total.\n\n" +
                    "### O problema do await sequencial\n\n" +
                    "```csharp\n// SEQUENCIAL — tempo total = soma de cada operação\nvar u1 = await BuscarUsuarioAsync(1); // aguarda 200ms\nvar u2 = await BuscarUsuarioAsync(2); // aguarda 200ms\nvar u3 = await BuscarUsuarioAsync(3); // aguarda 200ms\n// Tempo total: ~600ms\n```\n\n" +
                    "### Task.WhenAll — aguarda TODAS as tasks\n\n" +
                    "```csharp\n// PARALELO — as três operações rodam ao mesmo tempo\nvar task1 = BuscarUsuarioAsync(1);\nvar task2 = BuscarUsuarioAsync(2);\nvar task3 = BuscarUsuarioAsync(3);\n// ATENÇÃO: as tasks já começaram ao serem criadas!\n\nvar resultados = await Task.WhenAll(task1, task2, task3);\n// Tempo total: ~200ms (a mais lenta das três)\n// resultados[0] = usuário 1, resultados[1] = usuário 2, etc.\n\n// Sintaxe com IEnumerable\nvar ids = new[] { 1, 2, 3, 4, 5 };\nvar usuarios = await Task.WhenAll(ids.Select(id => BuscarUsuarioAsync(id)));\n```\n\n" +
                    "### Task.WhenAll vs Task.WaitAll\n\n" +
                    "```csharp\n// WhenAll — ASSÍNCRONO (retorna Task) — use sempre este!\nawait Task.WhenAll(task1, task2); // não bloqueia a thread\n\n// WaitAll — BLOQUEANTE (retorna void) — EVITE em código async!\nTask.WaitAll(task1, task2); // bloqueia a thread — pode causar deadlock\n```\n\n" +
                    "### Tratamento de erros com WhenAll\n\n" +
                    "```csharp\ntry\n{\n    await Task.WhenAll(task1, task2, task3);\n}\ncatch (Exception ex)\n{\n    // Apenas a primeira exceção é re-lançada no catch\n    // Para ver TODAS as exceções:\n    var excecoes = new[] { task1, task2, task3 }\n        .Where(t => t.IsFaulted)\n        .Select(t => t.Exception!.InnerException!)\n        .ToList();\n}\n```\n\n" +
                    "### Task.WhenAny — aguarda a PRIMEIRA task\n\n" +
                    "Útil para **race conditions** e timeouts:\n\n" +
                    "```csharp\n// Race: cache vs banco — quem responder primeiro vence\nvar taskBanco = BuscarNoBancoAsync(id);\nvar taskCache = BuscarNoCacheAsync(id);\n\nvar vencedora = await Task.WhenAny(taskBanco, taskCache);\nvar resultado = await vencedora; // garante que exceções sejam propagadas\n\n// As outras tasks CONTINUAM rodando em background — WhenAny não as cancela\n```\n\n" +
                    "### Timeout com WhenAny\n\n" +
                    "```csharp\nvar taskPrincipal = BuscarDadosAsync();\nvar taskTimeout   = Task.Delay(TimeSpan.FromSeconds(5));\n\nvar concluida = await Task.WhenAny(taskPrincipal, taskTimeout);\n\nif (concluida == taskTimeout)\n    throw new TimeoutException(\"Operação excedeu 5 segundos\");\n\nvar resultado = await taskPrincipal;\n```"
            },

            // ── Módulo 5: Design Patterns ─────────────────────────────────────────────
            new Licao
            {
                Id = 9, ModuloId = 5, Ordem = 1, XPRecompensa = 35, Ativo = true,
                Titulo = "Repository Pattern",
                Descricao = "Abstraindo acesso a dados com interfaces, DI e testabilidade",
                ConteudoTeoricoMarkdown =
                    "## Repository Pattern\n\n" +
                    "> 💡 **Analogia**: Pense em uma biblioteca. Você pede ao bibliotecário um livro pelo título " +
                    "— você não sabe se ele está na prateleira A, no depósito ou digitalizado. " +
                    "O bibliotecário (*repository*) sabe onde buscar. " +
                    "Você (*controller/service*) só se importa em receber o livro — " +
                    "os detalhes de onde está armazenado não são problema seu.\n\n" +
                    "O Repository Pattern isola a **lógica de acesso a dados** da lógica de negócio. " +
                    "Controllers e Services não sabem se os dados vêm de SQLite, SQL Server, uma API ou memória.\n\n" +
                    "### O problema sem Repository\n\n" +
                    "```csharp\n// SEM REPOSITORY — controller acoplado ao banco\npublic class PedidoController : ControllerBase\n{\n    private readonly AppDbContext _ctx;\n\n    [HttpGet(\"{id}\")]\n    public async Task<IActionResult> Get(int id)\n    {\n        var pedido = await _ctx.Pedidos.FindAsync(id); // acoplamento direto ao EF\n        return Ok(pedido);\n    }\n    // Problema: para testar, precisa de um banco real\n}\n```\n\n" +
                    "### A solução: Interface + Implementação\n\n" +
                    "```csharp\n// 1. Interface — define o CONTRATO (o que o repositório faz)\npublic interface IProdutoRepository\n{\n    Task<Produto?>           ObterPorIdAsync(int id);\n    Task<IEnumerable<Produto>> ObterTodosAsync();\n    Task<IEnumerable<Produto>> ObterPorCategoriaAsync(string categoria);\n    Task                      AdicionarAsync(Produto produto);\n    void                      Atualizar(Produto produto);\n    void                      Remover(Produto produto);\n    Task<bool>                SalvarAsync();\n}\n\n// 2. Implementação — DETALHE de infraestrutura (EF Core)\npublic class ProdutoRepository(AppDbContext ctx) : IProdutoRepository\n{\n    public async Task<Produto?> ObterPorIdAsync(int id)\n        => await ctx.Produtos.FindAsync(id);\n\n    public async Task<IEnumerable<Produto>> ObterTodosAsync()\n        => await ctx.Produtos.AsNoTracking().ToListAsync(); // AsNoTracking = mais rápido para leitura\n\n    public async Task AdicionarAsync(Produto produto)\n        => await ctx.Produtos.AddAsync(produto);\n\n    public void Atualizar(Produto produto)\n        => ctx.Produtos.Update(produto);\n\n    public async Task<bool> SalvarAsync()\n        => await ctx.SaveChangesAsync() > 0;\n}\n\n// 3. Controller — depende da INTERFACE, nunca da implementação\npublic class ProdutoController(IProdutoRepository repo) : ControllerBase\n{\n    [HttpGet(\"{id}\")]\n    public async Task<IActionResult> Get(int id)\n    {\n        var produto = await repo.ObterPorIdAsync(id);\n        return produto is null ? NotFound() : Ok(produto);\n    }\n}\n```\n\n" +
                    "### Registro no DI (Dependency Injection)\n\n" +
                    "```csharp\n// Program.cs\nbuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();\n// AddScoped = uma instância por requisição HTTP (ideal para DbContext)\n```\n\n" +
                    "### Testabilidade — o grande benefício\n\n" +
                    "```csharp\n// Em testes, substitua com um fake sem banco real\npublic class FakeProdutoRepository : IProdutoRepository\n{\n    private readonly List<Produto> _dados = new();\n\n    public Task<Produto?> ObterPorIdAsync(int id)\n        => Task.FromResult(_dados.FirstOrDefault(p => p.Id == id));\n\n    public Task AdicionarAsync(Produto p) { _dados.Add(p); return Task.CompletedTask; }\n    public Task<bool> SalvarAsync() => Task.FromResult(true);\n    // ...\n}\n\n// O Controller funciona sem nenhuma mudança — recebe a interface, não sabe qual implementação é\n```\n\n" +
                    "> **Princípio**: Controllers dependem de **abstrações** (interfaces), não de **detalhes** (EF Core, SQL). " +
                    "Isso é o **D** do SOLID — Dependency Inversion Principle."
            },

            new Licao
            {
                Id = 14, ModuloId = 5, Ordem = 2, XPRecompensa = 35, Ativo = true,
                Titulo = "Factory Pattern",
                Descricao = "Encapsulando criação de objetos com Simple Factory e Factory Method",
                ConteudoTeoricoMarkdown =
                    "## Factory Pattern\n\n" +
                    "O Factory Pattern encapsula a **lógica de criação** de objetos. " +
                    "O código cliente recebe um produto sem precisar saber qual classe concreta foi instanciada.\n\n" +
                    "### O problema sem Factory\n\n" +
                    "```csharp\n// SEM FACTORY — cliente acoplado a todas as classes concretas\npublic class PedidoService\n{\n    public void Notificar(string tipo, string mensagem)\n    {\n        if (tipo == \"email\")\n        {\n            var email = new NotificacaoEmail(); // acoplado\n            email.Enviar(mensagem);\n        }\n        else if (tipo == \"sms\")\n        {\n            var sms = new NotificacaoSms(); // acoplado\n            sms.Enviar(mensagem);\n        }\n        // Para adicionar \"push\", precisa editar este método — viola Open/Closed Principle\n    }\n}\n```\n\n" +
                    "### Simple Factory\n\n" +
                    "```csharp\n// Abstração do produto\npublic abstract class Notificacao\n{\n    public abstract void Enviar(string mensagem);\n}\n\n// Classes concretas\npublic class NotificacaoEmail : Notificacao\n{\n    public override void Enviar(string msg)\n        => Console.WriteLine($\"[EMAIL] {msg}\");\n}\n\npublic class NotificacaoSms : Notificacao\n{\n    public override void Enviar(string msg)\n        => Console.WriteLine($\"[SMS] {msg}\");\n}\n\npublic class NotificacaoPush : Notificacao\n{\n    public override void Enviar(string msg)\n        => Console.WriteLine($\"[PUSH] {msg}\");\n}\n\n// A Factory — único ponto de criação\npublic static class NotificacaoFactory\n{\n    public static Notificacao Criar(string tipo) => tipo switch\n    {\n        \"email\" => new NotificacaoEmail(),\n        \"sms\"   => new NotificacaoSms(),\n        \"push\"  => new NotificacaoPush(),\n        _ => throw new ArgumentException($\"Tipo desconhecido: {tipo}\")\n    };\n}\n\n// Uso — o cliente depende apenas da abstração\npublic class PedidoService\n{\n    public void Notificar(string tipo, string mensagem)\n    {\n        var notif = NotificacaoFactory.Criar(tipo); // não sabe qual classe concreta\n        notif.Enviar(mensagem);\n    }\n}\n```\n\n" +
                    "> O código cliente **não conhece** `NotificacaoEmail`, `NotificacaoSms` etc. " +
                    "Para adicionar um novo tipo, só modifica a Factory — o cliente não muda.\n\n" +
                    "### Factory integrada ao DI do ASP.NET Core\n\n" +
                    "```csharp\n// Com interfaces, o DI pode agir como factory\nbuilder.Services.AddScoped<IRelatorio, RelatorioExcel>(); // injeta sempre Excel\n// builder.Services.AddScoped<IRelatorio, RelatorioPdf>(); // troca sem tocar nos consumers\n\n// Keyed services (ASP.NET Core 8+)\nbuilder.Services.AddKeyedScoped<IRelatorio, RelatorioExcel>(\"excel\");\nbuilder.Services.AddKeyedScoped<IRelatorio, RelatorioPdf>(\"pdf\");\n\npublic class RelatorioService([FromKeyedServices(\"pdf\")] IRelatorio relatorio) { }\n```\n\n" +
                    "### Por que usar Factory?\n\n" +
                    "- O cliente depende da **abstração**, não do concreto\n" +
                    "- Centraliza **configuração, injeção e validação** na criação\n" +
                    "- Adicionar novos tipos não muda o código cliente (Open/Closed Principle)"
            },

            new Licao
            {
                Id = 15, ModuloId = 5, Ordem = 3, XPRecompensa = 35, Ativo = true,
                Titulo = "Singleton Pattern",
                Descricao = "Garantindo uma única instância com Lazy<T>, thread safety e DI",
                ConteudoTeoricoMarkdown =
                    "## Singleton Pattern\n\n" +
                    "Singleton garante que uma classe tenha **exatamente uma instância** " +
                    "durante toda a vida da aplicação, com um ponto global de acesso.\n\n" +
                    "### Implementação com Lazy<T> — thread-safe e lazy\n\n" +
                    "`Lazy<T>` é a forma recomendada em C# moderno: inicializa **apenas na primeira vez** " +
                    "que `.Value` é acessado, de forma **thread-safe**:\n\n" +
                    "```csharp\npublic sealed class ConfiguracaoApp\n{\n    // sealed impede herança que poderia quebrar a unicidade\n    private static readonly Lazy<ConfiguracaoApp> _instancia =\n        new(() => new ConfiguracaoApp());\n\n    public static ConfiguracaoApp Instancia => _instancia.Value;\n\n    // Configurações\n    public string Ambiente     { get; }\n    public string ApiBaseUrl   { get; }\n\n    private ConfiguracaoApp() // private: ninguém cria externamente\n    {\n        Ambiente   = Environment.GetEnvironmentVariable(\"ASPNETCORE_ENVIRONMENT\") ?? \"Production\";\n        ApiBaseUrl = Environment.GetEnvironmentVariable(\"API_URL\") ?? \"https://api.exemplo.com\";\n    }\n}\n\n// Uso\nvar config = ConfiguracaoApp.Instancia;\nConsole.WriteLine(config.Ambiente);\n```\n\n" +
                    "### Por que `sealed`?\n\n" +
                    "`sealed` impede que alguém herde da classe e crie subclasses, " +
                    "o que quebraria a garantia de unicidade:\n\n" +
                    "```csharp\n// SEM sealed — alguém pode criar subclasses\npublic class ConfigHerdada : ConfiguracaoApp { } // criaria uma segunda instância!\n\n// COM sealed — herança proibida\npublic sealed class ConfiguracaoApp { ... }\n// public class Sub : ConfiguracaoApp { } // ERRO de compilação\n```\n\n" +
                    "### AddSingleton no ASP.NET Core — a forma preferida\n\n" +
                    "Em aplicações ASP.NET Core, prefira o container de DI ao Singleton estático:\n\n" +
                    "```csharp\n// Program.cs\nbuilder.Services.AddSingleton<ICacheService, MemoryCacheService>();\n// AddSingleton  = UMA instância por aplicação\n// AddScoped     = UMA instância por requisição HTTP\n// AddTransient  = UMA NOVA instância a cada injeção\n\n// Vantagens sobre Singleton estático:\n// 1. Pode ser mockado em testes (depende da interface)\n// 2. O DI gerencia o ciclo de vida\n// 3. Sem estado global estático — mais previsível\n```\n\n" +
                    "### Quando usar e quando evitar\n\n" +
                    "```csharp\n// BOM: configurações globais imutáveis, loggers, conexão com recurso único\npublic sealed class DatabaseConfig\n{\n    private static readonly Lazy<DatabaseConfig> _inst = new(() => new());\n    public static DatabaseConfig Instancia => _inst.Value;\n    public string ConnectionString { get; } = \"Data Source=app.db\";\n    private DatabaseConfig() { }\n}\n\n// RUIM: estado mutável compartilhado sem lock — causa race conditions\npublic sealed class ContadorGlobal\n{\n    public static ContadorGlobal Instancia => ...\n    public int Valor { get; set; } // PROBLEMA: múltiplas threads escrevendo ao mesmo tempo!\n    // Solução: use Interlocked.Increment(ref _valor) para operações atômicas\n}\n```\n\n" +
                    "> **Regra**: Singleton é para **estado imutável ou cuidadosamente sincronizado**. " +
                    "Estado mutável compartilhado sem sincronização causa bugs difíceis de reproduzir."
            }
        );

        // Seed Exercícios — 3 por lição, alinhados com o conteúdo da lição
        modelBuilder.Entity<Exercicio>().HasData(

            // ── Lição 1: Variáveis e Tipos ─────────────────────────────────────────
            new Exercicio { Id = 1, LicaoId = 1, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual palavra-chave instrui o compilador C# a inferir o tipo de uma variável pelo valor atribuído?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"var\",\"dynamic\",\"object\",\"type\"]",
                RespostaCorreta = "var",
                Explicacao = "'var' é inferência de tipo em TEMPO DE COMPILAÇÃO — o tipo é fixado pelo compilador. 'dynamic' adia a checagem para o runtime. 'var' e o tipo explícito geram IL idêntico." },

            new Exercicio { Id = 2, LicaoId = 1, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Em C#, 'string' (minúsculo) e 'String' (maiúsculo) são o mesmo tipo.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'string' é um alias da linguagem para 'System.String'. São 100% intercambiáveis — geram o mesmo IL. Por convenção, use 'string' no código e 'String' apenas ao referenciar membros estáticos como String.IsNullOrEmpty()." },

            new Exercicio { Id = 3, LicaoId = 1, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para declarar um valor que não pode ser alterado e é resolvido em tempo de compilação, usa-se: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "const",
                Explicacao = "'const' é gravado diretamente no IL em tempo de compilação e nunca muda. Diferente de 'readonly', que é definida em tempo de execução (no construtor) e pode variar por instância." },

            // ── Lição 2: Controle de Fluxo ─────────────────────────────────────────
            new Exercicio { Id = 4, LicaoId = 2, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual estrutura é mais adequada para comparar uma variável contra múltiplos valores constantes em C#?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"switch\",\"if/else encadeado\",\"while\",\"do/while\"]",
                RespostaCorreta = "switch",
                Explicacao = "'switch' (e a switch expression moderna) é otimizado pelo compilador para comparar um valor contra constantes. O código fica mais legível e performático do que uma cadeia de if/else para esse propósito." },

            new Exercicio { Id = 5, LicaoId = 2, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Modificar a coleção (adicionar ou remover elementos) dentro de um foreach é permitido em C#.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "Modificar a coleção durante iteração com foreach lança InvalidOperationException. O enumerador detecta que a coleção foi alterada. Para remover durante iteração, use um for reverso ou filtre para uma nova lista com LINQ." },

            new Exercicio { Id = 6, LicaoId = 2, Ordem = 3, XPRecompensa = 5,
                Enunciado = "O operador ternário retorna um valor com base em uma condição. Sua sintaxe é: condição ____ valorVerdadeiro : valorFalso",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "?",
                Explicacao = "O operador ternário usa '?' para separar a condição do valor verdadeiro e ':' para separar os dois valores. Ex: string r = nota >= 7 ? \"Aprovado\" : \"Reprovado\". É equivalente a um if/else que retorna valor." },

            // ── Lição 3: Métodos e Funções ──────────────────────────────────────────
            new Exercicio { Id = 7, LicaoId = 3, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual modificador de acesso restringe um método ao escopo da própria classe, sendo invisível para qualquer código externo?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"private\",\"public\",\"protected\",\"internal\"]",
                RespostaCorreta = "private",
                Explicacao = "'private' é o mais restritivo: o membro só é acessível dentro da própria classe. É o padrão para membros de classe que não fazem parte da API pública. A regra de ouro: use o modificador mais restritivo possível." },

            new Exercicio { Id = 8, LicaoId = 3, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Dois métodos com o mesmo nome mas parâmetros diferentes na mesma classe configuram uma sobrecarga (overloading).",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Sobrecarga (overloading) permite múltiplos métodos com o mesmo nome, diferenciados por tipo, quantidade ou ordem de parâmetros. O compilador escolhe a versão correta em tempo de compilação com base nos argumentos." },

            new Exercicio { Id = 9, LicaoId = 3, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Um método que não retorna nenhum valor usa o tipo de retorno: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "void",
                Explicacao = "'void' declara ausência de retorno. Em métodos assíncronos sem retorno, use 'Task' em vez de 'async void' — Task pode ser aguardado e propaga exceções corretamente, enquanto 'async void' não." },

            // ── Lição 4: Classes e Objetos ──────────────────────────────────────────
            new Exercicio { Id = 10, LicaoId = 4, Ordem = 1, XPRecompensa = 5,
                Enunciado = "O que é um construtor em C#?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"Método especial que inicializa uma instância da classe\",\"Uma propriedade obrigatória\",\"Um tipo de herança\",\"Um método estático de fábrica\"]",
                RespostaCorreta = "Método especial que inicializa uma instância da classe",
                Explicacao = "O construtor tem o mesmo nome da classe, não tem tipo de retorno e é chamado automaticamente pelo operador 'new'. Seu objetivo é colocar o objeto em um estado válido desde o início." },

            new Exercicio { Id = 11, LicaoId = 4, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Uma classe pode ter múltiplos construtores, desde que tenham assinaturas diferentes (quantidade ou tipos de parâmetros).",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Isso é sobrecarga de construtores. É comum ter um construtor padrão (sem parâmetros) e outros com diferentes combinações. O operador 'new' escolhe qual construtor chamar pelos argumentos fornecidos." },

            new Exercicio { Id = 12, LicaoId = 4, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para criar uma instância de uma classe chamada Produto no C#, usa-se: var p = ____ Produto();",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "new",
                Explicacao = "'new' aloca memória no heap, executa o construtor e retorna uma referência ao novo objeto. Sem 'new', a variável ficaria null (referência nula) ou causaria erro de compilação." },

            // ── Lição 5: Herança e Polimorfismo ─────────────────────────────────────
            new Exercicio { Id = 13, LicaoId = 5, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Em C#, qual símbolo é usado para indicar que uma classe herda de outra?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\":\",\"extends\",\"inherits\",\"->\"]",
                RespostaCorreta = ":",
                Explicacao = "C# usa ':' tanto para herança de classe quanto para implementação de interfaces. Ex: 'public class Cachorro : Animal, IVoador'. Java usa 'extends' para classes e 'implements' para interfaces — C# unifica com ':'." },

            new Exercicio { Id = 14, LicaoId = 5, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Em C#, uma classe pode herdar diretamente de múltiplas classes ao mesmo tempo.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "C# não suporta herança múltipla de classes — uma classe só pode herdar de uma única classe. Para modelar comportamentos múltiplos, usa-se interfaces: uma classe pode implementar quantas interfaces precisar." },

            new Exercicio { Id = 15, LicaoId = 5, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para substituir a implementação de um método virtual na subclasse, usa-se a palavra-chave: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "override",
                Explicacao = "'override' na subclasse substitui a implementação do método marcado com 'virtual' ou 'abstract' na classe base. Sem 'override', você estaria criando um novo método que oculta (hides) o da base, não sobrescrevendo." },

            // ── Lição 6: Listas e Arrays ─────────────────────────────────────────────
            new Exercicio { Id = 16, LicaoId = 6, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual é a principal diferença entre Array e List<T> em C#?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"List<T> tem tamanho dinâmico; Array tem tamanho fixo\",\"Array é sempre mais rápido\",\"List<T> não aceita tipos primitivos\",\"São equivalentes em todos os aspectos\"]",
                RespostaCorreta = "List<T> tem tamanho dinâmico; Array tem tamanho fixo",
                Explicacao = "Array define o tamanho na criação e não pode mudar. List<T> cresce com Add() e encolhe com Remove() automaticamente. Arrays usam .Length; List<T> usa .Count." },

            new Exercicio { Id = 17, LicaoId = 6, Ordem = 2, XPRecompensa = 5,
                Enunciado = "O método List<T>.Add() adiciona o elemento no FINAL da lista.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Add() sempre insere no final da lista — é O(1) amortizado. Para inserir em uma posição específica, use Insert(índice, item). Para inserir no início, use Insert(0, item), que é O(n) pois desloca todos os elementos." },

            new Exercicio { Id = 18, LicaoId = 6, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para obter o número de elementos em uma List<T>, usa-se a propriedade: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "Count",
                Explicacao = "List<T>.Count retorna o número atual de elementos. Arrays usam .Length. O método de extensão LINQ .Count() também funciona em qualquer IEnumerable<T>, mas .Count a propriedade é O(1) enquanto .Count() método pode ser O(n)." },

            // ── Lição 7: LINQ Básico ─────────────────────────────────────────────────
            new Exercicio { Id = 19, LicaoId = 7, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual é a diferença entre First() e FirstOrDefault() no LINQ quando nenhum elemento satisfaz a condição?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"First() lança InvalidOperationException; FirstOrDefault() retorna null/default\",\"First() retorna null; FirstOrDefault() lança exceção\",\"São idênticos em comportamento\",\"First() retorna o primeiro sem filtro; FirstOrDefault() filtra\"]",
                RespostaCorreta = "First() lança InvalidOperationException; FirstOrDefault() retorna null/default",
                Explicacao = "First() garante que existe pelo menos um elemento — lança exceção se não houver. FirstOrDefault() é a versão segura: retorna null para tipos referência ou o valor padrão (0, false) para tipos de valor. Use FirstOrDefault() quando a ausência é um caso válido." },

            new Exercicio { Id = 20, LicaoId = 7, Ordem = 2, XPRecompensa = 5,
                Enunciado = "LINQ funciona apenas com coleções em memória (List, Array etc.).",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "LINQ funciona com qualquer IEnumerable<T> (em memória) ou IQueryable<T> (traducível para outras fontes). Via EF Core, o LINQ é traduzido para SQL e executado diretamente no banco de dados — não carrega todos os dados em memória para filtrar." },

            new Exercicio { Id = 21, LicaoId = 7, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para filtrar elementos de uma coleção com base em uma condição em LINQ, usa-se o método: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "Where",
                Explicacao = "Where(predicado) filtra os elementos que satisfazem a condição (predicado retorna true) e retorna IEnumerable<T>. Não modifica a coleção original. Ex: numeros.Where(n => n > 5) retorna apenas os maiores que 5." },

            // ── Lição 8: async/await ─────────────────────────────────────────────────
            new Exercicio { Id = 22, LicaoId = 8, Ordem = 1, XPRecompensa = 5,
                Enunciado = "O que acontece com a thread quando o runtime encontra um 'await' em um método assíncrono?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"A thread é liberada para atender outras tarefas enquanto aguarda\",\"O programa é pausado completamente\",\"Uma nova thread é criada automaticamente\",\"O método retorna null imediatamente\"]",
                RespostaCorreta = "A thread é liberada para atender outras tarefas enquanto aguarda",
                Explicacao = "'await' suspende o método atual sem bloquear a thread. A thread retorna ao pool e pode processar outras requisições. Quando a operação completa, o método é retomado (possivelmente em outra thread). Isso permite que um servidor atenda milhares de requisições concorrentes com poucas threads." },

            new Exercicio { Id = 23, LicaoId = 8, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Todo método que usa 'await' internamente deve ter 'async' na sua declaração.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'async' e 'await' são inseparáveis. 'async' marca o método como máquina de estado assíncrona e habilita o uso de 'await'. Sem 'async', 'await' é tratado como identificador normal e o código não compila conforme esperado." },

            new Exercicio { Id = 24, LicaoId = 8, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Um método assíncrono que não retorna valor algum deve usar o tipo de retorno: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "Task",
                Explicacao = "Use 'Task' (não 'async void') para métodos assíncronos sem retorno. Task pode ser aguardado pelo chamador e propaga exceções corretamente. 'async void' existe apenas para event handlers e não pode ser aguardado, tornando o tratamento de exceções muito difícil." },

            // ── Lição 9: Repository Pattern ──────────────────────────────────────────
            new Exercicio { Id = 25, LicaoId = 9, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual é o principal benefício do Repository Pattern?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"Desacoplar a lógica de negócio do acesso a dados\",\"Aumentar a velocidade das queries SQL\",\"Eliminar a necessidade de interfaces\",\"Substituir completamente o ORM\"]",
                RespostaCorreta = "Desacoplar a lógica de negócio do acesso a dados",
                Explicacao = "O Repository isola 'como' os dados são obtidos (SQL, EF, API externa) da lógica que decide 'o que' fazer com eles. Isso permite trocar o banco sem alterar Controllers/Services, e facilita muito os testes unitários." },

            new Exercicio { Id = 26, LicaoId = 9, Ordem = 2, XPRecompensa = 5,
                Enunciado = "No Repository Pattern, um Controller pode acessar o DbContext diretamente quando precisar de performance.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "Controllers NUNCA devem acessar infraestrutura diretamente. Para performance, otimize o Repository: use projeções com Select, AsNoTracking para leitura, paginação, índices. O Controller deve depender sempre da interface, nunca do EF Core." },

            new Exercicio { Id = 27, LicaoId = 9, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Em Clean Architecture, o Controller deve depender da ____ do Repository, não da implementação concreta.",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "interface",
                Explicacao = "Dependência de abstrações é o Dependency Inversion Principle (D do SOLID). O Controller recebe uma 'IRepository' no construtor — não sabe se é EF Core, Dapper ou um fake em memória. Isso é injeção de dependência: detalhes dependem de abstrações, não o contrário." },

            // ── Lição 10: Encapsulamento ─────────────────────────────────────────────
            new Exercicio { Id = 28, LicaoId = 10, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual modificador de acesso torna um membro visível apenas para a própria classe E para quaisquer subclasses?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"protected\",\"private\",\"internal\",\"public\"]",
                RespostaCorreta = "protected",
                Explicacao = "'protected' combina o acesso da própria classe com o das subclasses, independente do assembly. Use quando o membro faz parte do contrato de herança mas não deve ser exposto publicamente." },

            new Exercicio { Id = 29, LicaoId = 10, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Uma propriedade com 'public get' e 'private set' pode ser alterada por código fora da classe.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "Com 'private set', somente a própria classe pode escrever na propriedade. Código externo só pode ler. É o padrão de encapsulamento mais comum: expor leitura, proteger escrita para manter invariantes." },

            new Exercicio { Id = 30, LicaoId = 10, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para criar uma propriedade que só pode ser definida durante a inicialização do objeto (com { }), usa-se o acessor: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "init",
                Explicacao = "'init' (C# 9+) permite atribuição apenas no inicializador de objeto 'new Tipo { Prop = valor }'. Depois da construção, a propriedade fica somente leitura. Ideal para objetos imutáveis: DTOs, Value Objects, records." },

            // ── Lição 11: LINQ Avançado ──────────────────────────────────────────────
            new Exercicio { Id = 31, LicaoId = 11, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual método LINQ agrupa elementos em subconjuntos por uma chave?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"GroupBy\",\"Join\",\"Aggregate\",\"Partition\"]",
                RespostaCorreta = "GroupBy",
                Explicacao = "GroupBy(keySelector) agrupa os elementos por uma chave e retorna IEnumerable<IGrouping<TKey, TElement>>. Cada grupo tem .Key (a chave) e é iterável para acessar os elementos. Muito usado para totalizações por categoria, cliente, data etc." },

            new Exercicio { Id = 32, LicaoId = 11, Ordem = 2, XPRecompensa = 5,
                Enunciado = "O método Aggregate pode ser usado para concatenar strings de uma lista, acumulando o resultado elemento a elemento.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Aggregate implementa um fold/reduce genérico. Com (a, b) => $\"{a} {b}\" você concatena elementos: [\"C#\", \"é\", \"incrível\"] → \"C# é incrível\". Também pode somar, multiplicar ou qualquer operação que combine dois elementos em um." },

            new Exercicio { Id = 33, LicaoId = 11, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para retornar apenas os primeiros N elementos de uma coleção em LINQ, usa-se o método: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "Take",
                Explicacao = "Take(n) retorna no máximo n elementos do início da sequência. Se a coleção tiver menos de n elementos, retorna o que tiver. Combinado com Skip((página-1)*tamanho), implementa paginação eficiente." },

            // ── Lição 12: CancellationToken ──────────────────────────────────────────
            new Exercicio { Id = 34, LicaoId = 12, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual classe é usada para criar e emitir o sinal de cancelamento de um CancellationToken?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"CancellationTokenSource\",\"TaskCanceller\",\"CancellationFactory\",\"TokenManager\"]",
                RespostaCorreta = "CancellationTokenSource",
                Explicacao = "CancellationTokenSource cria o token (via .Token) e controla o cancelamento (via .Cancel() ou .CancelAfter()). O CancellationToken em si é passado para as operações — ele só observa o cancelamento, não o emite." },

            new Exercicio { Id = 35, LicaoId = 12, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Quando um CancellationToken é cancelado, Task.Delay(ms, token) lança automaticamente uma OperationCanceledException.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "A maioria dos métodos async do .NET aceita CancellationToken e reage ao cancelamento lançando OperationCanceledException. Isso inclui Task.Delay, HttpClient, EF Core (FindAsync, ToListAsync etc.), permitindo interrupção cooperativa." },

            new Exercicio { Id = 36, LicaoId = 12, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para verificar manualmente se um token foi cancelado e lançar a exceção padrão, usa-se: ct.____IfCancellationRequested()",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "Throw",
                Explicacao = "ThrowIfCancellationRequested() verifica ct.IsCancellationRequested e, se verdadeiro, lança OperationCanceledException. Use no início de loops ou entre operações longas para um ponto de saída limpo." },

            // ── Lição 13: Task.WhenAll e WhenAny ────────────────────────────────────
            new Exercicio { Id = 37, LicaoId = 13, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual método assíncrono aguarda a conclusão de TODAS as tasks de uma lista?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"Task.WhenAll\",\"Task.WhenAny\",\"Task.WaitAll\",\"Task.AwaitAll\"]",
                RespostaCorreta = "Task.WhenAll",
                Explicacao = "Task.WhenAll é assíncrono (retorna Task/Task<T[]>) e libera a thread enquanto aguarda. Task.WaitAll é o equivalente BLOQUEANTE — segura a thread e pode causar deadlock em contextos com SynchronizationContext. Sempre use WhenAll em código async." },

            new Exercicio { Id = 38, LicaoId = 13, Ordem = 2, XPRecompensa = 5,
                Enunciado = "Task.WhenAny completa assim que a PRIMEIRA task terminar, mesmo que as outras ainda estejam em execução.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "WhenAny retorna a primeira task concluída (com sucesso ou falha). As demais continuam em background — WhenAny não as cancela. É usado para race conditions (cache vs banco) e implementar timeouts customizados." },

            new Exercicio { Id = 39, LicaoId = 13, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para rodar múltiplas tasks em paralelo e aguardar todas assincronamente, usa-se: await Task.____(task1, task2)",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "WhenAll",
                Explicacao = "Task.WhenAll(task1, task2) aguarda que TODAS completem, com a thread liberada. O tempo total é o da task mais lenta, não a soma. Muito mais eficiente que 'await task1; await task2' em sequência quando as operações são independentes." },

            // ── Lição 14: Factory Pattern ────────────────────────────────────────────
            new Exercicio { Id = 40, LicaoId = 14, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual é o objetivo principal do Factory Pattern?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"Encapsular a lógica de criação de objetos\",\"Garantir uma única instância\",\"Observar mudanças de estado\",\"Separar interface de implementação de forma recursiva\"]",
                RespostaCorreta = "Encapsular a lógica de criação de objetos",
                Explicacao = "Factory centraliza 'como' criar objetos. O cliente recebe um produto sem conhecer a classe concreta — depende apenas da abstração (classe base ou interface). Isso permite adicionar novos tipos sem alterar o código cliente." },

            new Exercicio { Id = 41, LicaoId = 14, Ordem = 2, XPRecompensa = 5,
                Enunciado = "No Factory Pattern, o código cliente precisa conhecer todas as classes concretas que pode receber.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "Esse é exatamente o benefício do Factory: o cliente depende apenas da abstração (interface ou classe base). A Factory decide qual concreto instanciar. O cliente não importa 'NotificacaoEmail', 'NotificacaoSms' etc." },

            new Exercicio { Id = 42, LicaoId = 14, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Um método estático que decide qual subclasse instanciar com base em um parâmetro é chamado de ____ Factory.",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "Simple",
                Explicacao = "Simple Factory é o padrão mais básico: um método (geralmente estático) que escolhe qual classe concreta criar. Tecnicamente não é um dos 23 padrões GoF originais, mas é o ponto de partida antes de evoluir para Factory Method ou Abstract Factory." },

            // ── Lição 15: Singleton Pattern ──────────────────────────────────────────
            new Exercicio { Id = 43, LicaoId = 15, Ordem = 1, XPRecompensa = 5,
                Enunciado = "Qual classe do .NET garante inicialização lazy (sob demanda) e thread-safe de um Singleton?",
                Tipo = TipoExercicio.MultiplaEscolha,
                OpcoesJson = "[\"Lazy<T>\",\"ThreadLocal<T>\",\"Volatile<T>\",\"Concurrent<T>\"]",
                RespostaCorreta = "Lazy<T>",
                Explicacao = "Lazy<T> cria o valor apenas na primeira vez que .Value é acessado, de forma thread-safe por padrão. Elimina o problema de double-checked locking manual e é a forma moderna de implementar Singleton em C#." },

            new Exercicio { Id = 44, LicaoId = 15, Ordem = 2, XPRecompensa = 5,
                Enunciado = "No ASP.NET Core, AddSingleton registra o serviço de forma que uma única instância é criada e reutilizada durante toda a vida da aplicação.",
                Tipo = TipoExercicio.VerdadeiroFalso,
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "AddSingleton = uma instância por aplicação (toda a vida do processo). AddScoped = uma por requisição HTTP. AddTransient = uma nova a cada injeção. O container de DI é a forma preferida de Singleton em ASP.NET Core — é testável e não usa estado estático." },

            new Exercicio { Id = 45, LicaoId = 15, Ordem = 3, XPRecompensa = 5,
                Enunciado = "Para impedir que uma classe Singleton seja herdada (o que quebraria a unicidade), usa-se o modificador: ____",
                Tipo = TipoExercicio.PreencherEspacos,
                OpcoesJson = "[]",
                RespostaCorreta = "sealed",
                Explicacao = "'sealed' impede herança. Em Singletons é recomendado porque uma subclasse poderia criar uma segunda instância, quebrar o contrato ou expor o construtor privado. 'sealed' também pode gerar pequenas otimizações de performance em chamadas virtuais." },

            // ── CorrigirCodigo — distribuídos em lições já existentes ────────────────

            new Exercicio { Id = 46, LicaoId = 1, Ordem = 4, XPRecompensa = 8,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo não compila. Qual palavra-chave deve substituir o erro?\n\nconst int x;\nx = 10;",
                OpcoesJson = "[\"readonly\",\"var\",\"static\",\"fixed\"]",
                RespostaCorreta = "readonly",
                DicaTexto = "const exige inicialização na declaração. readonly pode ser definida no construtor.",
                Explicacao = "'const' exige valor na declaração. Para atribuir no corpo do código, use 'readonly' (em campos de classe) ou simplesmente remova o modificador." },

            new Exercicio { Id = 47, LicaoId = 3, Ordem = 4, XPRecompensa = 8,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "Encontre o bug:\n\nfor (int i = 0; i <= lista.Count; i++)\n{\n    Console.WriteLine(lista[i]);\n}",
                OpcoesJson = "[\"i <= lista.Count\",\"i < lista.Count\",\"i = 0\",\"lista[i]\"]",
                RespostaCorreta = "i < lista.Count",
                DicaTexto = "Índices em C# vão de 0 até Count-1. Usar <= gera IndexOutOfRangeException.",
                Explicacao = "O índice máximo válido é Count-1. Com '<=' o loop tenta acessar lista[Count], que não existe, lançando IndexOutOfRangeException." },

            new Exercicio { Id = 48, LicaoId = 7, Ordem = 4, XPRecompensa = 8,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo lança NullReferenceException. O que está errado?\n\nstring? nome = null;\nConsole.WriteLine(nome.Length);",
                OpcoesJson = "[\"nome.Length\",\"nome?.Length\",\"string? nome\",\"Console.WriteLine\"]",
                RespostaCorreta = "nome?.Length",
                DicaTexto = "Operador ?. acessa o membro somente se o objeto não for null.",
                Explicacao = "Usar '?.' (null-conditional) retorna null em vez de lançar exceção. Para nullable types, sempre prefira '?.' ou verifique antes com 'if (nome != null)'." },

            new Exercicio { Id = 49, LicaoId = 11, Ordem = 4, XPRecompensa = 8,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O método abaixo não aguarda a operação async corretamente:\n\npublic void BuscarDados()\n{\n    var result = ObterAsync();\n    Console.WriteLine(result);\n}",
                OpcoesJson = "[\"void → async Task\",\"result → await result\",\"ObterAsync() → ObterAsync\",\"Console.WriteLine\"]",
                RespostaCorreta = "void → async Task",
                DicaTexto = "Para usar await, o método precisa ser marcado como async e retornar Task.",
                Explicacao = "Sem 'async', não é possível usar 'await'. O método deve ser 'async Task BuscarDados()' e a chamada 'var result = await ObterAsync();' para aguardar corretamente." }
        );

        // Novas Lições — Módulos 6–13
        modelBuilder.Entity<Licao>().HasData(

            new Licao { Id = 16, ModuloId = 6, Ordem = 1, XPRecompensa = 15, Ativo = true,
                Titulo = "DbContext e Configuração",
                Descricao = "Configurando o EF Core, DbContext, DbSet e connection string",
                ConteudoTeoricoMarkdown =
                    "## Entity Framework Core — DbContext\n\n" +
                    "O **Entity Framework Core** é o ORM oficial do .NET. Ele mapeia classes C# para tabelas do banco, " +
                    "eliminando SQL manual para operações comuns.\n\n" +
                    "### Instalando\n\n" +
                    "```bash\ndotnet add package Microsoft.EntityFrameworkCore.Sqlite\ndotnet add package Microsoft.EntityFrameworkCore.Tools\n```\n\n" +
                    "### Criando o DbContext\n\n" +
                    "```csharp\npublic class AppDbContext : DbContext\n{\n    public DbSet<Produto> Produtos => Set<Produto>();\n\n" +
                    "    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }\n}\n```\n\n" +
                    "`DbSet<T>` representa uma tabela. Cada propriedade `DbSet` vira uma tabela no banco.\n\n" +
                    "### Registrando no Program.cs\n\n" +
                    "```csharp\nbuilder.Services.AddDbContext<AppDbContext>(opt =>\n    opt.UseSqlite(\"Data Source=app.db\"));\n```\n\n" +
                    "### A entidade\n\n" +
                    "```csharp\npublic class Produto\n{\n    public int Id { get; set; }        // PK por convenção\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n}\n```\n\n" +
                    "> **Convenção vs configuração**: EF infere `Id` como chave primária automaticamente. " +
                    "Para casos especiais, use `[Key]` ou `modelBuilder.Entity<T>().HasKey(...)`." },

            new Licao { Id = 17, ModuloId = 6, Ordem = 2, XPRecompensa = 15, Ativo = true,
                Titulo = "Migrations",
                Descricao = "Criando e aplicando migrations para versionar o schema do banco",
                ConteudoTeoricoMarkdown =
                    "## Migrations no EF Core\n\n" +
                    "Migrations são **snapshots incrementais** do schema. Cada migration registra o que mudou no modelo, " +
                    "permitindo evoluir o banco sem perder dados.\n\n" +
                    "### Fluxo básico\n\n" +
                    "```bash\n# 1. Criar migration (detecta mudanças no modelo)\ndotnet ef migrations add CriarTabelaProdutos\n\n# 2. Aplicar ao banco\ndotnet ef database update\n\n# 3. Reverter última migration\ndotnet ef database update NomeDaMigrationAnterior\n```\n\n" +
                    "### O que o EF gera\n\n" +
                    "```csharp\npublic partial class CriarTabelaProdutos : Migration\n{\n    protected override void Up(MigrationBuilder migrationBuilder)\n    {\n        migrationBuilder.CreateTable(\n            name: \"Produtos\",\n            columns: table => new\n            {\n                Id    = table.Column<int>(nullable: false).Annotation(\"Sqlite:Autoincrement\", true),\n                Nome  = table.Column<string>(nullable: false),\n                Preco = table.Column<decimal>(nullable: false)\n            },\n            constraints: t => t.PrimaryKey(\"PK_Produtos\", x => x.Id));\n    }\n\n    protected override void Down(MigrationBuilder migrationBuilder)\n    {\n        migrationBuilder.DropTable(name: \"Produtos\");\n    }\n}\n```\n\n" +
                    "> **Dica**: Commite as migrations junto com o código. Elas são parte do contrato do sistema." },

            new Licao { Id = 18, ModuloId = 6, Ordem = 3, XPRecompensa = 15, Ativo = true,
                Titulo = "CRUD com EF Core",
                Descricao = "Create, Read, Update e Delete usando DbContext e LINQ",
                ConteudoTeoricoMarkdown =
                    "## CRUD com Entity Framework Core\n\n" +
                    "Com o DbContext configurado, todas as operações são feitas via C# — sem escrever SQL.\n\n" +
                    "### Create\n\n" +
                    "```csharp\nvar produto = new Produto { Nome = \"Teclado\", Preco = 299.90m };\nctx.Produtos.Add(produto);\nawait ctx.SaveChangesAsync(); // persiste no banco\n```\n\n" +
                    "### Read\n\n" +
                    "```csharp\n// Todos\nvar todos = await ctx.Produtos.ToListAsync();\n\n// Por ID\nvar p = await ctx.Produtos.FindAsync(1);\n\n// Com filtro\nvar baratos = await ctx.Produtos\n    .Where(p => p.Preco < 100)\n    .OrderBy(p => p.Nome)\n    .ToListAsync();\n```\n\n" +
                    "### Update\n\n" +
                    "```csharp\nvar produto = await ctx.Produtos.FindAsync(id);\nif (produto is not null)\n{\n    produto.Preco = 349.90m;\n    await ctx.SaveChangesAsync(); // EF detecta a mudança automaticamente\n}\n```\n\n" +
                    "### Delete\n\n" +
                    "```csharp\nvar produto = await ctx.Produtos.FindAsync(id);\nif (produto is not null)\n{\n    ctx.Produtos.Remove(produto);\n    await ctx.SaveChangesAsync();\n}\n```\n\n" +
                    "> **Change Tracking**: O EF monitora todas as entidades carregadas. `SaveChangesAsync()` gera o SQL mínimo necessário para sincronizar as mudanças." },

            new Licao { Id = 19, ModuloId = 7, Ordem = 1, XPRecompensa = 15, Ativo = true,
                Titulo = "Criando uma API REST",
                Descricao = "Estrutura de uma Web API, roteamento e o pipeline do ASP.NET",
                ConteudoTeoricoMarkdown =
                    "## ASP.NET Core Web API\n\n" +
                    "O ASP.NET Core é o framework web do .NET. Uma **Web API REST** expõe recursos via HTTP, " +
                    "usando verbos (GET, POST, PUT, DELETE) e retornando JSON.\n\n" +
                    "### Program.cs mínimo\n\n" +
                    "```csharp\nvar builder = WebApplication.CreateBuilder(args);\nbuilder.Services.AddControllers();\n\nvar app = builder.Build();\napp.MapControllers();\napp.Run();\n```\n\n" +
                    "### Atributos de rota\n\n" +
                    "```csharp\n[ApiController]          // habilita validação automática e binding\n[Route(\"api/produtos\")] // rota base do controller\npublic class ProdutosController : ControllerBase { }\n```\n\n" +
                    "### ControllerBase vs Controller\n\n" +
                    "| | ControllerBase | Controller |\n|---|---|---|\n| API REST | ✅ preferido | ✅ |\n| Views MVC | ❌ | ✅ |\n| Peso | leve | completo |\n\n" +
                    "> Use sempre `ControllerBase` para APIs — `Controller` carrega suporte a Views que você não precisa." },

            new Licao { Id = 20, ModuloId = 7, Ordem = 2, XPRecompensa = 15, Ativo = true,
                Titulo = "Controllers e Actions",
                Descricao = "Criando endpoints, recebendo parâmetros e retornando IActionResult",
                ConteudoTeoricoMarkdown =
                    "## Controllers e Actions\n\n" +
                    "Cada método público de um controller é uma **action** — um endpoint HTTP.\n\n" +
                    "### CRUD completo\n\n" +
                    "```csharp\n[HttpGet]                          // GET /api/produtos\npublic async Task<IActionResult> Listar()\n    => Ok(await repo.ObterTodosAsync());\n\n[HttpGet(\"{id}\")]                  // GET /api/produtos/5\npublic async Task<IActionResult> ObterPorId(int id)\n{\n    var p = await repo.ObterPorIdAsync(id);\n    return p is null ? NotFound() : Ok(p);\n}\n\n[HttpPost]                         // POST /api/produtos\npublic async Task<IActionResult> Criar([FromBody] ProdutoDto dto)\n{\n    var produto = new Produto { Nome = dto.Nome, Preco = dto.Preco };\n    await repo.AdicionarAsync(produto);\n    return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);\n}\n\n[HttpPut(\"{id}\")]                  // PUT /api/produtos/5\npublic async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoDto dto) { ... }\n\n[HttpDelete(\"{id}\")]               // DELETE /api/produtos/5\npublic async Task<IActionResult> Deletar(int id) { ... }\n```\n\n" +
                    "### Binding de parâmetros\n\n" +
                    "| Atributo | Origem |\n|---|---|\n| `[FromRoute]` | URL `/api/produtos/{id}` |\n| `[FromQuery]` | Query string `?page=2` |\n| `[FromBody]` | Corpo JSON da requisição |\n| `[FromHeader]` | Cabeçalho HTTP |" },

            new Licao { Id = 21, ModuloId = 7, Ordem = 3, XPRecompensa = 15, Ativo = true,
                Titulo = "Status Codes e Respostas HTTP",
                Descricao = "Retornando os códigos HTTP corretos em cada situação",
                ConteudoTeoricoMarkdown =
                    "## Status Codes HTTP\n\n" +
                    "Usar os status codes corretos é fundamental para uma API REST bem projetada.\n\n" +
                    "### Métodos do ControllerBase\n\n" +
                    "```csharp\nOk(objeto)           // 200 — sucesso com corpo\nCreatedAtAction(...) // 201 — criado com Location header\nNoContent()          // 204 — sucesso sem corpo (PUT/DELETE)\nBadRequest(erro)     // 400 — dados inválidos\nUnauthorized()       // 401 — não autenticado\nForbidden()          // 403 — autenticado mas sem permissão\nNotFound()           // 404 — recurso não encontrado\nConflict(...)        // 409 — conflito (ex: email duplicado)\n```\n\n" +
                    "### Exemplo prático — DELETE\n\n" +
                    "```csharp\n[HttpDelete(\"{id}\")]\npublic async Task<IActionResult> Deletar(int id)\n{\n    var produto = await ctx.Produtos.FindAsync(id);\n    if (produto is null) return NotFound();   // 404\n\n    ctx.Produtos.Remove(produto);\n    await ctx.SaveChangesAsync();\n    return NoContent();                       // 204\n}\n```\n\n" +
                    "### Por que 201 em vez de 200 no POST?\n\n" +
                    "`CreatedAtAction` retorna 201 **e** um header `Location` apontando para o recurso criado. " +
                    "Clientes REST podem usar esse header para buscar o recurso recém-criado sem hardcodar URLs." },

            new Licao { Id = 22, ModuloId = 8, Ordem = 1, XPRecompensa = 15, Ativo = true,
                Titulo = "Data Annotations",
                Descricao = "Validando modelos com atributos [Required], [Range], [StringLength] e outros",
                ConteudoTeoricoMarkdown =
                    "## Data Annotations\n\n" +
                    "Data Annotations são **atributos** que definem regras de validação diretamente na classe. " +
                    "Com `[ApiController]`, o ASP.NET valida automaticamente antes de executar a action.\n\n" +
                    "### Atributos principais\n\n" +
                    "```csharp\npublic class CriarProdutoDto\n{\n    [Required(ErrorMessage = \"Nome é obrigatório\")]\n    [StringLength(100, MinimumLength = 3)]\n    public string Nome { get; set; } = string.Empty;\n\n    [Range(0.01, 99999.99, ErrorMessage = \"Preço deve estar entre 0,01 e 99.999,99\")]\n    public decimal Preco { get; set; }\n\n    [EmailAddress]\n    public string? EmailFornecedor { get; set; }\n\n    [RegularExpression(@\"^\\d{2}\\.\\d{3}-\\d{3}$\")]\n    public string? CodigoInterno { get; set; }\n}\n```\n\n" +
                    "### Como funciona com [ApiController]\n\n" +
                    "```csharp\n[HttpPost]\npublic async Task<IActionResult> Criar([FromBody] CriarProdutoDto dto)\n{\n    // Se dto for inválido, o ASP.NET retorna 400 automaticamente\n    // ModelState.IsValid sempre será true aqui\n    ...\n}\n```\n\n" +
                    "> Sem `[ApiController]`, você precisaria verificar `if (!ModelState.IsValid) return BadRequest(ModelState);` manualmente." },

            new Licao { Id = 23, ModuloId = 8, Ordem = 2, XPRecompensa = 15, Ativo = true,
                Titulo = "Padrão DTO",
                Descricao = "O que são DTOs, por que usá-los e como separar Request de Response",
                ConteudoTeoricoMarkdown =
                    "## Data Transfer Objects (DTOs)\n\n" +
                    "DTO é um objeto simples que transporta dados entre camadas. **Nunca exponha sua entidade de domínio diretamente na API** — isso vaza detalhes internos e cria acoplamento.\n\n" +
                    "### Por que usar DTOs?\n\n" +
                    "```csharp\n// ❌ Expor a entidade diretamente\n[HttpGet]\npublic async Task<IActionResult> Listar()\n    => Ok(await ctx.Usuarios.ToListAsync()); // vaza SenhaHash, CreatedAt, etc.\n\n// ✅ Usar um DTO\n[HttpGet]\npublic async Task<IActionResult> Listar()\n{\n    var usuarios = await ctx.Usuarios\n        .Select(u => new UsuarioResponseDto\n        {\n            Id = u.Id,\n            Nome = u.Nome,\n            Email = u.Email\n        }).ToListAsync();\n    return Ok(usuarios);\n}\n```\n\n" +
                    "### Separando Request e Response\n\n" +
                    "```csharp\n// Request — o que o cliente envia\npublic class CriarProdutoDto\n{\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n}\n\n// Response — o que a API retorna\npublic class ProdutoResponseDto\n{\n    public int Id { get; set; }\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n    public DateTime CriadoEm { get; set; }\n}\n```\n\n" +
                    "> **Regra de ouro**: Request DTOs definem o **contrato de entrada**. Response DTOs definem o **contrato de saída**. Mantenha-os separados e versionáveis." },

            new Licao { Id = 24, ModuloId = 8, Ordem = 3, XPRecompensa = 15, Ativo = true,
                Titulo = "FluentValidation",
                Descricao = "Validações expressivas e reutilizáveis com FluentValidation",
                ConteudoTeoricoMarkdown =
                    "## FluentValidation\n\n" +
                    "FluentValidation é uma biblioteca que define regras de validação em classes dedicadas, " +
                    "separando a validação do modelo e permitindo lógica complexa.\n\n" +
                    "```bash\ndotnet add package FluentValidation.AspNetCore\n```\n\n" +
                    "### Criando um validator\n\n" +
                    "```csharp\npublic class CriarProdutoValidator : AbstractValidator<CriarProdutoDto>\n{\n    public CriarProdutoValidator()\n    {\n        RuleFor(x => x.Nome)\n            .NotEmpty().WithMessage(\"Nome é obrigatório\")\n            .MinimumLength(3).WithMessage(\"Mínimo 3 caracteres\")\n            .MaximumLength(100);\n\n        RuleFor(x => x.Preco)\n            .GreaterThan(0).WithMessage(\"Preço deve ser positivo\");\n\n        // Validação condicional\n        When(x => x.EmailFornecedor is not null, () =>\n        {\n            RuleFor(x => x.EmailFornecedor).EmailAddress();\n        });\n    }\n}\n```\n\n" +
                    "### Registrando\n\n" +
                    "```csharp\nbuilder.Services.AddValidatorsFromAssemblyContaining<CriarProdutoValidator>();\n```\n\n" +
                    "> **Data Annotations vs FluentValidation**: Use Annotations para validações simples e diretas. Use FluentValidation quando precisar de lógica condicional, mensagens dinâmicas ou reuso entre validators." },

            new Licao { Id = 25, ModuloId = 9, Ordem = 1, XPRecompensa = 20, Ativo = true,
                Titulo = "Fundamentos do JWT",
                Descricao = "Estrutura do token JWT, claims e como funciona a autenticação stateless",
                ConteudoTeoricoMarkdown =
                    "## JSON Web Token (JWT)\n\n" +
                    "JWT é um padrão para transmitir informações de forma segura entre partes como um token compacto e autocontido. " +
                    "Diferente de sessões, é **stateless** — o servidor não precisa armazenar estado.\n\n" +
                    "### Estrutura do JWT\n\n" +
                    "Um JWT tem 3 partes separadas por `.`:\n\n" +
                    "```\neyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjMiLCJuYW1lIjoiTWFyaWEifQ.SflKxwRJS...\n   HEADER               PAYLOAD                              SIGNATURE\n```\n\n" +
                    "- **Header**: algoritmo usado (ex: HS256)\n- **Payload**: claims (dados do usuário)\n- **Signature**: garante que o token não foi adulterado\n\n" +
                    "### Claims\n\n" +
                    "Claims são pares chave-valor no payload:\n\n" +
                    "```csharp\nvar claims = new[]\n{\n    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),\n    new Claim(ClaimTypes.Name, usuario.Nome),\n    new Claim(ClaimTypes.Email, usuario.Email),\n    new Claim(ClaimTypes.Role, \"Admin\")  // roles para autorização\n};\n```\n\n" +
                    "> **Não coloque dados sensíveis no JWT!** O payload é apenas Base64 — qualquer pessoa pode decodificá-lo. A assinatura garante autenticidade, não confidencialidade." },

            new Licao { Id = 26, ModuloId = 9, Ordem = 2, XPRecompensa = 20, Ativo = true,
                Titulo = "Implementando JWT no .NET",
                Descricao = "Gerando tokens, configurando autenticação e protegendo endpoints",
                ConteudoTeoricoMarkdown =
                    "## Implementando JWT no ASP.NET Core\n\n" +
                    "### Instalação\n\n" +
                    "```bash\ndotnet add package Microsoft.AspNetCore.Authentication.JwtBearer\n```\n\n" +
                    "### Gerando o token\n\n" +
                    "```csharp\npublic string GerarToken(Usuario usuario)\n{\n    var chave = new SymmetricSecurityKey(\n        Encoding.UTF8.GetBytes(\"sua-chave-secreta-minimo-32-chars\"));\n    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);\n\n    var claims = new[]\n    {\n        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),\n        new Claim(ClaimTypes.Name, usuario.Nome)\n    };\n\n    var token = new JwtSecurityToken(\n        issuer: \"minha-api\",\n        audience: \"meu-app\",\n        claims: claims,\n        expires: DateTime.UtcNow.AddHours(8),\n        signingCredentials: credenciais);\n\n    return new JwtSecurityTokenHandler().WriteToken(token);\n}\n```\n\n" +
                    "### Configurando no Program.cs\n\n" +
                    "```csharp\nbuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)\n    .AddJwtBearer(opt =>\n    {\n        opt.TokenValidationParameters = new()\n        {\n            ValidateIssuerSigningKey = true,\n            IssuerSigningKey = new SymmetricSecurityKey(\n                Encoding.UTF8.GetBytes(config[\"Jwt:Key\"]!)),\n            ValidateIssuer = false,\n            ValidateAudience = false\n        };\n    });\n\napp.UseAuthentication();\napp.UseAuthorization();\n```" },

            new Licao { Id = 27, ModuloId = 9, Ordem = 3, XPRecompensa = 20, Ativo = true,
                Titulo = "Autorização com Roles",
                Descricao = "Protegendo endpoints com [Authorize], roles e claims personalizadas",
                ConteudoTeoricoMarkdown =
                    "## Autorização no ASP.NET Core\n\n" +
                    "**Autenticação** = quem você é. **Autorização** = o que você pode fazer.\n\n" +
                    "### [Authorize] básico\n\n" +
                    "```csharp\n[Authorize]  // qualquer usuário autenticado\n[HttpGet]\npublic IActionResult Protegido() => Ok(\"Apenas logados\");\n\n[AllowAnonymous]  // explicitamente público\n[HttpGet(\"publico\")]\npublic IActionResult Publico() => Ok(\"Todos podem\");\n```\n\n" +
                    "### Roles\n\n" +
                    "```csharp\n[Authorize(Roles = \"Admin\")]  // somente Admins\n[HttpDelete(\"{id}\")]\npublic IActionResult Deletar(int id) { ... }\n\n[Authorize(Roles = \"Admin,Moderador\")]  // Admin OU Moderador\n[HttpPut(\"{id}\")]\npublic IActionResult Atualizar(int id) { ... }\n```\n\n" +
                    "### Lendo claims na action\n\n" +
                    "```csharp\n[Authorize]\n[HttpGet(\"meu-perfil\")]\npublic IActionResult MeuPerfil()\n{\n    var userId = int.Parse(\n        User.FindFirstValue(ClaimTypes.NameIdentifier)!);\n    var nome = User.FindFirstValue(ClaimTypes.Name);\n    return Ok(new { userId, nome });\n}\n```\n\n" +
                    "> A propriedade `User` no controller é um `ClaimsPrincipal` populado automaticamente pelo middleware JWT a partir do token." },

            new Licao { Id = 28, ModuloId = 10, Ordem = 1, XPRecompensa = 18, Ativo = true,
                Titulo = "Exception Handler Global",
                Descricao = "Capturando exceções globalmente com UseExceptionHandler e Problem Details",
                ConteudoTeoricoMarkdown =
                    "## Tratamento Global de Erros\n\n" +
                    "Tratar erros individualmente em cada action gera código duplicado e inconsistente. " +
                    "O ASP.NET Core oferece mecanismos para centralizar o tratamento.\n\n" +
                    "### UseExceptionHandler\n\n" +
                    "```csharp\napp.UseExceptionHandler(errApp =>\n{\n    errApp.Run(async ctx =>\n    {\n        ctx.Response.StatusCode = 500;\n        ctx.Response.ContentType = \"application/json\";\n        var erro = ctx.Features.Get<IExceptionHandlerFeature>();\n        await ctx.Response.WriteAsJsonAsync(new\n        {\n            erro = \"Ocorreu um erro interno.\",\n            detalhe = erro?.Error.Message\n        });\n    });\n});\n```\n\n" +
                    "### Problem Details (recomendado no .NET 7+)\n\n" +
                    "```csharp\nbuilder.Services.AddProblemDetails();\napp.UseExceptionHandler();\napp.UseStatusCodePages();\n```\n\n" +
                    "O ASP.NET passa a retornar respostas de erro no formato RFC 7807:\n\n" +
                    "```json\n{\n  \"type\": \"https://tools.ietf.org/html/rfc7231#section-6.6.1\",\n  \"title\": \"An error occurred while processing your request.\",\n  \"status\": 500\n}\n```\n\n" +
                    "> **Nunca exponha stack traces em produção.** Use `app.Environment.IsDevelopment()` para mostrar detalhes apenas no desenvolvimento." },

            new Licao { Id = 29, ModuloId = 10, Ordem = 2, XPRecompensa = 18, Ativo = true,
                Titulo = "Criando Middleware",
                Descricao = "Pipeline de requisições, criando middleware customizado com IMiddleware",
                ConteudoTeoricoMarkdown =
                    "## Middleware no ASP.NET Core\n\n" +
                    "O pipeline do ASP.NET é uma cadeia de middlewares. Cada um pode processar a requisição, " +
                    "passar para o próximo (`next`) ou encerrar a resposta.\n\n" +
                    "```\nRequest →  [Auth] → [Logging] → [Routing] → Controller → Response\n```\n\n" +
                    "### Criando um middleware de logging\n\n" +
                    "```csharp\npublic class LoggingMiddleware : IMiddleware\n{\n    private readonly ILogger<LoggingMiddleware> _logger;\n\n    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)\n        => _logger = logger;\n\n    public async Task InvokeAsync(HttpContext context, RequestDelegate next)\n    {\n        var sw = Stopwatch.StartNew();\n        _logger.LogInformation(\"{Method} {Path} iniciado\",\n            context.Request.Method, context.Request.Path);\n\n        await next(context); // passa para o próximo middleware\n\n        sw.Stop();\n        _logger.LogInformation(\"{Method} {Path} → {Status} em {Ms}ms\",\n            context.Request.Method, context.Request.Path,\n            context.Response.StatusCode, sw.ElapsedMilliseconds);\n    }\n}\n```\n\n" +
                    "### Registrando\n\n" +
                    "```csharp\nbuilder.Services.AddTransient<LoggingMiddleware>();\napp.UseMiddleware<LoggingMiddleware>();\n```\n\n" +
                    "> A ordem importa! Middlewares são executados na ordem em que são registrados." },

            new Licao { Id = 30, ModuloId = 10, Ordem = 3, XPRecompensa = 18, Ativo = true,
                Titulo = "Logging com ILogger",
                Descricao = "Usando ILogger, níveis de log e configurando providers",
                ConteudoTeoricoMarkdown =
                    "## Logging no .NET\n\n" +
                    "O .NET tem um sistema de logging built-in via `ILogger<T>`. Evite `Console.WriteLine` em produção — use logging estruturado.\n\n" +
                    "### Injetando e usando\n\n" +
                    "```csharp\npublic class ProdutosController : ControllerBase\n{\n    private readonly ILogger<ProdutosController> _logger;\n\n    public ProdutosController(ILogger<ProdutosController> logger)\n        => _logger = logger;\n\n    [HttpGet]\n    public async Task<IActionResult> Listar()\n    {\n        _logger.LogInformation(\"Listando produtos\");\n        try\n        {\n            var produtos = await repo.ObterTodosAsync();\n            return Ok(produtos);\n        }\n        catch (Exception ex)\n        {\n            _logger.LogError(ex, \"Erro ao listar produtos\");\n            return StatusCode(500);\n        }\n    }\n}\n```\n\n" +
                    "### Níveis de log (em ordem crescente de severidade)\n\n" +
                    "| Nível | Uso |\n|---|---|\n| `LogTrace` | Diagnóstico muito detalhado |\n| `LogDebug` | Informações de depuração |\n| `LogInformation` | Fluxo normal da aplicação |\n| `LogWarning` | Situação inesperada mas recuperável |\n| `LogError` | Erro que causou falha na operação |\n| `LogCritical` | Falha grave — sistema pode parar |\n\n" +
                    "> Use **logging estruturado**: `_logger.LogInformation(\"Usuário {Id} criado\", usuario.Id)` em vez de interpolação. Facilita busca e análise nos sistemas de log." },

            new Licao { Id = 31, ModuloId = 11, Ordem = 1, XPRecompensa = 18, Ativo = true,
                Titulo = "Fundamentos de Testes Unitários",
                Descricao = "O que testar, estrutura Arrange-Act-Assert e escrevendo testes com xUnit",
                ConteudoTeoricoMarkdown =
                    "## Testes Unitários com xUnit\n\n" +
                    "Testes unitários verificam uma **unidade isolada** de código (método ou classe) sem dependências externas.\n\n" +
                    "### Setup\n\n" +
                    "```bash\ndotnet new xunit -n MeuProjeto.Tests\ndotnet add reference ../MeuProjeto/MeuProjeto.csproj\n```\n\n" +
                    "### Estrutura AAA\n\n" +
                    "```csharp\npublic class CalculadoraTests\n{\n    [Fact]  // marca como teste\n    public void Somar_DoisNumeros_RetornaSoma()\n    {\n        // Arrange — prepara o cenário\n        var calc = new Calculadora();\n\n        // Act — executa a ação\n        var resultado = calc.Somar(2, 3);\n\n        // Assert — verifica o resultado\n        Assert.Equal(5, resultado);\n    }\n\n    [Theory]  // teste parametrizado\n    [InlineData(2, 3, 5)]\n    [InlineData(-1, 1, 0)]\n    [InlineData(0, 0, 0)]\n    public void Somar_Parametrizado(int a, int b, int esperado)\n    {\n        var calc = new Calculadora();\n        Assert.Equal(esperado, calc.Somar(a, b));\n    }\n}\n```\n\n" +
                    "> **[Fact]** = um teste único. **[Theory]** + **[InlineData]** = mesmo teste com múltiplos inputs." },

            new Licao { Id = 32, ModuloId = 11, Ordem = 2, XPRecompensa = 18, Ativo = true,
                Titulo = "Mocking com Moq",
                Descricao = "Isolando dependências com mocks para testar unidades em isolamento",
                ConteudoTeoricoMarkdown =
                    "## Mocking com Moq\n\n" +
                    "Mocks substituem dependências reais (banco, email, API externa) por objetos controlados.\n\n" +
                    "```bash\ndotnet add package Moq\n```\n\n" +
                    "### Exemplo — testando um service\n\n" +
                    "```csharp\npublic class ProdutoServiceTests\n{\n    [Fact]\n    public async Task ObterPorId_ProdutoExistente_RetornaProduto()\n    {\n        // Arrange\n        var mockRepo = new Mock<IProdutoRepository>();\n        mockRepo.Setup(r => r.ObterPorIdAsync(1))\n                .ReturnsAsync(new Produto { Id = 1, Nome = \"Teclado\" });\n\n        var service = new ProdutoService(mockRepo.Object);\n\n        // Act\n        var produto = await service.ObterPorIdAsync(1);\n\n        // Assert\n        Assert.NotNull(produto);\n        Assert.Equal(\"Teclado\", produto.Nome);\n        mockRepo.Verify(r => r.ObterPorIdAsync(1), Times.Once);\n    }\n}\n```\n\n" +
                    "### Operações principais do Moq\n\n" +
                    "```csharp\nmock.Setup(x => x.Metodo()).Returns(valor);         // configura retorno\nmock.Setup(x => x.MetodoAsync()).ReturnsAsync(valor); // async\nmock.Verify(x => x.Metodo(), Times.Once());           // verifica chamada\nmock.Verify(x => x.Metodo(), Times.Never());          // verifica que NÃO foi chamado\n```" },

            new Licao { Id = 33, ModuloId = 11, Ordem = 3, XPRecompensa = 18, Ativo = true,
                Titulo = "Test-Driven Development",
                Descricao = "O ciclo Red-Green-Refactor e como o TDD guia o design do código",
                ConteudoTeoricoMarkdown =
                    "## TDD — Test-Driven Development\n\n" +
                    "TDD inverte o fluxo: você escreve o **teste primeiro**, depois o código para fazê-lo passar.\n\n" +
                    "### Ciclo Red-Green-Refactor\n\n" +
                    "```\n🔴 RED    → Escreva um teste que falha (o código ainda não existe)\n🟢 GREEN  → Escreva o mínimo de código para o teste passar\n🔵 REFACTOR → Melhore o código sem quebrar o teste\n```\n\n" +
                    "### Exemplo prático\n\n" +
                    "```csharp\n// 🔴 RED — escrevo o teste primeiro\n[Fact]\npublic void Desconto_PedidoAcima100_Aplica10Porcento()\n{\n    var pedido = new Pedido { Total = 200m };\n    pedido.AplicarDesconto();\n    Assert.Equal(180m, pedido.Total);\n}\n\n// 🟢 GREEN — mínimo para passar\npublic void AplicarDesconto()\n{\n    if (Total >= 100) Total *= 0.9m;\n}\n\n// 🔵 REFACTOR — melhoro o design\npublic void AplicarDesconto()\n{\n    const decimal LimiteDesconto = 100m;\n    const decimal PercentualDesconto = 0.10m;\n    if (Total >= LimiteDesconto)\n        Total -= Total * PercentualDesconto;\n}\n```\n\n" +
                    "> **Benefício principal**: TDD força você a pensar na **interface** antes da implementação, resultando em código mais modular e testável." },

            new Licao { Id = 34, ModuloId = 12, Ordem = 1, XPRecompensa = 18, Ativo = true,
                Titulo = "Princípios de DI",
                Descricao = "O que é injeção de dependência, inversão de controle e por que usar",
                ConteudoTeoricoMarkdown =
                    "## Injeção de Dependência\n\n" +
                    "**Injeção de Dependência (DI)** é um padrão onde um objeto recebe suas dependências de fora, " +
                    "em vez de criá-las internamente. Isso implementa o princípio **Dependency Inversion (D do SOLID)**.\n\n" +
                    "### Sem DI — fortemente acoplado\n\n" +
                    "```csharp\npublic class PedidoService\n{\n    private readonly EmailService _email = new EmailService(); // acoplamento!\n    private readonly SqlRepository _repo = new SqlRepository(\"connection...\");\n\n    public void Processar(Pedido p)\n    {\n        _repo.Salvar(p);\n        _email.Enviar(p.Email, \"Pedido confirmado\");\n    }\n    // impossível testar sem banco e servidor de email reais\n}\n```\n\n" +
                    "### Com DI — desacoplado\n\n" +
                    "```csharp\npublic class PedidoService\n{\n    private readonly IPedidoRepository _repo;\n    private readonly IEmailService _email;\n\n    public PedidoService(IPedidoRepository repo, IEmailService email)\n    {\n        _repo = repo;   // recebe de fora\n        _email = email; // recebe de fora\n    }\n\n    public void Processar(Pedido p)\n    {\n        _repo.Salvar(p);\n        _email.Enviar(p.Email, \"Pedido confirmado\");\n    }\n    // testável: basta passar mocks no construtor\n}\n```\n\n" +
                    "> **IoC (Inversion of Control)**: em vez de a classe controlar suas dependências, o **contêiner** injeta o que ela precisa." },

            new Licao { Id = 35, ModuloId = 12, Ordem = 2, XPRecompensa = 18, Ativo = true,
                Titulo = "IoC Container no .NET",
                Descricao = "Registrando serviços no IServiceCollection e resolvendo dependências",
                ConteudoTeoricoMarkdown =
                    "## IoC Container do ASP.NET Core\n\n" +
                    "O .NET tem um contêiner de DI built-in via `IServiceCollection`. " +
                    "Você registra serviços no `Program.cs` e o framework injeta automaticamente onde necessário.\n\n" +
                    "### Registrando serviços\n\n" +
                    "```csharp\n// Interfaces → implementações\nbuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();\nbuilder.Services.AddScoped<IEmailService, SmtpEmailService>();\nbuilder.Services.AddSingleton<ICacheService, MemoryCacheService>();\nbuilder.Services.AddTransient<IValidationService, ValidationService>();\n\n// Classe concreta (sem interface)\nbuilder.Services.AddScoped<ReportGenerator>();\n```\n\n" +
                    "### Resolvendo fora do construtor\n\n" +
                    "```csharp\n// Em casos especiais, resolução manual\nusing var scope = app.Services.CreateScope();\nvar repo = scope.ServiceProvider.GetRequiredService<IProdutoRepository>();\n```\n\n" +
                    "### Injeção em controllers\n\n" +
                    "```csharp\npublic class ProdutosController : ControllerBase\n{\n    public ProdutosController(\n        IProdutoRepository repo,    // injetado automaticamente\n        ILogger<ProdutosController> logger)  // também injetado\n    { ... }\n}\n```\n\n" +
                    "> O contêiner resolve recursivamente: se `ProdutoRepository` depende de `AppDbContext`, o contêiner injeta o `DbContext` também." },

            new Licao { Id = 36, ModuloId = 12, Ordem = 3, XPRecompensa = 18, Ativo = true,
                Titulo = "Lifetimes: Transient, Scoped, Singleton",
                Descricao = "Entendendo os ciclos de vida dos serviços e quando usar cada um",
                ConteudoTeoricoMarkdown =
                    "## Lifetimes de Serviços\n\n" +
                    "O lifetime define **por quanto tempo** uma instância do serviço é reutilizada.\n\n" +
                    "### Os três lifetimes\n\n" +
                    "| Lifetime | Instância criada | Quando usar |\n|---|---|---|\n| **Transient** | A cada injeção | Serviços leves, stateless |\n| **Scoped** | Uma por requisição HTTP | DbContext, repositórios |\n| **Singleton** | Uma para toda a app | Cache, configuração, logger |\n\n" +
                    "### Exemplos\n\n" +
                    "```csharp\n// Transient — nova instância toda vez\nbuilder.Services.AddTransient<IEmailValidator, EmailValidator>();\n\n// Scoped — mesma instância durante a requisição\nbuilder.Services.AddScoped<AppDbContext>();  // EF Core requer Scoped\nbuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();\n\n// Singleton — mesma instância para sempre\nbuilder.Services.AddSingleton<IConfiguration>(builder.Configuration);\nbuilder.Services.AddSingleton<ICacheService, InMemoryCacheService>();\n```\n\n" +
                    "### ⚠️ Captive Dependency\n\n" +
                    "```csharp\n// ERRO: Singleton capturando um Scoped\n// O Scoped vive mais do que deveria!\npublic class MeuSingleton\n{\n    public MeuSingleton(AppDbContext ctx) { }  // DbContext é Scoped!\n}\n// O .NET lança exceção se ValidateScopes = true\n```\n\n" +
                    "> Regra: **Singleton** nunca deve depender de **Scoped** ou **Transient**. Um serviço de vida longa não pode capturar um de vida curta." },

            new Licao { Id = 37, ModuloId = 13, Ordem = 1, XPRecompensa = 25, Ativo = true,
                Titulo = "Separação de Camadas",
                Descricao = "Por que separar em camadas, a Regra da Dependência e os círculos da Clean Architecture",
                ConteudoTeoricoMarkdown =
                    "## Clean Architecture\n\n" +
                    "Proposta por Robert C. Martin (Uncle Bob), a Clean Architecture organiza o código em **camadas concêntricas**, " +
                    "onde as dependências sempre apontam para dentro — do código de infraestrutura para o núcleo de domínio.\n\n" +
                    "### Os círculos (de dentro para fora)\n\n" +
                    "```\n┌─────────────────────────────────────────┐\n│  Infrastructure (EF, HTTP, Email)       │\n│  ┌───────────────────────────────────┐  │\n│  │  Interface Adapters (Controllers) │  │\n│  │  ┌─────────────────────────────┐  │  │\n│  │  │  Application (Use Cases)    │  │  │\n│  │  │  ┌───────────────────────┐  │  │  │\n│  │  │  │  Domain (Entities)    │  │  │  │\n│  │  │  └───────────────────────┘  │  │  │\n│  │  └─────────────────────────────┘  │  │\n│  └───────────────────────────────────┘  │\n└─────────────────────────────────────────┘\n```\n\n" +
                    "### A Regra da Dependência\n\n" +
                    "> *Dependências de código-fonte só podem apontar para dentro.*\n\n" +
                    "- **Domain**: entidades e regras de negócio puras — **zero dependências externas**\n" +
                    "- **Application**: casos de uso, orquestra o domínio — depende só do Domain\n" +
                    "- **Infrastructure**: EF Core, HTTP, email — implementa interfaces do Domain/Application\n" +
                    "- **Presentation**: controllers, CLI, gRPC — depende da Application\n\n" +
                    "> O Domain não conhece EF Core, ASP.NET ou qualquer framework. Ele é **puro C#** e pode ser testado sem infraestrutura." },

            new Licao { Id = 38, ModuloId = 13, Ordem = 2, XPRecompensa = 25, Ativo = true,
                Titulo = "Entities, Use Cases e Interfaces",
                Descricao = "Modelando o domínio, definindo Use Cases e usando o Dependency Inversion",
                ConteudoTeoricoMarkdown =
                    "## Entities, Use Cases e Interfaces\n\n" +
                    "### Entities (Domain Layer)\n\n" +
                    "```csharp\n// Domínio puro — sem atributos de EF, sem DTOs\npublic class Pedido\n{\n    public int Id { get; private set; }\n    public decimal Total { get; private set; }\n    public StatusPedido Status { get; private set; }\n\n    public void Confirmar()\n    {\n        if (Status != StatusPedido.Pendente)\n            throw new DomainException(\"Apenas pedidos pendentes podem ser confirmados\");\n        Status = StatusPedido.Confirmado;\n    }\n}\n```\n\n" +
                    "### Use Cases (Application Layer)\n\n" +
                    "```csharp\n// Um Use Case = uma ação do sistema\npublic class ConfirmarPedidoUseCase\n{\n    private readonly IPedidoRepository _repo;  // interface do Domain\n    private readonly IEmailService _email;     // interface do Domain\n\n    public ConfirmarPedidoUseCase(\n        IPedidoRepository repo, IEmailService email)\n    { _repo = repo; _email = email; }\n\n    public async Task ExecutarAsync(int pedidoId)\n    {\n        var pedido = await _repo.ObterPorIdAsync(pedidoId)\n            ?? throw new NotFoundException(\"Pedido não encontrado\");\n\n        pedido.Confirmar();  // lógica no domínio\n        await _repo.SalvarAsync();\n        await _email.EnviarConfirmacaoAsync(pedido);\n    }\n}\n```\n\n" +
                    "### Dependency Inversion aplicado\n\n" +
                    "```csharp\n// Domain define a interface\npublic interface IPedidoRepository\n{\n    Task<Pedido?> ObterPorIdAsync(int id);\n    Task SalvarAsync();\n}\n\n// Infrastructure implementa\npublic class EfPedidoRepository : IPedidoRepository\n{\n    // usa EF Core — Domain não sabe disso\n}\n```" },

            new Licao { Id = 39, ModuloId = 13, Ordem = 3, XPRecompensa = 25, Ativo = true,
                Titulo = "Clean Architecture em APIs .NET",
                Descricao = "Estrutura de projetos, organização de pastas e montando a aplicação",
                ConteudoTeoricoMarkdown =
                    "## Aplicando Clean Architecture em uma API .NET\n\n" +
                    "### Estrutura de projetos (solution)\n\n" +
                    "```\nMeuApp.sln\n├── MeuApp.Domain/           → Entities, Interfaces, Domain Exceptions\n├── MeuApp.Application/      → Use Cases, DTOs, Validators\n├── MeuApp.Infrastructure/   → EF Core, Repositórios, Serviços externos\n└── MeuApp.API/              → Controllers, Program.cs, Middleware\n```\n\n" +
                    "**Referências entre projetos:**\n```\nAPI → Application → Domain\nInfrastructure → Domain  (implementa interfaces)\nAPI → Infrastructure     (apenas para registrar DI)\n```\n\n" +
                    "### Program.cs — montando tudo\n\n" +
                    "```csharp\n// API sabe de tudo (apenas para registrar)\nbuilder.Services.AddScoped<IPedidoRepository, EfPedidoRepository>();\nbuilder.Services.AddScoped<IEmailService, SmtpEmailService>();\nbuilder.Services.AddScoped<ConfirmarPedidoUseCase>();\n```\n\n" +
                    "### Controller delegando para Use Case\n\n" +
                    "```csharp\n[HttpPost(\"{id}/confirmar\")]\npublic async Task<IActionResult> Confirmar(\n    int id,\n    [FromServices] ConfirmarPedidoUseCase useCase)\n{\n    await useCase.ExecutarAsync(id);\n    return NoContent();\n}\n// Controller tem zero lógica de negócio\n```\n\n" +
                    "> **Clean Architecture vs camadas tradicionais**: a diferença chave é que as interfaces ficam no **Domain** (centro), não na Infrastructure. Isso inverte a dependência e protege o núcleo do negócio." },

            // ── Módulo 1: Fundamentos C# — Lições adicionais ────────────────────────
            new Licao
            {
                Id = 40, ModuloId = 1, Ordem = 4, XPRecompensa = 15, Ativo = true,
                Titulo = "Strings em Profundidade",
                Descricao = "Interpolação, verbatim, métodos úteis, StringBuilder e comparação",
                ConteudoTeoricoMarkdown =
                    "## Strings em Profundidade\n\n" +
                    "> 💡 **Cenário real**: Em uma API, você formata mensagens de log, valida e-mails, " +
                    "constrói queries dinâmicas e serializa nomes de usuário. " +
                    "Saber manipular strings com eficiência é uma das habilidades mais usadas no dia a dia.\n\n" +
                    "Em C#, `string` é **imutável** — toda operação que \"modifica\" uma string na verdade cria uma nova. " +
                    "Entender isso explica por que concatenar strings em loop é ineficiente.\n\n" +
                    "### Interpolação de strings com `$`\n\n" +
                    "```csharp\nstring nome  = \"Maria\";\nint    idade = 30;\n\n" +
                    "// Interpolação — legível e segura\nstring msg = $\"Olá, {nome}! Você tem {idade} anos.\";\n\n" +
                    "// Expressões completas dentro de {}\nstring status = $\"Maior de idade: {(idade >= 18 ? \"sim\" : \"não\")}\";\n\n" +
                    "// Formatação numérica\ndecimal preco = 1999.9m;\nstring fmt = $\"Preço: {preco:C2}\"; // R$ 1.999,90 (depende da cultura)\nstring pad = $\"{42:D6}\";           // 000042\n```\n\n" +
                    "### Strings verbatim com `@`\n\n" +
                    "Evita a necessidade de escapar barras e permite múltiplas linhas:\n\n" +
                    "```csharp\n// Sem @ — precisa escapar cada \\\nstring caminho1 = \"C:\\\\Users\\\\Maria\\\\Docs\";\n\n" +
                    "// Com @ — barras literais, sem escape\nstring caminho2 = @\"C:\\Users\\Maria\\Docs\";\n\n" +
                    "// Múltiplas linhas (inclui as quebras de linha)\nstring sql = @\"\n    SELECT *\n    FROM Produtos\n    WHERE Ativo = 1\";\n\n" +
                    "// Combinação $@ — interpolação + verbatim\nstring pasta = \"Downloads\";\nstring path  = $@\"C:\\Users\\Maria\\{pasta}\";\n```\n\n" +
                    "### Métodos essenciais de string\n\n" +
                    "```csharp\nstring s = \"  Olá, Mundo!  \";\n\n" +
                    "s.Length                         // 15 — número de caracteres\ns.Trim()                         // \"Olá, Mundo!\" — remove espaços das bordas\ns.TrimStart() / s.TrimEnd()      // remove só da esquerda / direita\ns.ToUpper()                      // \"  OLÁ, MUNDO!  \"\ns.ToLower()                      // \"  olá, mundo!  \"\ns.Contains(\"Mundo\")              // true\ns.StartsWith(\"  Olá\")           // true\ns.EndsWith(\"!  \")               // true\ns.Replace(\"Mundo\", \"C#\")        // \"  Olá, C#!  \"\ns.Split(',')                     // [\"  Olá\", \" Mundo!  \"]\ns.Substring(6, 5)                // \"Mundo\" — a partir do índice 6, 5 chars\ns.IndexOf(\"Mundo\")              // 7 — posição da primeira ocorrência\nstring.IsNullOrEmpty(s)          // false — tem conteúdo\nstring.IsNullOrWhiteSpace(\"  \") // true — só espaços\n```\n\n" +
                    "### StringBuilder — concatenação eficiente\n\n" +
                    "Concatenar strings com `+` em um loop cria N objetos intermediários no heap:\n\n" +
                    "```csharp\n// ❌ Ineficiente — cria uma nova string a cada iteração\nstring resultado = \"\";\nfor (int i = 0; i < 1000; i++)\n    resultado += i.ToString(); // 1000 alocações!\n\n" +
                    "// ✅ Eficiente — StringBuilder acumula internamente, uma alocação final\nvar sb = new System.Text.StringBuilder();\nfor (int i = 0; i < 1000; i++)\n    sb.Append(i);\nstring resultado = sb.ToString(); // converte no final\n\n" +
                    "sb.AppendLine(\"nova linha\");\nsb.Insert(0, \"início \");\nsb.Replace(\"0\", \"zero\");\n```\n\n" +
                    "> **Regra prática**: use `+` ou `$` para 2–5 concatenações. Use `StringBuilder` em loops ou quando o número de concatenações for dinâmico.\n\n" +
                    "### Comparação de strings\n\n" +
                    "```csharp\n// Igualdade simples (case-sensitive)\nbool igual = \"abc\" == \"ABC\"; // false\n\n" +
                    "// Ignorar maiúsculas/minúsculas — forma correta\nbool iguali = string.Equals(\"abc\", \"ABC\", StringComparison.OrdinalIgnoreCase); // true\n\n" +
                    "// Nunca use .ToLower() == .ToLower() para comparar — cria strings desnecessárias\n// string.Equals com StringComparison é mais legível e performático\n\n" +
                    "// Ordenação e busca em texto (respeita locale)\nint cmp = string.Compare(\"café\", \"cafe\", StringComparison.CurrentCultureIgnoreCase);\n```"
            },

            new Licao
            {
                Id = 41, ModuloId = 1, Ordem = 5, XPRecompensa = 20, Ativo = true,
                Titulo = "Tratamento de Exceções",
                Descricao = "try/catch/finally, tipos de exceção, throw e criando exceções customizadas",
                ConteudoTeoricoMarkdown =
                    "## Tratamento de Exceções em C#\n\n" +
                    "> 💡 **Cenário real**: Sua API recebe uma requisição para buscar um produto por ID. " +
                    "O banco pode estar offline, o ID pode não existir, a conexão pode cair no meio. " +
                    "Tratamento de exceções é o que separa um sistema que *trava* de um que *responde adequadamente* " +
                    "a cada situação de erro.\n\n" +
                    "### try / catch / finally\n\n" +
                    "```csharp\ntry\n{\n    // Código que pode lançar exceção\n    int resultado = int.Parse(entrada); // FormatException se entrada não for número\n    int divisao   = 10 / resultado;     // DivideByZeroException se resultado == 0\n}\ncatch (FormatException ex)\n{\n    Console.WriteLine($\"Entrada inválida: {ex.Message}\");\n}\ncatch (DivideByZeroException)\n{\n    Console.WriteLine(\"Divisão por zero!\");\n}\ncatch (Exception ex) // captura qualquer outra exceção\n{\n    Console.WriteLine($\"Erro inesperado: {ex.Message}\");\n}\nfinally\n{\n    // Executado SEMPRE — com ou sem exceção — ideal para liberar recursos\n    Console.WriteLine(\"Operação finalizada.\");\n}\n```\n\n" +
                    "> **Ordem importa**: coloque catches mais **específicos** antes dos mais **genéricos**. " +
                    "`Exception` deve ser sempre o último catch.\n\n" +
                    "### Hierarquia de exceções\n\n" +
                    "```\nSystem.Exception\n├── System.SystemException\n│   ├── ArgumentException\n│   │   └── ArgumentNullException\n│   │   └── ArgumentOutOfRangeException\n│   ├── InvalidOperationException\n│   ├── NullReferenceException\n│   ├── IndexOutOfRangeException\n│   ├── DivideByZeroException\n│   └── FormatException\n└── System.ApplicationException (raramente usada)\n```\n\n" +
                    "### throw e relançando exceções\n\n" +
                    "```csharp\n// Lançar uma nova exceção\nif (valor < 0)\n    throw new ArgumentException(\"Valor não pode ser negativo.\", nameof(valor));\n\n// ❌ ERRADO — perde o stack trace original\ncatch (Exception ex)\n{\n    throw ex; // o stack trace aponta para esta linha, não para a origem do erro\n}\n\n// ✅ CORRETO — preserva o stack trace completo\ncatch (Exception)\n{\n    throw; // relança a exceção exatamente como veio\n}\n\n// Encapsular em outra exceção (com InnerException)\ncatch (Exception ex)\n{\n    throw new InvalidOperationException(\"Falha ao processar pedido.\", ex);\n}\n```\n\n" +
                    "### Criando exceções customizadas\n\n" +
                    "```csharp\n// Crie exceções customizadas para erros de domínio específicos\npublic class ProdutoNaoEncontradoException : Exception\n{\n    public int ProdutoId { get; }\n\n    public ProdutoNaoEncontradoException(int id)\n        : base($\"Produto {id} não encontrado.\")\n    {\n        ProdutoId = id;\n    }\n}\n\n// Uso\nvar produto = await repo.ObterPorIdAsync(id)\n    ?? throw new ProdutoNaoEncontradoException(id);\n```\n\n" +
                    "### using — liberação automática de recursos\n\n" +
                    "```csharp\n// Qualquer objeto que implementa IDisposable pode usar 'using'\n// Dispose() é chamado automaticamente ao sair do bloco — mesmo com exceção\nusing var conexao = new SqlConnection(connStr);\nusing var reader  = await cmd.ExecuteReaderAsync();\n// sem finally manual para fechar conexão\n```\n\n" +
                    "### Quando NÃO usar exceções\n\n" +
                    "```csharp\n// ❌ Controle de fluxo com exceção — antipadrão\ntry { var n = int.Parse(entrada); }\ncatch { /* entrada não é número */ }\n\n// ✅ Preferir TryParse para casos esperados\nif (!int.TryParse(entrada, out int n))\n    Console.WriteLine(\"Entrada inválida\");\n```\n\n" +
                    "> **Regra**: exceções são para situações **excepcionais** — erros que não deveriam acontecer " +
                    "no fluxo normal. Para validações de entrada do usuário, prefira retornar resultados de erro em vez de lançar."
            },

            // ── Módulo 2: Orientação a Objetos — Lições adicionais ──────────────────
            new Licao
            {
                Id = 42, ModuloId = 2, Ordem = 4, XPRecompensa = 20, Ativo = true,
                Titulo = "Interfaces",
                Descricao = "Definindo contratos, implementando múltiplas interfaces, interface vs classe abstrata",
                ConteudoTeoricoMarkdown =
                    "## Interfaces em C#\n\n" +
                    "> 💡 **Cenário real**: Sua aplicação precisa enviar notificações — ora por e-mail, " +
                    "ora por SMS, ora por push notification. Em vez de acoplar o código ao canal específico, " +
                    "você define uma interface `INotificador` com o método `EnviarAsync()`. " +
                    "Qualquer implementação concreta pode ser plugada sem alterar o código que usa a interface. " +
                    "Isso é o poder dos contratos.\n\n" +
                    "Uma **interface** define um **contrato** — o que uma classe deve fazer — sem ditar como ela faz. " +
                    "É a base do princípio de inversão de dependência (o D do SOLID).\n\n" +
                    "### Declarando e implementando interfaces\n\n" +
                    "```csharp\n// Por convenção, interfaces começam com I maiúsculo\npublic interface INotificador\n{\n    Task EnviarAsync(string destinatario, string mensagem);\n    bool Disponivel { get; } // propriedades também são permitidas\n}\n\n// Implementação concreta\npublic class NotificadorEmail : INotificador\n{\n    public bool Disponivel => true;\n\n    public async Task EnviarAsync(string destinatario, string mensagem)\n    {\n        // lógica de envio de e-mail\n        await smtp.SendAsync(destinatario, mensagem);\n    }\n}\n\n// Outra implementação — mesmo contrato, comportamento diferente\npublic class NotificadorSms : INotificador\n{\n    public bool Disponivel => smsGateway.Online;\n\n    public async Task EnviarAsync(string destinatario, string mensagem)\n    {\n        await smsGateway.SendAsync(destinatario, mensagem);\n    }\n}\n```\n\n" +
                    "### Múltiplas interfaces\n\n" +
                    "Uma classe pode herdar de **uma** classe mas implementar **N** interfaces:\n\n" +
                    "```csharp\npublic interface IExportavel  { byte[] Exportar(); }\npublic interface IImprimivel  { void Imprimir(); }\npublic interface IArquivavel  { Task ArquivarAsync(); }\n\npublic class Relatorio : DocumentoBase, IExportavel, IImprimivel, IArquivavel\n{\n    public byte[] Exportar()       => PdfGenerator.Gerar(this);\n    public void   Imprimir()       => Impressora.Enviar(this);\n    public async Task ArquivarAsync() => await Storage.SalvarAsync(this);\n}\n```\n\n" +
                    "### Interface vs Classe Abstrata\n\n" +
                    "| | Interface | Classe Abstrata |\n|---|---|---|\n| Estado (campos) | ❌ Não pode ter | ✅ Pode ter |\n| Implementação | Apenas default methods (C# 8+) | ✅ Pode ter métodos concretos |\n| Herança | Uma classe implementa N interfaces | Uma classe herda de apenas 1 |\n| Quando usar | Contrato entre hierarquias diferentes | Compartilhar lógica + forçar override |\n\n" +
                    "```csharp\n// Use interface quando classes de hierarquias diferentes precisam do mesmo contrato\n// Ex: Produto, Pedido e Usuario podem ser IExportavel — sem relação de herança\n\n// Use classe abstrata quando há lógica comum + pontos de extensão\npublic abstract class RelatorioBase\n{\n    protected abstract string GerarCorpo(); // subclasses DEVEM implementar\n\n    public void Imprimir() // lógica comum para todos\n    {\n        var cabecalho = \"=== RELATÓRIO ===\";\n        Console.WriteLine(cabecalho);\n        Console.WriteLine(GerarCorpo());\n    }\n}\n```\n\n" +
                    "### IDisposable — liberando recursos\n\n" +
                    "```csharp\npublic class ConexaoBanco : IDisposable\n{\n    private readonly SqlConnection _conn;\n    private bool _disposed = false;\n\n    public ConexaoBanco(string connStr)\n        => _conn = new SqlConnection(connStr);\n\n    public void Dispose()\n    {\n        if (!_disposed)\n        {\n            _conn.Dispose();\n            _disposed = true;\n        }\n    }\n}\n\n// 'using' chama Dispose() automaticamente\nusing var db = new ConexaoBanco(connStr);\n// ... operações ...\n// Dispose() é chamado aqui, mesmo se lançar exceção\n```\n\n" +
                    "> **Regra de ouro**: dependa de abstrações (interfaces), não de implementações concretas. " +
                    "Isso torna o código testável (fácil de mockar) e flexível (fácil de trocar implementações)."
            },

            new Licao
            {
                Id = 43, ModuloId = 2, Ordem = 5, XPRecompensa = 20, Ativo = true,
                Titulo = "Records e Imutabilidade",
                Descricao = "Record types, igualdade por valor, with expressions e quando usar records",
                ConteudoTeoricoMarkdown =
                    "## Records em C#\n\n" +
                    "> 💡 **Cenário real**: Sua API retorna dados de um produto — Id, Nome, Preço. " +
                    "Esse dado é transferido pela rede, nunca deveria ser modificado após criado, " +
                    "e dois objetos com os mesmos valores devem ser considerados iguais. " +
                    "Antes dos records, você precisaria de boilerplate enorme. Com records, é uma linha.\n\n" +
                    "**Records** (C# 9+) são tipos com **igualdade por valor** e **imutabilidade** embutidos. " +
                    "Ideais para DTOs, Value Objects e dados de configuração.\n\n" +
                    "### Record posicional — a forma mais concisa\n\n" +
                    "```csharp\n// Uma única linha declara propriedades, construtor, igualdade e ToString()\npublic record Produto(int Id, string Nome, decimal Preco);\n\nvar p1 = new Produto(1, \"Teclado\", 150m);\nvar p2 = new Produto(1, \"Teclado\", 150m);\n\nConsole.WriteLine(p1 == p2);     // True — igualdade por VALOR\nConsole.WriteLine(p1.Equals(p2)); // True\nConsole.WriteLine(p1);           // Produto { Id = 1, Nome = Teclado, Preco = 150 }\n\n// Desestruturação (posicional records)\nvar (id, nome, preco) = p1;\n```\n\n" +
                    "### Imutabilidade e a expressão `with`\n\n" +
                    "As propriedades de um record posicional são `init` — só podem ser definidas na criação:\n\n" +
                    "```csharp\nvar original = new Produto(1, \"Teclado\", 150m);\n\n// ❌ Não é possível modificar diretamente\n// original.Preco = 200m; // ERRO de compilação\n\n// ✅ Cria uma CÓPIA com os valores alterados — o original não muda\nvar comDesconto = original with { Preco = 120m };\n\nConsole.WriteLine(original.Preco);    // 150 — não foi alterado\nConsole.WriteLine(comDesconto.Preco); // 120\n\n// 'with' pode alterar múltiplas propriedades\nvar novo = original with { Id = 2, Nome = \"Mouse\" };\n```\n\n" +
                    "### Record class vs record struct\n\n" +
                    "```csharp\n// record (ou record class) — tipo de referência, imutável por padrão\npublic record Endereco(string Rua, string Cidade);\n\n// record struct — tipo de VALOR (stack), melhor performance para structs pequenas\npublic record struct Coordenada(double Lat, double Lng);\n\n// Record com corpo — para adicionar métodos ou validações\npublic record Dinheiro(decimal Valor, string Moeda)\n{\n    // Validação no construtor\n    public Dinheiro : this(Valor, Moeda) // chama o gerado\n    {\n        if (Valor < 0) throw new ArgumentException(\"Valor não pode ser negativo\");\n    }\n\n    public Dinheiro Somar(Dinheiro outro)\n    {\n        if (Moeda != outro.Moeda) throw new InvalidOperationException(\"Moedas diferentes\");\n        return this with { Valor = Valor + outro.Valor };\n    }\n}\n```\n\n" +
                    "### Records vs Classes\n\n" +
                    "| | Record | Class |\n|---|---|---|\n| Igualdade | Por **valor** | Por **referência** |\n| Imutabilidade | Padrão (init) | Não — precisa configurar |\n| `ToString()` | Gerado automaticamente | Precisa sobrescrever |\n| Herança | Suportada entre records | Suportada |\n| Quando usar | DTOs, Value Objects, configs | Entidades com estado mutável |\n\n" +
                    "```csharp\n// ✅ Records são ideais para:\npublic record ProdutoDto(int Id, string Nome, decimal Preco);    // DTO de resposta da API\npublic record Dinheiro(decimal Valor, string Moeda);              // Value Object\npublic record ConfiguracaoEmail(string Smtp, int Porta);          // config imutável\n\n// ✅ Classes são ideais para:\npublic class Pedido { /* estado muda: itens adicionados, status atualizado */ }\npublic class ContaBancaria { /* saldo muda com depósitos e saques */ }\n```\n\n" +
                    "> **Regra prática**: se o objeto representa um **dado imutável** que você quer comparar pelos seus valores (não pela identidade), use record. " +
                    "Se ele representa uma **entidade com ciclo de vida e estado mutável**, use classe."
            }
        );

        // Novos Exercícios — Módulos 6–13
        modelBuilder.Entity<Exercicio>().HasData(

            new Exercicio { Id = 50, LicaoId = 16, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual classe deve ser herdada para criar um contexto do Entity Framework Core?",
                OpcoesJson = "[\"DbContext\",\"DbSet\",\"EntityBase\",\"DataContext\"]",
                RespostaCorreta = "DbContext",
                Explicacao = "DbContext é a classe base do EF Core que gerencia a conexão, o rastreamento de entidades e as operações no banco." },

            new Exercicio { Id = 51, LicaoId = 16, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Como declarar uma tabela 'Produtos' no DbContext?",
                OpcoesJson = "[\"public DbSet<Produto> Produtos => Set<Produto>();\",\"public List<Produto> Produtos;\",\"public Table<Produto> Produtos;\",\"public IQueryable<Produto> Produtos;\"]",
                RespostaCorreta = "public DbSet<Produto> Produtos => Set<Produto>();",
                Explicacao = "DbSet<T> representa a tabela no banco. A propriedade Set<T>() é a forma recomendada de expor o DbSet." },

            new Exercicio { Id = 52, LicaoId = 16, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Complete o registro do DbContext no Program.cs:\n\nbuilder.Services.___<AppDbContext>(opt => opt.UseSqlite(\"Data Source=app.db\"));",
                OpcoesJson = "[\"AddDbContext\",\"AddSingleton\",\"UseDbContext\",\"RegisterContext\"]",
                RespostaCorreta = "AddDbContext",
                Explicacao = "AddDbContext registra o AppDbContext no contêiner de DI com o lifetime Scoped (uma instância por requisição)." },

            new Exercicio { Id = 53, LicaoId = 17, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual comando cria uma nova migration no EF Core?",
                OpcoesJson = "[\"dotnet ef migrations add NomeMigration\",\"dotnet ef database update\",\"dotnet ef schema create\",\"dotnet migrations new\"]",
                RespostaCorreta = "dotnet ef migrations add NomeMigration",
                Explicacao = "O comando 'add' detecta diferenças entre o modelo C# e o banco, gerando os arquivos Up() e Down() da migration." },

            new Exercicio { Id = 54, LicaoId = 17, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que faz o método Down() em uma migration?",
                OpcoesJson = "[\"Reverte as mudanças aplicadas pelo Up()\",\"Aplica as mudanças no banco\",\"Deleta o banco de dados\",\"Lista as migrations pendentes\"]",
                RespostaCorreta = "Reverte as mudanças aplicadas pelo Up()",
                Explicacao = "Down() é o inverso de Up(). Ele desfaz a migration, permitindo rollback para o estado anterior do schema." },

            new Exercicio { Id = 55, LicaoId = 17, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para aplicar as migrations pendentes ao banco, use:\n\ndotnet ef database ___",
                OpcoesJson = "[\"update\",\"apply\",\"migrate\",\"run\"]",
                RespostaCorreta = "update",
                Explicacao = "dotnet ef database update aplica todas as migrations ainda não executadas no banco de dados." },

            new Exercicio { Id = 56, LicaoId = 18, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Como salvar um novo objeto no banco com EF Core?",
                OpcoesJson = "[\"ctx.Produtos.Add(p); await ctx.SaveChangesAsync();\",\"ctx.Produtos.Insert(p);\",\"await ctx.Produtos.SaveAsync(p);\",\"ctx.Save(p);\"]",
                RespostaCorreta = "ctx.Produtos.Add(p); await ctx.SaveChangesAsync();",
                Explicacao = "Add() rastreia o objeto como 'Added'. SaveChangesAsync() gera o INSERT SQL e persiste no banco." },

            new Exercicio { Id = 57, LicaoId = 18, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual método busca uma entidade pelo ID de forma otimizada (verifica o cache antes do banco)?",
                OpcoesJson = "[\"FindAsync(id)\",\"FirstOrDefaultAsync(x => x.Id == id)\",\"GetByIdAsync(id)\",\"SingleAsync(id)\"]",
                RespostaCorreta = "FindAsync(id)",
                Explicacao = "FindAsync verifica primeiro o Change Tracker (cache em memória). Se não encontrar, vai ao banco. É a forma mais eficiente para busca por PK." },

            new Exercicio { Id = 58, LicaoId = 18, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo tenta atualizar um produto mas não persiste:\n\nvar p = await ctx.Produtos.FindAsync(id);\np.Preco = 499.90m;\n// faltou algo aqui",
                OpcoesJson = "[\"await ctx.SaveChangesAsync()\",\"ctx.Produtos.Update(p)\",\"ctx.Commit()\",\"ctx.Produtos.Save()\"]",
                RespostaCorreta = "await ctx.SaveChangesAsync()",
                Explicacao = "O EF rastreia a mudança automaticamente, mas só persiste quando SaveChangesAsync() é chamado. Sem ele, a alteração fica apenas em memória." },

            new Exercicio { Id = 59, LicaoId = 19, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual atributo identifica um controller como uma API REST no ASP.NET Core?",
                OpcoesJson = "[\"[ApiController]\",\"[RestController]\",\"[HttpController]\",\"[RouteController]\"]",
                RespostaCorreta = "[ApiController]",
                Explicacao = "[ApiController] habilita validação automática do modelo, binding de [FromBody] por padrão e respostas de erro padronizadas." },

            new Exercicio { Id = 60, LicaoId = 19, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Para APIs REST, qual classe base é recomendada?",
                OpcoesJson = "[\"ControllerBase\",\"Controller\",\"ApiBase\",\"RestController\"]",
                RespostaCorreta = "ControllerBase",
                Explicacao = "ControllerBase não inclui suporte a Views (Razor), tornando-o mais leve e adequado para APIs." },

            new Exercicio { Id = 61, LicaoId = 19, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para mapear todos os controllers no pipeline, use:\n\napp.___();",
                OpcoesJson = "[\"MapControllers\",\"UseControllers\",\"AddControllers\",\"RegisterControllers\"]",
                RespostaCorreta = "MapControllers",
                Explicacao = "MapControllers registra as rotas de todos os controllers no pipeline de requisições do ASP.NET Core." },

            new Exercicio { Id = 62, LicaoId = 20, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual atributo mapeia um método para requisições GET com parâmetro na rota?",
                OpcoesJson = "[\"[HttpGet(\\\"{id}\\\")]\",\"[Get(\\\"{id}\\\")]\",\"[Route(\\\"GET/{id}\\\")]\",\"[HttpParam(\\\"{id}\\\")]\"]",
                RespostaCorreta = "[HttpGet(\"{id}\")]",
                Explicacao = "[HttpGet(\"{id}\")] define que o método responde a GET /api/controller/5, onde 5 é vinculado ao parâmetro id." },

            new Exercicio { Id = 63, LicaoId = 20, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual atributo indica que um parâmetro vem do corpo JSON da requisição?",
                OpcoesJson = "[\"[FromBody]\",\"[FromJson]\",\"[FromRequest]\",\"[JsonParam]\"]",
                RespostaCorreta = "[FromBody]",
                Explicacao = "[FromBody] instrui o ASP.NET a desserializar o JSON do corpo da requisição para o parâmetro. Com [ApiController], é inferido automaticamente para tipos complexos." },

            new Exercicio { Id = 64, LicaoId = 20, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para retornar 201 com o cabeçalho Location no POST:\n\nreturn ___(nameof(ObterPorId), new { id = produto.Id }, produto);",
                OpcoesJson = "[\"CreatedAtAction\",\"Created\",\"OkCreated\",\"PostResult\"]",
                RespostaCorreta = "CreatedAtAction",
                Explicacao = "CreatedAtAction retorna HTTP 201 e inclui o header Location com a URL do recurso criado, seguindo o padrão REST." },

            new Exercicio { Id = 65, LicaoId = 21, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual status code retornar quando um recurso não é encontrado?",
                OpcoesJson = "[\"404 NotFound\",\"400 BadRequest\",\"204 NoContent\",\"500 InternalServerError\"]",
                RespostaCorreta = "404 NotFound",
                Explicacao = "404 indica que o recurso solicitado não existe no servidor. Use NotFound() no ControllerBase." },

            new Exercicio { Id = 66, LicaoId = 21, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual é o status code correto para um DELETE bem-sucedido sem corpo de resposta?",
                OpcoesJson = "[\"204 NoContent\",\"200 Ok\",\"201 Created\",\"202 Accepted\"]",
                RespostaCorreta = "204 NoContent",
                Explicacao = "204 indica sucesso sem corpo de resposta. É o padrão para DELETE e PUT quando não há dados a retornar." },

            new Exercicio { Id = 67, LicaoId = 21, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O status 400 BadRequest deve ser retornado quando:",
                OpcoesJson = "[\"Os dados enviados pelo cliente são inválidos\",\"O recurso não foi encontrado\",\"O servidor encontrou um erro interno\",\"O cliente não está autenticado\"]",
                RespostaCorreta = "Os dados enviados pelo cliente são inválidos",
                Explicacao = "400 é o erro do cliente — dados malformados, campos obrigatórios ausentes ou validações negadas. 404 = não encontrado, 500 = erro do servidor, 401 = não autenticado." },

            new Exercicio { Id = 68, LicaoId = 22, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual atributo marca uma propriedade como obrigatória na validação?",
                OpcoesJson = "[\"[Required]\",\"[NotNull]\",\"[Mandatory]\",\"[NotEmpty]\"]",
                RespostaCorreta = "[Required]",
                Explicacao = "[Required] indica que o campo não pode ser nulo nem vazio. Com [ApiController], o ASP.NET retorna 400 automaticamente se a validação falhar." },

            new Exercicio { Id = 69, LicaoId = 22, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Para validar que um número está entre 1 e 100, use:",
                OpcoesJson = "[\"[Range(1, 100)]\",\"[Between(1, 100)]\",\"[Min(1)][Max(100)]\",\"[Limit(1, 100)]\"]",
                RespostaCorreta = "[Range(1, 100)]",
                Explicacao = "[Range(min, max)] valida que o valor está dentro do intervalo especificado, inclusive nos extremos." },

            new Exercicio { Id = 70, LicaoId = 22, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para limitar o tamanho de uma string entre 3 e 50 caracteres:\n\n[___(50, MinimumLength = 3)]",
                OpcoesJson = "[\"StringLength\",\"MaxLength\",\"LengthRange\",\"TextSize\"]",
                RespostaCorreta = "StringLength",
                Explicacao = "[StringLength(max, MinimumLength = min)] valida tanto o máximo quanto o mínimo de caracteres em uma string." },

            new Exercicio { Id = 71, LicaoId = 23, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que não expor a entidade de domínio diretamente na API?",
                OpcoesJson = "[\"Vaza dados internos e cria acoplamento entre camadas\",\"Entidades não podem ser serializadas em JSON\",\"O EF Core não permite isso\",\"DTOs são mais rápidos de serializar\"]",
                RespostaCorreta = "Vaza dados internos e cria acoplamento entre camadas",
                Explicacao = "Expor a entidade diretamente revela campos como SenhaHash, audit fields e relacionamentos internos. DTOs definem um contrato explícito e estável para a API." },

            new Exercicio { Id = 72, LicaoId = 23, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que é um DTO de Request vs um DTO de Response?",
                OpcoesJson = "[\"Request = entrada do cliente; Response = saída da API\",\"Request = saída da API; Response = entrada do cliente\",\"São a mesma coisa com nomes diferentes\",\"Request é usado no GET, Response no POST\"]",
                RespostaCorreta = "Request = entrada do cliente; Response = saída da API",
                Explicacao = "Request DTOs definem o que o cliente envia (POST/PUT body). Response DTOs definem o que a API retorna. Mantê-los separados permite evoluir cada um independentemente." },

            new Exercicio { Id = 73, LicaoId = 23, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo vaza a senha dos usuários:\n\n[HttpGet]\npublic async Task<IActionResult> Listar()\n    => Ok(await ctx.Usuarios.ToListAsync());",
                OpcoesJson = "[\"Usar .Select() para projetar em um DTO sem SenhaHash\",\"Adicionar [JsonIgnore] na entidade\",\"Usar .AsNoTracking()\",\"Retornar NoContent()\"]",
                RespostaCorreta = "Usar .Select() para projetar em um DTO sem SenhaHash",
                Explicacao = "Projetar com .Select() em um DTO de Response garante que somente os campos desejados sejam retornados, eliminando dados sensíveis." },

            new Exercicio { Id = 74, LicaoId = 24, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual classe deve ser herdada para criar um validator com FluentValidation?",
                OpcoesJson = "[\"AbstractValidator<T>\",\"BaseValidator<T>\",\"FluentValidator<T>\",\"ModelValidator<T>\"]",
                RespostaCorreta = "AbstractValidator<T>",
                Explicacao = "AbstractValidator<T> é a classe base do FluentValidation. As regras são definidas no construtor usando RuleFor()." },

            new Exercicio { Id = 75, LicaoId = 24, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para validar que o campo Nome não é vazio com FluentValidation:\n\nRuleFor(x => x.Nome).___()",
                OpcoesJson = "[\"NotEmpty\",\"Required\",\"NotNull\",\"IsRequired\"]",
                RespostaCorreta = "NotEmpty",
                Explicacao = "NotEmpty() verifica que a string não é nula, vazia ou só espaços em branco. Equivale ao [Required] das Data Annotations, mas permite encadeamento." },

            new Exercicio { Id = 76, LicaoId = 24, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Quando preferir FluentValidation em vez de Data Annotations?",
                OpcoesJson = "[\"Quando precisar de lógica condicional ou reuso entre validators\",\"Quando o modelo tiver menos de 3 campos\",\"Quando a entidade for usada com EF Core\",\"FluentValidation sempre substitui Data Annotations\"]",
                RespostaCorreta = "Quando precisar de lógica condicional ou reuso entre validators",
                Explicacao = "Use Annotations para regras simples e diretas. FluentValidation brilha em validações condicionais (When), mensagens dinâmicas e validators reutilizáveis entre classes." },

            new Exercicio { Id = 77, LicaoId = 25, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Quais são as 3 partes de um JWT?",
                OpcoesJson = "[\"Header, Payload, Signature\",\"Header, Body, Footer\",\"Token, Claims, Hash\",\"Key, Value, Signature\"]",
                RespostaCorreta = "Header, Payload, Signature",
                Explicacao = "Header contém o algoritmo, Payload contém as claims (dados) e Signature garante a integridade do token." },

            new Exercicio { Id = 78, LicaoId = 25, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que JWT é considerado stateless?",
                OpcoesJson = "[\"O servidor não precisa armazenar sessões — toda informação está no token\",\"O token nunca expira\",\"Não usa banco de dados\",\"O cliente não armazena o token\"]",
                RespostaCorreta = "O servidor não precisa armazenar sessões — toda informação está no token",
                Explicacao = "Com JWT, o servidor valida a assinatura e lê as claims diretamente do token. Nenhum estado é armazenado server-side, facilitando escalabilidade horizontal." },

            new Exercicio { Id = 79, LicaoId = 25, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "É seguro armazenar senhas no payload de um JWT?",
                OpcoesJson = "[\"Não — o payload é apenas Base64 e pode ser decodificado\",\"Sim — a assinatura criptografa o conteúdo\",\"Sim — desde que use HTTPS\",\"Depende do algoritmo usado\"]",
                RespostaCorreta = "Não — o payload é apenas Base64 e pode ser decodificado",
                Explicacao = "A assinatura do JWT garante que o token não foi adulterado, mas não cifra o conteúdo. Qualquer pessoa com o token pode decodificar o payload." },

            new Exercicio { Id = 80, LicaoId = 26, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual a ordem correta dos middlewares de auth no pipeline?",
                OpcoesJson = "[\"UseAuthentication() antes de UseAuthorization()\",\"UseAuthorization() antes de UseAuthentication()\",\"A ordem não importa\",\"Apenas UseAuthorization() é necessário\"]",
                RespostaCorreta = "UseAuthentication() antes de UseAuthorization()",
                Explicacao = "UseAuthentication() identifica o usuário a partir do token. UseAuthorization() verifica as permissões. A autenticação deve ocorrer primeiro." },

            new Exercicio { Id = 81, LicaoId = 26, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para proteger uma rota exigindo autenticação, use o atributo:\n\n[___]",
                OpcoesJson = "[\"Authorize\",\"Protected\",\"RequireAuth\",\"Authenticated\"]",
                RespostaCorreta = "Authorize",
                Explicacao = "[Authorize] indica que somente usuários autenticados podem acessar o endpoint. Retorna 401 para não autenticados e 403 para autenticados sem permissão." },

            new Exercicio { Id = 82, LicaoId = 26, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual classe é usada para criar as credenciais de assinatura do JWT?",
                OpcoesJson = "[\"SigningCredentials\",\"JwtCredentials\",\"TokenCredentials\",\"SecurityCredentials\"]",
                RespostaCorreta = "SigningCredentials",
                Explicacao = "SigningCredentials combina a chave secreta com o algoritmo (ex: HmacSha256) para assinar o token." },

            new Exercicio { Id = 83, LicaoId = 27, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Para restringir um endpoint apenas a usuários com role 'Admin':",
                OpcoesJson = "[\"[Authorize(Roles = \\\"Admin\\\")]\",\"[RequireRole(\\\"Admin\\\")]\",\"[AdminOnly]\",\"[Authorize][Role(\\\"Admin\\\")]\"]",
                RespostaCorreta = "[Authorize(Roles = \"Admin\")]",
                Explicacao = "[Authorize(Roles = \"Admin\")] verifica se o ClaimsPrincipal possui a claim de role 'Admin'. Retorna 403 Forbidden se não tiver." },

            new Exercicio { Id = 84, LicaoId = 27, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para tornar um endpoint público em um controller com [Authorize] global:\n\n[___]\n[HttpGet(\"publico\")]\npublic IActionResult Publico() => Ok();",
                OpcoesJson = "[\"AllowAnonymous\",\"Public\",\"NoAuth\",\"SkipAuthorization\"]",
                RespostaCorreta = "AllowAnonymous",
                Explicacao = "[AllowAnonymous] sobrescreve qualquer [Authorize] no controller ou policy global, permitindo acesso sem autenticação." },

            new Exercicio { Id = 85, LicaoId = 27, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Como acessar o ID do usuário autenticado dentro de uma action?",
                OpcoesJson = "[\"User.FindFirstValue(ClaimTypes.NameIdentifier)\",\"Request.Headers[\\\"UserId\\\"]\",\"HttpContext.User.Id\",\"this.UserId\"]",
                RespostaCorreta = "User.FindFirstValue(ClaimTypes.NameIdentifier)",
                Explicacao = "A propriedade User do ControllerBase é um ClaimsPrincipal. FindFirstValue busca o valor da claim NameIdentifier, que por convenção armazena o ID do usuário." },

            new Exercicio { Id = 86, LicaoId = 28, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual a vantagem de centralizar o tratamento de erros?",
                OpcoesJson = "[\"Elimina código duplicado e garante respostas consistentes\",\"Melhora a performance da API\",\"Permite usar menos controllers\",\"É obrigatório pelo ASP.NET\"]",
                RespostaCorreta = "Elimina código duplicado e garante respostas consistentes",
                Explicacao = "Com tratamento centralizado, todos os erros não tratados recebem a mesma resposta padronizada sem precisar de try/catch em cada action." },

            new Exercicio { Id = 87, LicaoId = 28, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que não expor stack traces em produção?",
                OpcoesJson = "[\"Revela detalhes internos que podem ser explorados por atacantes\",\"Stack traces aumentam o tamanho da resposta\",\"O cliente não consegue interpretar\",\"O ASP.NET bloqueia automaticamente\"]",
                RespostaCorreta = "Revela detalhes internos que podem ser explorados por atacantes",
                Explicacao = "Stack traces expõem caminhos de arquivo, nomes de classes, versões de bibliotecas e lógica interna — informações valiosas para ataques." },

            new Exercicio { Id = 88, LicaoId = 28, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para verificar se a aplicação está em ambiente de desenvolvimento:\n\nif (app.Environment.___()){...}",
                OpcoesJson = "[\"IsDevelopment\",\"IsDebug\",\"IsDev\",\"IsLocal\"]",
                RespostaCorreta = "IsDevelopment",
                Explicacao = "IsDevelopment() retorna true quando ASPNETCORE_ENVIRONMENT é 'Development'. Use para habilitar detalhes de erro e outros recursos apenas no dev." },

            new Exercicio { Id = 89, LicaoId = 29, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que o parâmetro 'next' representa em um middleware?",
                OpcoesJson = "[\"O próximo middleware no pipeline\",\"A próxima requisição HTTP\",\"O controller a ser executado\",\"O retorno da action\"]",
                RespostaCorreta = "O próximo middleware no pipeline",
                Explicacao = "Chamar 'await next(context)' passa o controle para o próximo middleware. Não chamar 'next' encerra o pipeline e retorna a resposta atual." },

            new Exercicio { Id = 90, LicaoId = 29, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual interface implementar para criar um middleware tipado no .NET?",
                OpcoesJson = "[\"IMiddleware\",\"IRequestHandler\",\"IHttpMiddleware\",\"IPipelineStep\"]",
                RespostaCorreta = "IMiddleware",
                Explicacao = "IMiddleware define o contrato com um único método InvokeAsync(HttpContext, RequestDelegate). É a forma recomendada para middlewares com dependências injetadas." },

            new Exercicio { Id = 91, LicaoId = 29, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "A ordem de registro dos middlewares importa?",
                OpcoesJson = "[\"Sim — são executados na ordem em que são registrados\",\"Não — o ASP.NET otimiza a ordem\",\"Apenas para middlewares de autenticação\",\"Apenas em produção\"]",
                RespostaCorreta = "Sim — são executados na ordem em que são registrados",
                Explicacao = "O pipeline é uma cadeia sequencial. UseAuthentication() antes de UseAuthorization() é obrigatório, por exemplo. A ordem errada pode causar comportamentos inesperados." },

            new Exercicio { Id = 92, LicaoId = 30, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual nível de log usar para erros que causaram falha em uma operação?",
                OpcoesJson = "[\"LogError\",\"LogWarning\",\"LogCritical\",\"LogDebug\"]",
                RespostaCorreta = "LogError",
                Explicacao = "LogError é para erros que causaram falha na operação atual mas não derrubaram a aplicação. LogCritical é para falhas graves que podem parar o sistema." },

            new Exercicio { Id = 93, LicaoId = 30, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que preferir logging estruturado em vez de interpolação de string?",
                OpcoesJson = "[\"Permite busca e análise nos sistemas de log como Seq ou Elasticsearch\",\"É mais rápido de escrever\",\"Consome menos memória\",\"É obrigatório pelo ILogger\"]",
                RespostaCorreta = "Permite busca e análise nos sistemas de log como Seq ou Elasticsearch",
                Explicacao = "Com logging estruturado, os parâmetros são indexados como campos separados. Você pode buscar 'UserId = 42' em vez de fazer full-text search em strings." },

            new Exercicio { Id = 94, LicaoId = 30, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para injetar o logger no controller:\n\npublic ProdutosController(___<ProdutosController> logger) => _logger = logger;",
                OpcoesJson = "[\"ILogger\",\"Logger\",\"ILog\",\"LogService\"]",
                RespostaCorreta = "ILogger",
                Explicacao = "ILogger<T> é a interface de logging do .NET. O parâmetro de tipo T define a categoria do log (geralmente o nome da classe que está logando)." },

            new Exercicio { Id = 95, LicaoId = 31, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual atributo marca um método como teste unitário no xUnit?",
                OpcoesJson = "[\"[Fact]\",\"[Test]\",\"[TestMethod]\",\"[UnitTest]\"]",
                RespostaCorreta = "[Fact]",
                Explicacao = "[Fact] é o atributo do xUnit para testes sem parâmetros. NUnit usa [Test] e MSTest usa [TestMethod], mas xUnit usa [Fact]." },

            new Exercicio { Id = 96, LicaoId = 31, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que representa a fase 'Arrange' no padrão AAA?",
                OpcoesJson = "[\"Prepara os dados e dependências para o teste\",\"Executa o código sendo testado\",\"Verifica o resultado esperado\",\"Limpa os recursos após o teste\"]",
                RespostaCorreta = "Prepara os dados e dependências para o teste",
                Explicacao = "Arrange = preparar (instâncias, mocks, dados). Act = executar (chamar o método). Assert = verificar (o resultado é o esperado)." },

            new Exercicio { Id = 97, LicaoId = 31, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual atributo usar para um teste com múltiplos conjuntos de dados?",
                OpcoesJson = "[\"[Theory] com [InlineData]\",\"[Fact] com [InlineData]\",\"[ParameterizedTest]\",\"[DataDriven]\"]",
                RespostaCorreta = "[Theory] com [InlineData]",
                Explicacao = "[Theory] indica que o teste aceita parâmetros. [InlineData] fornece os valores. O xUnit executa o teste uma vez para cada [InlineData]." },

            new Exercicio { Id = 98, LicaoId = 32, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que usar mocks em testes unitários?",
                OpcoesJson = "[\"Para isolar a unidade testada de dependências externas\",\"Para tornar os testes mais rápidos de escrever\",\"Porque o banco de dados não funciona em testes\",\"Para testar a integração entre componentes\"]",
                RespostaCorreta = "Para isolar a unidade testada de dependências externas",
                Explicacao = "Mocks substituem banco, email, APIs externas por objetos controlados. Isso garante que o teste falha só por causa do código testado, não de dependências." },

            new Exercicio { Id = 99, LicaoId = 32, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para configurar um mock para retornar um valor async:\n\nmockRepo.___(r => r.ObterAsync(1)).___(new Produto());",
                OpcoesJson = "[\"Setup / ReturnsAsync\",\"Configure / Returns\",\"Mock / ReturnAsync\",\"When / ThenReturn\"]",
                RespostaCorreta = "Setup / ReturnsAsync",
                Explicacao = "Setup() configura qual método interceptar. ReturnsAsync() define o valor retornado em operações async. Para síncronos, use Returns()." },

            new Exercicio { Id = 100, LicaoId = 32, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Como verificar que um método do mock foi chamado exatamente uma vez?",
                OpcoesJson = "[\"mock.Verify(x => x.Metodo(), Times.Once())\",\"mock.Assert(x => x.Metodo(), 1)\",\"mock.Check(x => x.Metodo())\",\"Assert.Called(mock, 1)\"]",
                RespostaCorreta = "mock.Verify(x => x.Metodo(), Times.Once())",
                Explicacao = "Verify() é usado após o Act para confirmar que determinada interação ocorreu. Times.Once(), Times.Never(), Times.Exactly(n) controlam a contagem esperada." },

            new Exercicio { Id = 101, LicaoId = 33, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual a ordem correta do ciclo TDD?",
                OpcoesJson = "[\"Red → Green → Refactor\",\"Green → Red → Refactor\",\"Refactor → Red → Green\",\"Green → Refactor → Red\"]",
                RespostaCorreta = "Red → Green → Refactor",
                Explicacao = "Red = teste falha (código não existe). Green = mínimo para passar. Refactor = melhora sem quebrar. Repetir para cada nova funcionalidade." },

            new Exercicio { Id = 102, LicaoId = 33, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual o principal benefício de TDD além dos testes em si?",
                OpcoesJson = "[\"Força pensar na interface antes da implementação, melhorando o design\",\"Elimina a necessidade de documentação\",\"Garante zero bugs no código\",\"Dobra a velocidade de desenvolvimento\"]",
                RespostaCorreta = "Força pensar na interface antes da implementação, melhorando o design",
                Explicacao = "Ao escrever o teste primeiro, você define como quer usar o código. Isso naturalmente leva a APIs mais simples, classes menores e melhor separação de responsabilidades." },

            new Exercicio { Id = 103, LicaoId = 33, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Na fase Green do TDD, qual deve ser o objetivo?",
                OpcoesJson = "[\"Escrever o mínimo de código para o teste passar\",\"Escrever o código mais elegante possível\",\"Refatorar o código existente\",\"Adicionar mais casos de teste\"]",
                RespostaCorreta = "Escrever o mínimo de código para o teste passar",
                Explicacao = "Na fase Green, qualidade não importa — só fazer o teste passar. A refatoração vem depois, protegida pelos testes. Isso evita over-engineering prematuro." },

            new Exercicio { Id = 104, LicaoId = 34, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que é Injeção de Dependência?",
                OpcoesJson = "[\"Um padrão onde as dependências são recebidas de fora, não criadas internamente\",\"Um método para injetar código em tempo de execução\",\"Uma forma de herança avançada\",\"Um padrão para criar singletons\"]",
                RespostaCorreta = "Um padrão onde as dependências são recebidas de fora, não criadas internamente",
                Explicacao = "Com DI, a classe declara o que precisa (interfaces) e o contêiner fornece as implementações. Isso desacopla e torna o código testável." },

            new Exercicio { Id = 105, LicaoId = 34, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual o principal benefício de depender de interfaces em vez de classes concretas?",
                OpcoesJson = "[\"Permite trocar implementações e facilita testes com mocks\",\"Interfaces são mais rápidas que classes\",\"Reduz o consumo de memória\",\"É obrigatório pelo compilador\"]",
                RespostaCorreta = "Permite trocar implementações e facilita testes com mocks",
                Explicacao = "Dependendo de IProdutoRepository em vez de ProdutoRepository, você pode injetar um mock em testes sem tocar no banco, e trocar para PostgreSqlRepository sem mudar o service." },

            new Exercicio { Id = 106, LicaoId = 34, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo cria a dependência internamente (acoplamento):\n\npublic class PedidoService\n{\n    private readonly EmailService _email = new EmailService();\n}",
                OpcoesJson = "[\"Receber IEmailService pelo construtor em vez de instanciar\",\"Usar 'static' no EmailService\",\"Remover o campo _email\",\"Usar 'new' com interface\"]",
                RespostaCorreta = "Receber IEmailService pelo construtor em vez de instanciar",
                Explicacao = "Instanciar com 'new' cria acoplamento forte. Receber IEmailService pelo construtor inverte o controle — quem cria é o contêiner, não a classe." },

            new Exercicio { Id = 107, LicaoId = 35, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Como registrar IProdutoRepository com implementação ProdutoRepository?",
                OpcoesJson = "[\"builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>()\",\"builder.Services.Register<IProdutoRepository>(new ProdutoRepository())\",\"services.Inject<IProdutoRepository, ProdutoRepository>()\",\"builder.AddRepository<IProdutoRepository>()\"]",
                RespostaCorreta = "builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>()",
                Explicacao = "AddScoped registra a interface com sua implementação com lifetime Scoped. O primeiro tipo genérico é a interface, o segundo é a implementação concreta." },

            new Exercicio { Id = 108, LicaoId = 35, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Onde os serviços devem ser registrados no ASP.NET Core?",
                OpcoesJson = "[\"Em Program.cs, antes de builder.Build()\",\"Em cada controller que os usa\",\"No construtor do DbContext\",\"Em appsettings.json\"]",
                RespostaCorreta = "Em Program.cs, antes de builder.Build()",
                Explicacao = "O registro acontece na fase de configuração, antes de builder.Build(). Após o Build(), o contêiner é construído e não aceita mais registros." },

            new Exercicio { Id = 109, LicaoId = 35, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para obter um serviço registrado dentro de um método (fora do construtor):\n\nvar svc = scope.ServiceProvider.___<IEmailService>();",
                OpcoesJson = "[\"GetRequiredService\",\"Resolve\",\"Get\",\"Inject\"]",
                RespostaCorreta = "GetRequiredService",
                Explicacao = "GetRequiredService<T>() lança InvalidOperationException se o serviço não estiver registrado. Use GetService<T>() se o serviço for opcional (retorna null)." },

            new Exercicio { Id = 110, LicaoId = 36, Ordem = 1, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual lifetime usar para o DbContext do Entity Framework?",
                OpcoesJson = "[\"Scoped\",\"Singleton\",\"Transient\",\"Qualquer um\"]",
                RespostaCorreta = "Scoped",
                Explicacao = "DbContext deve ser Scoped — uma instância por requisição. Singleton causaria problemas de concorrência e Transient causaria múltiplos change trackers na mesma requisição." },

            new Exercicio { Id = 111, LicaoId = 36, Ordem = 2, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que é 'Captive Dependency'?",
                OpcoesJson = "[\"Singleton capturando um Scoped/Transient, fazendo o serviço viver mais do que deveria\",\"Uma dependência circular entre dois serviços\",\"Um serviço que não consegue ser resolvido\",\"Uma dependência registrada mais de uma vez\"]",
                RespostaCorreta = "Singleton capturando um Scoped/Transient, fazendo o serviço viver mais do que deveria",
                Explicacao = "Se um Singleton recebe um Scoped no construtor, ele captura essa instância para sempre — o Scoped passa a viver enquanto o Singleton viver, quebrando seu lifecycle." },

            new Exercicio { Id = 112, LicaoId = 36, Ordem = 3, XPRecompensa = 12,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual lifetime usar para um serviço de cache em memória que deve persistir entre requisições?",
                OpcoesJson = "[\"Singleton\",\"Scoped\",\"Transient\",\"Static\"]",
                RespostaCorreta = "Singleton",
                Explicacao = "Cache em memória deve ser Singleton — a mesma instância para toda a aplicação. Scoped criaria um cache novo por requisição, perdendo o valor entre calls." },

            new Exercicio { Id = 113, LicaoId = 37, Ordem = 1, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Na Clean Architecture, qual camada pode depender de frameworks externos como EF Core?",
                OpcoesJson = "[\"Infrastructure\",\"Domain\",\"Application\",\"Use Cases\"]",
                RespostaCorreta = "Infrastructure",
                Explicacao = "A camada de Infrastructure implementa interfaces do Domain usando frameworks concretos (EF Core, SMTP, etc.). O Domain e Application permanecem puros." },

            new Exercicio { Id = 114, LicaoId = 37, Ordem = 2, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que garante a 'Regra da Dependência' na Clean Architecture?",
                OpcoesJson = "[\"Dependências de código sempre apontam para dentro (em direção ao domínio)\",\"Cada camada tem exatamente 3 classes\",\"Controllers não podem ter lógica\",\"Interfaces só existem na camada de Application\"]",
                RespostaCorreta = "Dependências de código sempre apontam para dentro (em direção ao domínio)",
                Explicacao = "Infrastructure conhece Domain, mas Domain não conhece Infrastructure. Isso garante que o núcleo do negócio é independente de frameworks e pode ser testado isoladamente." },

            new Exercicio { Id = 115, LicaoId = 37, Ordem = 3, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que o Domain não deve ter dependências de frameworks externos?",
                OpcoesJson = "[\"Para poder ser testado e evoluído independentemente de infraestrutura\",\"Porque frameworks são muito pesados\",\"O compilador não permite\",\"Para reduzir o número de arquivos\"]",
                RespostaCorreta = "Para poder ser testado e evoluído independentemente de infraestrutura",
                Explicacao = "Um Domain puro pode ser testado com simples testes unitários sem banco, sem HTTP e sem configuração. Também pode trocar de ORM ou framework sem alterar a lógica de negócio." },

            new Exercicio { Id = 116, LicaoId = 38, Ordem = 1, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que é um Use Case na Clean Architecture?",
                OpcoesJson = "[\"Uma classe que orquestra uma ação específica do sistema\",\"Um método em um controller\",\"Uma entidade de domínio\",\"Um repositório\"]",
                RespostaCorreta = "Uma classe que orquestra uma ação específica do sistema",
                Explicacao = "Use Cases (ou Interactors) representam uma ação do sistema: ConfirmarPedido, CriarUsuario, GerarRelatorio. Cada Use Case tem uma única responsabilidade." },

            new Exercicio { Id = 117, LicaoId = 38, Ordem = 2, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Onde devem ficar as interfaces de repositório (ex: IPedidoRepository)?",
                OpcoesJson = "[\"Domain — o centro da arquitetura\",\"Infrastructure — junto com a implementação\",\"Application — junto com os Use Cases\",\"API — junto com os Controllers\"]",
                RespostaCorreta = "Domain — o centro da arquitetura",
                Explicacao = "Interfaces no Domain aplicam o Dependency Inversion: Infrastructure depende do Domain para implementar, não o contrário. O Domain define o contrato, Infrastructure cumpre." },

            new Exercicio { Id = 118, LicaoId = 38, Ordem = 3, XPRecompensa = 15,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "A entity abaixo tem lógica de negócio vazando para fora:\n\npublic class Pedido\n{\n    public StatusPedido Status { get; set; }  // set público!\n}",
                OpcoesJson = "[\"Tornar o set privado e criar método Confirmar() com validação\",\"Remover o Status\",\"Usar [Required] no Status\",\"Mover o Status para um DTO\"]",
                RespostaCorreta = "Tornar o set privado e criar método Confirmar() com validação",
                Explicacao = "Entities devem proteger seus invariantes com setters privados e métodos de domínio. Status = Confirmado diretamente viola o encapsulamento e permite estados inválidos." },

            new Exercicio { Id = 119, LicaoId = 39, Ordem = 1, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Em uma solução Clean Architecture, qual projeto deve referenciar todos os outros?",
                OpcoesJson = "[\"API/Presentation — apenas para compor o DI\",\"Domain — por ser o centro\",\"Infrastructure — por ter as implementações\",\"Application — por ter os Use Cases\"]",
                RespostaCorreta = "API/Presentation — apenas para compor o DI",
                Explicacao = "A API conhece todos os projetos somente para registrar as dependências no contêiner. Mas essa dependência é apenas de configuração — a lógica flui pelo Application até o Domain." },

            new Exercicio { Id = 120, LicaoId = 39, Ordem = 2, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual o papel do Controller numa Clean Architecture bem aplicada?",
                OpcoesJson = "[\"Receber a requisição e delegar para o Use Case — zero lógica de negócio\",\"Conter toda a lógica da feature\",\"Acessar o banco diretamente\",\"Instanciar entidades de domínio\"]",
                RespostaCorreta = "Receber a requisição e delegar para o Use Case — zero lógica de negócio",
                Explicacao = "Controllers são adaptadores: traduzem HTTP para chamadas da Application. A lógica fica nos Use Cases. Um controller gordo é sinal de violação da Clean Architecture." },

            new Exercicio { Id = 121, LicaoId = 39, Ordem = 3, XPRecompensa = 15,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual a diferença principal entre Clean Architecture e arquitetura em camadas tradicional?",
                OpcoesJson = "[\"As interfaces ficam no Domain (centro), invertendo a dependência em vez de apontar para Infrastructure\",\"Clean Architecture tem mais camadas\",\"Clean Architecture não usa banco de dados\",\"Não há diferença — são a mesma coisa\"]",
                RespostaCorreta = "As interfaces ficam no Domain (centro), invertendo a dependência em vez de apontar para Infrastructure",
                Explicacao = "Na arquitetura tradicional, o domínio depende da infraestrutura (Domain → Infrastructure). Na Clean, é o contrário: Infrastructure implementa interfaces do Domain, protegendo o núcleo." },

            // ── Exercícios adicionais — Lição 1: Variáveis e Tipos ──────────────────
            new Exercicio { Id = 122, LicaoId = 1, Ordem = 4, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual tipo de dado deve ser usado para armazenar valores monetários em C#?",
                OpcoesJson = "[\"decimal\",\"double\",\"float\",\"long\"]",
                RespostaCorreta = "decimal",
                Explicacao = "'decimal' usa aritmética de base 10, eliminando erros de arredondamento binário. 1.10 + 2.20 com double dá 3.3000000000000003; com decimal, 3.30 exato. Sempre use decimal para dinheiro.",
                DicaTexto = "Qual tipo foi criado especificamente para evitar erros de arredondamento em valores financeiros?" },

            new Exercicio { Id = 123, LicaoId = 1, Ordem = 5, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Em C#, uma variável declarada com 'var' pode mudar de tipo após a declaração.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "'var' é inferência de tipo em TEMPO DE COMPILAÇÃO — o tipo é fixado pelo valor inicial e não pode mudar. 'var x = 5;' cria um int permanente. Só 'dynamic' adia a verificação para runtime.",
                DicaTexto = "var é resolvido em tempo de compilação ou de execução?" },

            new Exercicio { Id = 124, LicaoId = 1, Ordem = 6, XPRecompensa = 5,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para converter uma string para int sem lançar exceção em caso de falha, usa-se int.____(entrada, out int valor)",
                OpcoesJson = "[]",
                RespostaCorreta = "TryParse",
                Explicacao = "int.TryParse() retorna bool e nunca lança exceção — ideal para validar entrada do usuário. int.Parse() lança FormatException se a string não for um número válido.",
                DicaTexto = "O método começa com 'Try' e retorna bool..." },

            new Exercicio { Id = 125, LicaoId = 1, Ordem = 7, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual operador atribui um valor padrão quando a variável é null?",
                OpcoesJson = "[\"??\",\"?.\",\"!.\",\"&&\"]",
                RespostaCorreta = "??",
                Explicacao = "O operador ?? é o 'null-coalescing operator': 'valor ?? padrão' retorna 'valor' se não for null, ou 'padrão' caso contrário. Ex: int resultado = idade ?? 0;",
                DicaTexto = "Esse operador é formado por dois pontos de interrogação..." },

            new Exercicio { Id = 126, LicaoId = 1, Ordem = 8, XPRecompensa = 5,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo tem um erro. Corrija-o:\ndecimal preco = 19.99;",
                OpcoesJson = "[\"decimal preco = 19.99m;\",\"decimal preco = (decimal)19.99;\",\"double preco = 19.99;\",\"float preco = 19.99f;\"]",
                RespostaCorreta = "decimal preco = 19.99m;",
                Explicacao = "Literais de ponto flutuante sem sufixo são 'double' por padrão. Para atribuir a uma variável decimal, use o sufixo 'm': 19.99m. Sem o sufixo, o compilador tenta converter double→decimal implicitamente, o que não é permitido.",
                DicaTexto = "Literais decimais precisam de um sufixo específico..." },

            new Exercicio { Id = 127, LicaoId = 1, Ordem = 9, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual é o range (intervalo) de valores do tipo 'byte' em C#?",
                OpcoesJson = "[\"0 a 255\",\"-128 a 127\",\"0 a 65535\",\"-32768 a 32767\"]",
                RespostaCorreta = "0 a 255",
                Explicacao = "'byte' é um inteiro sem sinal de 8 bits: armazena de 0 a 255 (2⁸ - 1). Para valores negativos, use 'sbyte' (-128 a 127). 'short' vai de -32.768 a 32.767 (16 bits).",
                DicaTexto = "byte tem 8 bits e é sem sinal (unsigned)..." },

            new Exercicio { Id = 128, LicaoId = 1, Ordem = 10, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "O tipo int? (nullable int) pode armazenar o valor null.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Adicionando '?' ao tipo de valor, tornamos ele nullable. 'int?' é equivalente a 'Nullable<int>' e pode ser null, 0, 1, ou qualquer int. Use .HasValue para verificar e .Value para acessar.",
                DicaTexto = "O '?' após o tipo transforma um value type em nullable..." },

            // ── Exercícios adicionais — Lição 2: Controle de Fluxo ──────────────────
            new Exercicio { Id = 129, LicaoId = 2, Ordem = 4, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que a instrução 'break' faz dentro de um loop?",
                OpcoesJson = "[\"Encerra o loop imediatamente\",\"Pula para a próxima iteração\",\"Reinicia o loop do início\",\"Não faz nada\"]",
                RespostaCorreta = "Encerra o loop imediatamente",
                Explicacao = "'break' encerra o loop completamente — nenhuma iteração adicional ocorre. 'continue' pula apenas a iteração atual e vai para a próxima. Conhecer a diferença evita bugs sutis em loops.",
                DicaTexto = "Qual instrução 'quebra' o loop e qual apenas 'continua' para a próxima?" },

            new Exercicio { Id = 130, LicaoId = 2, Ordem = 5, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "O loop 'do-while' garante que o bloco de código seja executado pelo menos uma vez.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Em 'do-while', o bloco é executado ANTES da verificação da condição. Isso garante ao menos uma execução mesmo que a condição seja falsa desde o início. Use quando precisa executar ao menos uma vez (ex: menu de opções).",
                DicaTexto = "Em do-while, a condição é verificada antes ou depois do bloco?" },

            new Exercicio { Id = 131, LicaoId = 2, Ordem = 6, XPRecompensa = 5,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para pular para a próxima iteração de um loop sem encerrar o loop, usa-se a instrução: ____",
                OpcoesJson = "[]",
                RespostaCorreta = "continue",
                Explicacao = "'continue' interrompe a iteração ATUAL e passa para a PRÓXIMA. O loop continua normalmente. Diferente de 'break', que encerra o loop por completo.",
                DicaTexto = "É o oposto de break — não encerra, apenas avança..." },

            new Exercicio { Id = 132, LicaoId = 2, Ordem = 7, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Na switch expression moderna de C#, qual símbolo representa o caso padrão (default)?",
                OpcoesJson = "[\"_\",\"*\",\"default\",\"else\"]",
                RespostaCorreta = "_",
                Explicacao = "Na switch expression (C# 8+), o underscore '_' é o discard pattern e funciona como o 'default' do switch clássico — captura qualquer valor não coberto pelos outros casos.",
                DicaTexto = "É um símbolo de descarte, usado como padrão em pattern matching..." },

            new Exercicio { Id = 133, LicaoId = 2, Ordem = 8, XPRecompensa = 5,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código itera mais vezes do que o esperado. Corrija para percorrer exatamente os índices 0, 1, 2, 3, 4:\nfor (int i = 0; i <= 5; i++)",
                OpcoesJson = "[\"for (int i = 0; i < 5; i++)\",\"for (int i = 1; i <= 5; i++)\",\"for (int i = 0; i <= 4; i++)\",\"for (int i = 0; i < 6; i++)\"]",
                RespostaCorreta = "for (int i = 0; i < 5; i++)",
                Explicacao = "Com 'i <= 5' o loop executa com i = 0, 1, 2, 3, 4, 5 — 6 iterações. Para 5 iterações (índices 0–4), use 'i < 5'. Esse é um dos bugs mais comuns em loops — o famoso 'off by one error'.",
                DicaTexto = "Conte quantas vezes o loop executa com <= vs <..." },

            new Exercicio { Id = 134, LicaoId = 2, Ordem = 9, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Um loop 'while' pode nunca executar seu bloco de código se a condição inicial for falsa.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'while' verifica a condição ANTES de executar o bloco. Se a condição já for falsa na primeira verificação, o bloco nunca executa (0 iterações). É o que o diferencia do 'do-while'.",
                DicaTexto = "while verifica antes ou depois de executar?" },

            new Exercicio { Id = 135, LicaoId = 2, Ordem = 10, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que 'foreach' é preferido a 'for' para percorrer coleções quando o índice não é necessário?",
                OpcoesJson = "[\"Mais legível e elimina erros de índice\",\"foreach é mais rápido que for\",\"foreach funciona com mais tipos\",\"for pode modificar a coleção, foreach não\"]",
                RespostaCorreta = "Mais legível e elimina erros de índice",
                Explicacao = "'foreach' é mais expressivo ('para cada item em coleção') e elimina erros como índice errado ou off-by-one. Quanto à performance, são equivalentes para arrays. Use 'for' quando precisar do índice ou modificar elementos por posição.",
                DicaTexto = "Pense na legibilidade e nos tipos de erro que cada um evita..." },

            // ── Exercícios adicionais — Lição 3: Métodos ────────────────────────────
            new Exercicio { Id = 136, LicaoId = 3, Ordem = 4, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que um método declarado como 'void' retorna?",
                OpcoesJson = "[\"Nada (sem valor de retorno)\",\"null\",\"0\",\"false\"]",
                RespostaCorreta = "Nada (sem valor de retorno)",
                Explicacao = "'void' significa ausência de retorno. O método executa sua lógica mas não produz valor. Diferente de retornar null (que é um valor). Um 'return;' vazio pode ser usado para sair antecipadamente de um void.",
                DicaTexto = "void vem do latim e significa 'vazio'..." },

            new Exercicio { Id = 137, LicaoId = 3, Ordem = 5, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Em C#, dois métodos podem ter o mesmo nome se tiverem parâmetros diferentes (tipos ou quantidade). Esse conceito se chama sobrecarga.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Sobrecarga (overloading) permite múltiplos métodos com o mesmo nome e assinaturas diferentes. O compilador escolhe qual versão chamar pelos argumentos. Ex: Calcular(int a, int b) e Calcular(double a, double b) coexistem.",
                DicaTexto = "O que diferencia dois métodos sobrecarregados?" },

            new Exercicio { Id = 138, LicaoId = 3, Ordem = 6, XPRecompensa = 5,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Parâmetros com valor padrão definido na assinatura do método são chamados parâmetros ____.",
                OpcoesJson = "[]",
                RespostaCorreta = "opcionais",
                Explicacao = "Parâmetros opcionais têm um valor padrão na assinatura: 'void Formatar(string texto, bool maiusculo = false)'. Se o chamador não fornecer o argumento, o valor padrão é usado. Devem vir APÓS os parâmetros obrigatórios.",
                DicaTexto = "Se o chamador não precisar fornecê-los, são..." },

            new Exercicio { Id = 139, LicaoId = 3, Ordem = 7, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que é um expression-bodied method (usando =>)?",
                OpcoesJson = "[\"Método de uma linha que retorna uma expressão sem 'return' explícito\",\"Método assíncrono\",\"Método estático\",\"Método sem parâmetros\"]",
                RespostaCorreta = "Método de uma linha que retorna uma expressão sem 'return' explícito",
                Explicacao = "'public int Dobro(int x) => x * 2;' é equivalente a '{ return x * 2; }'. O => elimina as chaves e o return para métodos simples de uma expressão, tornando o código mais conciso.",
                DicaTexto = "É uma forma mais curta de escrever métodos simples com =>" },

            new Exercicio { Id = 140, LicaoId = 3, Ordem = 8, XPRecompensa = 5,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O método abaixo tem um erro de tipo de retorno. Corrija-o:\npublic void Somar(int a, int b) { return a + b; }",
                OpcoesJson = "[\"public int Somar(int a, int b) { return a + b; }\",\"public void Somar(int a, int b) { return; }\",\"public string Somar(int a, int b) { return a + b; }\",\"public Somar(int a, int b) { return a + b; }\"]",
                RespostaCorreta = "public int Somar(int a, int b) { return a + b; }",
                Explicacao = "Um método declarado como 'void' não pode retornar valores. Como queremos retornar a soma (um int), o tipo de retorno deve ser 'int'. O compilador detecta isso em tempo de compilação.",
                DicaTexto = "void significa sem retorno. Se o método retorna algo, qual tipo deveria ser?" },

            new Exercicio { Id = 141, LicaoId = 3, Ordem = 9, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "A palavra-chave 'params' permite que um método receba um número variável de argumentos do mesmo tipo.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'public int Somar(params int[] nums)' pode ser chamado com Somar(1,2), Somar(1,2,3,4) ou Somar(). O 'params' converte os argumentos em um array automaticamente. Deve ser o último parâmetro da assinatura.",
                DicaTexto = "params vem de 'parameters' e aceita quantidade variável..." },

            new Exercicio { Id = 142, LicaoId = 3, Ordem = 10, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual modificador de acesso é o mais restritivo em C#?",
                OpcoesJson = "[\"private\",\"protected\",\"internal\",\"public\"]",
                RespostaCorreta = "private",
                Explicacao = "'private' restringe o acesso apenas à própria classe. É o mais restritivo — nem subclasses nem outras classes do mesmo assembly podem acessar. A regra de ouro é: use o modificador mais restritivo possível.",
                DicaTexto = "Qual modificador só permite acesso de dentro da própria classe?" },

            // ── Exercícios adicionais — Lição 4: Classes e Objetos ──────────────────
            new Exercicio { Id = 143, LicaoId = 4, Ordem = 4, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que o operador 'new' faz ao criar um objeto em C#?",
                OpcoesJson = "[\"Aloca memória no heap e chama o construtor\",\"Copia um objeto existente\",\"Aloca memória no stack\",\"Apenas chama o construtor sem alocar memória\"]",
                RespostaCorreta = "Aloca memória no heap e chama o construtor",
                Explicacao = "'new' realiza três ações: (1) aloca memória no heap para o objeto, (2) inicializa campos com valores padrão, (3) chama o construtor especificado. Retorna uma referência ao objeto criado.",
                DicaTexto = "Classes são tipos de referência — onde ficam armazenadas?" },

            new Exercicio { Id = 144, LicaoId = 4, Ordem = 5, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Uma classe em C# pode ter múltiplos construtores com parâmetros diferentes.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Sobrecarga de construtores permite criar objetos de formas diferentes: 'new Produto()' sem parâmetros, 'new Produto(\"Teclado\", 150m)' com parâmetros. O compilador escolhe o correto pelos argumentos passados.",
                DicaTexto = "É o mesmo conceito de sobrecarga aplicado ao construtor..." },

            new Exercicio { Id = 145, LicaoId = 4, Ordem = 6, XPRecompensa = 5,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Membros que pertencem à classe (e não às instâncias individuais) são declarados com o modificador ____.",
                OpcoesJson = "[]",
                RespostaCorreta = "static",
                Explicacao = "Membros 'static' existem uma única vez na classe, compartilhados por todas as instâncias. São acessados pelo nome da classe: 'Contador.Total', não por instância. Use para contadores globais, constantes e métodos utilitários.",
                DicaTexto = "Esse modificador faz o membro pertencer à classe, não ao objeto..." },

            new Exercicio { Id = 146, LicaoId = 4, Ordem = 7, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Dado: 'var p1 = new Produto { Nome = \"Teclado\" }; var p2 = p1; p2.Nome = \"Mouse\";'. Qual é o valor de p1.Nome?",
                OpcoesJson = "[\"Mouse\",\"Teclado\",\"null\",\"Erro de compilação\"]",
                RespostaCorreta = "Mouse",
                Explicacao = "Classes são tipos de referência. 'var p2 = p1' NÃO cria uma cópia — p2 aponta para o MESMO objeto no heap. Qualquer alteração via p2 afeta p1 também. Para cópia independente, implemente ICloneable ou use 'with' (records).",
                DicaTexto = "Classes são tipos de referência — p2 é uma cópia ou outra referência ao mesmo objeto?" },

            new Exercicio { Id = 147, LicaoId = 4, Ordem = 8, XPRecompensa = 5,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo tem um erro de nomenclatura. Corrija para seguir as convenções C#:\nvar p = new Produto { nome = \"Teclado\", preco = 150m };",
                OpcoesJson = "[\"var p = new Produto { Nome = \\\"Teclado\\\", Preco = 150m };\",\"var p = new produto { Nome = \\\"Teclado\\\", Preco = 150m };\",\"var p = Produto.New { Nome = \\\"Teclado\\\" };\",\"var p = new Produto(\\\"Teclado\\\", 150m);\"]",
                RespostaCorreta = "var p = new Produto { Nome = \"Teclado\", Preco = 150m };",
                Explicacao = "Em C#, propriedades públicas seguem PascalCase (primeira letra maiúscula). 'nome' e 'preco' devem ser 'Nome' e 'Preco'. O compilador não encontraria os membros com minúscula — seria um erro de compilação.",
                DicaTexto = "Propriedades em C# seguem PascalCase..." },

            new Exercicio { Id = 148, LicaoId = 4, Ordem = 9, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Inicializadores de objeto '{ }' permitem definir propriedades públicas sem necessidade de:",
                OpcoesJson = "[\"Um construtor específico para cada combinação\",\"O operador new\",\"O tipo da variável\",\"Propriedades públicas\"]",
                RespostaCorreta = "Um construtor específico para cada combinação",
                Explicacao = "Com inicializadores, 'new Produto { Nome = \"X\", Preco = 10m }' funciona mesmo sem um construtor Produto(string, decimal). Isso reduz o número de construtores necessários e torna a criação de objetos mais expressiva.",
                DicaTexto = "O que você não precisa escrever quando usa inicializadores?" },

            new Exercicio { Id = 149, LicaoId = 4, Ordem = 10, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "O encadeamento de construtores com ':this(...)' permite que um construtor chame outro construtor da mesma classe.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "': this(args)' chama outro construtor da mesma classe antes de executar o corpo do atual. Evita duplicação de lógica de inicialização. Para chamar o construtor da classe PAI, usa-se ':base(args)'.",
                DicaTexto = "this() chama construtor da mesma classe, base() chama da classe pai..." },

            // ── Exercícios adicionais — Lição 5: Herança e Polimorfismo ─────────────
            new Exercicio { Id = 150, LicaoId = 5, Ordem = 4, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Em C#, de quantas classes uma classe pode herdar diretamente?",
                OpcoesJson = "[\"Apenas uma\",\"Até duas\",\"Ilimitado\",\"Até três\"]",
                RespostaCorreta = "Apenas uma",
                Explicacao = "C# não suporta herança múltipla de classes — uma classe herda de exatamente uma classe (ou de Object implicitamente). Para comportamento múltiplo, use interfaces. Isso evita o 'diamond problem' e simplifica o modelo.",
                DicaTexto = "C# optou por simplicidade — sem herança múltipla de classes..." },

            new Exercicio { Id = 151, LicaoId = 5, Ordem = 5, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Uma classe abstrata pode conter métodos com implementação (métodos concretos).",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Classe abstrata pode ter: métodos abstratos (sem implementação, subclasses devem implementar) E métodos concretos (com implementação, subclasses podem usar ou sobrescrever). É o que a diferencia de uma interface.",
                DicaTexto = "Abstrata ≠ sem implementação. Pode misturar os dois tipos..." },

            new Exercicio { Id = 152, LicaoId = 5, Ordem = 6, XPRecompensa = 5,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para substituir um método virtual da classe pai na subclasse, usa-se a palavra-chave ____.",
                OpcoesJson = "[]",
                RespostaCorreta = "override",
                Explicacao = "'override' indica que a subclasse está substituindo a implementação herdada. O método na classe pai deve ser marcado como 'virtual' ou 'abstract'. Sem 'override', você oculta o método pai (comportamento diferente e perigoso).",
                DicaTexto = "Começa com 'over' — você está 'escrevendo por cima' do método pai..." },

            new Exercicio { Id = 153, LicaoId = 5, Ordem = 7, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Para chamar o construtor da classe pai dentro do construtor da subclasse, usa-se:",
                OpcoesJson = "[\"base(...)\",\"parent(...)\",\"super(...)\",\"this(...)\"]",
                RespostaCorreta = "base(...)",
                Explicacao = "':base(args)' na assinatura do construtor chama o construtor da classe pai. Ex: 'public ContaPremium(string titular) : base(titular) {}'. 'this(args)' chama outro construtor da MESMA classe. 'super' não existe em C#.",
                DicaTexto = "Em C#, 'base' refere-se à classe pai; 'this' refere-se à própria classe..." },

            new Exercicio { Id = 154, LicaoId = 5, Ordem = 8, XPRecompensa = 5,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O método abaixo não substitui corretamente o método virtual da classe pai. Corrija:\npublic class Cachorro : Animal { public string EmitirSom() => \"Au au!\"; }",
                OpcoesJson = "[\"public class Cachorro : Animal { public override string EmitirSom() => \\\"Au au!\\\"; }\",\"public class Cachorro : Animal { public virtual string EmitirSom() => \\\"Au au!\\\"; }\",\"public class Cachorro : Animal { public new string EmitirSom() => \\\"Au au!\\\"; }\",\"public class Cachorro(Animal) { public string EmitirSom() => \\\"Au au!\\\"; }\"]",
                RespostaCorreta = "public class Cachorro : Animal { public override string EmitirSom() => \"Au au!\"; }",
                Explicacao = "Sem 'override', EmitirSom() na subclasse apenas OCULTA o método da classe pai (diferente de sobrescrever). O polimorfismo não funcionaria: '(Animal)cachorro.EmitirSom()' chamaria o método do Animal, não do Cachorro.",
                DicaTexto = "Qual palavra-chave indica que estamos substituindo o método da classe pai?" },

            new Exercicio { Id = 155, LicaoId = 5, Ordem = 9, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Uma interface pode ser implementada por múltiplas classes sem relação de herança entre elas.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Produto, Usuario e Pedido podem todos implementar IExportavel mesmo sem herdar uns dos outros. É o poder das interfaces: definem contratos transversais a qualquer hierarquia de classes.",
                DicaTexto = "Interface é um contrato — qualquer classe pode assinar..." },

            new Exercicio { Id = 156, LicaoId = 5, Ordem = 10, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Polimorfismo permite que referências do tipo base chamem métodos da subclasse. Qual pré-requisito é necessário?",
                OpcoesJson = "[\"O método deve ser virtual na base e override na subclasse\",\"Os métodos devem ter nomes diferentes\",\"A subclasse deve ser static\",\"Não é possível em C#\"]",
                RespostaCorreta = "O método deve ser virtual na base e override na subclasse",
                Explicacao = "Para polimorfismo funcionar, o método na classe pai deve ser 'virtual' (ou 'abstract') e a subclasse deve usar 'override'. Assim, 'Animal a = new Cachorro(); a.EmitirSom()' chama o método do Cachorro, não do Animal.",
                DicaTexto = "Quais palavras-chave habilitam o polimorfismo em C#?" },

            // ── Exercícios adicionais — Lição 10: Encapsulamento ────────────────────
            new Exercicio { Id = 157, LicaoId = 10, Ordem = 4, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual modificador de acesso permite que apenas a própria classe e suas subclasses acessem um membro?",
                OpcoesJson = "[\"protected\",\"private\",\"internal\",\"public\"]",
                RespostaCorreta = "protected",
                Explicacao = "'protected' é mais restritivo que public/internal mas menos que private: a própria classe + qualquer subclasse pode acessar, independente do assembly. Ideal para membros que subclasses precisam acessar mas código externo não.",
                DicaTexto = "É o intermediário entre private e public, pensado para herança..." },

            new Exercicio { Id = 158, LicaoId = 10, Ordem = 5, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Uma propriedade declarada como '{ get; private set; }' permite leitura pública mas escrita apenas dentro da classe.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'public string Nome { get; private set; }' é um padrão de encapsulamento muito comum: qualquer código pode ler Nome, mas apenas a própria classe pode alterar. Garante que o estado só mude por lógica controlada.",
                DicaTexto = "get público + set privado = leitura pública, escrita controlada..." },

            new Exercicio { Id = 159, LicaoId = 10, Ordem = 6, XPRecompensa = 5,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "O modificador ____ (C# 9+) permite definir uma propriedade apenas durante a inicialização do objeto, não após.",
                OpcoesJson = "[]",
                RespostaCorreta = "init",
                Explicacao = "'init' é um setter especial: só pode ser chamado em inicializadores de objeto ('new Pedido { Id = 1 }') ou no construtor. Após a construção, a propriedade é efetivamente imutável. Ideal para objetos imutáveis.",
                DicaTexto = "É um tipo especial de setter introduzido no C# 9..." },

            new Exercicio { Id = 160, LicaoId = 10, Ordem = 7, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual é o principal benefício de encapsular o estado de um objeto?",
                OpcoesJson = "[\"Garantir que o objeto nunca fique em estado inválido\",\"Tornar o código mais rápido\",\"Reduzir o número de classes\",\"Permitir herança múltipla\"]",
                RespostaCorreta = "Garantir que o objeto nunca fique em estado inválido",
                Explicacao = "Encapsulamento protege invariantes: um ContaBancaria com _saldo privado e método Sacar() com validação nunca terá saldo negativo por acidente. Isso torna o sistema mais robusto e os bugs mais fáceis de encontrar.",
                DicaTexto = "O encapsulamento é sobre controle — quem pode mudar o estado e como..." },

            new Exercicio { Id = 161, LicaoId = 10, Ordem = 8, XPRecompensa = 5,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O campo abaixo expõe o saldo diretamente. Corrija para encapsulá-lo corretamente:\npublic class Conta { public decimal Saldo; }",
                OpcoesJson = "[\"public class Conta { public decimal Saldo { get; private set; } }\",\"public class Conta { private decimal Saldo; }\",\"public class Conta { protected decimal Saldo { get; set; } }\",\"public class Conta { internal decimal Saldo { get; } }\"]",
                RespostaCorreta = "public class Conta { public decimal Saldo { get; private set; } }",
                Explicacao = "Um campo público permite que qualquer código altere o saldo diretamente: 'conta.Saldo = -999'. Com '{ get; private set; }', a leitura é pública mas a escrita é controlada pelos métodos Depositar() e Sacar().",
                DicaTexto = "Troque o campo público por uma propriedade com set restrito..." },

            new Exercicio { Id = 162, LicaoId = 10, Ordem = 9, XPRecompensa = 5,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "O modificador 'internal' torna um membro acessível apenas dentro do mesmo assembly (projeto).",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'internal' é visível para qualquer classe do mesmo projeto (.dll/.exe), mas não para projetos externos. É útil para implementações que não fazem parte da API pública da biblioteca.",
                DicaTexto = "internal = interno ao assembly (projeto)..." },

            new Exercicio { Id = 163, LicaoId = 10, Ordem = 10, XPRecompensa = 5,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Uma propriedade com validação no setter que lança exceção se o valor for inválido é um exemplo de:",
                OpcoesJson = "[\"Encapsulamento com invariante\",\"Herança\",\"Polimorfismo\",\"Sobrecarga\"]",
                RespostaCorreta = "Encapsulamento com invariante",
                Explicacao = "Uma invariante é uma regra que o objeto SEMPRE deve satisfazer. Ex: Preco >= 0. O setter que valida e lança ArgumentException é o mecanismo que garante a invariante — ninguém pode colocar o objeto em estado inválido.",
                DicaTexto = "Invariante = regra que o objeto deve sempre satisfazer..." },

            // ── Exercícios — Lição 40: Strings em Profundidade ──────────────────────
            new Exercicio { Id = 164, LicaoId = 40, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual prefixo cria uma string interpolada em C#, permitindo embutir expressões com {}?",
                OpcoesJson = "[\"$\",\"@\",\"#\",\"%\"]",
                RespostaCorreta = "$",
                Explicacao = "O prefixo '$' antes das aspas cria uma string interpolada: $\"Olá, {nome}!\". Qualquer expressão C# pode ficar dentro de {}. É preferível à concatenação com + por ser mais legível e segura.",
                DicaTexto = "É um símbolo de cifrão que precede as aspas..." },

            new Exercicio { Id = 165, LicaoId = 40, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Em C#, concatenar strings com '+' em um loop cria uma nova string a cada iteração, o que é ineficiente.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Strings são imutáveis em C#. Cada '+' cria um novo objeto string no heap. Em 1000 iterações, isso gera ~1000 alocações. StringBuilder resolve isso acumulando internamente e criando uma única string no final com ToString().",
                DicaTexto = "Strings são imutáveis — o que acontece a cada operação de +?" },

            new Exercicio { Id = 166, LicaoId = 40, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para declarar a string C:\\Users\\Maria sem precisar escapar as barras, usa-se o prefixo ____\"C:\\Users\\Maria\"",
                OpcoesJson = "[]",
                RespostaCorreta = "@",
                Explicacao = "O prefixo '@' cria uma string verbatim: as barras são literais, sem necessidade de escape. @\"C:\\Users\\Maria\" é equivalente a \"C:\\\\Users\\\\Maria\". Também suporta strings de múltiplas linhas.",
                DicaTexto = "É um símbolo de arroba que indica string literal..." },

            new Exercicio { Id = 167, LicaoId = 40, Ordem = 4, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual método de string divide uma string em partes com base em um separador?",
                OpcoesJson = "[\"Split()\",\"Slice()\",\"Divide()\",\"Cut()\"]",
                RespostaCorreta = "Split()",
                Explicacao = "\"a,b,c\".Split(',') retorna string[] { \"a\", \"b\", \"c\" }. É o método correto para dividir strings por separador. 'Slice' não existe em string C# (existe em Span<T>). Slice/Divide/Cut são de outras linguagens.",
                DicaTexto = "Lembre do nome em inglês — 'dividir' em inglês é..." },

            new Exercicio { Id = 168, LicaoId = 40, Ordem = 5, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual classe usar para construir strings eficientemente em um loop?",
                OpcoesJson = "[\"StringBuilder\",\"StringHelper\",\"StringBuffer\",\"StringWriter\"]",
                RespostaCorreta = "StringBuilder",
                Explicacao = "System.Text.StringBuilder acumula texto internamente e realiza apenas uma alocação final no ToString(). Use quando concatenar muitas strings (em loops ou com contagem dinâmica). Para 2-3 concatenações, '+' ou '$' são suficientes.",
                DicaTexto = "O nome diz tudo — é um 'construtor' de strings..." },

            new Exercicio { Id = 169, LicaoId = 40, Ordem = 6, XPRecompensa = 10,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo compara strings de forma case-sensitive. Corrija para ignorar maiúsculas/minúsculas:\nbool igual = \"abc\" == \"ABC\";",
                OpcoesJson = "[\"bool igual = string.Equals(\\\"abc\\\", \\\"ABC\\\", StringComparison.OrdinalIgnoreCase);\",\"bool igual = \\\"abc\\\".ToLower() == \\\"ABC\\\".ToLower();\",\"bool igual = \\\"abc\\\".Equals(\\\"ABC\\\");\",\"bool igual = string.Compare(\\\"abc\\\", \\\"ABC\\\") == 0;\"]",
                RespostaCorreta = "bool igual = string.Equals(\"abc\", \"ABC\", StringComparison.OrdinalIgnoreCase);",
                Explicacao = "string.Equals com StringComparison.OrdinalIgnoreCase é a forma correta e eficiente. Usar .ToLower() == .ToLower() cria duas strings intermediárias desnecessárias. StringComparison deixa a intenção explícita no código.",
                DicaTexto = "Use o método estático string.Equals com um terceiro parâmetro de comparação..." },

            new Exercicio { Id = 170, LicaoId = 40, Ordem = 7, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "string.IsNullOrWhiteSpace(\"   \") retorna true.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "IsNullOrWhiteSpace() retorna true se a string for null, vazia (\"\") OU contiver apenas espaços em branco. É mais abrangente que IsNullOrEmpty() — use quando uma string de só espaços também deve ser tratada como inválida.",
                DicaTexto = "WhiteSpace inclui espaços, tabs e quebras de linha..." },

            new Exercicio { Id = 171, LicaoId = 40, Ordem = 8, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para remover espaços em branco do início e do fim de uma string, usa-se o método .____().",
                OpcoesJson = "[]",
                RespostaCorreta = "Trim",
                Explicacao = "\"  olá  \".Trim() retorna \"olá\". TrimStart() remove apenas do início, TrimEnd() apenas do fim. É essencial ao processar entrada do usuário ou dados vindos de arquivos.",
                DicaTexto = "Em inglês, 'aparar' ou 'cortar as bordas' é..." },

            // ── Exercícios — Lição 41: Tratamento de Exceções ───────────────────────
            new Exercicio { Id = 172, LicaoId = 41, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O bloco 'finally' é executado:",
                OpcoesJson = "[\"Sempre, com ou sem exceção\",\"Apenas quando há exceção\",\"Apenas quando não há exceção\",\"Apenas em modo debug\"]",
                RespostaCorreta = "Sempre, com ou sem exceção",
                Explicacao = "'finally' executa SEMPRE: após o try normal, após um catch, mesmo após um return. É ideal para liberar recursos (fechar arquivos, conexões) independentemente do que aconteça. O 'using' statement é uma alternativa moderna.",
                DicaTexto = "O nome 'finally' sugere 'por fim, sempre acontece'..." },

            new Exercicio { Id = 173, LicaoId = 41, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "'throw;' (sem argumentos) relança a exceção atual preservando o stack trace original.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'throw;' relança a exceção exatamente como recebida — o stack trace aponta para onde o erro realmente aconteceu. 'throw ex;' redefine o stack trace para a linha do catch, dificultando o diagnóstico. Sempre prefira 'throw;'.",
                DicaTexto = "Qual preserva o stack trace original — throw ou throw ex?" },

            new Exercicio { Id = 174, LicaoId = 41, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para criar uma exceção customizada em C#, herde da classe ____.",
                OpcoesJson = "[]",
                RespostaCorreta = "Exception",
                Explicacao = "Todas as exceções em C# derivam de System.Exception. Para criar a sua: 'public class ProdutoNaoEncontradoException : Exception'. Adicione propriedades extras para contexto e chame o construtor base com a mensagem.",
                DicaTexto = "É a classe raiz de todas as exceções em .NET..." },

            new Exercicio { Id = 175, LicaoId = 41, Ordem = 4, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual exceção é lançada ao acessar um índice inválido em um array?",
                OpcoesJson = "[\"IndexOutOfRangeException\",\"ArgumentException\",\"NullReferenceException\",\"InvalidOperationException\"]",
                RespostaCorreta = "IndexOutOfRangeException",
                Explicacao = "Acessar 'array[array.Length]' ou índice negativo lança IndexOutOfRangeException. NullReferenceException ocorre ao acessar membro de objeto null. ArgumentException é para argumentos inválidos passados a métodos.",
                DicaTexto = "O índice saiu do intervalo válido — qual exceção descreve isso?" },

            new Exercicio { Id = 176, LicaoId = 41, Ordem = 5, XPRecompensa = 10,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código abaixo perde o stack trace original. Corrija-o:\ncatch (Exception ex) { throw ex; }",
                OpcoesJson = "[\"catch (Exception) { throw; }\",\"catch (Exception ex) { throw new Exception(ex.Message); }\",\"catch { throw new Exception(); }\",\"catch (Exception ex) { return; }\"]",
                RespostaCorreta = "catch (Exception) { throw; }",
                Explicacao = "'throw ex' reescreve o stack trace — a linha do catch vira a origem do erro. 'throw;' relança preservando toda a pilha de chamadas original, essencial para diagnosticar onde o erro realmente aconteceu.",
                DicaTexto = "Use throw sem parâmetros para preservar o stack trace..." },

            new Exercicio { Id = 177, LicaoId = 41, Ordem = 6, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Exceções mais específicas devem ser capturadas ANTES das mais genéricas no bloco catch.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Se 'catch (Exception)' vier antes de 'catch (FormatException)', o FormatException nunca será alcançado — Exception captura tudo. O compilador inclusive gera um warning/erro. Sempre do mais específico ao mais genérico.",
                DicaTexto = "O compilador processa os catches em ordem — qual deve vir primeiro?" },

            new Exercicio { Id = 178, LicaoId = 41, Ordem = 7, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Por que NÃO se deve usar exceções para controle de fluxo normal (ex: validar entrada do usuário)?",
                OpcoesJson = "[\"Criar exceções é caro — gera stack trace e alocações no heap\",\"Exceções não funcionam em produção\",\"O compilador não permite\",\"Exceções só funcionam em métodos async\"]",
                RespostaCorreta = "Criar exceções é caro — gera stack trace e alocações no heap",
                Explicacao = "Lançar uma exceção captura o stack trace completo, o que é custoso em performance. Para fluxos esperados (usuário digitou texto em campo numérico), use int.TryParse(), retorno de bool/Result, ou validação explícita. Exceções para situações verdadeiramente excepcionais.",
                DicaTexto = "O que acontece internamente quando uma exceção é lançada?" },

            new Exercicio { Id = 179, LicaoId = 41, Ordem = 8, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para garantir que um recurso seja liberado mesmo se uma exceção for lançada, use a instrução ____ ao declarar o objeto.",
                OpcoesJson = "[]",
                RespostaCorreta = "using",
                Explicacao = "'using var conn = new SqlConnection(str);' chama conn.Dispose() automaticamente ao sair do escopo — mesmo com exceção. Funciona com qualquer objeto que implemente IDisposable. É mais seguro que try/finally manual.",
                DicaTexto = "É uma instrução que garante a liberação automática de recursos..." },

            // ── Exercícios — Lição 42: Interfaces ───────────────────────────────────
            new Exercicio { Id = 180, LicaoId = 42, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "O que uma interface define em C#?",
                OpcoesJson = "[\"Um contrato (o que fazer), sem implementação (como fazer)\",\"Uma classe que não pode ser instanciada\",\"Uma classe com todos os métodos abstratos\",\"Um tipo de dado primitivo\"]",
                RespostaCorreta = "Um contrato (o que fazer), sem implementação (como fazer)",
                Explicacao = "Interface é um contrato puro: define quais membros uma classe deve ter, sem dizer como implementá-los. Isso permite que classes completamente diferentes compartilhem o mesmo contrato e sejam usadas de forma intercambiável.",
                DicaTexto = "Interface define o 'o quê', não o 'como'..." },

            new Exercicio { Id = 181, LicaoId = 42, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Uma classe C# pode implementar múltiplas interfaces ao mesmo tempo.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "Uma classe herda de UMA classe mas pode implementar N interfaces: 'class Relatorio : DocumentoBase, IExportavel, IImprimivel, IArquivavel'. Isso resolve o problema de comportamento múltiplo sem herança múltipla de classes.",
                DicaTexto = "Classes: herança única. Interfaces: ilimitadas..." },

            new Exercicio { Id = 182, LicaoId = 42, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Por convenção em C#, nomes de interfaces começam com a letra ____.",
                OpcoesJson = "[]",
                RespostaCorreta = "I",
                Explicacao = "A convenção da Microsoft é prefixar interfaces com 'I' maiúsculo: IDisposable, IEnumerable, IRepository, INotificador. Isso torna imediatamente claro que é uma interface no código.",
                DicaTexto = "É a inicial de 'Interface'..." },

            new Exercicio { Id = 183, LicaoId = 42, Ordem = 4, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual a diferença principal entre interface e classe abstrata?",
                OpcoesJson = "[\"Interface não pode ter estado (campos); abstrata pode ter lógica e campos\",\"Interface pode ser instanciada; abstrata não\",\"Classe abstrata suporta múltipla herança\",\"Não há diferença — são a mesma coisa\"]",
                RespostaCorreta = "Interface não pode ter estado (campos); abstrata pode ter lógica e campos",
                Explicacao = "Interface: contrato puro, sem estado, sem lógica (antes C# 8). Classe abstrata: pode ter campos, lógica concreta E métodos abstratos. Use interface para contratos transversais; abstrata para compartilhar implementação entre classes relacionadas.",
                DicaTexto = "Interface é puro contrato; abstrata pode misturar contrato e implementação..." },

            new Exercicio { Id = 184, LicaoId = 42, Ordem = 5, XPRecompensa = 10,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "A classe abaixo implementa INotificador mas não implementa o membro obrigatório. Corrija:\npublic interface INotificador { Task EnviarAsync(string dest, string msg); }\npublic class NotificadorEmail : INotificador { }",
                OpcoesJson = "[\"public class NotificadorEmail : INotificador { public async Task EnviarAsync(string dest, string msg) => await smtp.SendAsync(dest, msg); }\",\"public class NotificadorEmail { public Task EnviarAsync(string dest, string msg) => Task.CompletedTask; }\",\"public abstract class NotificadorEmail : INotificador { }\",\"public class NotificadorEmail : INotificador { private Task EnviarAsync(string dest, string msg) => Task.CompletedTask; }\"]",
                RespostaCorreta = "public class NotificadorEmail : INotificador { public async Task EnviarAsync(string dest, string msg) => await smtp.SendAsync(dest, msg); }",
                Explicacao = "Implementar uma interface é obrigatório — todos os membros devem ser implementados como public. O compilador gera erro CS0535 se algum membro faltar. Apenas classes abstratas podem deixar membros sem implementar.",
                DicaTexto = "Toda classe que implementa uma interface deve implementar todos os seus membros como public..." },

            new Exercicio { Id = 185, LicaoId = 42, Ordem = 6, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "IDisposable é uma interface do .NET que define o método Dispose() para liberar recursos não gerenciados.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "IDisposable tem um único método: void Dispose(). Objetos que usam recursos externos (arquivos, conexões, handles) devem implementá-la. O 'using' statement chama Dispose() automaticamente ao sair do escopo.",
                DicaTexto = "É a interface que habilita o uso do 'using' statement..." },

            new Exercicio { Id = 186, LicaoId = 42, Ordem = 7, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Quando é preferível usar uma interface em vez de uma classe abstrata?",
                OpcoesJson = "[\"Quando classes de hierarquias diferentes precisam do mesmo contrato\",\"Quando há lógica comum para compartilhar\",\"Quando a performance é crítica\",\"Quando a classe tem muitos campos\"]",
                RespostaCorreta = "Quando classes de hierarquias diferentes precisam do mesmo contrato",
                Explicacao = "Produto, Usuario e Pedido podem implementar IExportavel sem relação entre si. Interface é o mecanismo para comportamento transversal. Se há lógica comum para compartilhar, considere classe abstrata ou composição.",
                DicaTexto = "Interface une classes sem relação de herança pelo contrato..." },

            new Exercicio { Id = 187, LicaoId = 42, Ordem = 8, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "O princípio de inversão de dependência (D do SOLID) diz: dependa de ____, não de implementações concretas.",
                OpcoesJson = "[]",
                RespostaCorreta = "abstrações",
                Explicacao = "Interfaces são o mecanismo de abstração em C#. Em vez de 'EmailService emailService', declare 'INotificador notificador'. Isso torna o código testável (mock fácil) e flexível (trocar implementação sem mudar quem usa).",
                DicaTexto = "Interfaces e classes abstratas são exemplos de..." },

            // ── Exercícios — Lição 43: Records e Imutabilidade ──────────────────────
            new Exercicio { Id = 188, LicaoId = 43, Ordem = 1, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Como dois records são comparados por padrão em C#?",
                OpcoesJson = "[\"Pelos valores das propriedades (igualdade por valor)\",\"Pela referência de memória (igualdade por referência)\",\"Pelo hash code\",\"Records não suportam comparação\"]",
                RespostaCorreta = "Pelos valores das propriedades (igualdade por valor)",
                Explicacao = "Records implementam igualdade por valor automaticamente. 'new Produto(1, \"X\") == new Produto(1, \"X\")' retorna true, mesmo sendo objetos diferentes na memória. Isso é oposto às classes, onde == compara referências.",
                DicaTexto = "Records foram criados para ter semântica de valor — como int e decimal..." },

            new Exercicio { Id = 189, LicaoId = 43, Ordem = 2, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "A expressão 'with' modifica o record original, retornando void.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Falso",
                Explicacao = "'with' cria uma CÓPIA do record com os valores especificados alterados. O record original permanece inalterado. Ex: 'var desconto = produto with { Preco = produto.Preco * 0.9m }' — produto continua com o preço original.",
                DicaTexto = "Records são imutáveis — 'with' cria uma nova instância ou modifica a existente?" },

            new Exercicio { Id = 190, LicaoId = 43, Ordem = 3, XPRecompensa = 10,
                Tipo = TipoExercicio.PreencherEspacos,
                Enunciado = "Para criar uma cópia de um record com alguns valores alterados, usa-se a expressão ____.",
                OpcoesJson = "[]",
                RespostaCorreta = "with",
                Explicacao = "'var novo = original with { Preco = 200m };' cria um novo record com todas as propriedades de 'original', exceto Preco que recebe 200m. É a forma idiomática de \"modificar\" records imutáveis.",
                DicaTexto = "É uma palavra em inglês que significa 'com'..." },

            new Exercicio { Id = 191, LicaoId = 43, Ordem = 4, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual a sintaxe correta para um record posicional em C#?",
                OpcoesJson = "[\"public record Ponto(int X, int Y);\",\"public record class Ponto { int X; int Y; }\",\"public record Ponto : (int X, int Y);\",\"record<int, int> Ponto;\"]",
                RespostaCorreta = "public record Ponto(int X, int Y);",
                Explicacao = "O record posicional declara propriedades, construtor, igualdade e ToString() em uma única linha. As propriedades são geradas como 'init', tornando o record imutável por padrão. Por convenção, usam PascalCase.",
                DicaTexto = "Os parâmetros vão entre parênteses logo após o nome do record..." },

            new Exercicio { Id = 192, LicaoId = 43, Ordem = 5, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Quando é mais adequado usar um record em vez de uma classe?",
                OpcoesJson = "[\"Para objetos imutáveis com igualdade por valor (DTOs, Value Objects)\",\"Para entidades com ciclo de vida e estado mutável\",\"Para objetos que precisam de herança complexa\",\"Para objetos que compartilham estado global\"]",
                RespostaCorreta = "Para objetos imutáveis com igualdade por valor (DTOs, Value Objects)",
                Explicacao = "Records são ideais para: DTOs de API (ProdutoResponseDto), Value Objects (Dinheiro, Endereco), configurações imutáveis. Classes são melhores para: entidades de domínio (Pedido com estado mutável), objetos com ciclo de vida.",
                DicaTexto = "Pense em quais objetos nunca deveriam mudar após criados..." },

            new Exercicio { Id = 193, LicaoId = 43, Ordem = 6, XPRecompensa = 10,
                Tipo = TipoExercicio.CorrigirCodigo,
                Enunciado = "O código tenta modificar um record diretamente. Corrija usando a abordagem correta:\nvar produto = new Produto(1, \"Teclado\", 150m);\nproduto.Preco = 120m;",
                OpcoesJson = "[\"var produto = new Produto(1, \\\"Teclado\\\", 150m); var comDesconto = produto with { Preco = 120m };\",\"var produto = new Produto(1, \\\"Teclado\\\", 150m); produto = new Produto(1, \\\"Teclado\\\", 120m);\",\"var produto = new Produto(1, \\\"Teclado\\\", 150m); produto.Preco = 120m;\",\"Produto.Preco = 120m;\"]",
                RespostaCorreta = "var produto = new Produto(1, \"Teclado\", 150m); var comDesconto = produto with { Preco = 120m };",
                Explicacao = "Propriedades de records posicionais são 'init' — imutáveis após criação. A forma correta é usar 'with' para criar uma nova instância com o valor alterado. 'produto.Preco = 120m' seria erro de compilação.",
                DicaTexto = "Records são imutáveis — use 'with' para criar uma variação..." },

            new Exercicio { Id = 194, LicaoId = 43, Ordem = 7, XPRecompensa = 10,
                Tipo = TipoExercicio.VerdadeiroFalso,
                Enunciado = "Records geram automaticamente uma implementação de ToString() que exibe o nome do tipo e os valores das propriedades.",
                OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
                RespostaCorreta = "Verdadeiro",
                Explicacao = "'Console.WriteLine(new Produto(1, \"Teclado\", 150m))' imprime: 'Produto { Id = 1, Nome = Teclado, Preco = 150 }'. Isso é gerado automaticamente — com classes, ToString() retornaria apenas o nome do tipo sem os valores.",
                DicaTexto = "O compilador gera ToString() automaticamente para records..." },

            new Exercicio { Id = 195, LicaoId = 43, Ordem = 8, XPRecompensa = 10,
                Tipo = TipoExercicio.MultiplaEscolha,
                Enunciado = "Qual a diferença entre 'record' e 'record struct' em C#?",
                OpcoesJson = "[\"record é tipo de referência (heap); record struct é tipo de valor (stack)\",\"record struct é imutável; record não é\",\"record struct suporta herança; record não\",\"Não há diferença — são sinônimos\"]",
                RespostaCorreta = "record é tipo de referência (heap); record struct é tipo de valor (stack)",
                Explicacao = "'record' (ou 'record class') é armazenado no heap como classes. 'record struct' é armazenado no stack como structs, com melhor performance para dados pequenos e frequentes. Ambos têm igualdade por valor, mas record struct não suporta herança.",
                DicaTexto = "A diferença é onde o objeto vive na memória — heap ou stack..." }
        );
    }
}
