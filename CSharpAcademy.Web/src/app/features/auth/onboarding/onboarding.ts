import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface PerguntaOnboarding {
  id: number;
  texto: string;
  opcoes: string[];
}

interface ResultadoOnboarding {
  nivelAtual: number;
  nomeNivel: string;
  acertos: number;
  total: number;
}

@Component({
  selector: 'app-onboarding',
  standalone: false,
  templateUrl: './onboarding.html',
  styleUrl: './onboarding.css',
})
export class Onboarding implements OnInit {
  perguntas: PerguntaOnboarding[] = [];
  respostas: number[] = [];

  etapa: 'intro' | 'quiz' | 'resultado' = 'intro';
  indiceAtual = 0;
  respostaSelecionada: number | null = null;
  respondida = false;
  carregando = false;

  resultado: ResultadoOnboarding | null = null;

  readonly nomesNivel = ['', 'Iniciante', 'Intermediário', 'Avançado', 'Expert'];
  readonly iconesNivel = ['', '🌱', '📚', '🚀', '💎'];

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.http.get<PerguntaOnboarding[]>('/api/onboarding/perguntas').subscribe({
      next: p => { this.perguntas = p; this.cdr.detectChanges(); },
      error: () => this.router.navigate(['/dashboard'])
    });
  }

  get perguntaAtual(): PerguntaOnboarding | null {
    return this.perguntas[this.indiceAtual] ?? null;
  }

  get progresso(): number {
    return this.perguntas.length > 0
      ? Math.round((this.indiceAtual / this.perguntas.length) * 100)
      : 0;
  }

  iniciarQuiz(): void {
    this.etapa = 'quiz';
  }

  selecionarResposta(idx: number): void {
    if (this.respondida) return;
    this.respostaSelecionada = idx;
  }

  confirmarResposta(): void {
    if (this.respostaSelecionada === null) return;
    this.respostas[this.indiceAtual] = this.respostaSelecionada;
    this.respondida = true;
    this.cdr.detectChanges();

    setTimeout(() => {
      if (this.indiceAtual < this.perguntas.length - 1) {
        this.indiceAtual++;
        this.respostaSelecionada = null;
        this.respondida = false;
      } else {
        this.enviarResultado();
      }
      this.cdr.detectChanges();
    }, 800);
  }

  pularOnboarding(): void {
    this.router.navigate(['/dashboard']);
  }

  private enviarResultado(): void {
    this.carregando = true;
    this.http.post<ResultadoOnboarding>('/api/onboarding/definir-nivel', { respostas: this.respostas }).subscribe({
      next: res => {
        this.resultado = res;
        this.etapa = 'resultado';
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => this.router.navigate(['/dashboard'])
    });
  }

  irParaDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
