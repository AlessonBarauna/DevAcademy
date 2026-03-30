using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedExerciciosEProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exercicios",
                columns: new[] { "Id", "Enunciado", "Explicacao", "LicaoId", "OpcoesJson", "Ordem", "RespostaCorreta", "Tipo", "XPRecompensa" },
                values: new object[,]
                {
                    { 1, "Qual palavra-chave permite que o compilador C# infira o tipo de uma variável automaticamente?", "'var' é uma palavra-chave de inferência de tipo em tempo de compilação. O tipo é definido pelo compilador com base no valor atribuído.", 1, "[\"var\",\"dynamic\",\"object\",\"auto\"]", 1, "var", 1, 5 },
                    { 2, "Em C#, 'string' e 'String' representam o mesmo tipo.", "'string' é um alias da linguagem para 'System.String'. Ambos são idênticos e intercambiáveis.", 1, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 3, "Para declarar um valor que não pode ser alterado em C#, usa-se a palavra-chave: ____", "'const' declara uma constante em tempo de compilação. Diferente de 'readonly', que é resolvida em tempo de execução.", 1, "[]", 3, "const", 3, 5 },
                    { 4, "Qual estrutura é mais adequada em C# para comparar uma variável contra múltiplos valores constantes?", "'switch' é otimizado para comparação de um valor contra constantes. A switch expression do C# 8+ torna ainda mais conciso.", 2, "[\"switch\",\"if/else\",\"while\",\"do/while\"]", 1, "switch", 1, 5 },
                    { 5, "É possível modificar os elementos de uma coleção durante uma iteração com foreach.", "foreach não permite modificar a coleção durante a iteração. Para isso, use um loop for convencional ou crie uma nova lista.", 2, "[\"Verdadeiro\",\"Falso\"]", 2, "Falso", 2, 5 },
                    { 6, "O operador ternário em C# segue a sintaxe: condição ____ valorTrue : valorFalse", "O operador ternário usa '?' para separar a condição do valor verdadeiro, e ':' para separar os dois valores.", 2, "[]", 3, "?", 3, 5 },
                    { 7, "Qual modificador de acesso restringe um método ao escopo da própria classe?", "'private' limita o acesso ao membro apenas dentro da classe que o declara. É o modificador mais restritivo.", 3, "[\"private\",\"public\",\"protected\",\"internal\"]", 1, "private", 1, 5 },
                    { 8, "Em C# é possível ter dois métodos com o mesmo nome desde que tenham assinaturas diferentes (sobrecarga).", "Sobrecarga (overloading) permite múltiplos métodos com o mesmo nome, diferenciados pelo número ou tipo de parâmetros.", 3, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 9, "Um método que não retorna nenhum valor usa o tipo de retorno: ____", "'void' indica ausência de retorno. Para métodos assíncronos sem retorno, usa-se 'Task' no lugar de 'async void'.", 3, "[]", 3, "void", 3, 5 },
                    { 10, "O que é um construtor em C#?", "O construtor tem o mesmo nome da classe, não tem tipo de retorno e é chamado automaticamente ao instanciar o objeto com 'new'.", 4, "[\"Método especial que inicializa uma instância da classe\",\"Uma propriedade obrigatória\",\"Um tipo de herança\",\"Uma interface implícita\"]", 1, "Método especial que inicializa uma instância da classe", 1, 5 },
                    { 11, "Uma classe pode ter múltiplos construtores com parâmetros diferentes.", "Isso é sobrecarga de construtor. É comum ter um construtor padrão (sem parâmetros) e outros construtores com parâmetros.", 4, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 12, "Para criar uma nova instância de uma classe chamada Carro, usamos: var carro = ____ Carro();", "'new' aloca memória e chama o construtor da classe para inicializar o objeto.", 4, "[]", 3, "new", 3, 5 },
                    { 13, "Qual símbolo é usado em C# para indicar que uma classe herda de outra?", "Em C# usamos ':' para herança (e também para implementar interfaces). Ex: 'public class Cachorro : Animal'.", 5, "[\":\",\"extends\",\"inherits\",\"->\"]", 1, ":", 1, 5 },
                    { 14, "Em C#, uma classe pode herdar de múltiplas classes ao mesmo tempo.", "C# não suporta herança múltipla de classes. Para comportamento múltiplo, usa-se interfaces, que podem ser implementadas em quantidade ilimitada.", 5, "[\"Verdadeiro\",\"Falso\"]", 2, "Falso", 2, 5 },
                    { 15, "Para sobrescrever um método virtual na subclasse, usa-se a palavra-chave: ____", "'override' indica que o método substitui a implementação da classe base marcada com 'virtual' ou 'abstract'.", 5, "[]", 3, "override", 3, 5 },
                    { 16, "Qual é a principal diferença entre Array e List<T> em C#?", "Arrays têm tamanho definido na criação. List<T> cresce dinamicamente com Add() e encolhe com Remove().", 6, "[\"List<T> tem tamanho dinâmico, Array tem tamanho fixo\",\"Array é mais seguro que List<T>\",\"List<T> não aceita tipos genéricos\",\"São equivalentes em todos os aspectos\"]", 1, "List<T> tem tamanho dinâmico, Array tem tamanho fixo", 1, 5 },
                    { 17, "O método List<T>.Add() adiciona elementos ao início da lista.", "Add() adiciona ao FINAL da lista. Para adicionar no início ou em posição específica, use Insert(index, item).", 6, "[\"Verdadeiro\",\"Falso\"]", 2, "Falso", 2, 5 },
                    { 18, "Para obter a quantidade de elementos em uma List<T>, usa-se a propriedade: ____", "List<T>.Count retorna o número de elementos. Arrays usam .Length. LINQ usa .Count() (método de extensão).", 6, "[]", 3, "Count", 3, 5 },
                    { 19, "Qual método LINQ retorna o primeiro elemento que satisfaz uma condição e lança exceção se não encontrar?", "First() lança InvalidOperationException se não houver elemento. Use FirstOrDefault() para retornar null/default quando não encontrar.", 7, "[\"First()\",\"Select()\",\"Where()\",\"Take()\"]", 1, "First()", 1, 5 },
                    { 20, "LINQ só funciona com coleções em memória (LINQ to Objects).", "LINQ funciona com qualquer IQueryable<T> ou IEnumerable<T>: banco de dados via EF Core (LINQ to SQL), XML, arquivos, etc.", 7, "[\"Verdadeiro\",\"Falso\"]", 2, "Falso", 2, 5 },
                    { 21, "Para filtrar elementos em LINQ com uma condição, usa-se o método: ____", "Where() filtra elementos que satisfazem um predicado. Retorna IEnumerable<T> com os elementos que passaram no filtro.", 7, "[]", 3, "Where", 3, 5 },
                    { 22, "O que acontece quando a runtime encontra um 'await' em um método assíncrono?", "await suspende o método atual sem bloquear a thread. A thread retorna ao pool e pode processar outras requisições, melhorando a escalabilidade.", 8, "[\"A thread é liberada para outras tarefas enquanto aguarda\",\"O programa é pausado completamente\",\"Uma nova thread é criada automaticamente\",\"O método é cancelado\"]", 1, "A thread é liberada para outras tarefas enquanto aguarda", 1, 5 },
                    { 23, "Todo método que usa 'await' deve ter 'async' em sua declaração.", "'async' e 'await' são inseparáveis. 'async' marca o método como assíncrono e habilita o uso de 'await' dentro dele.", 8, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 24, "Um método assíncrono que não retorna valor deve ter tipo de retorno: ____", "Use 'Task' (não 'async void') para métodos assíncronos sem retorno. 'async void' não pode ser aguardado e dificulta o tratamento de exceções.", 8, "[]", 3, "Task", 3, 5 },
                    { 25, "Qual é o principal benefício do Repository Pattern?", "O Repository isola 'como' os dados são obtidos (SQL, EF, API externa) da lógica que decide 'o que' fazer com eles. Facilita testes e troca de infraestrutura.", 9, "[\"Desacoplar a lógica de negócio do acesso a dados\",\"Aumentar a velocidade das queries\",\"Reduzir a quantidade de código\",\"Substituir completamente o ORM\"]", 1, "Desacoplar a lógica de negócio do acesso a dados", 1, 5 },
                    { 26, "No Repository Pattern, um Controller pode acessar o DbContext diretamente quando precisar de performance.", "Controllers NUNCA devem acessar infraestrutura diretamente. Se precisar de performance, otimize o Repository (projeções, AsNoTracking, paginação).", 9, "[\"Verdadeiro\",\"Falso\"]", 2, "Falso", 2, 5 },
                    { 27, "Em Clean Architecture, o Controller deve depender da ____ do Repository, não da implementação concreta.", "Dependência de abstrações (interfaces) é o Dependency Inversion Principle (D do SOLID). Permite trocar SQLite por SQL Server sem alterar o Controller.", 9, "[]", 3, "interface", 3, 5 }
                });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConteudoTeoricoMarkdown",
                value: "## Variáveis em C#\n\nC# é uma linguagem fortemente tipada. Cada variável possui um tipo definido em tempo de compilação.\n\n### Tipos primitivos\n```csharp\nint idade = 25;\ndouble salario = 4500.50;\nbool ativo = true;\nstring nome = \"João\";\n```\n\n### Inferência de tipo com var\nA palavra-chave `var` permite que o compilador infira o tipo:\n```csharp\nvar cidade = \"São Paulo\"; // string\nvar populacao = 12_000_000; // int\n```\n\n### Constantes\n```csharp\nconst double PI = 3.14159;\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConteudoTeoricoMarkdown",
                value: "## Controle de Fluxo\n\n### if/else\n```csharp\nif (nota >= 7)\n    Console.WriteLine(\"Aprovado\");\nelse\n    Console.WriteLine(\"Reprovado\");\n```\n\n### switch expression (C# moderno)\n```csharp\nvar resultado = nota switch\n{\n    >= 9 => \"A\",\n    >= 7 => \"B\",\n    >= 5 => \"C\",\n    _ => \"D\"\n};\n```\n\n### foreach\n```csharp\nvar nomes = new[] { \"Ana\", \"Bruno\", \"Carlos\" };\nforeach (var nome in nomes)\n    Console.WriteLine(nome);\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConteudoTeoricoMarkdown",
                value: "## Métodos em C#\n\n### Declaração básica\n```csharp\npublic int Somar(int a, int b) => a + b;\n```\n\n### Parâmetros opcionais\n```csharp\npublic string Saudar(string nome, string prefixo = \"Olá\")\n    => $\"{prefixo}, {nome}!\";\n```\n\n### Sobrecarga\nDois métodos com o mesmo nome mas parâmetros diferentes:\n```csharp\npublic double Calcular(double x) => x * x;\npublic double Calcular(double x, double y) => x * y;\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConteudoTeoricoMarkdown",
                value: "## Classes em C#\n\n```csharp\npublic class Produto\n{\n    public int Id { get; set; }\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n\n    public Produto(string nome, decimal preco)\n    {\n        Nome = nome;\n        Preco = preco;\n    }\n\n    public string Descricao() => $\"{Nome} - R$ {Preco:F2}\";\n}\n\n// Uso:\nvar produto = new Produto(\"Notebook\", 3500m);\nConsole.WriteLine(produto.Descricao());\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConteudoTeoricoMarkdown",
                value: "## Herança\n\n```csharp\npublic class Animal\n{\n    public string Nome { get; set; } = string.Empty;\n    public virtual string EmitirSom() => \"...\";\n}\n\npublic class Cachorro : Animal\n{\n    public override string EmitirSom() => \"Au au!\";\n}\n```\n\n## Interfaces\n```csharp\npublic interface IVoador\n{\n    void Voar();\n}\n\npublic class Passaro : Animal, IVoador\n{\n    public override string EmitirSom() => \"Piu piu!\";\n    public void Voar() => Console.WriteLine(\"Voando...\");\n}\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConteudoTeoricoMarkdown",
                value: "## Coleções em C#\n\n### Array (tamanho fixo)\n```csharp\nint[] numeros = { 1, 2, 3, 4, 5 };\nConsole.WriteLine(numeros.Length); // 5\n```\n\n### List<T> (tamanho dinâmico)\n```csharp\nvar nomes = new List<string> { \"Ana\", \"Bruno\" };\nnomes.Add(\"Carlos\");\nnomes.Remove(\"Ana\");\nConsole.WriteLine(nomes.Count); // 2\n```\n\n### Dictionary<TKey, TValue>\n```csharp\nvar capitais = new Dictionary<string, string>\n{\n    [\"SP\"] = \"São Paulo\",\n    [\"RJ\"] = \"Rio de Janeiro\"\n};\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConteudoTeoricoMarkdown",
                value: "## LINQ — Language Integrated Query\n\n```csharp\nvar numeros = new List<int> { 5, 3, 8, 1, 9, 2 };\n\n// Filtrar\nvar pares = numeros.Where(n => n % 2 == 0).ToList();\n\n// Transformar\nvar dobrados = numeros.Select(n => n * 2).ToList();\n\n// Ordenar\nvar ordenados = numeros.OrderBy(n => n).ToList();\n\n// Primeiro elemento\nvar maior = numeros.Max();\nvar primeiro = numeros.First(n => n > 5);\n\n// Encadeamento\nvar resultado = numeros\n    .Where(n => n > 3)\n    .OrderByDescending(n => n)\n    .Select(n => $\"Número: {n}\")\n    .ToList();\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConteudoTeoricoMarkdown",
                value: "## Programação Assíncrona\n\nasync/await permite executar operações sem bloquear a thread principal.\n\n```csharp\npublic async Task<string> BuscarDadosAsync(int id)\n{\n    // Simula chamada a banco/API\n    await Task.Delay(100);\n    return $\"Dados do id {id}\";\n}\n\n// Chamando:\nvar dados = await BuscarDadosAsync(42);\n```\n\n### Múltiplas tasks em paralelo\n```csharp\nvar task1 = BuscarDadosAsync(1);\nvar task2 = BuscarDadosAsync(2);\n\nvar resultados = await Task.WhenAll(task1, task2);\n```\n\n### Método sem retorno\n```csharp\npublic async Task SalvarAsync(string dados)\n{\n    await File.WriteAllTextAsync(\"arquivo.txt\", dados);\n}\n```");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConteudoTeoricoMarkdown",
                value: "## Repository Pattern\n\nIsola a lógica de acesso a dados da lógica de negócio.\n\n```csharp\n// 1. Interface — define o contrato\npublic interface IProdutoRepository\n{\n    Task<Produto?> ObterPorIdAsync(int id);\n    Task<IEnumerable<Produto>> ObterTodosAsync();\n    Task AdicionarAsync(Produto produto);\n    Task<bool> SalvarAsync();\n}\n\n// 2. Implementação — detalhe de infraestrutura\npublic class ProdutoRepository(AppDbContext ctx) : IProdutoRepository\n{\n    public async Task<Produto?> ObterPorIdAsync(int id)\n        => await ctx.Produtos.FindAsync(id);\n\n    public async Task AdicionarAsync(Produto produto)\n        => await ctx.Produtos.AddAsync(produto);\n\n    public async Task<bool> SalvarAsync()\n        => await ctx.SaveChangesAsync() > 0;\n}\n\n// 3. Controller — depende da interface, nunca da implementação\npublic class ProdutoController(IProdutoRepository repo) : ControllerBase { }\n```");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConteudoTeoricoMarkdown",
                value: "## Variáveis em C#\n\nC# é uma linguagem fortemente tipada...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConteudoTeoricoMarkdown",
                value: "## Controle de Fluxo\n\nDecisões e repetições em C#...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConteudoTeoricoMarkdown",
                value: "## Métodos\n\nMétodos encapsulam comportamento...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConteudoTeoricoMarkdown",
                value: "## Classes em C#\n\nClasses são moldes para objetos...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConteudoTeoricoMarkdown",
                value: "## Herança\n\nReutilização de código através de herança...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConteudoTeoricoMarkdown",
                value: "## Coleções\n\nListas são coleções dinâmicas...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConteudoTeoricoMarkdown",
                value: "## LINQ\n\nLanguage Integrated Query permite consultas...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConteudoTeoricoMarkdown",
                value: "## Programação Assíncrona\n\nasync/await simplifica código assíncrono...");

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConteudoTeoricoMarkdown",
                value: "## Repository Pattern\n\nO padrão Repository isola a lógica de acesso a dados...");
        }
    }
}
