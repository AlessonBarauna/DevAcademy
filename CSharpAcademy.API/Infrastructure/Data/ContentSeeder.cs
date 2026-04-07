using CSharpAcademy.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademy.API.Infrastructure.Data;

/// <summary>
/// Atualiza o conteúdo teórico das lições em bancos já existentes.
/// Roda a cada deploy e é idempotente — seguro chamar múltiplas vezes.
/// Necessário porque EnsureCreated() não reaplicar seed data em BDs existentes.
/// </summary>
public static class ContentSeeder
{
    public static async Task AtualizarSeNecessarioAsync(AppDbContext db)
    {
        var licao1 = await db.Licoes
            .AsNoTracking()
            .Where(l => l.Id == 1)
            .Select(l => l.ConteudoTeoricoMarkdown)
            .FirstOrDefaultAsync();

        if (licao1 != null && licao1.Contains("Cenário real"))
        {
            // Conteúdo base já está atualizado — verifica apenas se novas lições/exercícios existem
            await InserirNovosConteudosSeNecessarioAsync(db);
            return;
        }

        await AtualizarConteudoAsync(db);
        await InserirNovosConteudosSeNecessarioAsync(db);
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

    private static async Task InserirNovosConteudosSeNecessarioAsync(AppDbContext db)
    {
        await InserirNovasLicoesAsync(db);
        await InserirNovosExerciciosAsync(db);
    }

    // ── Novas Lições (40-43) ──────────────────────────────────────────────────

    private static async Task InserirNovasLicoesAsync(AppDbContext db)
    {
        var idsExistentes = await db.Licoes
            .Where(l => l.Id >= 40 && l.Id <= 43)
            .Select(l => l.Id)
            .ToListAsync();

        var novasLicoes = ObterNovasLicoes()
            .Where(l => !idsExistentes.Contains(l.Id))
            .ToList();

        if (novasLicoes.Count == 0) return;

        db.Licoes.AddRange(novasLicoes);
        await db.SaveChangesAsync();
    }

    private static List<Domain.Licao> ObterNovasLicoes() =>
    [
        new Domain.Licao
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
                "string msg = $\"Olá, {nome}! Você tem {idade} anos.\";\n\n" +
                "decimal preco = 1999.9m;\nstring fmt = $\"Preço: {preco:C2}\";\nstring pad = $\"{42:D6}\"; // 000042\n```\n\n" +
                "### Strings verbatim com `@`\n\n" +
                "```csharp\nstring caminho1 = \"C:\\\\Users\\\\Maria\\\\Docs\";\nstring caminho2 = @\"C:\\Users\\Maria\\Docs\";\n\n" +
                "string pasta = \"Downloads\";\nstring path  = $@\"C:\\Users\\Maria\\{pasta}\";\n```\n\n" +
                "### Métodos essenciais de string\n\n" +
                "```csharp\nstring s = \"  Olá, Mundo!  \";\n\n" +
                "s.Trim()                         // \"Olá, Mundo!\"\ns.ToUpper()                      // \"  OLÁ, MUNDO!  \"\ns.Contains(\"Mundo\")              // true\ns.Replace(\"Mundo\", \"C#\")        // \"  Olá, C#!  \"\ns.Split(',')                     // [\"  Olá\", \" Mundo!  \"]\nstring.IsNullOrWhiteSpace(\"  \") // true\n```\n\n" +
                "### StringBuilder — concatenação eficiente\n\n" +
                "```csharp\n// ❌ Ineficiente — cria uma nova string a cada iteração\nstring resultado = \"\";\nfor (int i = 0; i < 1000; i++)\n    resultado += i.ToString();\n\n" +
                "// ✅ Eficiente\nvar sb = new System.Text.StringBuilder();\nfor (int i = 0; i < 1000; i++)\n    sb.Append(i);\nstring resultado = sb.ToString();\n```\n\n" +
                "> **Regra**: use `+` para 2–5 concatenações. Use `StringBuilder` em loops.\n\n" +
                "### Comparação de strings\n\n" +
                "```csharp\nbool iguali = string.Equals(\"abc\", \"ABC\", StringComparison.OrdinalIgnoreCase); // true\n```"
        },

        new Domain.Licao
        {
            Id = 41, ModuloId = 1, Ordem = 5, XPRecompensa = 20, Ativo = true,
            Titulo = "Tratamento de Exceções",
            Descricao = "try/catch/finally, tipos de exceção, throw e criando exceções customizadas",
            ConteudoTeoricoMarkdown =
                "## Tratamento de Exceções em C#\n\n" +
                "> 💡 **Cenário real**: Sua API recebe uma requisição para buscar um produto por ID. " +
                "O banco pode estar offline, o ID pode não existir, a conexão pode cair no meio. " +
                "Tratamento de exceções é o que separa um sistema que *trava* de um que *responde adequadamente*.\n\n" +
                "### try / catch / finally\n\n" +
                "```csharp\ntry\n{\n    int resultado = int.Parse(entrada);\n    int divisao   = 10 / resultado;\n}\ncatch (FormatException ex)\n{\n    Console.WriteLine($\"Entrada inválida: {ex.Message}\");\n}\ncatch (DivideByZeroException)\n{\n    Console.WriteLine(\"Divisão por zero!\");\n}\ncatch (Exception ex)\n{\n    Console.WriteLine($\"Erro inesperado: {ex.Message}\");\n}\nfinally\n{\n    Console.WriteLine(\"Operação finalizada.\");\n}\n```\n\n" +
                "> **Ordem importa**: catches mais **específicos** antes dos mais **genéricos**.\n\n" +
                "### throw e relançando exceções\n\n" +
                "```csharp\n// ❌ ERRADO — perde o stack trace\ncatch (Exception ex) { throw ex; }\n\n// ✅ CORRETO — preserva o stack trace\ncatch (Exception) { throw; }\n\n// Encapsular em outra exceção\ncatch (Exception ex)\n    throw new InvalidOperationException(\"Falha ao processar pedido.\", ex);\n```\n\n" +
                "### Exceções customizadas\n\n" +
                "```csharp\npublic class ProdutoNaoEncontradoException : Exception\n{\n    public int ProdutoId { get; }\n    public ProdutoNaoEncontradoException(int id)\n        : base($\"Produto {id} não encontrado.\") => ProdutoId = id;\n}\n```\n\n" +
                "### using — liberação automática de recursos\n\n" +
                "```csharp\nusing var conexao = new SqlConnection(connStr);\n// Dispose() chamado automaticamente — mesmo com exceção\n```\n\n" +
                "> **Regra**: exceções são para situações **excepcionais**. Para validações esperadas, prefira `TryParse` e retornos de erro."
        },

        new Domain.Licao
        {
            Id = 42, ModuloId = 2, Ordem = 4, XPRecompensa = 20, Ativo = true,
            Titulo = "Interfaces",
            Descricao = "Definindo contratos, implementando múltiplas interfaces, interface vs classe abstrata",
            ConteudoTeoricoMarkdown =
                "## Interfaces em C#\n\n" +
                "> 💡 **Cenário real**: Sua aplicação precisa enviar notificações — ora por e-mail, " +
                "ora por SMS, ora por push notification. Você define uma interface `INotificador`. " +
                "Qualquer implementação concreta pode ser plugada sem alterar o código que usa a interface.\n\n" +
                "Uma **interface** define um **contrato** — o que uma classe deve fazer — sem ditar como ela faz.\n\n" +
                "### Declarando e implementando interfaces\n\n" +
                "```csharp\npublic interface INotificador\n{\n    Task EnviarAsync(string destinatario, string mensagem);\n    bool Disponivel { get; }\n}\n\npublic class NotificadorEmail : INotificador\n{\n    public bool Disponivel => true;\n    public async Task EnviarAsync(string destinatario, string mensagem)\n        => await smtp.SendAsync(destinatario, mensagem);\n}\n```\n\n" +
                "### Múltiplas interfaces\n\n" +
                "```csharp\npublic class Relatorio : DocumentoBase, IExportavel, IImprimivel, IArquivavel\n{\n    public byte[] Exportar()          => PdfGenerator.Gerar(this);\n    public void   Imprimir()          => Impressora.Enviar(this);\n    public async Task ArquivarAsync() => await Storage.SalvarAsync(this);\n}\n```\n\n" +
                "### Interface vs Classe Abstrata\n\n" +
                "| | Interface | Classe Abstrata |\n|---|---|---|\n| Estado (campos) | ❌ | ✅ |\n| Implementação | Default methods (C# 8+) | ✅ |\n| Herança | N interfaces | Apenas 1 |\n| Quando usar | Contrato entre hierarquias | Compartilhar lógica |\n\n" +
                "> **Regra de ouro**: dependa de abstrações (interfaces), não de implementações concretas."
        },

        new Domain.Licao
        {
            Id = 43, ModuloId = 2, Ordem = 5, XPRecompensa = 20, Ativo = true,
            Titulo = "Records e Imutabilidade",
            Descricao = "Record types, igualdade por valor, with expressions e quando usar records",
            ConteudoTeoricoMarkdown =
                "## Records em C#\n\n" +
                "> 💡 **Cenário real**: Sua API retorna dados de um produto — Id, Nome, Preço. " +
                "Esse dado é transferido pela rede, nunca deveria ser modificado após criado, " +
                "e dois objetos com os mesmos valores devem ser considerados iguais.\n\n" +
                "**Records** (C# 9+) são tipos com **igualdade por valor** e **imutabilidade** embutidos.\n\n" +
                "### Record posicional\n\n" +
                "```csharp\npublic record Produto(int Id, string Nome, decimal Preco);\n\nvar p1 = new Produto(1, \"Teclado\", 150m);\nvar p2 = new Produto(1, \"Teclado\", 150m);\n\nConsole.WriteLine(p1 == p2); // True — igualdade por VALOR\nConsole.WriteLine(p1);       // Produto { Id = 1, Nome = Teclado, Preco = 150 }\n```\n\n" +
                "### Expressão `with`\n\n" +
                "```csharp\nvar original    = new Produto(1, \"Teclado\", 150m);\nvar comDesconto = original with { Preco = 120m };\n\nConsole.WriteLine(original.Preco);    // 150 — não alterado\nConsole.WriteLine(comDesconto.Preco); // 120\n```\n\n" +
                "### Records vs Classes\n\n" +
                "| | Record | Class |\n|---|---|---|\n| Igualdade | Por **valor** | Por **referência** |\n| Imutabilidade | Padrão | Não |\n| ToString() | Gerado automaticamente | Precisa sobrescrever |\n| Quando usar | DTOs, Value Objects | Entidades com estado |\n\n" +
                "> **Regra**: se o objeto representa um **dado imutável** a ser comparado pelos valores, use record. " +
                "Se tem **ciclo de vida e estado mutável**, use classe."
        }
    ];

