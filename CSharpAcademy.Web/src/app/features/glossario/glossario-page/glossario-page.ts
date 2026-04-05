import { Component } from '@angular/core';
import { Router } from '@angular/router';

export interface TermoGlossario {
  termo: string;
  categoria: string;
  definicao: string;
  exemplo?: string;
}

const TERMOS: TermoGlossario[] = [
  // Fundamentos
  { termo: 'variável', categoria: 'Fundamentos', definicao: 'Espaço de memória nomeado que armazena um valor que pode mudar durante a execução.', exemplo: 'int idade = 25;\nstring nome = "Ana";' },
  { termo: 'constante', categoria: 'Fundamentos', definicao: 'Valor que não pode ser alterado após a atribuição. Declarado com const.', exemplo: 'const double PI = 3.14159;' },
  { termo: 'tipo de valor', categoria: 'Fundamentos', definicao: 'Tipos que armazenam o valor diretamente na stack: int, double, bool, char, struct, enum.', exemplo: 'int x = 10; // cópia\nint y = x;\ny = 20; // x continua 10' },
  { termo: 'tipo de referência', categoria: 'Fundamentos', definicao: 'Tipos que armazenam uma referência (ponteiro) para o objeto na heap: class, string, array, delegate.', exemplo: 'var lista = new List<int>();\nvar outra = lista; // mesma referência' },
  { termo: 'nullable', categoria: 'Fundamentos', definicao: 'Permite que tipos de valor aceitem null. Declarado com ? após o tipo.', exemplo: 'int? numero = null;\nif (numero.HasValue)\n    Console.WriteLine(numero.Value);' },
  { termo: 'inferência de tipo', categoria: 'Fundamentos', definicao: 'O compilador deduz o tipo da variável automaticamente usando var.', exemplo: 'var mensagem = "Olá"; // string\nvar numero = 42;      // int' },

  // POO
  { termo: 'classe', categoria: 'POO', definicao: 'Molde (blueprint) que define propriedades e comportamentos de objetos. Tipo de referência.', exemplo: 'public class Carro\n{\n    public string Modelo { get; set; } = "";\n    public void Buzinar() => Console.WriteLine("Beep!");\n}' },
  { termo: 'objeto', categoria: 'POO', definicao: 'Instância de uma classe criada com new. Ocupa espaço na heap.', exemplo: 'var carro = new Carro();\ncarro.Modelo = "Fusca";\ncarro.Buzinar();' },
  { termo: 'herança', categoria: 'POO', definicao: 'Mecanismo onde uma classe (filha) herda membros de outra (pai) usando : NomeBase.', exemplo: 'public class Animal { public void Comer() {} }\npublic class Cachorro : Animal { public void Latir() {} }' },
  { termo: 'polimorfismo', categoria: 'POO', definicao: 'Capacidade de tratar objetos de tipos diferentes de forma uniforme via herança ou interface.', exemplo: 'Animal a = new Cachorro();\na.Comer(); // funciona!' },
  { termo: 'encapsulamento', categoria: 'POO', definicao: 'Ocultação dos detalhes internos de uma classe. Controla acesso com public, private, protected, internal.', exemplo: 'public class ContaBancaria\n{\n    private decimal saldo;\n    public void Depositar(decimal v) { if (v > 0) saldo += v; }\n}' },
  { termo: 'abstração', categoria: 'POO', definicao: 'Exposição apenas do essencial, escondendo a complexidade. Feita via classes abstratas ou interfaces.', exemplo: 'public abstract class Forma\n{\n    public abstract double Area();\n}' },
  { termo: 'interface', categoria: 'POO', definicao: 'Contrato que define o que uma classe deve implementar, sem nenhuma implementação. Prefixo I por convenção.', exemplo: 'public interface IImprimivel\n{\n    void Imprimir();\n}\npublic class Relatorio : IImprimivel\n{\n    public void Imprimir() => Console.WriteLine("...");\n}' },
  { termo: 'classe abstrata', categoria: 'POO', definicao: 'Classe que não pode ser instanciada diretamente. Pode ter métodos abstratos (sem implementação) e concretos.', exemplo: 'public abstract class Veiculo\n{\n    public abstract void Mover();\n    public void Parar() => Console.WriteLine("Parado");\n}' },
  { termo: 'record', categoria: 'POO', definicao: 'Tipo imutável por padrão com igualdade por valor, ideal para DTOs. Suporta with para criar cópias modificadas.', exemplo: 'record Pessoa(string Nome, int Idade);\nvar p1 = new Pessoa("Ana", 30);\nvar p2 = p1 with { Idade = 31 };' },
  { termo: 'struct', categoria: 'POO', definicao: 'Tipo de valor semelhante à classe, mas armazenado na stack. Sem herança, melhor para dados pequenos e imutáveis.', exemplo: 'public struct Ponto { public int X; public int Y; }\nvar p = new Ponto { X = 3, Y = 4 };' },

  // Generics
  { termo: 'generics', categoria: 'Generics', definicao: 'Permite escrever código que funciona com qualquer tipo, definido na chamada. Evita boxing e duplicação.', exemplo: 'public T Primeiro<T>(List<T> lista) => lista[0];\nvar n = Primeiro(new List<int> { 1, 2, 3 }); // T = int' },
  { termo: 'constraints', categoria: 'Generics', definicao: 'Restrições sobre o tipo genérico T usando where. Ex: where T : class, new(), IComparable.', exemplo: 'public T Criar<T>() where T : new() => new T();' },
  { termo: 'List<T>', categoria: 'Generics', definicao: 'Lista dinâmica fortemente tipada. Alternativa ao array quando o tamanho varia.', exemplo: 'var nomes = new List<string>();\nnomes.Add("Ana");\nnomes.Remove("Ana");' },
  { termo: 'Dictionary<K,V>', categoria: 'Generics', definicao: 'Coleção de pares chave-valor com acesso O(1) pela chave.', exemplo: 'var idades = new Dictionary<string, int>();\nidades["Ana"] = 30;\nConsole.WriteLine(idades["Ana"]);' },

  // LINQ
  { termo: 'LINQ', categoria: 'LINQ', definicao: 'Language Integrated Query — consultas tipadas diretamente no C# para coleções, banco, XML e mais.', exemplo: 'var pares = Enumerable.Range(1, 10)\n    .Where(n => n % 2 == 0)\n    .ToList();' },
  { termo: 'Where', categoria: 'LINQ', definicao: 'Filtra elementos da coleção que satisfazem um predicado.', exemplo: 'var adultos = pessoas.Where(p => p.Idade >= 18);' },
  { termo: 'Select', categoria: 'LINQ', definicao: 'Projeta (transforma) cada elemento em um novo formato.', exemplo: 'var nomes = pessoas.Select(p => p.Nome);' },
  { termo: 'OrderBy / ThenBy', categoria: 'LINQ', definicao: 'Ordena a coleção por um ou mais critérios.', exemplo: 'var ord = pessoas.OrderBy(p => p.Sobrenome)\n                 .ThenBy(p => p.Nome);' },
  { termo: 'GroupBy', categoria: 'LINQ', definicao: 'Agrupa elementos por uma chave, retornando IGrouping<K,V>.', exemplo: 'var porDept = funcionarios.GroupBy(f => f.Departamento);' },
  { termo: 'FirstOrDefault', categoria: 'LINQ', definicao: 'Retorna o primeiro elemento que satisfaz o predicado, ou default (null/0) se não encontrar.', exemplo: 'var ana = pessoas.FirstOrDefault(p => p.Nome == "Ana");' },
  { termo: 'Any / All', categoria: 'LINQ', definicao: 'Any: verifica se algum elemento satisfaz o predicado. All: verifica se todos satisfazem.', exemplo: 'bool temAdulto = pessoas.Any(p => p.Idade >= 18);\nbool todosMaiores = pessoas.All(p => p.Idade >= 18);' },
  { termo: 'deferred execution', categoria: 'LINQ', definicao: 'Consultas LINQ não executam ao serem definidas, mas apenas ao iterar (foreach, ToList, Count...).', exemplo: 'var q = lista.Where(x => x > 0); // não executa ainda\nforeach (var x in q) { } // executa aqui' },

  // async/await
  { termo: 'async/await', categoria: 'Async', definicao: 'Modelo de programação assíncrona que evita bloqueio de thread. async marca o método, await suspende até a Task completar.', exemplo: 'public async Task<string> BuscarAsync()\n{\n    var resp = await httpClient.GetStringAsync(url);\n    return resp;\n}' },
  { termo: 'Task', categoria: 'Async', definicao: 'Representa uma operação assíncrona. Task<T> retorna valor; Task sem tipo é como void assíncrono.', exemplo: 'Task t = Task.Run(() => Console.WriteLine("paralelo"));\nawait t;' },
  { termo: 'Task.WhenAll', categoria: 'Async', definicao: 'Aguarda múltiplas Tasks em paralelo, retornando quando todas terminam.', exemplo: 'var t1 = BuscarAsync(1);\nvar t2 = BuscarAsync(2);\nvar resultados = await Task.WhenAll(t1, t2);' },
  { termo: 'CancellationToken', categoria: 'Async', definicao: 'Mecanismo para cancelar operações assíncronas de forma cooperativa.', exemplo: 'var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));\nawait LongOperationAsync(cts.Token);' },
  { termo: 'deadlock', categoria: 'Async', definicao: 'Travamento quando .Result ou .Wait() é chamado em contextos de sincronização (ex: UI, ASP.NET clássico). Evite com async até a raiz.', exemplo: '// ERRADO — pode causar deadlock:\nvar result = GetDataAsync().Result;\n// CERTO:\nvar result = await GetDataAsync();' },

  // Delegates e Eventos
  { termo: 'delegate', categoria: 'Delegates', definicao: 'Tipo que representa uma referência para um método. Base para eventos, callbacks e lambdas.', exemplo: 'delegate int Operacao(int a, int b);\nOperacao soma = (a, b) => a + b;\nConsole.WriteLine(soma(3, 4));' },
  { termo: 'Func<T>', categoria: 'Delegates', definicao: 'Delegate genérico que representa um método que retorna valor. Último tipo é o retorno.', exemplo: 'Func<int, int, int> somar = (a, b) => a + b;\nConsole.WriteLine(somar(2, 3)); // 5' },
  { termo: 'Action<T>', categoria: 'Delegates', definicao: 'Delegate genérico para métodos que não retornam valor (void).', exemplo: 'Action<string> imprimir = msg => Console.WriteLine(msg);\nimprimir("Olá!");' },
  { termo: 'Predicate<T>', categoria: 'Delegates', definicao: 'Delegate que recebe T e retorna bool. Equivale a Func<T, bool>.', exemplo: 'Predicate<int> isPar = n => n % 2 == 0;\nConsole.WriteLine(isPar(4)); // true' },
  { termo: 'lambda', categoria: 'Delegates', definicao: 'Função anônima declarada com =>. Pode capturar variáveis do escopo (closure).', exemplo: 'var quadrados = nums.Select(n => n * n);\n// corpo de bloco:\nvar div = nums.Where(n => {\n    return n % 3 == 0;\n});' },
  { termo: 'event', categoria: 'Delegates', definicao: 'Mecanismo de publicação-assinatura baseado em delegates. Encapsula o delegate para que só o dono dispare.', exemplo: 'public event EventHandler? Click;\nprivado void Disparar() => Click?.Invoke(this, EventArgs.Empty);' },

  // Tratamento de Erros
  { termo: 'exception', categoria: 'Erros', definicao: 'Objeto que representa um erro em tempo de execução. Herda de Exception.', exemplo: 'throw new InvalidOperationException("Saldo insuficiente");' },
  { termo: 'try/catch/finally', categoria: 'Erros', definicao: 'try: bloco protegido. catch: trata a exceção. finally: sempre executa (cleanup).', exemplo: 'try { var n = int.Parse(input); }\ncatch (FormatException ex) { Console.WriteLine(ex.Message); }\nfinally { Console.WriteLine("fim"); }' },
  { termo: 'using statement', categoria: 'Erros', definicao: 'Garante que recursos IDisposable sejam liberados, mesmo com exceções. Equivale a try/finally com Dispose().', exemplo: 'using var stream = File.OpenRead("dados.txt");\n// stream é fechado automaticamente ao sair do escopo' },

  // Pattern Matching
  { termo: 'pattern matching', categoria: 'Avançado', definicao: 'Verificação e extração de dados baseada na forma/estrutura do valor. Switch expressions, is patterns, etc.', exemplo: 'string Classificar(object obj) => obj switch\n{\n    int n when n < 0 => "negativo",\n    int n => $"inteiro {n}",\n    string s => $"texto: {s}",\n    _ => "outro"\n};' },
  { termo: 'switch expression', categoria: 'Avançado', definicao: 'Versão moderna do switch como expressão (retorna valor). Mais concisa que switch statement.', exemplo: 'var desconto = categoria switch\n{\n    "vip"    => 0.20m,\n    "regular"=> 0.10m,\n    _        => 0m\n};' },
  { termo: 'extension method', categoria: 'Avançado', definicao: 'Método estático que pode ser chamado como se fosse membro de outro tipo. Primeiro parâmetro com this.', exemplo: 'public static class StringExt\n{\n    public static bool EhVazio(this string s) => string.IsNullOrEmpty(s);\n}\n"".EhVazio(); // true' },
  { termo: 'span<T>', categoria: 'Avançado', definicao: 'Visão de memória contígua sem alocação. Evita cópias ao trabalhar com subsets de arrays/strings.', exemplo: 'string texto = "Olá, mundo!";\nReadOnlySpan<char> parte = texto.AsSpan(0, 3); // "Olá"' },
  { termo: 'IDisposable', categoria: 'Avançado', definicao: 'Interface com método Dispose() para liberar recursos não gerenciados (streams, conexões, handles).', exemplo: 'public class MeuRecurso : IDisposable\n{\n    public void Dispose() => // libera recursos\n}' },

  // .NET / Runtime
  { termo: 'GC', categoria: '.NET Runtime', definicao: 'Garbage Collector — gerencia memória automaticamente, coletando objetos sem referências na heap.', exemplo: '// GC roda automaticamente\n// Evite GC.Collect() manual — deixe o runtime decidir' },
  { termo: 'boxing/unboxing', categoria: '.NET Runtime', definicao: 'Boxing: converte tipo de valor em object (heap). Unboxing: extrai de volta. Custoso — evite em hot paths.', exemplo: 'int x = 42;\nobject box = x;     // boxing\nint y = (int)box;   // unboxing' },
  { termo: 'namespace', categoria: '.NET Runtime', definicao: 'Organiza tipos em hierarquias lógicas para evitar conflitos de nome.', exemplo: 'namespace MeuApp.Services\n{\n    public class EmailService { }\n}' },
  { termo: 'assembly', categoria: '.NET Runtime', definicao: 'Unidade de implantação no .NET — um .dll ou .exe contendo IL compilado e metadados.', exemplo: '// Uma solução .NET pode ter vários assemblies (projetos)\n// Referenciados via PackageReference ou ProjectReference' },
];

@Component({
  selector: 'app-glossario-page',
  standalone: false,
  templateUrl: './glossario-page.html',
  styleUrl: './glossario-page.css',
})
export class GlossarioPage {
  busca = '';
  categoriaAtiva = 'Todas';
  termoExpandido: string | null = null;

  readonly categorias = ['Todas', ...Array.from(new Set(TERMOS.map(t => t.categoria)))];

  get termosFiltrados(): TermoGlossario[] {
    const q = this.busca.toLowerCase().trim();
    return TERMOS.filter(t => {
      const matchCategoria = this.categoriaAtiva === 'Todas' || t.categoria === this.categoriaAtiva;
      const matchBusca = !q
        || t.termo.toLowerCase().includes(q)
        || t.definicao.toLowerCase().includes(q)
        || (t.exemplo?.toLowerCase().includes(q) ?? false);
      return matchCategoria && matchBusca;
    });
  }

  get totalVisible(): number { return this.termosFiltrados.length; }

  toggleTermo(termo: string): void {
    this.termoExpandido = this.termoExpandido === termo ? null : termo;
  }

  limparBusca(): void {
    this.busca = '';
  }

  constructor(private router: Router) {}

  voltar(): void { this.router.navigate(['/dashboard']); }
}
