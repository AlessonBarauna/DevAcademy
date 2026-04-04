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

        // Seed Módulos
        modelBuilder.Entity<Modulo>().HasData(
            new Modulo { Id = 1, Titulo = "Fundamentos C#", Descricao = "Variáveis, tipos, operadores e controle de fluxo", Ordem = 1, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true, PreRequisitoId = null },
            new Modulo { Id = 2, Titulo = "Orientação a Objetos", Descricao = "Classes, herança, polimorfismo e encapsulamento", Ordem = 2, NivelMinimo = NivelDificuldade.Iniciante, Ativo = true, PreRequisitoId = 1 },
            new Modulo { Id = 3, Titulo = "Coleções e LINQ", Descricao = "List, Dictionary, Array e consultas LINQ", Ordem = 3, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true, PreRequisitoId = 2 },
            new Modulo { Id = 4, Titulo = "Async/Await e Concorrência", Descricao = "Programação assíncrona, Tasks e threading", Ordem = 4, NivelMinimo = NivelDificuldade.Intermediario, Ativo = true, PreRequisitoId = 3 },
            new Modulo { Id = 5, Titulo = "Design Patterns", Descricao = "Repository, Factory, Singleton e outros padrões", Ordem = 5, NivelMinimo = NivelDificuldade.Avancado, Ativo = true, PreRequisitoId = 4 }
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
                    "// Parsing seguro de strings\nbool ok = int.TryParse(\"abc\", out int val); // ok = false, val = 0 — nunca lança exceção\nint n   = int.Parse(\"42\");                  // lança FormatException se inválido\n```"
            },

            new Licao
            {
                Id = 2, ModuloId = 1, Ordem = 2, XPRecompensa = 10, Ativo = true,
                Titulo = "Controle de Fluxo",
                Descricao = "if/else, operador ternário, switch expression, for, foreach e while",
                ConteudoTeoricoMarkdown =
                    "## Controle de Fluxo em C#\n\n" +
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
                    "```csharp\nforeach (var n in numeros)\n{\n    if (n == 0) continue;  // pula para a próxima iteração\n    if (n < 0)  break;     // encerra o loop completamente\n    Console.WriteLine(100 / n);\n}\n```"
            },

            new Licao
            {
                Id = 3, ModuloId = 1, Ordem = 3, XPRecompensa = 15, Ativo = true,
                Titulo = "Métodos e Funções",
                Descricao = "Modificadores de acesso, parâmetros, sobrecarga e métodos expressão",
                ConteudoTeoricoMarkdown =
                    "## Métodos em C#\n\n" +
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
                    "```csharp\npublic int Somar(params int[] numeros) => numeros.Sum();\n\nSomar(1, 2, 3);      // 6\nSomar(10, 20);       // 30\nSomar();             // 0\n```"
            },

            // ── Módulo 2: Orientação a Objetos ───────────────────────────────────────
            new Licao
            {
                Id = 4, ModuloId = 2, Ordem = 1, XPRecompensa = 15, Ativo = true,
                Titulo = "Classes e Objetos",
                Descricao = "Propriedades, construtores múltiplos, new e inicializadores de objeto",
                ConteudoTeoricoMarkdown =
                    "## Classes e Objetos em C#\n\n" +
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
                    "```csharp\npublic class Contador\n{\n    public static int Total { get; private set; } = 0;\n\n    public Contador() { Total++; } // cada new incrementa o contador estático\n}\n\nvar a = new Contador();\nvar b = new Contador();\nConsole.WriteLine(Contador.Total); // 2 — acesso via nome da classe\n```"
            },

            new Licao
            {
                Id = 5, ModuloId = 2, Ordem = 2, XPRecompensa = 20, Ativo = true,
                Titulo = "Herança e Polimorfismo",
                Descricao = "Herança com :, virtual/override, classes abstratas e interfaces",
                ConteudoTeoricoMarkdown =
                    "## Herança e Polimorfismo\n\n" +
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
                    "```csharp\npublic class ContaPremium : Conta\n{\n    public ContaPremium(string titular) : base(titular) { } // chama construtor da base\n    public override string Descricao() => base.Descricao() + \" [PREMIUM]\";\n}\n```"
            },

            new Licao
            {
                Id = 10, ModuloId = 2, Ordem = 3, XPRecompensa = 20, Ativo = true,
                Titulo = "Encapsulamento",
                Descricao = "Modificadores de acesso, propriedades com validação e acessor init",
                ConteudoTeoricoMarkdown =
                    "## Encapsulamento\n\n" +
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
                    "```csharp\n// Preferir IEnumerable<T> em parâmetros — mais flexível\npublic void Imprimir(IEnumerable<string> itens)\n{\n    foreach (var item in itens)\n        Console.WriteLine(item);\n}\n\nImprimir(nomes);     // List<string>\nImprimir(outroArray); // string[]\n```"
            },

            new Licao
            {
                Id = 7, ModuloId = 3, Ordem = 2, XPRecompensa = 25, Ativo = true,
                Titulo = "LINQ Básico",
                Descricao = "Where, Select, OrderBy, First vs FirstOrDefault, Any/All e uso com EF Core",
                ConteudoTeoricoMarkdown =
                    "## LINQ — Language Integrated Query\n\n" +
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
    }
}
