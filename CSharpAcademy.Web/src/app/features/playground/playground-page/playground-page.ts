import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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

export interface SnippetHistorico {
  id: string;
  titulo: string;
  codigo: string;
  dataIso: string;
}

const CHAVE_HISTORICO = 'pg_historico';
const CHAVE_RASCUNHO  = 'pg_rascunho';
const MAX_HISTORICO   = 20;

@Component({
  selector: 'app-playground-page',
  standalone: false,
  templateUrl: './playground-page.html',
  styleUrl: './playground-page.css',
})
export class PlaygroundPage implements OnInit {
  codigo = '';
  resultado: ResultadoExecucao | null = null;
  executando = false;
  abas: 'editor' | 'output' = 'editor';

  historicoAberto = false;
  historico: SnippetHistorico[] = [];

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
    { Tipo: "quadrado" }  => f.Lado1 * f.Lado1,
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

  ngOnInit(): void {
    this.carregarHistorico();
    this.codigo = localStorage.getItem(CHAVE_RASCUNHO) ?? this.exemplos[0].codigo;
  }

  // ── Execução ──────────────────────────────────────

  executar(): void {
    if (this.executando) return;
    this.executando = true;
    this.resultado = null;

    this.salvarRascunho();

    this.http.post<ResultadoExecucao>('/api/playground/executar', { codigo: this.codigo }).subscribe({
      next: res => {
        this.resultado = res;
        this.abas = 'output';
        this.executando = false;
        this.adicionarAoHistorico(this.codigo);
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

  // ── Rascunho (auto-save no localStorage) ─────────

  private salvarRascunho(): void {
    localStorage.setItem(CHAVE_RASCUNHO, this.codigo);
  }

  onCodigoChange(): void {
    this.salvarRascunho();
  }

  // ── Histórico ─────────────────────────────────────

  private carregarHistorico(): void {
    try {
      this.historico = JSON.parse(localStorage.getItem(CHAVE_HISTORICO) ?? '[]');
    } catch {
      this.historico = [];
    }
  }

  private adicionarAoHistorico(codigo: string): void {
    const titulo = this.extrairTitulo(codigo);
    const item: SnippetHistorico = {
      id: Date.now().toString(),
      titulo,
      codigo,
      dataIso: new Date().toISOString(),
    };

    // Evita duplicata consecutiva
    if (this.historico[0]?.codigo === codigo) return;

    this.historico = [item, ...this.historico].slice(0, MAX_HISTORICO);
    localStorage.setItem(CHAVE_HISTORICO, JSON.stringify(this.historico));
  }

  private extrairTitulo(codigo: string): string {
    const linhas = codigo.split('\n').map(l => l.trim()).filter(Boolean);
    const primeiraUtil = linhas.find(l => !l.startsWith('//')) ?? linhas[0] ?? 'Sem título';
    return primeiraUtil.length > 40 ? primeiraUtil.slice(0, 38) + '…' : primeiraUtil;
  }

  carregarDoHistorico(item: SnippetHistorico): void {
    this.codigo = item.codigo;
    this.resultado = null;
    this.historicoAberto = false;
    this.salvarRascunho();
    this.cdr.detectChanges();
  }

  removerDoHistorico(id: string, event: Event): void {
    event.stopPropagation();
    this.historico = this.historico.filter(h => h.id !== id);
    localStorage.setItem(CHAVE_HISTORICO, JSON.stringify(this.historico));
    this.cdr.detectChanges();
  }

  limparHistorico(): void {
    this.historico = [];
    localStorage.removeItem(CHAVE_HISTORICO);
    this.historicoAberto = false;
    this.cdr.detectChanges();
  }

  formatarData(iso: string): string {
    const d = new Date(iso);
    return d.toLocaleDateString('pt-BR', { day: '2-digit', month: '2-digit' })
      + ' ' + d.toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' });
  }

  // ── Exemplos ──────────────────────────────────────

  carregarExemplo(ex: Exemplo): void {
    this.codigo = ex.codigo;
    this.resultado = null;
    this.abas = 'editor';
    this.salvarRascunho();
    this.cdr.detectChanges();
  }

  limpar(): void {
    this.codigo = '';
    this.resultado = null;
    this.salvarRascunho();
    this.cdr.detectChanges();
  }

  // ── Utilitários ───────────────────────────────────

  get linhas(): number { return this.codigo.split('\n').length; }
  get temSaida(): boolean { return !!(this.resultado?.saida); }
  get temErro(): boolean { return !!(this.resultado?.erro); }

  voltar(): void { this.router.navigate(['/dashboard']); }

  onTab(event: Event): void {
    const ke = event as KeyboardEvent;
    ke.preventDefault();
    const textarea = ke.target as HTMLTextAreaElement;
    const start = textarea.selectionStart;
    const end = textarea.selectionEnd;
    this.codigo = this.codigo.substring(0, start) + '    ' + this.codigo.substring(end);
    setTimeout(() => { textarea.selectionStart = textarea.selectionEnd = start + 4; });
  }
}
