using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class ExpandirConteudoSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Licoes",
                columns: new[] { "Id", "Ativo", "ConteudoTeoricoMarkdown", "Descricao", "ModuloId", "Ordem", "Titulo", "XPRecompensa" },
                values: new object[,]
                {
                    { 10, true, "## Encapsulamento\n\nEncapsulamento esconde os detalhes internos e expõe apenas o necessário.\n\n### Modificadores de acesso\n```csharp\npublic class ContaBancaria\n{\n    private decimal _saldo;           // só a classe acessa\n    protected string Titular { get; } // classe + herdeiras\n    internal int AgenciaId { get; }   // mesmo assembly\n    public decimal Saldo => _saldo;   // leitura pública, escrita privada\n\n    public void Depositar(decimal valor)\n    {\n        if (valor <= 0) throw new ArgumentException(\"Valor inválido\");\n        _saldo += valor;\n    }\n\n    public bool Sacar(decimal valor)\n    {\n        if (valor > _saldo) return false;\n        _saldo -= valor;\n        return true;\n    }\n}\n```\n\n### Propriedades com validação\n```csharp\npublic class Produto\n{\n    private decimal _preco;\n\n    public decimal Preco\n    {\n        get => _preco;\n        set\n        {\n            if (value < 0) throw new ArgumentException(\"Preço não pode ser negativo\");\n            _preco = value;\n        }\n    }\n}\n```\n\n### init — propriedade somente para construção\n```csharp\npublic class Pedido\n{\n    public int Id { get; init; }     // só pode ser definido no new {}\n    public DateTime Criado { get; } = DateTime.UtcNow;\n}\n\nvar pedido = new Pedido { Id = 42 }; // OK\n// pedido.Id = 1;                    // ERRO em tempo de compilação\n```", "Modificadores de acesso, propriedades e princípio do encapsulamento", 2, 3, "Encapsulamento", 20 },
                    { 11, true, "## LINQ Avançado\n\n### GroupBy — agrupa por chave\n```csharp\nvar pedidos = new List<Pedido> { ... };\n\nvar porCliente = pedidos\n    .GroupBy(p => p.ClienteId)\n    .Select(g => new {\n        ClienteId = g.Key,\n        Total = g.Sum(p => p.Valor),\n        Quantidade = g.Count()\n    });\n```\n\n### Join — combina duas coleções\n```csharp\nvar resultado = clientes.Join(\n    pedidos,\n    c => c.Id,\n    p => p.ClienteId,\n    (c, p) => new { c.Nome, p.Valor }\n);\n```\n\n### Aggregate — redução customizada\n```csharp\nvar numeros = new[] { 1, 2, 3, 4, 5 };\n\n// Equivalente a um fold/reduce\nvar produto = numeros.Aggregate(1, (acc, n) => acc * n); // 120\nvar frase = new[] { \"C#\", \"é\", \"incrível\" }\n    .Aggregate((a, b) => $\"{a} {b}\"); // \"C# é incrível\"\n```\n\n### Any, All, Contains\n```csharp\nvar tem18 = idades.Any(i => i >= 18);\nvar todosMaiores = idades.All(i => i >= 0);\nvar temDez = numeros.Contains(10);\n```\n\n### Paginação\n```csharp\nint pagina = 2, tamanho = 10;\nvar paginado = produtos\n    .OrderBy(p => p.Nome)\n    .Skip((pagina - 1) * tamanho)\n    .Take(tamanho)\n    .ToList();\n```", "GroupBy, Join, Aggregate e projeções complexas", 3, 3, "LINQ Avançado", 25 },
                    { 12, true, "## CancellationToken\n\nPermite cancelar operações assíncronas de forma cooperativa (sem matar threads à força).\n\n### Criando e passando o token\n```csharp\nusing var cts = new CancellationTokenSource();\ncts.CancelAfter(TimeSpan.FromSeconds(5)); // cancela após 5s\n\ntry\n{\n    var resultado = await BuscarDadosAsync(cts.Token);\n}\ncatch (OperationCanceledException)\n{\n    Console.WriteLine(\"Operação cancelada!\");\n}\n```\n\n### Recebendo e respeitando o token\n```csharp\npublic async Task<string> BuscarDadosAsync(CancellationToken ct = default)\n{\n    // Verifica antes de operações longas\n    ct.ThrowIfCancellationRequested();\n\n    await Task.Delay(2000, ct); // Task.Delay já respeita o token\n\n    return \"dados\";\n}\n```\n\n### Em ASP.NET Core\nOs controllers recebem o token automaticamente:\n```csharp\n[HttpGet]\npublic async Task<IActionResult> Get(CancellationToken ct)\n{\n    var dados = await _repo.ObterTodosAsync(ct);\n    return Ok(dados);\n}\n```\nSe o cliente desconectar, `ct` é cancelado automaticamente.", "Cancelamento cooperativo de operações assíncronas", 4, 2, "CancellationToken", 30 },
                    { 13, true, "## Paralelismo com Tasks\n\n### Task.WhenAll — aguarda TODAS as tasks\n```csharp\n// Sem paralelismo (sequencial — lento)\nvar u1 = await BuscarUsuarioAsync(1); // espera\nvar u2 = await BuscarUsuarioAsync(2); // espera\n\n// Com paralelismo — muito mais rápido!\nvar task1 = BuscarUsuarioAsync(1);\nvar task2 = BuscarUsuarioAsync(2);\nvar (u1, u2) = await (task1, task2); // ambas simultâneas\n\n// Ou com array:\nvar ids = new[] { 1, 2, 3, 4 };\nvar usuarios = await Task.WhenAll(ids.Select(id => BuscarUsuarioAsync(id)));\n```\n\n### Task.WhenAny — aguarda a PRIMEIRA a completar\n```csharp\n// Cache race: quem responder primeiro vence\nvar taskBanco = BuscarNoBancoAsync(id);\nvar taskCache = BuscarNoCacheAsync(id);\n\nvar primeira = await Task.WhenAny(taskBanco, taskCache);\nvar resultado = await primeira; // garante exceções propagadas\n```\n\n### Cuidados\n```csharp\n// Task.WhenAll propaga TODAS as exceções\ntry\n{\n    await Task.WhenAll(task1, task2, task3);\n}\ncatch (Exception ex)\n{\n    // ex contém apenas a primeira — inspecione task1.Exception, etc.\n}\n```", "Execução paralela de múltiplas tasks assíncronas", 4, 3, "Task.WhenAll e Task.WhenAny", 30 },
                    { 14, true, "## Factory Pattern\n\nEncapsula a criação de objetos, permitindo que o código cliente não conheça as classes concretas.\n\n### Simple Factory\n```csharp\npublic abstract class Notificacao\n{\n    public abstract void Enviar(string mensagem);\n}\n\npublic class NotificacaoEmail : Notificacao\n{\n    public override void Enviar(string msg) => Console.WriteLine($\"Email: {msg}\");\n}\n\npublic class NotificacaoSms : Notificacao\n{\n    public override void Enviar(string msg) => Console.WriteLine($\"SMS: {msg}\");\n}\n\npublic static class NotificacaoFactory\n{\n    public static Notificacao Criar(string tipo) => tipo switch\n    {\n        \"email\" => new NotificacaoEmail(),\n        \"sms\"   => new NotificacaoSms(),\n        _ => throw new ArgumentException($\"Tipo desconhecido: {tipo}\")\n    };\n}\n\n// Uso — o cliente não instancia diretamente\nvar notif = NotificacaoFactory.Criar(\"email\");\nnotif.Enviar(\"Pedido confirmado!\");\n```\n\n### Por que usar?\n- O tipo concreto pode mudar sem alterar o código cliente\n- Centraliza a lógica de criação (configurações, validações)\n- Facilita testes (pode injetar factory mockada)", "Criando objetos sem expor a lógica de construção", 5, 2, "Factory Pattern", 35 },
                    { 15, true, "## Singleton Pattern\n\nGarante que uma classe tenha apenas uma instância durante toda a vida da aplicação.\n\n### Implementação thread-safe com Lazy<T>\n```csharp\npublic sealed class ConfiguracaoApp\n{\n    // Lazy<T> garante thread-safety e lazy initialization\n    private static readonly Lazy<ConfiguracaoApp> _instancia =\n        new(() => new ConfiguracaoApp());\n\n    public static ConfiguracaoApp Instancia => _instancia.Value;\n\n    public string Ambiente { get; private set; }\n    public string ConnectionString { get; private set; }\n\n    private ConfiguracaoApp()\n    {\n        Ambiente = Environment.GetEnvironmentVariable(\"ASPNETCORE_ENVIRONMENT\") ?? \"Development\";\n        ConnectionString = \"Data Source=app.db\";\n    }\n}\n\n// Uso\nvar config = ConfiguracaoApp.Instancia;\nConsole.WriteLine(config.Ambiente);\n```\n\n### Em ASP.NET Core — prefira o container de DI\n```csharp\n// Program.cs — AddSingleton registra uma única instância\nbuilder.Services.AddSingleton<ICacheService, MemoryCacheService>();\n\n// O container garante a unicidade e facilita os testes\n// (diferente do Singleton estático que dificulta mock)\n```\n\n### Quando usar?\n- Configurações globais, loggers, conexões com recursos únicos\n- **Evite** para estado mutável compartilhado — causa bugs em concorrência", "Garantindo uma única instância com thread safety", 5, 3, "Singleton Pattern", 35 }
                });

            migrationBuilder.InsertData(
                table: "Exercicios",
                columns: new[] { "Id", "Enunciado", "Explicacao", "LicaoId", "OpcoesJson", "Ordem", "RespostaCorreta", "Tipo", "XPRecompensa" },
                values: new object[,]
                {
                    { 28, "Qual modificador de acesso permite que membros sejam acessados apenas pela classe e suas subclasses?", "'protected' é visível dentro da classe e em qualquer classe que herde dela. Ideal para membros que fazem parte do contrato de herança.", 10, "[\"protected\",\"private\",\"internal\",\"public\"]", 1, "protected", 1, 5 },
                    { 29, "Uma propriedade com 'get' público e 'set' privado pode ser alterada de fora da classe.", "Com 'private set', apenas a própria classe pode modificar o valor. Isso é encapsulamento: expõe leitura mas protege escrita.", 10, "[\"Verdadeiro\",\"Falso\"]", 2, "Falso", 2, 5 },
                    { 30, "Para criar uma propriedade que só pode ser definida durante a inicialização do objeto, usa-se o acessor: ____", "'init' (C# 9+) permite definir o valor apenas no inicializador de objeto '{ }'. Depois disso, a propriedade torna-se somente leitura.", 10, "[]", 3, "init", 3, 5 },
                    { 31, "Qual método LINQ agrupa elementos por uma chave?", "GroupBy agrupa elementos em subconjuntos por uma chave. Retorna IEnumerable<IGrouping<TKey, TElement>>.", 11, "[\"GroupBy\",\"Join\",\"Aggregate\",\"Partition\"]", 1, "GroupBy", 1, 5 },
                    { 32, "O método Aggregate pode ser usado para concatenar strings de uma lista.", "Aggregate é uma redução genérica. Pode somar, multiplicar, concatenar ou qualquer operação que combine dois elementos em um.", 11, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 33, "Para pegar apenas os primeiros N elementos de uma coleção em LINQ, usa-se o método: ____", "Take(n) retorna os primeiros n elementos. Combinado com Skip, implementa paginação: Skip((page-1)*size).Take(size).", 11, "[]", 3, "Take", 3, 5 },
                    { 34, "Qual classe é usada para criar e controlar um CancellationToken?", "CancellationTokenSource cria o token e expõe o método Cancel(). O token em si (CancellationToken) é passado para as operações que devem respeitar o cancelamento.", 12, "[\"CancellationTokenSource\",\"TaskCanceller\",\"CancellationController\",\"TokenFactory\"]", 1, "CancellationTokenSource", 1, 5 },
                    { 35, "Quando um CancellationToken é cancelado, Task.Delay lança automaticamente uma OperationCanceledException.", "A maioria dos métodos async do .NET aceita CancellationToken e lança OperationCanceledException quando cancelado. Isso inclui Task.Delay, HttpClient, EF Core, etc.", 12, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 36, "Para verificar manualmente se um token foi cancelado e lançar exceção, usa-se: ct.____IfCancellationRequested()", "ThrowIfCancellationRequested() verifica ct.IsCancellationRequested e lança OperationCanceledException se verdadeiro. Use no início de loops ou operações longas.", 12, "[]", 3, "Throw", 3, 5 },
                    { 37, "Qual método aguarda a conclusão de TODAS as tasks de um array?", "Task.WhenAll é assíncrono (retorna Task) e aguarda todas. Task.WaitAll bloqueia a thread atual — evite em código async.", 13, "[\"Task.WhenAll\",\"Task.WhenAny\",\"Task.WaitAll\",\"Task.RunAll\"]", 1, "Task.WhenAll", 1, 5 },
                    { 38, "Task.WhenAny completa assim que QUALQUER uma das tasks terminar, mesmo que as outras ainda estejam rodando.", "WhenAny retorna a primeira task que completar (com sucesso ou erro). As outras continuam executando em background.", 13, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 39, "Para executar múltiplas tasks em paralelo e aguardar todas, usamos: await Task.____( task1, task2 )", "Task.WhenAll(task1, task2) inicia ambas simultaneamente e retorna quando as duas terminam. Muito mais eficiente que await task1; await task2 em sequência.", 13, "[]", 3, "WhenAll", 3, 5 },
                    { 40, "Qual é o principal objetivo do Factory Pattern?", "Factory encapsula 'como' criar objetos. O cliente recebe um produto sem conhecer a classe concreta — depende apenas de uma abstração.", 14, "[\"Encapsular a criação de objetos\",\"Garantir uma única instância\",\"Separar interface de implementação\",\"Observar mudanças de estado\"]", 1, "Encapsular a criação de objetos", 1, 5 },
                    { 41, "No Factory Pattern, o código cliente precisa conhecer todas as classes concretas que pode receber.", "Esse é justamente o benefício: o cliente depende apenas da abstração (interface ou classe base). A factory decide qual concreto instanciar.", 14, "[\"Verdadeiro\",\"Falso\"]", 2, "Falso", 2, 5 },
                    { 42, "Um método estático que cria e retorna instâncias de diferentes subclasses é chamado de ____ Factory.", "Simple Factory é o padrão mais básico: um método (geralmente estático) que decide qual classe concreta instanciar com base em um parâmetro.", 14, "[]", 3, "Simple", 3, 5 },
                    { 43, "Qual classe do .NET garante inicialização lazy e thread-safe de um Singleton?", "Lazy<T> inicializa o valor apenas na primeira vez que .Value é acessado, de forma thread-safe. É a forma preferida de implementar Singleton em C#.", 15, "[\"Lazy<T>\",\"ThreadLocal<T>\",\"Concurrent<T>\",\"Once<T>\"]", 1, "Lazy<T>", 1, 5 },
                    { 44, "Em ASP.NET Core, AddSingleton registra uma instância que é criada uma única vez e reutilizada em toda a aplicação.", "AddSingleton = uma instância por aplicação. AddScoped = uma por requisição HTTP. AddTransient = uma nova a cada injeção.", 15, "[\"Verdadeiro\",\"Falso\"]", 2, "Verdadeiro", 2, 5 },
                    { 45, "Para impedir que uma classe Singleton seja herdada, usa-se o modificador: ____", "'sealed' impede herança. É recomendado em Singletons para garantir que ninguém possa criar subclasses que quebrem a unicidade da instância.", 15, "[]", 3, "sealed", 3, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
