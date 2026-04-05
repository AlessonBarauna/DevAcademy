import { ChangeDetectorRef, Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface ResultadoExecucao {
  saida: string | null;
  erro: string | null;
  tempoMs: number;
}

interface Exemplo {
  titulo: string;
  codigo: string;
}

@Component({
  selector: 'app-playground-page',
  standalone: false,
  templateUrl: './playground-page.html',
  styleUrl: './playground-page.css',
})
export class PlaygroundPage {
  codigo = `// Olá, Playground!
// Escreva C# aqui e clique em Executar.

var numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var pares = numeros.Where(n => n % 2 == 0).ToList();

Console.WriteLine("Números pares:");
foreach (var n in pares)
    Console.WriteLine($"  {n}");

Console.WriteLine($"\\nSoma: {pares.Sum()}");`;

  resultado: ResultadoExecucao | null = null;
  executando = false;
  abas: 'editor' | 'output' = 'editor';

  readonly exemplos: Exemplo[] = [
    {
      titulo: 'Hello World',
      codigo: `Console.WriteLine("Olá, C# Academy!");
Console.WriteLine($"Data: {DateTime.Now:dd/MM/yyyy}");`
    },
    {
      titulo: 'LINQ',
      codigo: `var nomes = new[] { "Ana", "Bruno", "Carlos", "Diana", "Eduardo" };

var filtrados = nomes
    .Where(n => n.Length > 4)
    .OrderBy(n => n)
    .Select(n => n.ToUpper());

foreach (var nome in filtrados)
    Console.WriteLine(nome);`
    },
    {
      titulo: 'Classes e OOP',
      codigo: `public class Produto
{
    public string Nome { get; init; } = "";
    public decimal Preco { get; init; }
    public override string ToString() => $"{Nome}: R$ {Preco:F2}";
}

var produtos = new List<Produto>
{
    new() { Nome = "Teclado", Preco = 299.90m },
    new() { Nome = "Mouse",   Preco = 149.90m },
    new() { Nome = "Monitor", Preco = 1299.00m },
};

var total = produtos.Sum(p => p.Preco);
produtos.ForEach(p => Console.WriteLine(p));
Console.WriteLine($"\\nTotal: R$ {total:F2}");`
    },
    {
      titulo: 'async/await',
      codigo: `async Task<string> BuscarDadosAsync(int id)
{
    await Task.Delay(100); // simula I/O
    return $"Dados do item #{id}";
}

var tarefas = Enumerable.Range(1, 5)
    .Select(i => BuscarDadosAsync(i));

var resultados = await Task.WhenAll(tarefas);

foreach (var r in resultados)
    Console.WriteLine(r);

Console.WriteLine("\\nTodas as tarefas concluídas!");`
    },
    {
      titulo: 'Records e Pattern Matching',
      codigo: `record Forma(string Tipo, double Lado1, double Lado2 = 0);

static double Area(Forma f) => f switch
{
    { Tipo: "quadrado" } => f.Lado1 * f.Lado1,
    { Tipo: "retangulo" } => f.Lado1 * f.Lado2,
    { Tipo: "triangulo" } => f.Lado1 * f.Lado2 / 2,
    _ => throw new ArgumentException($"Forma desconhecida: {f.Tipo}")
};

var formas = new[]
{
    new Forma("quadrado", 5),
    new Forma("retangulo", 4, 6),
    new Forma("triangulo", 3, 8),
};

foreach (var f in formas)
    Console.WriteLine($"{f.Tipo}: área = {Area(f):F1}");`
    },
    {
      titulo: 'Generics e Delegates',
      codigo: `static T Reduzir<T>(IEnumerable<T> lista, T inicial, Func<T, T, T> acumulador)
{
    var resultado = inicial;
    foreach (var item in lista)
        resultado = acumulador(resultado, item);
    return resultado;
}

var nums = new[] { 1, 2, 3, 4, 5 };

var soma    = Reduzir(nums, 0, (acc, x) => acc + x);
var produto = Reduzir(nums, 1, (acc, x) => acc * x);
var maximo  = Reduzir(nums, int.MinValue, Math.Max);

Console.WriteLine($"Soma: {soma}");
Console.WriteLine($"Produto: {produto}");
Console.WriteLine($"Máximo: {maximo}");`
    },
  ];

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  executar(): void {
    if (this.executando) return;
    this.executando = true;
    this.resultado = null;

    this.http.post<ResultadoExecucao>('/api/playground/executar', { codigo: this.codigo }).subscribe({
      next: res => {
        this.resultado = res;
        this.abas = 'output';
        this.executando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.resultado = { saida: null, erro: 'Erro ao conectar com o servidor.', tempoMs: 0 };
        this.abas = 'output';
        this.executando = false;
        this.cdr.detectChanges();
      }
    });
  }

  carregarExemplo(ex: Exemplo): void {
    this.codigo = ex.codigo;
    this.resultado = null;
    this.abas = 'editor';
    this.cdr.detectChanges();
  }

  limpar(): void {
    this.codigo = '';
    this.resultado = null;
    this.cdr.detectChanges();
  }

  get linhas(): number {
    return this.codigo.split('\n').length;
  }

  get temSaida(): boolean {
    return !!(this.resultado?.saida);
  }

  get temErro(): boolean {
    return !!(this.resultado?.erro);
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }

  onTab(event: Event): void {
    const ke = event as KeyboardEvent;
    ke.preventDefault();
    const textarea = ke.target as HTMLTextAreaElement;
    const start = textarea.selectionStart;
    const end = textarea.selectionEnd;
    this.codigo = this.codigo.substring(0, start) + '    ' + this.codigo.substring(end);
    setTimeout(() => {
      textarea.selectionStart = textarea.selectionEnd = start + 4;
    });
  }
}
