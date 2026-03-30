using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class AprofundarConteudoLicoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual palavra-chave instrui o compilador C# a inferir o tipo de uma variável pelo valor atribuído?", "'var' é inferência de tipo em TEMPO DE COMPILAÇÃO — o tipo é fixado pelo compilador. 'dynamic' adia a checagem para o runtime. 'var' e o tipo explícito geram IL idêntico.", "[\"var\",\"dynamic\",\"object\",\"type\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Em C#, 'string' (minúsculo) e 'String' (maiúsculo) são o mesmo tipo.", "'string' é um alias da linguagem para 'System.String'. São 100% intercambiáveis — geram o mesmo IL. Por convenção, use 'string' no código e 'String' apenas ao referenciar membros estáticos como String.IsNullOrEmpty()." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para declarar um valor que não pode ser alterado e é resolvido em tempo de compilação, usa-se: ____", "'const' é gravado diretamente no IL em tempo de compilação e nunca muda. Diferente de 'readonly', que é definida em tempo de execução (no construtor) e pode variar por instância." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual estrutura é mais adequada para comparar uma variável contra múltiplos valores constantes em C#?", "'switch' (e a switch expression moderna) é otimizado pelo compilador para comparar um valor contra constantes. O código fica mais legível e performático do que uma cadeia de if/else para esse propósito.", "[\"switch\",\"if/else encadeado\",\"while\",\"do/while\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Modificar a coleção (adicionar ou remover elementos) dentro de um foreach é permitido em C#.", "Modificar a coleção durante iteração com foreach lança InvalidOperationException. O enumerador detecta que a coleção foi alterada. Para remover durante iteração, use um for reverso ou filtre para uma nova lista com LINQ." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "O operador ternário retorna um valor com base em uma condição. Sua sintaxe é: condição ____ valorVerdadeiro : valorFalso", "O operador ternário usa '?' para separar a condição do valor verdadeiro e ':' para separar os dois valores. Ex: string r = nota >= 7 ? \"Aprovado\" : \"Reprovado\". É equivalente a um if/else que retorna valor." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Qual modificador de acesso restringe um método ao escopo da própria classe, sendo invisível para qualquer código externo?", "'private' é o mais restritivo: o membro só é acessível dentro da própria classe. É o padrão para membros de classe que não fazem parte da API pública. A regra de ouro: use o modificador mais restritivo possível." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Dois métodos com o mesmo nome mas parâmetros diferentes na mesma classe configuram uma sobrecarga (overloading).", "Sobrecarga (overloading) permite múltiplos métodos com o mesmo nome, diferenciados por tipo, quantidade ou ordem de parâmetros. O compilador escolhe a versão correta em tempo de compilação com base nos argumentos." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 9,
                column: "Explicacao",
                value: "'void' declara ausência de retorno. Em métodos assíncronos sem retorno, use 'Task' em vez de 'async void' — Task pode ser aguardado e propaga exceções corretamente, enquanto 'async void' não.");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Explicacao", "OpcoesJson" },
                values: new object[] { "O construtor tem o mesmo nome da classe, não tem tipo de retorno e é chamado automaticamente pelo operador 'new'. Seu objetivo é colocar o objeto em um estado válido desde o início.", "[\"Método especial que inicializa uma instância da classe\",\"Uma propriedade obrigatória\",\"Um tipo de herança\",\"Um método estático de fábrica\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Uma classe pode ter múltiplos construtores, desde que tenham assinaturas diferentes (quantidade ou tipos de parâmetros).", "Isso é sobrecarga de construtores. É comum ter um construtor padrão (sem parâmetros) e outros com diferentes combinações. O operador 'new' escolhe qual construtor chamar pelos argumentos fornecidos." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para criar uma instância de uma classe chamada Produto no C#, usa-se: var p = ____ Produto();", "'new' aloca memória no heap, executa o construtor e retorna uma referência ao novo objeto. Sem 'new', a variável ficaria null (referência nula) ou causaria erro de compilação." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Em C#, qual símbolo é usado para indicar que uma classe herda de outra?", "C# usa ':' tanto para herança de classe quanto para implementação de interfaces. Ex: 'public class Cachorro : Animal, IVoador'. Java usa 'extends' para classes e 'implements' para interfaces — C# unifica com ':'." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Em C#, uma classe pode herdar diretamente de múltiplas classes ao mesmo tempo.", "C# não suporta herança múltipla de classes — uma classe só pode herdar de uma única classe. Para modelar comportamentos múltiplos, usa-se interfaces: uma classe pode implementar quantas interfaces precisar." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para substituir a implementação de um método virtual na subclasse, usa-se a palavra-chave: ____", "'override' na subclasse substitui a implementação do método marcado com 'virtual' ou 'abstract' na classe base. Sem 'override', você estaria criando um novo método que oculta (hides) o da base, não sobrescrevendo." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "Array define o tamanho na criação e não pode mudar. List<T> cresce com Add() e encolhe com Remove() automaticamente. Arrays usam .Length; List<T> usa .Count.", "[\"List<T> tem tamanho dinâmico; Array tem tamanho fixo\",\"Array é sempre mais rápido\",\"List<T> não aceita tipos primitivos\",\"São equivalentes em todos os aspectos\"]", "List<T> tem tamanho dinâmico; Array tem tamanho fixo" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Enunciado", "Explicacao", "RespostaCorreta" },
                values: new object[] { "O método List<T>.Add() adiciona o elemento no FINAL da lista.", "Add() sempre insere no final da lista — é O(1) amortizado. Para inserir em uma posição específica, use Insert(índice, item). Para inserir no início, use Insert(0, item), que é O(n) pois desloca todos os elementos.", "Verdadeiro" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para obter o número de elementos em uma List<T>, usa-se a propriedade: ____", "List<T>.Count retorna o número atual de elementos. Arrays usam .Length. O método de extensão LINQ .Count() também funciona em qualquer IEnumerable<T>, mas .Count a propriedade é O(1) enquanto .Count() método pode ser O(n)." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "Qual é a diferença entre First() e FirstOrDefault() no LINQ quando nenhum elemento satisfaz a condição?", "First() garante que existe pelo menos um elemento — lança exceção se não houver. FirstOrDefault() é a versão segura: retorna null para tipos referência ou o valor padrão (0, false) para tipos de valor. Use FirstOrDefault() quando a ausência é um caso válido.", "[\"First() lança InvalidOperationException; FirstOrDefault() retorna null/default\",\"First() retorna null; FirstOrDefault() lança exceção\",\"São idênticos em comportamento\",\"First() retorna o primeiro sem filtro; FirstOrDefault() filtra\"]", "First() lança InvalidOperationException; FirstOrDefault() retorna null/default" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "LINQ funciona apenas com coleções em memória (List, Array etc.).", "LINQ funciona com qualquer IEnumerable<T> (em memória) ou IQueryable<T> (traducível para outras fontes). Via EF Core, o LINQ é traduzido para SQL e executado diretamente no banco de dados — não carrega todos os dados em memória para filtrar." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para filtrar elementos de uma coleção com base em uma condição em LINQ, usa-se o método: ____", "Where(predicado) filtra os elementos que satisfazem a condição (predicado retorna true) e retorna IEnumerable<T>. Não modifica a coleção original. Ex: numeros.Where(n => n > 5) retorna apenas os maiores que 5." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "O que acontece com a thread quando o runtime encontra um 'await' em um método assíncrono?", "'await' suspende o método atual sem bloquear a thread. A thread retorna ao pool e pode processar outras requisições. Quando a operação completa, o método é retomado (possivelmente em outra thread). Isso permite que um servidor atenda milhares de requisições concorrentes com poucas threads.", "[\"A thread é liberada para atender outras tarefas enquanto aguarda\",\"O programa é pausado completamente\",\"Uma nova thread é criada automaticamente\",\"O método retorna null imediatamente\"]", "A thread é liberada para atender outras tarefas enquanto aguarda" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Todo método que usa 'await' internamente deve ter 'async' na sua declaração.", "'async' e 'await' são inseparáveis. 'async' marca o método como máquina de estado assíncrona e habilita o uso de 'await'. Sem 'async', 'await' é tratado como identificador normal e o código não compila conforme esperado." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Um método assíncrono que não retorna valor algum deve usar o tipo de retorno: ____", "Use 'Task' (não 'async void') para métodos assíncronos sem retorno. Task pode ser aguardado pelo chamador e propaga exceções corretamente. 'async void' existe apenas para event handlers e não pode ser aguardado, tornando o tratamento de exceções muito difícil." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Explicacao", "OpcoesJson" },
                values: new object[] { "O Repository isola 'como' os dados são obtidos (SQL, EF, API externa) da lógica que decide 'o que' fazer com eles. Isso permite trocar o banco sem alterar Controllers/Services, e facilita muito os testes unitários.", "[\"Desacoplar a lógica de negócio do acesso a dados\",\"Aumentar a velocidade das queries SQL\",\"Eliminar a necessidade de interfaces\",\"Substituir completamente o ORM\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 26,
                column: "Explicacao",
                value: "Controllers NUNCA devem acessar infraestrutura diretamente. Para performance, otimize o Repository: use projeções com Select, AsNoTracking para leitura, paginação, índices. O Controller deve depender sempre da interface, nunca do EF Core.");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 27,
                column: "Explicacao",
                value: "Dependência de abstrações é o Dependency Inversion Principle (D do SOLID). O Controller recebe uma 'IRepository' no construtor — não sabe se é EF Core, Dapper ou um fake em memória. Isso é injeção de dependência: detalhes dependem de abstrações, não o contrário.");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Qual modificador de acesso torna um membro visível apenas para a própria classe E para quaisquer subclasses?", "'protected' combina o acesso da própria classe com o das subclasses, independente do assembly. Use quando o membro faz parte do contrato de herança mas não deve ser exposto publicamente." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Uma propriedade com 'public get' e 'private set' pode ser alterada por código fora da classe.", "Com 'private set', somente a própria classe pode escrever na propriedade. Código externo só pode ler. É o padrão de encapsulamento mais comum: expor leitura, proteger escrita para manter invariantes." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para criar uma propriedade que só pode ser definida durante a inicialização do objeto (com { }), usa-se o acessor: ____", "'init' (C# 9+) permite atribuição apenas no inicializador de objeto 'new Tipo { Prop = valor }'. Depois da construção, a propriedade fica somente leitura. Ideal para objetos imutáveis: DTOs, Value Objects, records." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Qual método LINQ agrupa elementos em subconjuntos por uma chave?", "GroupBy(keySelector) agrupa os elementos por uma chave e retorna IEnumerable<IGrouping<TKey, TElement>>. Cada grupo tem .Key (a chave) e é iterável para acessar os elementos. Muito usado para totalizações por categoria, cliente, data etc." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "O método Aggregate pode ser usado para concatenar strings de uma lista, acumulando o resultado elemento a elemento.", "Aggregate implementa um fold/reduce genérico. Com (a, b) => $\"{a} {b}\" você concatena elementos: [\"C#\", \"é\", \"incrível\"] → \"C# é incrível\". Também pode somar, multiplicar ou qualquer operação que combine dois elementos em um." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para retornar apenas os primeiros N elementos de uma coleção em LINQ, usa-se o método: ____", "Take(n) retorna no máximo n elementos do início da sequência. Se a coleção tiver menos de n elementos, retorna o que tiver. Combinado com Skip((página-1)*tamanho), implementa paginação eficiente." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual classe é usada para criar e emitir o sinal de cancelamento de um CancellationToken?", "CancellationTokenSource cria o token (via .Token) e controla o cancelamento (via .Cancel() ou .CancelAfter()). O CancellationToken em si é passado para as operações — ele só observa o cancelamento, não o emite.", "[\"CancellationTokenSource\",\"TaskCanceller\",\"CancellationFactory\",\"TokenManager\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Quando um CancellationToken é cancelado, Task.Delay(ms, token) lança automaticamente uma OperationCanceledException.", "A maioria dos métodos async do .NET aceita CancellationToken e reage ao cancelamento lançando OperationCanceledException. Isso inclui Task.Delay, HttpClient, EF Core (FindAsync, ToListAsync etc.), permitindo interrupção cooperativa." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para verificar manualmente se um token foi cancelado e lançar a exceção padrão, usa-se: ct.____IfCancellationRequested()", "ThrowIfCancellationRequested() verifica ct.IsCancellationRequested e, se verdadeiro, lança OperationCanceledException. Use no início de loops ou entre operações longas para um ponto de saída limpo." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual método assíncrono aguarda a conclusão de TODAS as tasks de uma lista?", "Task.WhenAll é assíncrono (retorna Task/Task<T[]>) e libera a thread enquanto aguarda. Task.WaitAll é o equivalente BLOQUEANTE — segura a thread e pode causar deadlock em contextos com SynchronizationContext. Sempre use WhenAll em código async.", "[\"Task.WhenAll\",\"Task.WhenAny\",\"Task.WaitAll\",\"Task.AwaitAll\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Task.WhenAny completa assim que a PRIMEIRA task terminar, mesmo que as outras ainda estejam em execução.", "WhenAny retorna a primeira task concluída (com sucesso ou falha). As demais continuam em background — WhenAny não as cancela. É usado para race conditions (cache vs banco) e implementar timeouts customizados." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para rodar múltiplas tasks em paralelo e aguardar todas assincronamente, usa-se: await Task.____(task1, task2)", "Task.WhenAll(task1, task2) aguarda que TODAS completem, com a thread liberada. O tempo total é o da task mais lenta, não a soma. Muito mais eficiente que 'await task1; await task2' em sequência quando as operações são independentes." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "Qual é o objetivo principal do Factory Pattern?", "Factory centraliza 'como' criar objetos. O cliente recebe um produto sem conhecer a classe concreta — depende apenas da abstração (classe base ou interface). Isso permite adicionar novos tipos sem alterar o código cliente.", "[\"Encapsular a lógica de criação de objetos\",\"Garantir uma única instância\",\"Observar mudanças de estado\",\"Separar interface de implementação de forma recursiva\"]", "Encapsular a lógica de criação de objetos" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 41,
                column: "Explicacao",
                value: "Esse é exatamente o benefício do Factory: o cliente depende apenas da abstração (interface ou classe base). A Factory decide qual concreto instanciar. O cliente não importa 'NotificacaoEmail', 'NotificacaoSms' etc.");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Um método estático que decide qual subclasse instanciar com base em um parâmetro é chamado de ____ Factory.", "Simple Factory é o padrão mais básico: um método (geralmente estático) que escolhe qual classe concreta criar. Tecnicamente não é um dos 23 padrões GoF originais, mas é o ponto de partida antes de evoluir para Factory Method ou Abstract Factory." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual classe do .NET garante inicialização lazy (sob demanda) e thread-safe de um Singleton?", "Lazy<T> cria o valor apenas na primeira vez que .Value é acessado, de forma thread-safe por padrão. Elimina o problema de double-checked locking manual e é a forma moderna de implementar Singleton em C#.", "[\"Lazy<T>\",\"ThreadLocal<T>\",\"Volatile<T>\",\"Concurrent<T>\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "No ASP.NET Core, AddSingleton registra o serviço de forma que uma única instância é criada e reutilizada durante toda a vida da aplicação.", "AddSingleton = uma instância por aplicação (toda a vida do processo). AddScoped = uma por requisição HTTP. AddTransient = uma nova a cada injeção. O container de DI é a forma preferida de Singleton em ASP.NET Core — é testável e não usa estado estático." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para impedir que uma classe Singleton seja herdada (o que quebraria a unicidade), usa-se o modificador: ____", "'sealed' impede herança. Em Singletons é recomendado porque uma subclasse poderia criar uma segunda instância, quebrar o contrato ou expor o construtor privado. 'sealed' também pode gerar pequenas otimizações de performance em chamadas virtuais." });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Variáveis e Tipos de Dados em C#\n\nC# é **fortemente tipado**: toda variável tem um tipo definido em tempo de compilação. O compilador valida cada operação com base nesses tipos, eliminando erros antes de rodar o programa.\n\n### Tipos primitivos\n\n```csharp\n// Inteiros\nbyte   b = 255;              // 0–255 (8 bits)\nshort  s = 32_767;           // –32.768 a 32.767 (16 bits)\nint    i = 2_147_483_647;    // tipo padrão para inteiros (32 bits)\nlong   l = 9_999_999_999L;   // sufixo L obrigatório (64 bits)\n\n// Ponto flutuante\nfloat   f = 3.14f;    // sufixo f obrigatório (~7 dígitos de precisão)\ndouble  d = 3.14159;  // padrão para decimais (~15 dígitos)\ndecimal m = 19.99m;   // sufixo m — use para DINHEIRO (sem erro de arredondamento binário)\n\n// Outros\nbool   ok   = true;\nchar   c    = 'A';      // aspas simples — um único caractere Unicode\nstring nome = \"Maria\"; // aspas duplas — sequência de chars (tipo referência)\n```\n\n### var — inferência de tipo\n\nA palavra-chave `var` instrui o compilador a deduzir o tipo pelo valor atribuído. O tipo é **fixo** após a declaração — `var` não é `dynamic`:\n\n```csharp\nvar cidade  = \"São Paulo\"; // string\nvar preco   = 9.99m;       // decimal\nvar ativo   = true;        // bool\n// cidade = 42;            // ERRO de compilação — tipo já é string\n```\n\n> Use `var` quando o tipo é **óbvio** pelo lado direito. Evite quando prejudica a leitura.\n\n### string e String são o mesmo tipo\n\n`string` (minúsculo) é um **alias da linguagem** para `System.String`. São 100% intercambiáveis — use `string` por convenção no código:\n\n```csharp\nstring a = \"olá\";   // alias preferido\nString b = \"olá\";   // classe do framework — mesmo tipo\nbool igual = a.GetType() == b.GetType(); // true\n```\n\n### const vs readonly\n\n```csharp\nconst double PI = 3.14159265;   // compilação — valor gravado no IL, nunca muda\nreadonly int Capacidade;         // definida no construtor — pode variar por instância\n\n// const exige valor em tempo de compilação:\n// const DateTime Hoje = DateTime.Now; // ERRO — DateTime.Now não é constante\n```\n\n### Tipos Nullable\n\nTipos de valor (`int`, `bool`, etc.) não aceitam `null` por padrão. Adicione `?` para torná-los anuláveis:\n\n```csharp\nint? idade = null;\nif (idade.HasValue)\n    Console.WriteLine(idade.Value);\n\n// Operador de coalescência nula\nint resultado = idade ?? 0;  // usa 0 se idade for null\nidade ??= 18;               // atribui 18 apenas se ainda for null\n```\n\n### Conversão de tipos\n\n```csharp\n// Implícita — sem perda de dados\nint  x = 100;\nlong y = x;   // long é maior, conversão automática\n\n// Explícita (cast) — pode perder dados\ndouble d = 9.99;\nint    i = (int)d;  // i = 9, parte decimal descartada\n\n// Parsing seguro de strings\nbool ok = int.TryParse(\"abc\", out int val); // ok = false, val = 0 — nunca lança exceção\nint n   = int.Parse(\"42\");                  // lança FormatException se inválido\n```", "Tipos primitivos, inferência com var, const, nullable e conversão de tipos" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Controle de Fluxo em C#\n\nControle de fluxo determina **quais instruções são executadas** e em que ordem.\n\n### if / else if / else\n\n```csharp\nif (nota >= 9)\n    Console.WriteLine(\"Excelente\");\nelse if (nota >= 7)\n    Console.WriteLine(\"Aprovado\");\nelse\n    Console.WriteLine(\"Reprovado\");\n```\n\n### Operador ternário `? :`\n\nA forma mais concisa de um `if/else` que retorna um valor:\n\n```csharp\n// sintaxe: condição ? valorSeVerdadeiro : valorSeFalso\nstring resultado = nota >= 7 ? \"Aprovado\" : \"Reprovado\";\nint absoluto = x >= 0 ? x : -x;\n\n// Pode ser encadeado (use com moderação)\nstring nivel = nota >= 9 ? \"A\" : nota >= 7 ? \"B\" : \"C\";\n```\n\n### switch expression (C# 8+)\n\nForma moderna e concisa de múltiplas comparações:\n\n```csharp\nstring nivel = nota switch\n{\n    >= 9  => \"Excelente\",\n    >= 7  => \"Aprovado\",\n    >= 5  => \"Recuperação\",\n    _     => \"Reprovado\"   // _ é o caso padrão (default)\n};\n\n// switch statement clássico (ainda útil para tipos exatos)\nswitch (diaSemana)\n{\n    case \"Sábado\":\n    case \"Domingo\":\n        Console.WriteLine(\"Fim de semana\"); break;\n    default:\n        Console.WriteLine(\"Dia útil\"); break;\n}\n```\n\n> **Quando usar switch vs if/else?** `switch` é preferível ao comparar uma variável contra **múltiplos valores constantes**.\n\n### for e foreach\n\n```csharp\n// for — use quando precisa do índice\nfor (int i = 0; i < 5; i++)\n    Console.WriteLine(i);\n\n// foreach — use para percorrer coleções (mais legível)\nvar nomes = new[] { \"Ana\", \"Bruno\", \"Carlos\" };\nforeach (var nome in nomes)\n    Console.WriteLine(nome);\n\n// ATENÇÃO: NÃO modifique a coleção dentro do foreach\n// foreach (var item in lista) { lista.Remove(item); } // InvalidOperationException!\n// Para remover durante iteração, use um for reverso ou crie uma nova lista\n```\n\n### while e do-while\n\n```csharp\n// while — verifica ANTES de executar (pode não executar nenhuma vez)\nint i = 0;\nwhile (i < 3) { Console.WriteLine(i); i++; }\n\n// do-while — executa pelo menos UMA vez\ndo {\n    Console.Write(\"Tente novamente: \");\n    entrada = Console.ReadLine();\n} while (entrada != \"sair\");\n```\n\n### break e continue\n\n```csharp\nforeach (var n in numeros)\n{\n    if (n == 0) continue;  // pula para a próxima iteração\n    if (n < 0)  break;     // encerra o loop completamente\n    Console.WriteLine(100 / n);\n}\n```", "if/else, operador ternário, switch expression, for, foreach e while" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Métodos em C#\n\nMétodos encapsulam um bloco de lógica reutilizável. São a unidade básica de comportamento em C#.\n\n### Anatomia de um método\n\n```csharp\n//  modificador  retorno  nome     parâmetros\n    public       int      Somar   (int a, int b)\n    {\n        return a + b;\n    }\n\n// Expression-bodied (=> ) — para métodos de uma linha\npublic int Somar(int a, int b) => a + b;\npublic string Saudar(string nome) => $\"Olá, {nome}!\";\n```\n\n### Modificadores de acesso\n\nControlam de onde o método pode ser chamado:\n\n```csharp\npublic class Calculadora\n{\n    public  double Resultado { get; private set; }  // public: acessível em qualquer lugar\n    private double Calcular(double a, double b) => a + b; // private: só dentro desta classe\n\n    // protected: esta classe + quem herdar dela\n    // internal:  qualquer classe do mesmo projeto (assembly)\n    // protected internal: union de ambos\n\n    public void Somar(double a, double b)\n        => Resultado = Calcular(a, b); // OK — Calcular é private mas estamos na mesma classe\n}\n\n// var c = new Calculadora();\n// c.Calcular(1, 2); // ERRO — Calcular é private\n```\n\n> A regra é: **o mais restritivo possível**. Se algo não precisa ser público, faça `private`.\n\n### void — métodos sem retorno\n\n```csharp\npublic void Logar(string mensagem)\n{\n    Console.WriteLine($\"[{DateTime.Now:HH:mm:ss}] {mensagem}\");\n    // sem return (ou return; vazio para sair antecipadamente)\n}\n```\n\n### Parâmetros opcionais e nomeados\n\n```csharp\npublic string Formatar(string texto, bool maiusculo = false, string prefixo = \"\")\n    => prefixo + (maiusculo ? texto.ToUpper() : texto);\n\n// Chamadas equivalentes:\nFormatar(\"olá\");                            // usa defaults\nFormatar(\"olá\", maiusculo: true);           // parâmetro nomeado\nFormatar(\"olá\", prefixo: \">> \", maiusculo: true); // ordem livre com nomes\n```\n\n### Sobrecarga (Overloading)\n\nDois métodos com o **mesmo nome** mas **assinaturas diferentes** (tipos ou quantidade de parâmetros):\n\n```csharp\npublic int    Calcular(int a, int b)       => a + b;\npublic double Calcular(double a, double b) => a + b;\npublic int    Calcular(int a, int b, int c) => a + b + c;\n\n// O compilador escolhe a versão correta pelo tipo dos argumentos\nCalcular(1, 2);        // chama versão int\nCalcular(1.5, 2.5);    // chama versão double\n```\n\n### params — número variável de argumentos\n\n```csharp\npublic int Somar(params int[] numeros) => numeros.Sum();\n\nSomar(1, 2, 3);      // 6\nSomar(10, 20);       // 30\nSomar();             // 0\n```", "Modificadores de acesso, parâmetros, sobrecarga e métodos expressão" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Classes e Objetos em C#\n\nUma **classe** é um molde (blueprint). Um **objeto** é uma instância concreta desse molde, criada com `new`.\n\n### Declaração de classe\n\n```csharp\npublic class Produto\n{\n    // Propriedades (preferidas a campos públicos)\n    public int    Id    { get; set; }\n    public string Nome  { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n\n    // Método de instância\n    public string Descricao() => $\"{Nome} — R$ {Preco:F2}\";\n}\n```\n\n### Construtores\n\nO construtor tem o **mesmo nome da classe**, não tem tipo de retorno e é chamado automaticamente pelo `new`:\n\n```csharp\npublic class Produto\n{\n    public string Nome  { get; }\n    public decimal Preco { get; }\n\n    // Construtor padrão (sem parâmetros)\n    public Produto() { Nome = \"Sem nome\"; Preco = 0; }\n\n    // Construtor com parâmetros\n    public Produto(string nome, decimal preco)\n    {\n        Nome  = nome;\n        Preco = preco;\n    }\n\n    // Encadeamento: chama o outro construtor com : this(...)\n    public Produto(string nome) : this(nome, 0m) { }\n}\n```\n\n**Uma classe pode ter múltiplos construtores** — isso se chama sobrecarga de construtores. O compilador escolhe qual chamar com base nos argumentos passados ao `new`.\n\n### Criando objetos com `new`\n\n`new` aloca memória no heap, chama o construtor e retorna a referência ao objeto:\n\n```csharp\nvar p1 = new Produto();                   // construtor padrão\nvar p2 = new Produto(\"Notebook\", 3500m);   // construtor com 2 parâmetros\nvar p3 = new Produto(\"Mouse\");             // construtor com 1 parâmetro\n\nConsole.WriteLine(p2.Descricao()); // Notebook — R$ 3500,00\n```\n\n### Inicializadores de objeto `{ }`\n\nPermite definir propriedades públicas sem precisar de um construtor específico:\n\n```csharp\nvar p = new Produto { Id = 1, Nome = \"Teclado\", Preco = 150m };\n\n// Equivalente a:\nvar p = new Produto();\np.Id    = 1;\np.Nome  = \"Teclado\";\np.Preco = 150m;\n```\n\n### Membros estáticos\n\nMembros estáticos pertencem à **classe**, não a instâncias individuais:\n\n```csharp\npublic class Contador\n{\n    public static int Total { get; private set; } = 0;\n\n    public Contador() { Total++; } // cada new incrementa o contador estático\n}\n\nvar a = new Contador();\nvar b = new Contador();\nConsole.WriteLine(Contador.Total); // 2 — acesso via nome da classe\n```", "Propriedades, construtores múltiplos, new e inicializadores de objeto" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Herança e Polimorfismo\n\n**Herança** permite que uma classe reutilize e estenda o comportamento de outra. **Polimorfismo** permite tratar objetos de subclasses de forma uniforme.\n\n### Herança com `:`\n\n```csharp\npublic class Animal\n{\n    public string Nome { get; set; } = string.Empty;\n    public virtual string EmitirSom() => \"...\";\n    public void Respirar() => Console.WriteLine($\"{Nome} respira\");\n}\n\npublic class Cachorro : Animal\n{\n    public override string EmitirSom() => \"Au au!\";\n}\n\npublic class Gato : Animal\n{\n    public override string EmitirSom() => \"Miau!\";\n}\n\n// Polimorfismo: trate subclasses como o tipo base\nAnimal[] animais = { new Cachorro { Nome = \"Rex\" }, new Gato { Nome = \"Mimi\" } };\nforeach (var a in animais)\n    Console.WriteLine($\"{a.Nome}: {a.EmitirSom()}\"); // cada um responde diferente\n```\n\n### C# NÃO suporta herança múltipla de classes\n\nUma classe só pode herdar de **uma única classe**:\n\n```csharp\n// ERRO de compilação — herança múltipla de classes não existe em C#\n// public class Mula : Cavalo, Burro { }\n\n// SOLUÇÃO: use interfaces para comportamento múltiplo\npublic interface ITransporte { void Transportar(); }\npublic interface IResistente { int NivelResistencia { get; } }\npublic class Mula : Animal, ITransporte, IResistente\n{\n    public void Transportar() => Console.WriteLine(\"Transportando carga\");\n    public int NivelResistencia => 8;\n}\n```\n\n> C# herda de **uma classe** e implementa **n interfaces** — essa é a forma correta de modelar comportamentos múltiplos.\n\n### virtual e override\n\n`virtual` marca um método como substituível. `override` na subclasse substitui a implementação:\n\n```csharp\npublic class Forma\n{\n    public virtual double Area() => 0;\n    public virtual string Descricao() => $\"Área: {Area():F2}\";\n}\n\npublic class Circulo : Forma\n{\n    public double Raio { get; set; }\n    public override double Area() => Math.PI * Raio * Raio;\n    // Descricao() não é sobrescrita — usa a da classe base com Area() correto\n}\n```\n\n### Classes abstratas\n\nNão podem ser instanciadas diretamente. Forçam subclasses a implementar métodos:\n\n```csharp\npublic abstract class Relatorio\n{\n    public abstract string GerarConteudo(); // subclasses DEVEM implementar\n    public void Imprimir() => Console.WriteLine(GerarConteudo()); // método concreto\n}\n\n// var r = new Relatorio(); // ERRO — classe abstrata não pode ser instanciada\n```\n\n### base — acessando a classe pai\n\n```csharp\npublic class ContaPremium : Conta\n{\n    public ContaPremium(string titular) : base(titular) { } // chama construtor da base\n    public override string Descricao() => base.Descricao() + \" [PREMIUM]\";\n}\n```", "Herança com :, virtual/override, classes abstratas e interfaces" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Coleções em C#\n\nC# oferece diversas estruturas de coleção. As mais usadas no dia a dia são `Array`, `List<T>` e `Dictionary<TKey, TValue>`.\n\n### Array — tamanho fixo\n\n```csharp\n// Declaração e inicialização\nint[]    numeros  = { 1, 2, 3, 4, 5 };\nstring[] nomes    = new string[3]; // array de 3 posições, todas null\n\nConsole.WriteLine(numeros.Length); // 5 — propriedade Length para tamanho\nnumeros[0] = 99;   // acesso por índice (base 0)\nnumeros[5] = 1;    // IndexOutOfRangeException — índice inválido!\n\n// Arrays são de tamanho FIXO — não é possível adicionar ou remover elementos\n```\n\n### List<T> — tamanho dinâmico\n\n`List<T>` é o array dinâmico do C# — cresce e encolhe automaticamente:\n\n```csharp\nvar nomes = new List<string> { \"Ana\" };\n\n// Add() SEMPRE adiciona ao FINAL da lista\nnomes.Add(\"Bruno\");         // [\"Ana\", \"Bruno\"]\nnomes.Add(\"Carlos\");        // [\"Ana\", \"Bruno\", \"Carlos\"]\n\n// Insert(índice, item) — insere em posição específica\nnomes.Insert(0, \"Zara\");   // [\"Zara\", \"Ana\", \"Bruno\", \"Carlos\"]\n\n// Remove e RemoveAt\nnomes.Remove(\"Ana\");        // remove a primeira ocorrência de \"Ana\"\nnomes.RemoveAt(0);          // remove pelo índice\n\n// Tamanho e acesso\nConsole.WriteLine(nomes.Count);  // Count retorna o número de elementos\nConsole.WriteLine(nomes[0]);     // acesso por índice como array\n\n// Verificação\nbool tem = nomes.Contains(\"Bruno\"); // true\n```\n\n> **Array vs List<T>**: Array tem tamanho **fixo** e usa `.Length`. List<T> tem tamanho **dinâmico** e usa `.Count`.\n\n### Dictionary<TKey, TValue> — pares chave-valor\n\nLookup em O(1) por chave — muito mais rápido do que buscar em lista:\n\n```csharp\nvar capitais = new Dictionary<string, string>\n{\n    [\"SP\"] = \"São Paulo\",\n    [\"RJ\"] = \"Rio de Janeiro\",\n    [\"MG\"] = \"Belo Horizonte\"\n};\n\ncapitais[\"BA\"] = \"Salvador\";   // adiciona ou atualiza\n\n// Leitura segura\nif (capitais.TryGetValue(\"PR\", out string? capital))\n    Console.WriteLine(capital);\nelse\n    Console.WriteLine(\"Estado não encontrado\");\n\nConsole.WriteLine(capitais.Count);           // 4\nbool existe = capitais.ContainsKey(\"SP\");    // true\n```\n\n### IEnumerable<T> — a abstração base\n\nArray, List, Dictionary e outras coleções implementam `IEnumerable<T>`, que é o tipo aceito pelo `foreach` e pelo LINQ:\n\n```csharp\n// Preferir IEnumerable<T> em parâmetros — mais flexível\npublic void Imprimir(IEnumerable<string> itens)\n{\n    foreach (var item in itens)\n        Console.WriteLine(item);\n}\n\nImprimir(nomes);     // List<string>\nImprimir(outroArray); // string[]\n```", "Array (tamanho fixo), List<T> (dinâmico) e Dictionary<TKey, TValue>" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## LINQ — Language Integrated Query\n\nLINQ é um conjunto de **métodos de extensão** sobre `IEnumerable<T>` (e `IQueryable<T>`) que permite consultar coleções com sintaxe fluente e expressiva.\n\n### Filtragem com Where\n\n```csharp\nvar numeros = new List<int> { 5, 3, 8, 1, 9, 2, 7, 4 };\n\nvar pares   = numeros.Where(n => n % 2 == 0).ToList(); // [8, 2, 4]\nvar maiores = numeros.Where(n => n > 5).ToList();       // [8, 9, 7]\n\n// Where NÃO modifica a lista original — retorna nova sequência\n```\n\n### Transformação com Select\n\n```csharp\nvar nomes    = new[] { \"ana\", \"bruno\", \"carlos\" };\nvar maiusc   = nomes.Select(n => n.ToUpper()).ToList(); // [\"ANA\", \"BRUNO\", \"CARLOS\"]\nvar comprimentos = nomes.Select(n => n.Length).ToList(); // [3, 5, 6]\n```\n\n### Ordenação\n\n```csharp\nvar ordenados     = numeros.OrderBy(n => n).ToList();            // crescente\nvar decrescentes  = numeros.OrderByDescending(n => n).ToList();  // decrescente\nvar porNome       = pessoas.OrderBy(p => p.Sobrenome).ThenBy(p => p.Nome).ToList();\n```\n\n### First e FirstOrDefault\n\nEssa é uma diferença **crítica** no dia a dia:\n\n```csharp\nvar numeros = new List<int> { 1, 2, 3 };\n\n// First() lança InvalidOperationException se nenhum elemento satisfaz a condição\nvar x = numeros.First(n => n > 10); // LANÇA EXCEÇÃO — nenhum elemento > 10\n\n// FirstOrDefault() retorna o valor padrão do tipo se não encontrar\nvar y = numeros.FirstOrDefault(n => n > 10);       // retorna 0 (default de int)\nvar z = nomes.FirstOrDefault(n => n == \"Inexistente\"); // retorna null (default de string)\n\n// Boas práticas: use FirstOrDefault + verificação de null\nvar usuario = usuarios.FirstOrDefault(u => u.Id == id);\nif (usuario is null) throw new KeyNotFoundException();\n```\n\n### Any, All e Count\n\n```csharp\nbool temAdulto  = pessoas.Any(p => p.Idade >= 18);   // verdadeiro se pelo menos um\nbool todosMaior = pessoas.All(p => p.Idade >= 0);    // verdadeiro se todos\nint  qtdAtivos  = usuarios.Count(u => u.Ativo);      // conta os que satisfazem\n\nint  total = numeros.Sum();\nint  maior = numeros.Max();\ndouble media = numeros.Average();\n```\n\n### Encadeamento de operações\n\n```csharp\nvar resultado = produtos\n    .Where(p => p.Preco > 100)\n    .OrderBy(p => p.Nome)\n    .Select(p => new { p.Nome, p.Preco })\n    .ToList();\n```\n\n### LINQ funciona além de coleções em memória\n\n`IQueryable<T>` implementa LINQ com **tradução para SQL** — o banco executa o filtro, não o C#. Isso é o que torna o EF Core eficiente:\n\n```csharp\n// LINQ to Objects (IEnumerable) — filtra na memória\nvar ativos = lista.Where(u => u.Ativo).ToList();\n\n// LINQ to SQL via EF Core (IQueryable) — vira WHERE na query SQL!\nvar ativos = await ctx.Usuarios\n    .Where(u => u.Ativo)\n    .OrderBy(u => u.Nome)\n    .ToListAsync(); // SELECT * FROM Usuarios WHERE Ativo = 1 ORDER BY Nome\n```\n\n> **Regra de ouro**: chame `.ToList()` / `.ToListAsync()` **apenas no final** — antes disso, a query ainda não foi executada (lazy evaluation).", "Where, Select, OrderBy, First vs FirstOrDefault, Any/All e uso com EF Core" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Programação Assíncrona com async/await\n\nEm aplicações modernas (APIs, apps, games), bloquear uma thread esperando I/O é um desperdício. O modelo `async/await` permite que uma thread seja **liberada** enquanto espera por operações lentas.\n\n### O problema do código síncrono\n\n```csharp\n// BLOQUEANTE — a thread fica presa esperando o banco responder\npublic IActionResult Get(int id)\n{\n    var usuario = db.Usuarios.Find(id); // thread bloqueada por 50ms\n    return Ok(usuario);\n}\n// Em um servidor com 1000 req/s, isso pode esgotar o ThreadPool!\n```\n\n### async/await — a thread é liberada\n\n```csharp\n// ASSÍNCRONO — a thread é devolvida ao pool enquanto o banco responde\npublic async Task<IActionResult> Get(int id)\n{\n    var usuario = await db.Usuarios.FindAsync(id); // thread liberada!\n    return Ok(usuario);\n    // a thread retorna aqui, podendo atender outras requisições\n}\n```\n\n> Quando o código encontra um `await`, o método é **suspenso** e a thread retorna ao pool. Quando a operação completa, uma thread (possivelmente diferente) retoma o método de onde parou.\n\n### Task e Task<T>\n\n```csharp\n// Task representa uma operação assíncrona sem valor de retorno\npublic async Task SalvarAsync(string dados)\n{\n    await File.WriteAllTextAsync(\"arquivo.txt\", dados);\n}\n\n// Task<T> representa uma operação que retorna um valor\npublic async Task<string> BuscarAsync(int id)\n{\n    await Task.Delay(100); // simula chamada ao banco\n    return $\"Resultado do id {id}\";\n}\n\nvar resultado = await BuscarAsync(42);\n```\n\n### async Task vs async void\n\n**Nunca use `async void`** exceto em event handlers:\n\n```csharp\n// async void — NÃO pode ser aguardado, exceções não são propagadas\npublic async void Ruim() { await Task.Delay(100); }\n\n// async Task — pode ser aguardado, exceções propagam corretamente\npublic async Task Bom() { await Task.Delay(100); }\n\n// Problema com async void:\nasync void MetodoRuim()\n{\n    throw new Exception(\"Erro silencioso!\"); // vai crashar o app sem poder ser capturado\n}\n```\n\n### Tratamento de exceções\n\n```csharp\ntry\n{\n    var resultado = await BuscarAsync(99);\n}\ncatch (HttpRequestException ex)\n{\n    Console.WriteLine($\"Erro de rede: {ex.Message}\");\n}\ncatch (OperationCanceledException)\n{\n    Console.WriteLine(\"Operação cancelada pelo usuário\");\n}\nfinally\n{\n    // finally funciona normalmente em async\n    Console.WriteLine(\"Finalizado\");\n}\n```\n\n### Task.Run — trabalho CPU-bound\n\n```csharp\n// Use Task.Run apenas para CPU-bound (processamento pesado), NÃO para I/O\nvar resultado = await Task.Run(() => CalcularPrimosAte(1_000_000));\n\n// Para I/O (banco, HTTP, arquivo), use os métodos Async nativos — NÃO Task.Run\n// ERRADO: await Task.Run(() => db.Usuarios.Find(id));\n// CERTO:  await db.Usuarios.FindAsync(id);\n```", "Programação assíncrona, Task<T>, thread liberada e async Task vs async void" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Repository Pattern\n\nO Repository Pattern isola a **lógica de acesso a dados** da lógica de negócio. Controllers e Services não sabem se os dados vêm de SQLite, SQL Server, uma API ou memória.\n\n### O problema sem Repository\n\n```csharp\n// SEM REPOSITORY — controller acoplado ao banco\npublic class PedidoController : ControllerBase\n{\n    private readonly AppDbContext _ctx;\n\n    [HttpGet(\"{id}\")]\n    public async Task<IActionResult> Get(int id)\n    {\n        var pedido = await _ctx.Pedidos.FindAsync(id); // acoplamento direto ao EF\n        return Ok(pedido);\n    }\n    // Problema: para testar, precisa de um banco real\n}\n```\n\n### A solução: Interface + Implementação\n\n```csharp\n// 1. Interface — define o CONTRATO (o que o repositório faz)\npublic interface IProdutoRepository\n{\n    Task<Produto?>           ObterPorIdAsync(int id);\n    Task<IEnumerable<Produto>> ObterTodosAsync();\n    Task<IEnumerable<Produto>> ObterPorCategoriaAsync(string categoria);\n    Task                      AdicionarAsync(Produto produto);\n    void                      Atualizar(Produto produto);\n    void                      Remover(Produto produto);\n    Task<bool>                SalvarAsync();\n}\n\n// 2. Implementação — DETALHE de infraestrutura (EF Core)\npublic class ProdutoRepository(AppDbContext ctx) : IProdutoRepository\n{\n    public async Task<Produto?> ObterPorIdAsync(int id)\n        => await ctx.Produtos.FindAsync(id);\n\n    public async Task<IEnumerable<Produto>> ObterTodosAsync()\n        => await ctx.Produtos.AsNoTracking().ToListAsync(); // AsNoTracking = mais rápido para leitura\n\n    public async Task AdicionarAsync(Produto produto)\n        => await ctx.Produtos.AddAsync(produto);\n\n    public void Atualizar(Produto produto)\n        => ctx.Produtos.Update(produto);\n\n    public async Task<bool> SalvarAsync()\n        => await ctx.SaveChangesAsync() > 0;\n}\n\n// 3. Controller — depende da INTERFACE, nunca da implementação\npublic class ProdutoController(IProdutoRepository repo) : ControllerBase\n{\n    [HttpGet(\"{id}\")]\n    public async Task<IActionResult> Get(int id)\n    {\n        var produto = await repo.ObterPorIdAsync(id);\n        return produto is null ? NotFound() : Ok(produto);\n    }\n}\n```\n\n### Registro no DI (Dependency Injection)\n\n```csharp\n// Program.cs\nbuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();\n// AddScoped = uma instância por requisição HTTP (ideal para DbContext)\n```\n\n### Testabilidade — o grande benefício\n\n```csharp\n// Em testes, substitua com um fake sem banco real\npublic class FakeProdutoRepository : IProdutoRepository\n{\n    private readonly List<Produto> _dados = new();\n\n    public Task<Produto?> ObterPorIdAsync(int id)\n        => Task.FromResult(_dados.FirstOrDefault(p => p.Id == id));\n\n    public Task AdicionarAsync(Produto p) { _dados.Add(p); return Task.CompletedTask; }\n    public Task<bool> SalvarAsync() => Task.FromResult(true);\n    // ...\n}\n\n// O Controller funciona sem nenhuma mudança — recebe a interface, não sabe qual implementação é\n```\n\n> **Princípio**: Controllers dependem de **abstrações** (interfaces), não de **detalhes** (EF Core, SQL). Isso é o **D** do SOLID — Dependency Inversion Principle.", "Abstraindo acesso a dados com interfaces, DI e testabilidade" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Encapsulamento\n\nEncapsulamento significa **esconder os detalhes internos** e expor apenas o que é necessário. É um dos pilares da OOP e evita que código externo coloque objetos em estados inválidos.\n\n### Modificadores de acesso\n\n| Modificador | Acessível por |\n|---|---|\n| `public` | Qualquer lugar |\n| `private` | Apenas a própria classe |\n| `protected` | Classe + subclasses |\n| `internal` | Qualquer classe do mesmo assembly (projeto) |\n| `protected internal` | Subclasses + mesmo assembly |\n| `private protected` | Subclasses dentro do mesmo assembly |\n\n```csharp\npublic class ContaBancaria\n{\n    private decimal _saldo;                  // campo privado — ninguém acessa diretamente\n    public  decimal Saldo => _saldo;         // propriedade somente leitura\n    protected string Titular { get; }        // visível para subclasses\n    internal int    AgenciaId { get; set; }  // visível no mesmo projeto\n\n    public void Depositar(decimal valor)\n    {\n        if (valor <= 0) throw new ArgumentException(\"Valor deve ser positivo\");\n        _saldo += valor; // único ponto de mutação\n    }\n\n    public bool Sacar(decimal valor)\n    {\n        if (valor > _saldo) return false;\n        _saldo -= valor;\n        return true;\n    }\n}\n```\n\n### Propriedades com get público e set privado\n\nO padrão mais comum: leitura pública, escrita controlada:\n\n```csharp\npublic class Produto\n{\n    private decimal _preco;\n\n    // Propriedade com validação no set\n    public decimal Preco\n    {\n        get => _preco;\n        set\n        {\n            if (value < 0) throw new ArgumentException(\"Preço não pode ser negativo\");\n            _preco = value;\n        }\n    }\n\n    // Alternativa concisa: set privado\n    public string Codigo { get; private set; } = Guid.NewGuid().ToString();\n}\n\n// var p = new Produto();\n// p.Preco = -10; // ArgumentException\n// p.Codigo = \"x\"; // ERRO de compilação — set é private\n```\n\n### init — somente no inicializador\n\nC# 9 introduziu `init`, que permite definir a propriedade **apenas durante a construção** do objeto:\n\n```csharp\npublic class Pedido\n{\n    public int      Id      { get; init; }\n    public DateTime Criado  { get; } = DateTime.UtcNow;\n    public string   Cliente { get; init; } = string.Empty;\n}\n\nvar pedido = new Pedido { Id = 42, Cliente = \"João\" }; // OK\n// pedido.Id = 1; // ERRO — init só permite atribuição na inicialização\n```\n\n> `init` é ideal para **objetos imutáveis após criação** — DTOs, Value Objects, records.\n\n### Por que encapsular?\n\n- **Invariantes garantidas**: o objeto nunca fica em estado inválido\n- **Facilidade de mudança**: a implementação interna pode mudar sem quebrar quem usa\n- **Testabilidade**: o comportamento é previsível e isolado", "Modificadores de acesso, propriedades com validação e acessor init" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## LINQ Avançado\n\nCom os operadores básicos dominados, o LINQ se torna ainda mais poderoso para cenários de agregação, junção e paginação.\n\n### GroupBy — agrupamento\n\n```csharp\nvar pedidos = new List<Pedido> { ... };\n\n// Agrupa pedidos por ClienteId e calcula totais\nvar resumo = pedidos\n    .GroupBy(p => p.ClienteId)\n    .Select(g => new\n    {\n        ClienteId  = g.Key,\n        Total      = g.Sum(p => p.Valor),\n        Quantidade = g.Count()\n    })\n    .OrderByDescending(r => r.Total)\n    .ToList();\n\n// g.Key é o valor da chave (ClienteId)\n// g é um IGrouping<TKey, TElement> — iterável\n```\n\n### Join — combinando duas coleções\n\n```csharp\n// Inner join: retorna apenas registros com correspondência em ambos os lados\nvar resultado = clientes.Join(\n    pedidos,\n    c => c.Id,         // chave de clientes\n    p => p.ClienteId,  // chave de pedidos\n    (c, p) => new { NomeCliente = c.Nome, Pedido = p.Numero, Valor = p.Valor }\n);\n\n// Com EF Core, prefira Include() para navigation properties\nvar clientesComPedidos = await ctx.Clientes.Include(c => c.Pedidos).ToListAsync();\n```\n\n### Aggregate — redução personalizada\n\n```csharp\nvar numeros = new[] { 1, 2, 3, 4, 5 };\n\n// Acumula um resultado percorrendo a sequência\nint produto = numeros.Aggregate(1, (acc, n) => acc * n); // 120 (1×2×3×4×5)\n\nstring frase = new[] { \"C#\", \"é\", \"incrível\" }\n    .Aggregate((a, b) => $\"{a} {b}\"); // \"C# é incrível\"\n\n// Equivalente manual:\nint resultado = 1;\nforeach (var n in numeros) resultado *= n;\n```\n\n### Paginação com Skip e Take\n\n```csharp\nint pagina  = 2;\nint tamanho = 10;\n\nvar paginado = produtos\n    .OrderBy(p => p.Nome)\n    .Skip((pagina - 1) * tamanho)  // pula os da página anterior\n    .Take(tamanho)                 // pega apenas os desta página\n    .ToList();\n\n// Página 1: Skip(0).Take(10)  → itens 0–9\n// Página 2: Skip(10).Take(10) → itens 10–19\n```\n\n> `Take(n)` retorna no máximo `n` elementos. Se a coleção tiver menos, retorna o que tiver (não lança exceção).\n\n### Any, All e Contains\n\n```csharp\nbool temPromocao = produtos.Any(p => p.Desconto > 0);\nbool todosAtivos = produtos.All(p => p.Ativo);\nbool temId5      = ids.Contains(5);\n```\n\n### Distinct, Union, Intersect, Except\n\n```csharp\nvar a = new[] { 1, 2, 3, 4 };\nvar b = new[] { 3, 4, 5, 6 };\n\na.Distinct()    // remove duplicatas da mesma sequência\na.Union(b)      // {1,2,3,4,5,6} — todos sem duplicatas\na.Intersect(b)  // {3,4}         — apenas os comuns\na.Except(b)     // {1,2}         — apenas os de 'a' que não estão em 'b'\n```", "GroupBy, Join, Aggregate, Skip/Take e projeções complexas" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## CancellationToken — Cancelamento Cooperativo\n\nC# usa um modelo de cancelamento **cooperativo**: você pede para parar (signal), e o código verifica e honra esse pedido. Não existe matar uma thread à força de forma segura.\n\n### CancellationTokenSource e CancellationToken\n\n```csharp\n// CancellationTokenSource cria e controla o token\nusing var cts = new CancellationTokenSource();\n\n// O token é passado para as operações que devem respeitá-lo\nCancellationToken token = cts.Token;\n\n// Cancelar após 5 segundos (timeout)\ncts.CancelAfter(TimeSpan.FromSeconds(5));\n\n// Cancelar manualmente\ncts.Cancel();\n```\n\n### Usando o token em operações assíncronas\n\n```csharp\npublic async Task<string> BuscarDadosAsync(int id, CancellationToken ct = default)\n{\n    // Verificação manual antes de operação longa\n    ct.ThrowIfCancellationRequested(); // lança OperationCanceledException se cancelado\n\n    await Task.Delay(2000, ct); // Task.Delay respeita o token automaticamente\n\n    ct.ThrowIfCancellationRequested(); // verificar novamente após cada etapa longa\n\n    return $\"Dados do id {id}\";\n}\n```\n\n### Capturando o cancelamento\n\n```csharp\nusing var cts = new CancellationTokenSource();\ncts.CancelAfter(3000); // timeout de 3s\n\ntry\n{\n    var dados = await BuscarDadosAsync(1, cts.Token);\n    Console.WriteLine(dados);\n}\ncatch (OperationCanceledException)\n{\n    Console.WriteLine(\"Operação cancelada — timeout atingido ou usuário cancelou\");\n}\n```\n\n### Em ASP.NET Core — cancelamento automático\n\nO framework injeta o token do request automaticamente. Se o cliente desconectar, o token é cancelado:\n\n```csharp\n[HttpGet(\"{id}\")]\npublic async Task<IActionResult> Get(int id, CancellationToken ct)\n{\n    // ct é cancelado se o cliente desconectar antes de receber a resposta\n    var dados = await _repo.ObterAsync(id, ct);\n    return Ok(dados);\n}\n```\n\n### Verificação manual com IsCancellationRequested\n\n```csharp\npublic async Task ProcessarLoteAsync(IEnumerable<int> ids, CancellationToken ct)\n{\n    foreach (var id in ids)\n    {\n        // Verificação não-lançante dentro de loops\n        if (ct.IsCancellationRequested)\n        {\n            Console.WriteLine(\"Processamento interrompido pelo usuário\");\n            break;\n        }\n\n        await ProcessarItemAsync(id, ct);\n    }\n}\n```\n\n> `ct.ThrowIfCancellationRequested()` é equivalente a:\n> `if (ct.IsCancellationRequested) throw new OperationCanceledException(ct);`", "Cancelamento cooperativo com CancellationTokenSource e ThrowIfCancellationRequested" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Paralelismo com Task.WhenAll e Task.WhenAny\n\nQuando há múltiplas operações **independentes entre si**, rodá-las em paralelo pode reduzir drasticamente o tempo total.\n\n### O problema do await sequencial\n\n```csharp\n// SEQUENCIAL — tempo total = soma de cada operação\nvar u1 = await BuscarUsuarioAsync(1); // aguarda 200ms\nvar u2 = await BuscarUsuarioAsync(2); // aguarda 200ms\nvar u3 = await BuscarUsuarioAsync(3); // aguarda 200ms\n// Tempo total: ~600ms\n```\n\n### Task.WhenAll — aguarda TODAS as tasks\n\n```csharp\n// PARALELO — as três operações rodam ao mesmo tempo\nvar task1 = BuscarUsuarioAsync(1);\nvar task2 = BuscarUsuarioAsync(2);\nvar task3 = BuscarUsuarioAsync(3);\n// ATENÇÃO: as tasks já começaram ao serem criadas!\n\nvar resultados = await Task.WhenAll(task1, task2, task3);\n// Tempo total: ~200ms (a mais lenta das três)\n// resultados[0] = usuário 1, resultados[1] = usuário 2, etc.\n\n// Sintaxe com IEnumerable\nvar ids = new[] { 1, 2, 3, 4, 5 };\nvar usuarios = await Task.WhenAll(ids.Select(id => BuscarUsuarioAsync(id)));\n```\n\n### Task.WhenAll vs Task.WaitAll\n\n```csharp\n// WhenAll — ASSÍNCRONO (retorna Task) — use sempre este!\nawait Task.WhenAll(task1, task2); // não bloqueia a thread\n\n// WaitAll — BLOQUEANTE (retorna void) — EVITE em código async!\nTask.WaitAll(task1, task2); // bloqueia a thread — pode causar deadlock\n```\n\n### Tratamento de erros com WhenAll\n\n```csharp\ntry\n{\n    await Task.WhenAll(task1, task2, task3);\n}\ncatch (Exception ex)\n{\n    // Apenas a primeira exceção é re-lançada no catch\n    // Para ver TODAS as exceções:\n    var excecoes = new[] { task1, task2, task3 }\n        .Where(t => t.IsFaulted)\n        .Select(t => t.Exception!.InnerException!)\n        .ToList();\n}\n```\n\n### Task.WhenAny — aguarda a PRIMEIRA task\n\nÚtil para **race conditions** e timeouts:\n\n```csharp\n// Race: cache vs banco — quem responder primeiro vence\nvar taskBanco = BuscarNoBancoAsync(id);\nvar taskCache = BuscarNoCacheAsync(id);\n\nvar vencedora = await Task.WhenAny(taskBanco, taskCache);\nvar resultado = await vencedora; // garante que exceções sejam propagadas\n\n// As outras tasks CONTINUAM rodando em background — WhenAny não as cancela\n```\n\n### Timeout com WhenAny\n\n```csharp\nvar taskPrincipal = BuscarDadosAsync();\nvar taskTimeout   = Task.Delay(TimeSpan.FromSeconds(5));\n\nvar concluida = await Task.WhenAny(taskPrincipal, taskTimeout);\n\nif (concluida == taskTimeout)\n    throw new TimeoutException(\"Operação excedeu 5 segundos\");\n\nvar resultado = await taskPrincipal;\n```", "Execução paralela de tasks, WhenAll vs WaitAll e race com WhenAny" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Factory Pattern\n\nO Factory Pattern encapsula a **lógica de criação** de objetos. O código cliente recebe um produto sem precisar saber qual classe concreta foi instanciada.\n\n### O problema sem Factory\n\n```csharp\n// SEM FACTORY — cliente acoplado a todas as classes concretas\npublic class PedidoService\n{\n    public void Notificar(string tipo, string mensagem)\n    {\n        if (tipo == \"email\")\n        {\n            var email = new NotificacaoEmail(); // acoplado\n            email.Enviar(mensagem);\n        }\n        else if (tipo == \"sms\")\n        {\n            var sms = new NotificacaoSms(); // acoplado\n            sms.Enviar(mensagem);\n        }\n        // Para adicionar \"push\", precisa editar este método — viola Open/Closed Principle\n    }\n}\n```\n\n### Simple Factory\n\n```csharp\n// Abstração do produto\npublic abstract class Notificacao\n{\n    public abstract void Enviar(string mensagem);\n}\n\n// Classes concretas\npublic class NotificacaoEmail : Notificacao\n{\n    public override void Enviar(string msg)\n        => Console.WriteLine($\"[EMAIL] {msg}\");\n}\n\npublic class NotificacaoSms : Notificacao\n{\n    public override void Enviar(string msg)\n        => Console.WriteLine($\"[SMS] {msg}\");\n}\n\npublic class NotificacaoPush : Notificacao\n{\n    public override void Enviar(string msg)\n        => Console.WriteLine($\"[PUSH] {msg}\");\n}\n\n// A Factory — único ponto de criação\npublic static class NotificacaoFactory\n{\n    public static Notificacao Criar(string tipo) => tipo switch\n    {\n        \"email\" => new NotificacaoEmail(),\n        \"sms\"   => new NotificacaoSms(),\n        \"push\"  => new NotificacaoPush(),\n        _ => throw new ArgumentException($\"Tipo desconhecido: {tipo}\")\n    };\n}\n\n// Uso — o cliente depende apenas da abstração\npublic class PedidoService\n{\n    public void Notificar(string tipo, string mensagem)\n    {\n        var notif = NotificacaoFactory.Criar(tipo); // não sabe qual classe concreta\n        notif.Enviar(mensagem);\n    }\n}\n```\n\n> O código cliente **não conhece** `NotificacaoEmail`, `NotificacaoSms` etc. Para adicionar um novo tipo, só modifica a Factory — o cliente não muda.\n\n### Factory integrada ao DI do ASP.NET Core\n\n```csharp\n// Com interfaces, o DI pode agir como factory\nbuilder.Services.AddScoped<IRelatorio, RelatorioExcel>(); // injeta sempre Excel\n// builder.Services.AddScoped<IRelatorio, RelatorioPdf>(); // troca sem tocar nos consumers\n\n// Keyed services (ASP.NET Core 8+)\nbuilder.Services.AddKeyedScoped<IRelatorio, RelatorioExcel>(\"excel\");\nbuilder.Services.AddKeyedScoped<IRelatorio, RelatorioPdf>(\"pdf\");\n\npublic class RelatorioService([FromKeyedServices(\"pdf\")] IRelatorio relatorio) { }\n```\n\n### Por que usar Factory?\n\n- O cliente depende da **abstração**, não do concreto\n- Centraliza **configuração, injeção e validação** na criação\n- Adicionar novos tipos não muda o código cliente (Open/Closed Principle)", "Encapsulando criação de objetos com Simple Factory e Factory Method" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Singleton Pattern\n\nSingleton garante que uma classe tenha **exatamente uma instância** durante toda a vida da aplicação, com um ponto global de acesso.\n\n### Implementação com Lazy<T> — thread-safe e lazy\n\n`Lazy<T>` é a forma recomendada em C# moderno: inicializa **apenas na primeira vez** que `.Value` é acessado, de forma **thread-safe**:\n\n```csharp\npublic sealed class ConfiguracaoApp\n{\n    // sealed impede herança que poderia quebrar a unicidade\n    private static readonly Lazy<ConfiguracaoApp> _instancia =\n        new(() => new ConfiguracaoApp());\n\n    public static ConfiguracaoApp Instancia => _instancia.Value;\n\n    // Configurações\n    public string Ambiente     { get; }\n    public string ApiBaseUrl   { get; }\n\n    private ConfiguracaoApp() // private: ninguém cria externamente\n    {\n        Ambiente   = Environment.GetEnvironmentVariable(\"ASPNETCORE_ENVIRONMENT\") ?? \"Production\";\n        ApiBaseUrl = Environment.GetEnvironmentVariable(\"API_URL\") ?? \"https://api.exemplo.com\";\n    }\n}\n\n// Uso\nvar config = ConfiguracaoApp.Instancia;\nConsole.WriteLine(config.Ambiente);\n```\n\n### Por que `sealed`?\n\n`sealed` impede que alguém herde da classe e crie subclasses, o que quebraria a garantia de unicidade:\n\n```csharp\n// SEM sealed — alguém pode criar subclasses\npublic class ConfigHerdada : ConfiguracaoApp { } // criaria uma segunda instância!\n\n// COM sealed — herança proibida\npublic sealed class ConfiguracaoApp { ... }\n// public class Sub : ConfiguracaoApp { } // ERRO de compilação\n```\n\n### AddSingleton no ASP.NET Core — a forma preferida\n\nEm aplicações ASP.NET Core, prefira o container de DI ao Singleton estático:\n\n```csharp\n// Program.cs\nbuilder.Services.AddSingleton<ICacheService, MemoryCacheService>();\n// AddSingleton  = UMA instância por aplicação\n// AddScoped     = UMA instância por requisição HTTP\n// AddTransient  = UMA NOVA instância a cada injeção\n\n// Vantagens sobre Singleton estático:\n// 1. Pode ser mockado em testes (depende da interface)\n// 2. O DI gerencia o ciclo de vida\n// 3. Sem estado global estático — mais previsível\n```\n\n### Quando usar e quando evitar\n\n```csharp\n// BOM: configurações globais imutáveis, loggers, conexão com recurso único\npublic sealed class DatabaseConfig\n{\n    private static readonly Lazy<DatabaseConfig> _inst = new(() => new());\n    public static DatabaseConfig Instancia => _inst.Value;\n    public string ConnectionString { get; } = \"Data Source=app.db\";\n    private DatabaseConfig() { }\n}\n\n// RUIM: estado mutável compartilhado sem lock — causa race conditions\npublic sealed class ContadorGlobal\n{\n    public static ContadorGlobal Instancia => ...\n    public int Valor { get; set; } // PROBLEMA: múltiplas threads escrevendo ao mesmo tempo!\n    // Solução: use Interlocked.Increment(ref _valor) para operações atômicas\n}\n```\n\n> **Regra**: Singleton é para **estado imutável ou cuidadosamente sincronizado**. Estado mutável compartilhado sem sincronização causa bugs difíceis de reproduzir.", "Garantindo uma única instância com Lazy<T>, thread safety e DI" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual palavra-chave permite que o compilador C# infira o tipo de uma variável automaticamente?", "'var' é uma palavra-chave de inferência de tipo em tempo de compilação. O tipo é definido pelo compilador com base no valor atribuído.", "[\"var\",\"dynamic\",\"object\",\"auto\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Em C#, 'string' e 'String' representam o mesmo tipo.", "'string' é um alias da linguagem para 'System.String'. Ambos são idênticos e intercambiáveis." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para declarar um valor que não pode ser alterado em C#, usa-se a palavra-chave: ____", "'const' declara uma constante em tempo de compilação. Diferente de 'readonly', que é resolvida em tempo de execução." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual estrutura é mais adequada em C# para comparar uma variável contra múltiplos valores constantes?", "'switch' é otimizado para comparação de um valor contra constantes. A switch expression do C# 8+ torna ainda mais conciso.", "[\"switch\",\"if/else\",\"while\",\"do/while\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "É possível modificar os elementos de uma coleção durante uma iteração com foreach.", "foreach não permite modificar a coleção durante a iteração. Para isso, use um loop for convencional ou crie uma nova lista." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "O operador ternário em C# segue a sintaxe: condição ____ valorTrue : valorFalse", "O operador ternário usa '?' para separar a condição do valor verdadeiro, e ':' para separar os dois valores." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Qual modificador de acesso restringe um método ao escopo da própria classe?", "'private' limita o acesso ao membro apenas dentro da classe que o declara. É o modificador mais restritivo." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Em C# é possível ter dois métodos com o mesmo nome desde que tenham assinaturas diferentes (sobrecarga).", "Sobrecarga (overloading) permite múltiplos métodos com o mesmo nome, diferenciados pelo número ou tipo de parâmetros." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 9,
                column: "Explicacao",
                value: "'void' indica ausência de retorno. Para métodos assíncronos sem retorno, usa-se 'Task' no lugar de 'async void'.");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Explicacao", "OpcoesJson" },
                values: new object[] { "O construtor tem o mesmo nome da classe, não tem tipo de retorno e é chamado automaticamente ao instanciar o objeto com 'new'.", "[\"Método especial que inicializa uma instância da classe\",\"Uma propriedade obrigatória\",\"Um tipo de herança\",\"Uma interface implícita\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Uma classe pode ter múltiplos construtores com parâmetros diferentes.", "Isso é sobrecarga de construtor. É comum ter um construtor padrão (sem parâmetros) e outros construtores com parâmetros." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para criar uma nova instância de uma classe chamada Carro, usamos: var carro = ____ Carro();", "'new' aloca memória e chama o construtor da classe para inicializar o objeto." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Qual símbolo é usado em C# para indicar que uma classe herda de outra?", "Em C# usamos ':' para herança (e também para implementar interfaces). Ex: 'public class Cachorro : Animal'." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Em C#, uma classe pode herdar de múltiplas classes ao mesmo tempo.", "C# não suporta herança múltipla de classes. Para comportamento múltiplo, usa-se interfaces, que podem ser implementadas em quantidade ilimitada." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para sobrescrever um método virtual na subclasse, usa-se a palavra-chave: ____", "'override' indica que o método substitui a implementação da classe base marcada com 'virtual' ou 'abstract'." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "Arrays têm tamanho definido na criação. List<T> cresce dinamicamente com Add() e encolhe com Remove().", "[\"List<T> tem tamanho dinâmico, Array tem tamanho fixo\",\"Array é mais seguro que List<T>\",\"List<T> não aceita tipos genéricos\",\"São equivalentes em todos os aspectos\"]", "List<T> tem tamanho dinâmico, Array tem tamanho fixo" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Enunciado", "Explicacao", "RespostaCorreta" },
                values: new object[] { "O método List<T>.Add() adiciona elementos ao início da lista.", "Add() adiciona ao FINAL da lista. Para adicionar no início ou em posição específica, use Insert(index, item).", "Falso" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para obter a quantidade de elementos em uma List<T>, usa-se a propriedade: ____", "List<T>.Count retorna o número de elementos. Arrays usam .Length. LINQ usa .Count() (método de extensão)." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "Qual método LINQ retorna o primeiro elemento que satisfaz uma condição e lança exceção se não encontrar?", "First() lança InvalidOperationException se não houver elemento. Use FirstOrDefault() para retornar null/default quando não encontrar.", "[\"First()\",\"Select()\",\"Where()\",\"Take()\"]", "First()" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "LINQ só funciona com coleções em memória (LINQ to Objects).", "LINQ funciona com qualquer IQueryable<T> ou IEnumerable<T>: banco de dados via EF Core (LINQ to SQL), XML, arquivos, etc." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para filtrar elementos em LINQ com uma condição, usa-se o método: ____", "Where() filtra elementos que satisfazem um predicado. Retorna IEnumerable<T> com os elementos que passaram no filtro." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "O que acontece quando a runtime encontra um 'await' em um método assíncrono?", "await suspende o método atual sem bloquear a thread. A thread retorna ao pool e pode processar outras requisições, melhorando a escalabilidade.", "[\"A thread é liberada para outras tarefas enquanto aguarda\",\"O programa é pausado completamente\",\"Uma nova thread é criada automaticamente\",\"O método é cancelado\"]", "A thread é liberada para outras tarefas enquanto aguarda" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Todo método que usa 'await' deve ter 'async' em sua declaração.", "'async' e 'await' são inseparáveis. 'async' marca o método como assíncrono e habilita o uso de 'await' dentro dele." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Um método assíncrono que não retorna valor deve ter tipo de retorno: ____", "Use 'Task' (não 'async void') para métodos assíncronos sem retorno. 'async void' não pode ser aguardado e dificulta o tratamento de exceções." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Explicacao", "OpcoesJson" },
                values: new object[] { "O Repository isola 'como' os dados são obtidos (SQL, EF, API externa) da lógica que decide 'o que' fazer com eles. Facilita testes e troca de infraestrutura.", "[\"Desacoplar a lógica de negócio do acesso a dados\",\"Aumentar a velocidade das queries\",\"Reduzir a quantidade de código\",\"Substituir completamente o ORM\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 26,
                column: "Explicacao",
                value: "Controllers NUNCA devem acessar infraestrutura diretamente. Se precisar de performance, otimize o Repository (projeções, AsNoTracking, paginação).");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 27,
                column: "Explicacao",
                value: "Dependência de abstrações (interfaces) é o Dependency Inversion Principle (D do SOLID). Permite trocar SQLite por SQL Server sem alterar o Controller.");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Qual modificador de acesso permite que membros sejam acessados apenas pela classe e suas subclasses?", "'protected' é visível dentro da classe e em qualquer classe que herde dela. Ideal para membros que fazem parte do contrato de herança." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Uma propriedade com 'get' público e 'set' privado pode ser alterada de fora da classe.", "Com 'private set', apenas a própria classe pode modificar o valor. Isso é encapsulamento: expõe leitura mas protege escrita." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para criar uma propriedade que só pode ser definida durante a inicialização do objeto, usa-se o acessor: ____", "'init' (C# 9+) permite definir o valor apenas no inicializador de objeto '{ }'. Depois disso, a propriedade torna-se somente leitura." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Qual método LINQ agrupa elementos por uma chave?", "GroupBy agrupa elementos em subconjuntos por uma chave. Retorna IEnumerable<IGrouping<TKey, TElement>>." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "O método Aggregate pode ser usado para concatenar strings de uma lista.", "Aggregate é uma redução genérica. Pode somar, multiplicar, concatenar ou qualquer operação que combine dois elementos em um." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para pegar apenas os primeiros N elementos de uma coleção em LINQ, usa-se o método: ____", "Take(n) retorna os primeiros n elementos. Combinado com Skip, implementa paginação: Skip((page-1)*size).Take(size)." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual classe é usada para criar e controlar um CancellationToken?", "CancellationTokenSource cria o token e expõe o método Cancel(). O token em si (CancellationToken) é passado para as operações que devem respeitar o cancelamento.", "[\"CancellationTokenSource\",\"TaskCanceller\",\"CancellationController\",\"TokenFactory\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Quando um CancellationToken é cancelado, Task.Delay lança automaticamente uma OperationCanceledException.", "A maioria dos métodos async do .NET aceita CancellationToken e lança OperationCanceledException quando cancelado. Isso inclui Task.Delay, HttpClient, EF Core, etc." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para verificar manualmente se um token foi cancelado e lançar exceção, usa-se: ct.____IfCancellationRequested()", "ThrowIfCancellationRequested() verifica ct.IsCancellationRequested e lança OperationCanceledException se verdadeiro. Use no início de loops ou operações longas." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual método aguarda a conclusão de TODAS as tasks de um array?", "Task.WhenAll é assíncrono (retorna Task) e aguarda todas. Task.WaitAll bloqueia a thread atual — evite em código async.", "[\"Task.WhenAll\",\"Task.WhenAny\",\"Task.WaitAll\",\"Task.RunAll\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Task.WhenAny completa assim que QUALQUER uma das tasks terminar, mesmo que as outras ainda estejam rodando.", "WhenAny retorna a primeira task que completar (com sucesso ou erro). As outras continuam executando em background." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para executar múltiplas tasks em paralelo e aguardar todas, usamos: await Task.____( task1, task2 )", "Task.WhenAll(task1, task2) inicia ambas simultaneamente e retorna quando as duas terminam. Muito mais eficiente que await task1; await task2 em sequência." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson", "RespostaCorreta" },
                values: new object[] { "Qual é o principal objetivo do Factory Pattern?", "Factory encapsula 'como' criar objetos. O cliente recebe um produto sem conhecer a classe concreta — depende apenas de uma abstração.", "[\"Encapsular a criação de objetos\",\"Garantir uma única instância\",\"Separar interface de implementação\",\"Observar mudanças de estado\"]", "Encapsular a criação de objetos" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 41,
                column: "Explicacao",
                value: "Esse é justamente o benefício: o cliente depende apenas da abstração (interface ou classe base). A factory decide qual concreto instanciar.");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Um método estático que cria e retorna instâncias de diferentes subclasses é chamado de ____ Factory.", "Simple Factory é o padrão mais básico: um método (geralmente estático) que decide qual classe concreta instanciar com base em um parâmetro." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Enunciado", "Explicacao", "OpcoesJson" },
                values: new object[] { "Qual classe do .NET garante inicialização lazy e thread-safe de um Singleton?", "Lazy<T> inicializa o valor apenas na primeira vez que .Value é acessado, de forma thread-safe. É a forma preferida de implementar Singleton em C#.", "[\"Lazy<T>\",\"ThreadLocal<T>\",\"Concurrent<T>\",\"Once<T>\"]" });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Em ASP.NET Core, AddSingleton registra uma instância que é criada uma única vez e reutilizada em toda a aplicação.", "AddSingleton = uma instância por aplicação. AddScoped = uma por requisição HTTP. AddTransient = uma nova a cada injeção." });

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Enunciado", "Explicacao" },
                values: new object[] { "Para impedir que uma classe Singleton seja herdada, usa-se o modificador: ____", "'sealed' impede herança. É recomendado em Singletons para garantir que ninguém possa criar subclasses que quebrem a unicidade da instância." });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Variáveis em C#\n\nC# é uma linguagem fortemente tipada. Cada variável possui um tipo definido em tempo de compilação.\n\n### Tipos primitivos\n```csharp\nint idade = 25;\ndouble salario = 4500.50;\nbool ativo = true;\nstring nome = \"João\";\n```\n\n### Inferência de tipo com var\nA palavra-chave `var` permite que o compilador infira o tipo:\n```csharp\nvar cidade = \"São Paulo\"; // string\nvar populacao = 12_000_000; // int\n```\n\n### Constantes\n```csharp\nconst double PI = 3.14159;\n```", "int, string, bool, double e inferência de tipo" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Controle de Fluxo\n\n### if/else\n```csharp\nif (nota >= 7)\n    Console.WriteLine(\"Aprovado\");\nelse\n    Console.WriteLine(\"Reprovado\");\n```\n\n### switch expression (C# moderno)\n```csharp\nvar resultado = nota switch\n{\n    >= 9 => \"A\",\n    >= 7 => \"B\",\n    >= 5 => \"C\",\n    _ => \"D\"\n};\n```\n\n### foreach\n```csharp\nvar nomes = new[] { \"Ana\", \"Bruno\", \"Carlos\" };\nforeach (var nome in nomes)\n    Console.WriteLine(nome);\n```", "if/else, switch, loops for e foreach" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Métodos em C#\n\n### Declaração básica\n```csharp\npublic int Somar(int a, int b) => a + b;\n```\n\n### Parâmetros opcionais\n```csharp\npublic string Saudar(string nome, string prefixo = \"Olá\")\n    => $\"{prefixo}, {nome}!\";\n```\n\n### Sobrecarga\nDois métodos com o mesmo nome mas parâmetros diferentes:\n```csharp\npublic double Calcular(double x) => x * x;\npublic double Calcular(double x, double y) => x * y;\n```", "Declaração, parâmetros, retorno e sobrecarga" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Classes em C#\n\n```csharp\npublic class Produto\n{\n    public int Id { get; set; }\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n\n    public Produto(string nome, decimal preco)\n    {\n        Nome = nome;\n        Preco = preco;\n    }\n\n    public string Descricao() => $\"{Nome} - R$ {Preco:F2}\";\n}\n\n// Uso:\nvar produto = new Produto(\"Notebook\", 3500m);\nConsole.WriteLine(produto.Descricao());\n```", "Propriedades, construtores e instanciação" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Herança\n\n```csharp\npublic class Animal\n{\n    public string Nome { get; set; } = string.Empty;\n    public virtual string EmitirSom() => \"...\";\n}\n\npublic class Cachorro : Animal\n{\n    public override string EmitirSom() => \"Au au!\";\n}\n```\n\n## Interfaces\n```csharp\npublic interface IVoador\n{\n    void Voar();\n}\n\npublic class Passaro : Animal, IVoador\n{\n    public override string EmitirSom() => \"Piu piu!\";\n    public void Voar() => Console.WriteLine(\"Voando...\");\n}\n```", "Herança, override e interfaces" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Coleções em C#\n\n### Array (tamanho fixo)\n```csharp\nint[] numeros = { 1, 2, 3, 4, 5 };\nConsole.WriteLine(numeros.Length); // 5\n```\n\n### List<T> (tamanho dinâmico)\n```csharp\nvar nomes = new List<string> { \"Ana\", \"Bruno\" };\nnomes.Add(\"Carlos\");\nnomes.Remove(\"Ana\");\nConsole.WriteLine(nomes.Count); // 2\n```\n\n### Dictionary<TKey, TValue>\n```csharp\nvar capitais = new Dictionary<string, string>\n{\n    [\"SP\"] = \"São Paulo\",\n    [\"RJ\"] = \"Rio de Janeiro\"\n};\n```", "List<T>, Array e operações comuns" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## LINQ — Language Integrated Query\n\n```csharp\nvar numeros = new List<int> { 5, 3, 8, 1, 9, 2 };\n\n// Filtrar\nvar pares = numeros.Where(n => n % 2 == 0).ToList();\n\n// Transformar\nvar dobrados = numeros.Select(n => n * 2).ToList();\n\n// Ordenar\nvar ordenados = numeros.OrderBy(n => n).ToList();\n\n// Primeiro elemento\nvar maior = numeros.Max();\nvar primeiro = numeros.First(n => n > 5);\n\n// Encadeamento\nvar resultado = numeros\n    .Where(n => n > 3)\n    .OrderByDescending(n => n)\n    .Select(n => $\"Número: {n}\")\n    .ToList();\n```", "Where, Select, OrderBy e First" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Programação Assíncrona\n\nasync/await permite executar operações sem bloquear a thread principal.\n\n```csharp\npublic async Task<string> BuscarDadosAsync(int id)\n{\n    // Simula chamada a banco/API\n    await Task.Delay(100);\n    return $\"Dados do id {id}\";\n}\n\n// Chamando:\nvar dados = await BuscarDadosAsync(42);\n```\n\n### Múltiplas tasks em paralelo\n```csharp\nvar task1 = BuscarDadosAsync(1);\nvar task2 = BuscarDadosAsync(2);\n\nvar resultados = await Task.WhenAll(task1, task2);\n```\n\n### Método sem retorno\n```csharp\npublic async Task SalvarAsync(string dados)\n{\n    await File.WriteAllTextAsync(\"arquivo.txt\", dados);\n}\n```", "Tarefas assíncronas com Task e async/await" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Repository Pattern\n\nIsola a lógica de acesso a dados da lógica de negócio.\n\n```csharp\n// 1. Interface — define o contrato\npublic interface IProdutoRepository\n{\n    Task<Produto?> ObterPorIdAsync(int id);\n    Task<IEnumerable<Produto>> ObterTodosAsync();\n    Task AdicionarAsync(Produto produto);\n    Task<bool> SalvarAsync();\n}\n\n// 2. Implementação — detalhe de infraestrutura\npublic class ProdutoRepository(AppDbContext ctx) : IProdutoRepository\n{\n    public async Task<Produto?> ObterPorIdAsync(int id)\n        => await ctx.Produtos.FindAsync(id);\n\n    public async Task AdicionarAsync(Produto produto)\n        => await ctx.Produtos.AddAsync(produto);\n\n    public async Task<bool> SalvarAsync()\n        => await ctx.SaveChangesAsync() > 0;\n}\n\n// 3. Controller — depende da interface, nunca da implementação\npublic class ProdutoController(IProdutoRepository repo) : ControllerBase { }\n```", "Abstraindo o acesso a dados com Repository" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Encapsulamento\n\nEncapsulamento esconde os detalhes internos e expõe apenas o necessário.\n\n### Modificadores de acesso\n```csharp\npublic class ContaBancaria\n{\n    private decimal _saldo;           // só a classe acessa\n    protected string Titular { get; } // classe + herdeiras\n    internal int AgenciaId { get; }   // mesmo assembly\n    public decimal Saldo => _saldo;   // leitura pública, escrita privada\n\n    public void Depositar(decimal valor)\n    {\n        if (valor <= 0) throw new ArgumentException(\"Valor inválido\");\n        _saldo += valor;\n    }\n\n    public bool Sacar(decimal valor)\n    {\n        if (valor > _saldo) return false;\n        _saldo -= valor;\n        return true;\n    }\n}\n```\n\n### Propriedades com validação\n```csharp\npublic class Produto\n{\n    private decimal _preco;\n\n    public decimal Preco\n    {\n        get => _preco;\n        set\n        {\n            if (value < 0) throw new ArgumentException(\"Preço não pode ser negativo\");\n            _preco = value;\n        }\n    }\n}\n```\n\n### init — propriedade somente para construção\n```csharp\npublic class Pedido\n{\n    public int Id { get; init; }     // só pode ser definido no new {}\n    public DateTime Criado { get; } = DateTime.UtcNow;\n}\n\nvar pedido = new Pedido { Id = 42 }; // OK\n// pedido.Id = 1;                    // ERRO em tempo de compilação\n```", "Modificadores de acesso, propriedades e princípio do encapsulamento" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## LINQ Avançado\n\n### GroupBy — agrupa por chave\n```csharp\nvar pedidos = new List<Pedido> { ... };\n\nvar porCliente = pedidos\n    .GroupBy(p => p.ClienteId)\n    .Select(g => new {\n        ClienteId = g.Key,\n        Total = g.Sum(p => p.Valor),\n        Quantidade = g.Count()\n    });\n```\n\n### Join — combina duas coleções\n```csharp\nvar resultado = clientes.Join(\n    pedidos,\n    c => c.Id,\n    p => p.ClienteId,\n    (c, p) => new { c.Nome, p.Valor }\n);\n```\n\n### Aggregate — redução customizada\n```csharp\nvar numeros = new[] { 1, 2, 3, 4, 5 };\n\n// Equivalente a um fold/reduce\nvar produto = numeros.Aggregate(1, (acc, n) => acc * n); // 120\nvar frase = new[] { \"C#\", \"é\", \"incrível\" }\n    .Aggregate((a, b) => $\"{a} {b}\"); // \"C# é incrível\"\n```\n\n### Any, All, Contains\n```csharp\nvar tem18 = idades.Any(i => i >= 18);\nvar todosMaiores = idades.All(i => i >= 0);\nvar temDez = numeros.Contains(10);\n```\n\n### Paginação\n```csharp\nint pagina = 2, tamanho = 10;\nvar paginado = produtos\n    .OrderBy(p => p.Nome)\n    .Skip((pagina - 1) * tamanho)\n    .Take(tamanho)\n    .ToList();\n```", "GroupBy, Join, Aggregate e projeções complexas" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## CancellationToken\n\nPermite cancelar operações assíncronas de forma cooperativa (sem matar threads à força).\n\n### Criando e passando o token\n```csharp\nusing var cts = new CancellationTokenSource();\ncts.CancelAfter(TimeSpan.FromSeconds(5)); // cancela após 5s\n\ntry\n{\n    var resultado = await BuscarDadosAsync(cts.Token);\n}\ncatch (OperationCanceledException)\n{\n    Console.WriteLine(\"Operação cancelada!\");\n}\n```\n\n### Recebendo e respeitando o token\n```csharp\npublic async Task<string> BuscarDadosAsync(CancellationToken ct = default)\n{\n    // Verifica antes de operações longas\n    ct.ThrowIfCancellationRequested();\n\n    await Task.Delay(2000, ct); // Task.Delay já respeita o token\n\n    return \"dados\";\n}\n```\n\n### Em ASP.NET Core\nOs controllers recebem o token automaticamente:\n```csharp\n[HttpGet]\npublic async Task<IActionResult> Get(CancellationToken ct)\n{\n    var dados = await _repo.ObterTodosAsync(ct);\n    return Ok(dados);\n}\n```\nSe o cliente desconectar, `ct` é cancelado automaticamente.", "Cancelamento cooperativo de operações assíncronas" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Paralelismo com Tasks\n\n### Task.WhenAll — aguarda TODAS as tasks\n```csharp\n// Sem paralelismo (sequencial — lento)\nvar u1 = await BuscarUsuarioAsync(1); // espera\nvar u2 = await BuscarUsuarioAsync(2); // espera\n\n// Com paralelismo — muito mais rápido!\nvar task1 = BuscarUsuarioAsync(1);\nvar task2 = BuscarUsuarioAsync(2);\nvar (u1, u2) = await (task1, task2); // ambas simultâneas\n\n// Ou com array:\nvar ids = new[] { 1, 2, 3, 4 };\nvar usuarios = await Task.WhenAll(ids.Select(id => BuscarUsuarioAsync(id)));\n```\n\n### Task.WhenAny — aguarda a PRIMEIRA a completar\n```csharp\n// Cache race: quem responder primeiro vence\nvar taskBanco = BuscarNoBancoAsync(id);\nvar taskCache = BuscarNoCacheAsync(id);\n\nvar primeira = await Task.WhenAny(taskBanco, taskCache);\nvar resultado = await primeira; // garante exceções propagadas\n```\n\n### Cuidados\n```csharp\n// Task.WhenAll propaga TODAS as exceções\ntry\n{\n    await Task.WhenAll(task1, task2, task3);\n}\ncatch (Exception ex)\n{\n    // ex contém apenas a primeira — inspecione task1.Exception, etc.\n}\n```", "Execução paralela de múltiplas tasks assíncronas" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Factory Pattern\n\nEncapsula a criação de objetos, permitindo que o código cliente não conheça as classes concretas.\n\n### Simple Factory\n```csharp\npublic abstract class Notificacao\n{\n    public abstract void Enviar(string mensagem);\n}\n\npublic class NotificacaoEmail : Notificacao\n{\n    public override void Enviar(string msg) => Console.WriteLine($\"Email: {msg}\");\n}\n\npublic class NotificacaoSms : Notificacao\n{\n    public override void Enviar(string msg) => Console.WriteLine($\"SMS: {msg}\");\n}\n\npublic static class NotificacaoFactory\n{\n    public static Notificacao Criar(string tipo) => tipo switch\n    {\n        \"email\" => new NotificacaoEmail(),\n        \"sms\"   => new NotificacaoSms(),\n        _ => throw new ArgumentException($\"Tipo desconhecido: {tipo}\")\n    };\n}\n\n// Uso — o cliente não instancia diretamente\nvar notif = NotificacaoFactory.Criar(\"email\");\nnotif.Enviar(\"Pedido confirmado!\");\n```\n\n### Por que usar?\n- O tipo concreto pode mudar sem alterar o código cliente\n- Centraliza a lógica de criação (configurações, validações)\n- Facilita testes (pode injetar factory mockada)", "Criando objetos sem expor a lógica de construção" });

            migrationBuilder.UpdateData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ConteudoTeoricoMarkdown", "Descricao" },
                values: new object[] { "## Singleton Pattern\n\nGarante que uma classe tenha apenas uma instância durante toda a vida da aplicação.\n\n### Implementação thread-safe com Lazy<T>\n```csharp\npublic sealed class ConfiguracaoApp\n{\n    // Lazy<T> garante thread-safety e lazy initialization\n    private static readonly Lazy<ConfiguracaoApp> _instancia =\n        new(() => new ConfiguracaoApp());\n\n    public static ConfiguracaoApp Instancia => _instancia.Value;\n\n    public string Ambiente { get; private set; }\n    public string ConnectionString { get; private set; }\n\n    private ConfiguracaoApp()\n    {\n        Ambiente = Environment.GetEnvironmentVariable(\"ASPNETCORE_ENVIRONMENT\") ?? \"Development\";\n        ConnectionString = \"Data Source=app.db\";\n    }\n}\n\n// Uso\nvar config = ConfiguracaoApp.Instancia;\nConsole.WriteLine(config.Ambiente);\n```\n\n### Em ASP.NET Core — prefira o container de DI\n```csharp\n// Program.cs — AddSingleton registra uma única instância\nbuilder.Services.AddSingleton<ICacheService, MemoryCacheService>();\n\n// O container garante a unicidade e facilita os testes\n// (diferente do Singleton estático que dificulta mock)\n```\n\n### Quando usar?\n- Configurações globais, loggers, conexões com recursos únicos\n- **Evite** para estado mutável compartilhado — causa bugs em concorrência", "Garantindo uma única instância com thread safety" });
        }
    }
}
