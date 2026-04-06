using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Data;

/// <summary>
/// Atualiza o conteúdo teórico das lições em bancos já existentes.
/// Roda a cada deploy e é idempotente — seguro chamar múltiplas vezes.
/// Necessário porque EnsureCreated() não reaplicar seed data em BDs existentes.
/// </summary>
public static class ContentSeeder
{
    // Versão atual do conteúdo — incremente ao fazer mudanças significativas
    private const string VersaoConteudo = "v2-conteudo-iniciantes";
    private const string ChaveVersao    = "content_version";

    public static async Task AtualizarSeNecessarioAsync(AppDbContext db)
    {
        // Verifica se o conteúdo já está na versão atual via tabela de configuração
        // Como não temos tabela de config, usa heurística: verifica se a lição 1
        // já tem o contexto real adicionado nesta versão
        var licao1 = await db.Licoes
            .AsNoTracking()
            .Where(l => l.Id == 1)
            .Select(l => l.ConteudoTeoricoMarkdown)
            .FirstOrDefaultAsync();

        if (licao1 != null && licao1.Contains("Cenário real"))
            return; // Conteúdo já está atualizado

        await AtualizarConteudoAsync(db);
    }

    private static async Task AtualizarConteudoAsync(AppDbContext db)
    {
        var atualizacoes = ObterAtualizacoes();

        foreach (var (id, conteudo) in atualizacoes)
        {
            await db.Licoes
                .Where(l => l.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(l => l.ConteudoTeoricoMarkdown, conteudo));
        }
    }

    private static Dictionary<int, string> ObterAtualizacoes() => new()
    {
        // ── Lição 1: Variáveis e Tipos ───────────────────────────────────────────
        [1] =
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
            "| Datas e horas | `DateTime` | Nunca use string para armazenar datas! |",

        // ── Lição 2: Controle de Fluxo ───────────────────────────────────────────
        [2] =
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
            "| Executar pelo menos uma vez, depois verificar | `do-while` |",

        // ── Lição 3: Métodos e Funções ────────────────────────────────────────────
        [3] =
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
            "Um bom nome **documenta sozinho**: `CalcularImpostoNFe()` é melhor do que `Calcular()` com um comentário explicando.",
    };
}