    // ── Novos Exercícios (122-195) ────────────────────────────────────────────

    private static async Task InserirNovosExerciciosAsync(AppDbContext db)
    {
        var idsExistentes = await db.Exercicios
            .Where(e => e.Id >= 122 && e.Id <= 195)
            .Select(e => e.Id)
            .ToListAsync();

        var novosExercicios = ObterNovosExercicios()
            .Where(e => !idsExistentes.Contains(e.Id))
            .ToList();

        if (novosExercicios.Count == 0) return;

        db.Exercicios.AddRange(novosExercicios);
        await db.SaveChangesAsync();
    }

    private static List<Domain.Exercicio> ObterNovosExercicios() =>
    [
        // ── Exercícios adicionais — Lição 1: Variáveis e Tipos ──────────────────
        new Domain.Exercicio { Id = 122, LicaoId = 1, Ordem = 4, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual tipo de dado deve ser usado para armazenar valores monetários em C#?",
            OpcoesJson = "[\"decimal\",\"double\",\"float\",\"long\"]",
            RespostaCorreta = "decimal",
            Explicacao = "'decimal' usa aritmética de base 10, eliminando erros de arredondamento binário. Sempre use decimal para dinheiro.",
            DicaTexto = "Qual tipo foi criado especificamente para evitar erros de arredondamento em valores financeiros?" },

        new Domain.Exercicio { Id = 123, LicaoId = 1, Ordem = 5, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Em C#, uma variável declarada com 'var' pode mudar de tipo após a declaração.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Falso",
            Explicacao = "'var' é inferência de tipo em TEMPO DE COMPILAÇÃO — o tipo é fixado pelo valor inicial e não pode mudar.",
            DicaTexto = "var é resolvido em tempo de compilação ou de execução?" },

        new Domain.Exercicio { Id = 124, LicaoId = 1, Ordem = 6, XPRecompensa = 5,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para converter uma string para int sem lançar exceção em caso de falha, usa-se int.____(entrada, out int valor)",
            OpcoesJson = "[]",
            RespostaCorreta = "TryParse",
            Explicacao = "int.TryParse() retorna bool e nunca lança exceção — ideal para validar entrada do usuário.",
            DicaTexto = "O método começa com 'Try' e retorna bool..." },

        new Domain.Exercicio { Id = 125, LicaoId = 1, Ordem = 7, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual operador atribui um valor padrão quando a variável é null?",
            OpcoesJson = "[\"??\",\"?.\",\"!.\",\"&&\"]",
            RespostaCorreta = "??",
            Explicacao = "O operador ?? é o 'null-coalescing operator': 'valor ?? padrão' retorna 'valor' se não for null, ou 'padrão' caso contrário.",
            DicaTexto = "Esse operador é formado por dois pontos de interrogação..." },

        new Domain.Exercicio { Id = 126, LicaoId = 1, Ordem = 8, XPRecompensa = 5,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O código abaixo tem um erro. Corrija-o:\ndecimal preco = 19.99;",
            OpcoesJson = "[\"decimal preco = 19.99m;\",\"decimal preco = (decimal)19.99;\",\"double preco = 19.99;\",\"float preco = 19.99f;\"]",
            RespostaCorreta = "decimal preco = 19.99m;",
            Explicacao = "Literais de ponto flutuante sem sufixo são 'double' por padrão. Para atribuir a uma variável decimal, use o sufixo 'm'.",
            DicaTexto = "Literais decimais precisam de um sufixo específico..." },

        new Domain.Exercicio { Id = 127, LicaoId = 1, Ordem = 9, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual é o range (intervalo) de valores do tipo 'byte' em C#?",
            OpcoesJson = "[\"0 a 255\",\"-128 a 127\",\"0 a 65535\",\"-32768 a 32767\"]",
            RespostaCorreta = "0 a 255",
            Explicacao = "'byte' é um inteiro sem sinal de 8 bits: armazena de 0 a 255 (2⁸ - 1).",
            DicaTexto = "byte tem 8 bits e é sem sinal (unsigned)..." },

        new Domain.Exercicio { Id = 128, LicaoId = 1, Ordem = 10, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "O tipo int? (nullable int) pode armazenar o valor null.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Adicionando '?' ao tipo de valor, tornamos ele nullable. 'int?' pode ser null, 0, 1, ou qualquer int.",
            DicaTexto = "O '?' após o tipo transforma um value type em nullable..." },

        // ── Exercícios adicionais — Lição 2: Controle de Fluxo ──────────────────
        new Domain.Exercicio { Id = 129, LicaoId = 2, Ordem = 4, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "O que a instrução 'break' faz dentro de um loop?",
            OpcoesJson = "[\"Encerra o loop imediatamente\",\"Pula para a próxima iteração\",\"Reinicia o loop do início\",\"Não faz nada\"]",
            RespostaCorreta = "Encerra o loop imediatamente",
            Explicacao = "'break' encerra o loop completamente. 'continue' pula apenas a iteração atual e vai para a próxima.",
            DicaTexto = "Qual instrução 'quebra' o loop e qual apenas 'continua' para a próxima?" },

        new Domain.Exercicio { Id = 130, LicaoId = 2, Ordem = 5, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "O loop 'do-while' garante que o bloco de código seja executado pelo menos uma vez.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Em 'do-while', o bloco é executado ANTES da verificação da condição. Garante ao menos uma execução.",
            DicaTexto = "Em do-while, a condição é verificada antes ou depois do bloco?" },

        new Domain.Exercicio { Id = 131, LicaoId = 2, Ordem = 6, XPRecompensa = 5,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para pular para a próxima iteração de um loop sem encerrar o loop, usa-se a instrução: ____",
            OpcoesJson = "[]",
            RespostaCorreta = "continue",
            Explicacao = "'continue' interrompe a iteração ATUAL e passa para a PRÓXIMA. O loop continua normalmente.",
            DicaTexto = "É o oposto de break — não encerra, apenas avança..." },

        new Domain.Exercicio { Id = 132, LicaoId = 2, Ordem = 7, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Na switch expression moderna de C#, qual símbolo representa o caso padrão (default)?",
            OpcoesJson = "[\"_\",\"*\",\"default\",\"else\"]",
            RespostaCorreta = "_",
            Explicacao = "Na switch expression (C# 8+), o underscore '_' é o discard pattern e funciona como o 'default'.",
            DicaTexto = "É um símbolo de descarte, usado como padrão em pattern matching..." },

        new Domain.Exercicio { Id = 133, LicaoId = 2, Ordem = 8, XPRecompensa = 5,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O código itera mais vezes do que o esperado. Corrija para percorrer exatamente os índices 0, 1, 2, 3, 4:\nfor (int i = 0; i <= 5; i++)",
            OpcoesJson = "[\"for (int i = 0; i < 5; i++)\",\"for (int i = 1; i <= 5; i++)\",\"for (int i = 0; i <= 4; i++)\",\"for (int i = 0; i < 6; i++)\"]",
            RespostaCorreta = "for (int i = 0; i < 5; i++)",
            Explicacao = "Com 'i <= 5' o loop executa 6 vezes (0–5). Para 5 iterações (índices 0–4), use 'i < 5'.",
            DicaTexto = "Conte quantas vezes o loop executa com <= vs <..." },

        new Domain.Exercicio { Id = 134, LicaoId = 2, Ordem = 9, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Um loop 'while' pode nunca executar seu bloco de código se a condição inicial for falsa.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "'while' verifica a condição ANTES de executar o bloco. Se falsa na primeira verificação, o bloco nunca executa.",
            DicaTexto = "while verifica antes ou depois de executar?" },

        new Domain.Exercicio { Id = 135, LicaoId = 2, Ordem = 10, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Por que 'foreach' é preferido a 'for' para percorrer coleções quando o índice não é necessário?",
            OpcoesJson = "[\"Mais legível e elimina erros de índice\",\"foreach é mais rápido que for\",\"foreach funciona com mais tipos\",\"for pode modificar a coleção, foreach não\"]",
            RespostaCorreta = "Mais legível e elimina erros de índice",
            Explicacao = "'foreach' é mais expressivo e elimina erros como índice errado ou off-by-one.",
            DicaTexto = "Pense na legibilidade e nos tipos de erro que cada um evita..." },

        // ── Exercícios adicionais — Lição 3: Métodos ────────────────────────────
        new Domain.Exercicio { Id = 136, LicaoId = 3, Ordem = 4, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "O que um método declarado como 'void' retorna?",
            OpcoesJson = "[\"Nada (sem valor de retorno)\",\"null\",\"0\",\"false\"]",
            RespostaCorreta = "Nada (sem valor de retorno)",
            Explicacao = "'void' significa ausência de retorno. O método executa sua lógica mas não produz valor.",
            DicaTexto = "void vem do latim e significa 'vazio'..." },

        new Domain.Exercicio { Id = 137, LicaoId = 3, Ordem = 5, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Em C#, dois métodos podem ter o mesmo nome se tiverem parâmetros diferentes (tipos ou quantidade). Esse conceito se chama sobrecarga.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Sobrecarga (overloading) permite múltiplos métodos com o mesmo nome e assinaturas diferentes.",
            DicaTexto = "O que diferencia dois métodos sobrecarregados?" },

        new Domain.Exercicio { Id = 138, LicaoId = 3, Ordem = 6, XPRecompensa = 5,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Parâmetros com valor padrão definido na assinatura do método são chamados parâmetros ____.",
            OpcoesJson = "[]",
            RespostaCorreta = "opcionais",
            Explicacao = "Parâmetros opcionais têm um valor padrão na assinatura. Se o chamador não fornecer o argumento, o valor padrão é usado.",
            DicaTexto = "Se o chamador não precisar fornecê-los, são..." },

        new Domain.Exercicio { Id = 139, LicaoId = 3, Ordem = 7, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "O que é um expression-bodied method (usando =>)?",
            OpcoesJson = "[\"Método de uma linha que retorna uma expressão sem 'return' explícito\",\"Método assíncrono\",\"Método estático\",\"Método sem parâmetros\"]",
            RespostaCorreta = "Método de uma linha que retorna uma expressão sem 'return' explícito",
            Explicacao = "'public int Dobro(int x) => x * 2;' elimina as chaves e o return para métodos simples.",
            DicaTexto = "É uma forma mais curta de escrever métodos simples com =>" },

        new Domain.Exercicio { Id = 140, LicaoId = 3, Ordem = 8, XPRecompensa = 5,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O método abaixo tem um erro de tipo de retorno. Corrija-o:\npublic void Somar(int a, int b) { return a + b; }",
            OpcoesJson = "[\"public int Somar(int a, int b) { return a + b; }\",\"public void Somar(int a, int b) { return; }\",\"public string Somar(int a, int b) { return a + b; }\",\"public Somar(int a, int b) { return a + b; }\"]",
            RespostaCorreta = "public int Somar(int a, int b) { return a + b; }",
            Explicacao = "Um método declarado como 'void' não pode retornar valores. O tipo de retorno deve ser 'int'.",
            DicaTexto = "void significa sem retorno. Se o método retorna algo, qual tipo deveria ser?" },

        new Domain.Exercicio { Id = 141, LicaoId = 3, Ordem = 9, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "A palavra-chave 'params' permite que um método receba um número variável de argumentos do mesmo tipo.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "'public int Somar(params int[] nums)' pode ser chamado com Somar(1,2), Somar(1,2,3,4) ou Somar().",
            DicaTexto = "params vem de 'parameters' e aceita quantidade variável..." },

        new Domain.Exercicio { Id = 142, LicaoId = 3, Ordem = 10, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual modificador de acesso é o mais restritivo em C#?",
            OpcoesJson = "[\"private\",\"protected\",\"internal\",\"public\"]",
            RespostaCorreta = "private",
            Explicacao = "'private' restringe o acesso apenas à própria classe. É o mais restritivo.",
            DicaTexto = "Qual modificador só permite acesso de dentro da própria classe?" },

        // ── Exercícios adicionais — Lição 4: Classes e Objetos ──────────────────
        new Domain.Exercicio { Id = 143, LicaoId = 4, Ordem = 4, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "O que o operador 'new' faz ao criar um objeto em C#?",
            OpcoesJson = "[\"Aloca memória no heap e chama o construtor\",\"Copia um objeto existente\",\"Aloca memória no stack\",\"Apenas chama o construtor sem alocar memória\"]",
            RespostaCorreta = "Aloca memória no heap e chama o construtor",
            Explicacao = "'new' aloca memória no heap, inicializa campos e chama o construtor especificado.",
            DicaTexto = "Classes são tipos de referência — onde ficam armazenadas?" },

        new Domain.Exercicio { Id = 144, LicaoId = 4, Ordem = 5, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Uma classe em C# pode ter múltiplos construtores com parâmetros diferentes.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Sobrecarga de construtores permite criar objetos de formas diferentes. O compilador escolhe o correto pelos argumentos passados.",
            DicaTexto = "É o mesmo conceito de sobrecarga aplicado ao construtor..." },

        new Domain.Exercicio { Id = 145, LicaoId = 4, Ordem = 6, XPRecompensa = 5,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Membros que pertencem à classe (e não às instâncias individuais) são declarados com o modificador ____.",
            OpcoesJson = "[]",
            RespostaCorreta = "static",
            Explicacao = "Membros 'static' existem uma única vez na classe, compartilhados por todas as instâncias.",
            DicaTexto = "Esse modificador faz o membro pertencer à classe, não ao objeto..." },

        new Domain.Exercicio { Id = 146, LicaoId = 4, Ordem = 7, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Dado: 'var p1 = new Produto { Nome = \"Teclado\" }; var p2 = p1; p2.Nome = \"Mouse\";'. Qual é o valor de p1.Nome?",
            OpcoesJson = "[\"Mouse\",\"Teclado\",\"null\",\"Erro de compilação\"]",
            RespostaCorreta = "Mouse",
            Explicacao = "Classes são tipos de referência. 'var p2 = p1' NÃO cria uma cópia — p2 aponta para o MESMO objeto. Qualquer alteração via p2 afeta p1 também.",
            DicaTexto = "Classes são tipos de referência — p2 é uma cópia ou outra referência ao mesmo objeto?" },

        new Domain.Exercicio { Id = 147, LicaoId = 4, Ordem = 8, XPRecompensa = 5,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O código abaixo tem um erro de nomenclatura. Corrija para seguir as convenções C#:\nvar p = new Produto { nome = \"Teclado\", preco = 150m };",
            OpcoesJson = "[\"var p = new Produto { Nome = \\\"Teclado\\\", Preco = 150m };\",\"var p = new produto { Nome = \\\"Teclado\\\", Preco = 150m };\",\"var p = Produto.New { Nome = \\\"Teclado\\\" };\",\"var p = new Produto(\\\"Teclado\\\", 150m);\"]",
            RespostaCorreta = "var p = new Produto { Nome = \"Teclado\", Preco = 150m };",
            Explicacao = "Propriedades públicas em C# seguem PascalCase. 'nome' e 'preco' devem ser 'Nome' e 'Preco'.",
            DicaTexto = "Propriedades em C# seguem PascalCase..." },

        new Domain.Exercicio { Id = 148, LicaoId = 4, Ordem = 9, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Inicializadores de objeto '{ }' permitem definir propriedades públicas sem necessidade de:",
            OpcoesJson = "[\"Um construtor específico para cada combinação\",\"O operador new\",\"O tipo da variável\",\"Propriedades públicas\"]",
            RespostaCorreta = "Um construtor específico para cada combinação",
            Explicacao = "Com inicializadores, 'new Produto { Nome = \"X\", Preco = 10m }' funciona sem um construtor com esses parâmetros.",
            DicaTexto = "O que você não precisa escrever quando usa inicializadores?" },

        new Domain.Exercicio { Id = 149, LicaoId = 4, Ordem = 10, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "O encadeamento de construtores com ':this(...)' permite que um construtor chame outro construtor da mesma classe.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "': this(args)' chama outro construtor da mesma classe. Para chamar o construtor da classe PAI, usa-se ':base(args)'.",
            DicaTexto = "this() chama construtor da mesma classe, base() chama da classe pai..." },

        // ── Exercícios adicionais — Lição 5: Herança e Polimorfismo ─────────────
        new Domain.Exercicio { Id = 150, LicaoId = 5, Ordem = 4, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Em C#, de quantas classes uma classe pode herdar diretamente?",
            OpcoesJson = "[\"Apenas uma\",\"Até duas\",\"Ilimitado\",\"Até três\"]",
            RespostaCorreta = "Apenas uma",
            Explicacao = "C# não suporta herança múltipla de classes. Para comportamento múltiplo, use interfaces.",
            DicaTexto = "C# optou por simplicidade — sem herança múltipla de classes..." },

        new Domain.Exercicio { Id = 151, LicaoId = 5, Ordem = 5, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Uma classe abstrata pode conter métodos com implementação (métodos concretos).",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Classe abstrata pode ter métodos abstratos (sem implementação) E métodos concretos (com implementação).",
            DicaTexto = "Abstrata ≠ sem implementação. Pode misturar os dois tipos..." },

        new Domain.Exercicio { Id = 152, LicaoId = 5, Ordem = 6, XPRecompensa = 5,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para substituir um método virtual da classe pai na subclasse, usa-se a palavra-chave ____.",
            OpcoesJson = "[]",
            RespostaCorreta = "override",
            Explicacao = "'override' indica que a subclasse está substituindo a implementação herdada. O método na classe pai deve ser 'virtual' ou 'abstract'.",
            DicaTexto = "Começa com 'over' — você está 'escrevendo por cima' do método pai..." },

        new Domain.Exercicio { Id = 153, LicaoId = 5, Ordem = 7, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Para chamar o construtor da classe pai dentro do construtor da subclasse, usa-se:",
            OpcoesJson = "[\"base(...)\",\"parent(...)\",\"super(...)\",\"this(...)\"]",
            RespostaCorreta = "base(...)",
            Explicacao = "':base(args)' chama o construtor da classe pai. 'this(args)' chama outro construtor da MESMA classe. 'super' não existe em C#.",
            DicaTexto = "Em C#, 'base' refere-se à classe pai; 'this' refere-se à própria classe..." },

        new Domain.Exercicio { Id = 154, LicaoId = 5, Ordem = 8, XPRecompensa = 5,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O método abaixo não substitui corretamente o método virtual da classe pai. Corrija:\npublic class Cachorro : Animal { public string EmitirSom() => \"Au au!\"; }",
            OpcoesJson = "[\"public class Cachorro : Animal { public override string EmitirSom() => \\\"Au au!\\\"; }\",\"public class Cachorro : Animal { public virtual string EmitirSom() => \\\"Au au!\\\"; }\",\"public class Cachorro : Animal { public new string EmitirSom() => \\\"Au au!\\\"; }\",\"public class Cachorro(Animal) { public string EmitirSom() => \\\"Au au!\\\"; }\"]",
            RespostaCorreta = "public class Cachorro : Animal { public override string EmitirSom() => \"Au au!\"; }",
            Explicacao = "Sem 'override', EmitirSom() apenas OCULTA o método da classe pai. O polimorfismo não funcionaria.",
            DicaTexto = "Qual palavra-chave indica que estamos substituindo o método da classe pai?" },

        new Domain.Exercicio { Id = 155, LicaoId = 5, Ordem = 9, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Uma interface pode ser implementada por múltiplas classes sem relação de herança entre elas.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Produto, Usuario e Pedido podem todos implementar IExportavel mesmo sem herdar uns dos outros.",
            DicaTexto = "Interface é um contrato — qualquer classe pode assinar..." },

        new Domain.Exercicio { Id = 156, LicaoId = 5, Ordem = 10, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Polimorfismo permite que referências do tipo base chamem métodos da subclasse. Qual pré-requisito é necessário?",
            OpcoesJson = "[\"O método deve ser virtual na base e override na subclasse\",\"Os métodos devem ter nomes diferentes\",\"A subclasse deve ser static\",\"Não é possível em C#\"]",
            RespostaCorreta = "O método deve ser virtual na base e override na subclasse",
            Explicacao = "Para polimorfismo funcionar, o método na classe pai deve ser 'virtual' e a subclasse deve usar 'override'.",
            DicaTexto = "Quais palavras-chave habilitam o polimorfismo em C#?" },

        // ── Exercícios adicionais — Lição 10: Encapsulamento ────────────────────
        new Domain.Exercicio { Id = 157, LicaoId = 10, Ordem = 4, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual modificador de acesso permite que apenas a própria classe e suas subclasses acessem um membro?",
            OpcoesJson = "[\"protected\",\"private\",\"internal\",\"public\"]",
            RespostaCorreta = "protected",
            Explicacao = "'protected' é mais restritivo que public/internal mas menos que private: a própria classe + qualquer subclasse pode acessar.",
            DicaTexto = "É o intermediário entre private e public, pensado para herança..." },

        new Domain.Exercicio { Id = 158, LicaoId = 10, Ordem = 5, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Uma propriedade declarada como '{ get; private set; }' permite leitura pública mas escrita apenas dentro da classe.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "'public string Nome { get; private set; }': qualquer código pode ler Nome, mas apenas a própria classe pode alterar.",
            DicaTexto = "get público + set privado = leitura pública, escrita controlada..." },

        new Domain.Exercicio { Id = 159, LicaoId = 10, Ordem = 6, XPRecompensa = 5,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Uma propriedade cujo set só pode ser chamado na inicialização do objeto usa o modificador de acesso ____.",
            OpcoesJson = "[]",
            RespostaCorreta = "init",
            Explicacao = "'{ get; init; }' permite definir a propriedade no inicializador de objeto mas não depois. É mais restritivo que private set — nem a própria classe pode alterar após a construção.",
            DicaTexto = "É um modificador C# 9+ para propriedades imutáveis após inicialização..." },

        new Domain.Exercicio { Id = 160, LicaoId = 10, Ordem = 7, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual o propósito principal do encapsulamento em OOP?",
            OpcoesJson = "[\"Proteger o estado interno e expor apenas o necessário\",\"Herdar comportamento de outras classes\",\"Permitir múltiplas implementações\",\"Melhorar a performance\"]",
            RespostaCorreta = "Proteger o estado interno e expor apenas o necessário",
            Explicacao = "Encapsulamento controla o acesso ao estado interno de um objeto, prevenindo modificações inválidas e expondo apenas uma interface pública segura.",
            DicaTexto = "Encapsulamento é sobre controlar o acesso..." },

        new Domain.Exercicio { Id = 161, LicaoId = 10, Ordem = 8, XPRecompensa = 5,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O campo abaixo viola o encapsulamento. Corrija para usar propriedade com acesso controlado:\npublic class Conta { public decimal saldo; }",
            OpcoesJson = "[\"public class Conta { public decimal Saldo { get; private set; } }\",\"public class Conta { private decimal Saldo { get; set; } }\",\"public class Conta { public decimal Saldo; }\",\"public class Conta { internal decimal saldo; }\"]",
            RespostaCorreta = "public class Conta { public decimal Saldo { get; private set; } }",
            Explicacao = "Campos públicos permitem qualquer código modificar o estado diretamente. A solução é usar propriedade com private set, garantindo que só a classe controla mudanças no saldo.",
            DicaTexto = "Campos públicos quebram o encapsulamento — use propriedade com set controlado..." },

        new Domain.Exercicio { Id = 162, LicaoId = 10, Ordem = 9, XPRecompensa = 5,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "O modificador 'internal' torna um membro acessível apenas dentro do mesmo projeto (assembly).",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "'internal' restringe acesso ao assembly (projeto). É útil para implementações que não devem ser expostas publicamente mas precisam ser compartilhadas entre classes do mesmo projeto.",
            DicaTexto = "internal = dentro do assembly (projeto compilado)..." },

        new Domain.Exercicio { Id = 163, LicaoId = 10, Ordem = 10, XPRecompensa = 5,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual é a convenção de nomenclatura para campos privados em C#?",
            OpcoesJson = "[\"_camelCase (ex: _nome)\",\"PascalCase (ex: Nome)\",\"SCREAMING_SNAKE_CASE (ex: NOME)\",\"camelCase sem prefixo (ex: nome)\"]",
            RespostaCorreta = "_camelCase (ex: _nome)",
            Explicacao = "A convenção da Microsoft para campos privados é prefixo underscore + camelCase: '_nome', '_saldo', '_conexao'. Isso distingue visualmente campos privados de parâmetros e variáveis locais.",
            DicaTexto = "O prefixo underscore indica que é um campo privado da classe..." },

        // ── Exercícios — Lição 40: Strings em Profundidade ──────────────────────
        new Domain.Exercicio { Id = 164, LicaoId = 40, Ordem = 1, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual prefixo cria uma string interpolada em C#, permitindo embutir expressões com {}?",
            OpcoesJson = "[\"$\",\"@\",\"#\",\"%\"]",
            RespostaCorreta = "$",
            Explicacao = "O prefixo '$' cria uma string interpolada: $\"Olá, {nome}!\". Qualquer expressão C# pode ficar dentro de {}.",
            DicaTexto = "É um símbolo de cifrão que precede as aspas..." },

        new Domain.Exercicio { Id = 165, LicaoId = 40, Ordem = 2, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Em C#, concatenar strings com '+' em um loop cria uma nova string a cada iteração, o que é ineficiente.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Strings são imutáveis. Cada '+' cria um novo objeto string. Em 1000 iterações, isso gera ~1000 alocações. StringBuilder resolve isso.",
            DicaTexto = "Strings são imutáveis — o que acontece a cada operação de +?" },

        new Domain.Exercicio { Id = 166, LicaoId = 40, Ordem = 3, XPRecompensa = 10,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para declarar a string C:\\Users\\Maria sem precisar escapar as barras, usa-se o prefixo ____\"C:\\Users\\Maria\"",
            OpcoesJson = "[]",
            RespostaCorreta = "@",
            Explicacao = "O prefixo '@' cria uma string verbatim: as barras são literais, sem necessidade de escape.",
            DicaTexto = "É um símbolo de arroba que indica string literal..." },

        new Domain.Exercicio { Id = 167, LicaoId = 40, Ordem = 4, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual método de string divide uma string em partes com base em um separador?",
            OpcoesJson = "[\"Split()\",\"Slice()\",\"Divide()\",\"Cut()\"]",
            RespostaCorreta = "Split()",
            Explicacao = "\"a,b,c\".Split(',') retorna string[] { \"a\", \"b\", \"c\" }. 'Slice/Divide/Cut' são de outras linguagens.",
            DicaTexto = "Lembre do nome em inglês — 'dividir' em inglês é..." },

        new Domain.Exercicio { Id = 168, LicaoId = 40, Ordem = 5, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual classe usar para construir strings eficientemente em um loop?",
            OpcoesJson = "[\"StringBuilder\",\"StringHelper\",\"StringBuffer\",\"StringWriter\"]",
            RespostaCorreta = "StringBuilder",
            Explicacao = "System.Text.StringBuilder acumula texto internamente e realiza apenas uma alocação final no ToString().",
            DicaTexto = "O nome diz tudo — é um 'construtor' de strings..." },

        new Domain.Exercicio { Id = 169, LicaoId = 40, Ordem = 6, XPRecompensa = 10,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O código abaixo compara strings de forma case-sensitive. Corrija para ignorar maiúsculas/minúsculas:\nbool igual = \"abc\" == \"ABC\";",
            OpcoesJson = "[\"bool igual = string.Equals(\\\"abc\\\", \\\"ABC\\\", StringComparison.OrdinalIgnoreCase);\",\"bool igual = \\\"abc\\\".ToLower() == \\\"ABC\\\".ToLower();\",\"bool igual = \\\"abc\\\".Equals(\\\"ABC\\\");\",\"bool igual = string.Compare(\\\"abc\\\", \\\"ABC\\\") == 0;\"]",
            RespostaCorreta = "bool igual = string.Equals(\"abc\", \"ABC\", StringComparison.OrdinalIgnoreCase);",
            Explicacao = "string.Equals com StringComparison.OrdinalIgnoreCase é a forma correta e eficiente.",
            DicaTexto = "Use o método estático string.Equals com um terceiro parâmetro de comparação..." },

        new Domain.Exercicio { Id = 170, LicaoId = 40, Ordem = 7, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "string.IsNullOrWhiteSpace(\"   \") retorna true.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "IsNullOrWhiteSpace() retorna true se a string for null, vazia OU contiver apenas espaços em branco.",
            DicaTexto = "WhiteSpace inclui espaços, tabs e quebras de linha..." },

        new Domain.Exercicio { Id = 171, LicaoId = 40, Ordem = 8, XPRecompensa = 10,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para remover espaços em branco do início e do fim de uma string, usa-se o método .____().",
            OpcoesJson = "[]",
            RespostaCorreta = "Trim",
            Explicacao = "\"  olá  \".Trim() retorna \"olá\". TrimStart() remove do início, TrimEnd() do fim.",
            DicaTexto = "Em inglês, 'aparar' ou 'cortar as bordas' é..." },

        // ── Exercícios — Lição 41: Tratamento de Exceções ───────────────────────
        new Domain.Exercicio { Id = 172, LicaoId = 41, Ordem = 1, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "O bloco 'finally' é executado:",
            OpcoesJson = "[\"Sempre, com ou sem exceção\",\"Apenas quando há exceção\",\"Apenas quando não há exceção\",\"Apenas em modo debug\"]",
            RespostaCorreta = "Sempre, com ou sem exceção",
            Explicacao = "'finally' executa SEMPRE: após o try normal, após um catch, mesmo após um return.",
            DicaTexto = "O nome 'finally' sugere 'por fim, sempre acontece'..." },

        new Domain.Exercicio { Id = 173, LicaoId = 41, Ordem = 2, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "'throw;' (sem argumentos) relança a exceção atual preservando o stack trace original.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "'throw;' relança a exceção exatamente como recebida. 'throw ex;' redefine o stack trace para a linha do catch.",
            DicaTexto = "Qual preserva o stack trace original — throw ou throw ex?" },

        new Domain.Exercicio { Id = 174, LicaoId = 41, Ordem = 3, XPRecompensa = 10,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para criar uma exceção customizada em C#, herde da classe ____.",
            OpcoesJson = "[]",
            RespostaCorreta = "Exception",
            Explicacao = "Todas as exceções em C# derivam de System.Exception.",
            DicaTexto = "É a classe raiz de todas as exceções em .NET..." },

        new Domain.Exercicio { Id = 175, LicaoId = 41, Ordem = 4, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual exceção é lançada ao acessar um índice inválido em um array?",
            OpcoesJson = "[\"IndexOutOfRangeException\",\"ArgumentException\",\"NullReferenceException\",\"InvalidOperationException\"]",
            RespostaCorreta = "IndexOutOfRangeException",
            Explicacao = "Acessar um índice inválido lança IndexOutOfRangeException. NullReferenceException ocorre ao acessar membro de objeto null.",
            DicaTexto = "O índice saiu do intervalo válido — qual exceção descreve isso?" },

        new Domain.Exercicio { Id = 176, LicaoId = 41, Ordem = 5, XPRecompensa = 10,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O código abaixo perde o stack trace original. Corrija-o:\ncatch (Exception ex) { throw ex; }",
            OpcoesJson = "[\"catch (Exception) { throw; }\",\"catch (Exception ex) { throw new Exception(ex.Message); }\",\"catch { throw new Exception(); }\",\"catch (Exception ex) { return; }\"]",
            RespostaCorreta = "catch (Exception) { throw; }",
            Explicacao = "'throw ex' reescreve o stack trace. 'throw;' relança preservando toda a pilha de chamadas original.",
            DicaTexto = "Use throw sem parâmetros para preservar o stack trace..." },

        new Domain.Exercicio { Id = 177, LicaoId = 41, Ordem = 6, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Exceções mais específicas devem ser capturadas ANTES das mais genéricas no bloco catch.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Se 'catch (Exception)' vier antes de 'catch (FormatException)', o FormatException nunca será alcançado.",
            DicaTexto = "O compilador processa os catches em ordem — qual deve vir primeiro?" },

        new Domain.Exercicio { Id = 178, LicaoId = 41, Ordem = 7, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Por que NÃO se deve usar exceções para controle de fluxo normal (ex: validar entrada do usuário)?",
            OpcoesJson = "[\"Criar exceções é caro — gera stack trace e alocações no heap\",\"Exceções não funcionam em produção\",\"O compilador não permite\",\"Exceções só funcionam em métodos async\"]",
            RespostaCorreta = "Criar exceções é caro — gera stack trace e alocações no heap",
            Explicacao = "Lançar uma exceção captura o stack trace completo, o que é custoso. Para fluxos esperados, use TryParse ou validação explícita.",
            DicaTexto = "O que acontece internamente quando uma exceção é lançada?" },

        new Domain.Exercicio { Id = 179, LicaoId = 41, Ordem = 8, XPRecompensa = 10,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para garantir que um recurso seja liberado mesmo se uma exceção for lançada, use a instrução ____ ao declarar o objeto.",
            OpcoesJson = "[]",
            RespostaCorreta = "using",
            Explicacao = "'using var conn = new SqlConnection(str);' chama Dispose() automaticamente ao sair do escopo — mesmo com exceção.",
            DicaTexto = "É uma instrução que garante a liberação automática de recursos..." },

        // ── Exercícios — Lição 42: Interfaces ───────────────────────────────────
        new Domain.Exercicio { Id = 180, LicaoId = 42, Ordem = 1, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "O que uma interface define em C#?",
            OpcoesJson = "[\"Um contrato (o que fazer), sem implementação (como fazer)\",\"Uma classe que não pode ser instanciada\",\"Uma classe com todos os métodos abstratos\",\"Um tipo de dado primitivo\"]",
            RespostaCorreta = "Um contrato (o que fazer), sem implementação (como fazer)",
            Explicacao = "Interface é um contrato puro: define quais membros uma classe deve ter, sem dizer como implementá-los.",
            DicaTexto = "Interface define o 'o quê', não o 'como'..." },

        new Domain.Exercicio { Id = 181, LicaoId = 42, Ordem = 2, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Uma classe C# pode implementar múltiplas interfaces ao mesmo tempo.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "Uma classe herda de UMA classe mas pode implementar N interfaces. Resolve o problema de comportamento múltiplo.",
            DicaTexto = "Classes: herança única. Interfaces: ilimitadas..." },

        new Domain.Exercicio { Id = 182, LicaoId = 42, Ordem = 3, XPRecompensa = 10,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Por convenção em C#, nomes de interfaces começam com a letra ____.",
            OpcoesJson = "[]",
            RespostaCorreta = "I",
            Explicacao = "A convenção da Microsoft é prefixar interfaces com 'I' maiúsculo: IDisposable, IEnumerable, IRepository.",
            DicaTexto = "É a inicial de 'Interface'..." },

        new Domain.Exercicio { Id = 183, LicaoId = 42, Ordem = 4, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual a diferença principal entre interface e classe abstrata?",
            OpcoesJson = "[\"Interface não pode ter estado (campos); abstrata pode ter lógica e campos\",\"Interface pode ser instanciada; abstrata não\",\"Classe abstrata suporta múltipla herança\",\"Não há diferença — são a mesma coisa\"]",
            RespostaCorreta = "Interface não pode ter estado (campos); abstrata pode ter lógica e campos",
            Explicacao = "Interface: contrato puro, sem estado. Classe abstrata: pode ter campos, lógica concreta E métodos abstratos.",
            DicaTexto = "Interface é puro contrato; abstrata pode misturar contrato e implementação..." },

        new Domain.Exercicio { Id = 184, LicaoId = 42, Ordem = 5, XPRecompensa = 10,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "A classe abaixo implementa INotificador mas não implementa o membro obrigatório. Corrija:\npublic interface INotificador { Task EnviarAsync(string dest, string msg); }\npublic class NotificadorEmail : INotificador { }",
            OpcoesJson = "[\"public class NotificadorEmail : INotificador { public async Task EnviarAsync(string dest, string msg) => await smtp.SendAsync(dest, msg); }\",\"public class NotificadorEmail { public Task EnviarAsync(string dest, string msg) => Task.CompletedTask; }\",\"public abstract class NotificadorEmail : INotificador { }\",\"public class NotificadorEmail : INotificador { private Task EnviarAsync(string dest, string msg) => Task.CompletedTask; }\"]",
            RespostaCorreta = "public class NotificadorEmail : INotificador { public async Task EnviarAsync(string dest, string msg) => await smtp.SendAsync(dest, msg); }",
            Explicacao = "Implementar uma interface é obrigatório — todos os membros devem ser implementados como public.",
            DicaTexto = "Toda classe que implementa uma interface deve implementar todos os seus membros como public..." },

        new Domain.Exercicio { Id = 185, LicaoId = 42, Ordem = 6, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "IDisposable é uma interface do .NET que define o método Dispose() para liberar recursos não gerenciados.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "IDisposable tem um único método: void Dispose(). O 'using' statement chama Dispose() automaticamente.",
            DicaTexto = "É a interface que habilita o uso do 'using' statement..." },

        new Domain.Exercicio { Id = 186, LicaoId = 42, Ordem = 7, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Quando é preferível usar uma interface em vez de uma classe abstrata?",
            OpcoesJson = "[\"Quando classes de hierarquias diferentes precisam do mesmo contrato\",\"Quando há lógica comum para compartilhar\",\"Quando a performance é crítica\",\"Quando a classe tem muitos campos\"]",
            RespostaCorreta = "Quando classes de hierarquias diferentes precisam do mesmo contrato",
            Explicacao = "Produto, Usuario e Pedido podem implementar IExportavel sem relação entre si.",
            DicaTexto = "Interface une classes sem relação de herança pelo contrato..." },

        new Domain.Exercicio { Id = 187, LicaoId = 42, Ordem = 8, XPRecompensa = 10,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "O princípio de inversão de dependência (D do SOLID) diz: dependa de ____, não de implementações concretas.",
            OpcoesJson = "[]",
            RespostaCorreta = "abstrações",
            Explicacao = "Interfaces são o mecanismo de abstração em C#. 'INotificador notificador' em vez de 'EmailService emailService' torna o código testável e flexível.",
            DicaTexto = "Interfaces e classes abstratas são exemplos de..." },

        // ── Exercícios — Lição 43: Records e Imutabilidade ──────────────────────
        new Domain.Exercicio { Id = 188, LicaoId = 43, Ordem = 1, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Como dois records são comparados por padrão em C#?",
            OpcoesJson = "[\"Pelos valores das propriedades (igualdade por valor)\",\"Pela referência de memória (igualdade por referência)\",\"Pelo hash code\",\"Records não suportam comparação\"]",
            RespostaCorreta = "Pelos valores das propriedades (igualdade por valor)",
            Explicacao = "Records implementam igualdade por valor automaticamente. 'new Produto(1, \"X\") == new Produto(1, \"X\")' retorna true.",
            DicaTexto = "Records foram criados para ter semântica de valor — como int e decimal..." },

        new Domain.Exercicio { Id = 189, LicaoId = 43, Ordem = 2, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "A expressão 'with' modifica o record original, retornando void.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Falso",
            Explicacao = "'with' cria uma CÓPIA do record com os valores especificados alterados. O record original permanece inalterado.",
            DicaTexto = "Records são imutáveis — 'with' cria uma nova instância ou modifica a existente?" },

        new Domain.Exercicio { Id = 190, LicaoId = 43, Ordem = 3, XPRecompensa = 10,
            Tipo = TipoExercicio.PreencherEspacos,
            Enunciado = "Para criar uma cópia de um record com alguns valores alterados, usa-se a expressão ____.",
            OpcoesJson = "[]",
            RespostaCorreta = "with",
            Explicacao = "'var novo = original with { Preco = 200m };' cria um novo record com todas as propriedades de 'original', exceto Preco.",
            DicaTexto = "É uma palavra em inglês que significa 'com'..." },

        new Domain.Exercicio { Id = 191, LicaoId = 43, Ordem = 4, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual a sintaxe correta para um record posicional em C#?",
            OpcoesJson = "[\"public record Ponto(int X, int Y);\",\"public record class Ponto { int X; int Y; }\",\"public record Ponto : (int X, int Y);\",\"record<int, int> Ponto;\"]",
            RespostaCorreta = "public record Ponto(int X, int Y);",
            Explicacao = "O record posicional declara propriedades, construtor, igualdade e ToString() em uma única linha.",
            DicaTexto = "Os parâmetros vão entre parênteses logo após o nome do record..." },

        new Domain.Exercicio { Id = 192, LicaoId = 43, Ordem = 5, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Quando é mais adequado usar um record em vez de uma classe?",
            OpcoesJson = "[\"Para objetos imutáveis com igualdade por valor (DTOs, Value Objects)\",\"Para entidades com ciclo de vida e estado mutável\",\"Para objetos que precisam de herança complexa\",\"Para objetos que compartilham estado global\"]",
            RespostaCorreta = "Para objetos imutáveis com igualdade por valor (DTOs, Value Objects)",
            Explicacao = "Records são ideais para DTOs de API, Value Objects, configurações imutáveis. Classes para entidades de domínio.",
            DicaTexto = "Pense em quais objetos nunca deveriam mudar após criados..." },

        new Domain.Exercicio { Id = 193, LicaoId = 43, Ordem = 6, XPRecompensa = 10,
            Tipo = TipoExercicio.CorrigirCodigo,
            Enunciado = "O código tenta modificar um record diretamente. Corrija usando a abordagem correta:\nvar produto = new Produto(1, \"Teclado\", 150m);\nproduto.Preco = 120m;",
            OpcoesJson = "[\"var produto = new Produto(1, \\\"Teclado\\\", 150m); var comDesconto = produto with { Preco = 120m };\",\"var produto = new Produto(1, \\\"Teclado\\\", 150m); produto = new Produto(1, \\\"Teclado\\\", 120m);\",\"var produto = new Produto(1, \\\"Teclado\\\", 150m); produto.Preco = 120m;\",\"Produto.Preco = 120m;\"]",
            RespostaCorreta = "var produto = new Produto(1, \"Teclado\", 150m); var comDesconto = produto with { Preco = 120m };",
            Explicacao = "Propriedades de records posicionais são 'init'. A forma correta é usar 'with' para criar uma nova instância.",
            DicaTexto = "Records são imutáveis — use 'with' para criar uma variação..." },

        new Domain.Exercicio { Id = 194, LicaoId = 43, Ordem = 7, XPRecompensa = 10,
            Tipo = TipoExercicio.VerdadeiroFalso,
            Enunciado = "Records geram automaticamente uma implementação de ToString() que exibe o nome do tipo e os valores das propriedades.",
            OpcoesJson = "[\"Verdadeiro\",\"Falso\"]",
            RespostaCorreta = "Verdadeiro",
            Explicacao = "'Console.WriteLine(new Produto(1, \"Teclado\", 150m))' imprime: 'Produto { Id = 1, Nome = Teclado, Preco = 150 }'.",
            DicaTexto = "O compilador gera ToString() automaticamente para records..." },

        new Domain.Exercicio { Id = 195, LicaoId = 43, Ordem = 8, XPRecompensa = 10,
            Tipo = TipoExercicio.MultiplaEscolha,
            Enunciado = "Qual a diferença entre 'record' e 'record struct' em C#?",
            OpcoesJson = "[\"record é tipo de referência (heap); record struct é tipo de valor (stack)\",\"record struct é imutável; record não é\",\"record struct suporta herança; record não\",\"Não há diferença — são sinônimos\"]",
            RespostaCorreta = "record é tipo de referência (heap); record struct é tipo de valor (stack)",
            Explicacao = "'record' é armazenado no heap. 'record struct' é armazenado no stack, com melhor performance para dados pequenos.",
            DicaTexto = "A diferença é onde o objeto vive na memória — heap ou stack..." }
    ];

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
