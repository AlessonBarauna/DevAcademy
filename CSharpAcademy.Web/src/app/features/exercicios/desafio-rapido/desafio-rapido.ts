import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { Exercicio, RespostaExercicioResult } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-desafio-rapido',
  standalone: false,
  templateUrl: './desafio-rapido.html',
  styleUrl: './desafio-rapido.css',
})
export class DesafioRapido implements OnInit, OnDestroy {
  exercicios: Exercicio[] = [];
  indiceAtual = 0;
  carregando = true;
  semExercicios = false;
  respondendo = false;
  respostaSelecionada = '';
  respostaTexto = '';
  resultado: RespostaExercicioResult | null = null;

  tempoTotal = 60;
  tempoRestante = 60;
  private timerInterval: ReturnType<typeof setInterval> | null = null;

  acertos = 0;
  totalXP = 0;
  finalizou = false;

  constructor(
    private moduloService: ModuloService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloService.getDesafioRapido(5).subscribe({
      next: res => {
        this.semExercicios = res.semExercicios;
        this.exercicios = res.exercicios;
        this.carregando = false;
        if (!res.semExercicios && res.exercicios.length > 0) this.iniciarTimer();
        this.cdr.detectChanges();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  ngOnDestroy(): void { this.pararTimer(); }

  get exercicioAtual(): Exercicio | null { return this.exercicios[this.indiceAtual] ?? null; }

  get opcoes(): string[] {
    try { return JSON.parse(this.exercicioAtual?.opcoesJson ?? '[]'); }
    catch { return []; }
  }

  get tipoOpcoes(): boolean {
    const t = this.exercicioAtual?.tipo;
    return t === 'MultiplaEscolha' || t === 'VerdadeiroFalso' || t === 'CorrigirCodigo';
  }

  get timerPercent(): number { return (this.tempoRestante / this.tempoTotal) * 100; }

  get timerCor(): string {
    if (this.tempoRestante > 30) return 'verde';
    if (this.tempoRestante > 10) return 'amarelo';
    return 'vermelho';
  }

  get percentualAcerto(): number {
    if (this.exercicios.length === 0) return 0;
    return Math.round((this.acertos / this.exercicios.length) * 100);
  }

  iniciarTimer(): void {
    this.pararTimer();
    this.timerInterval = setInterval(() => {
      this.tempoRestante--;
      this.cdr.detectChanges();
      if (this.tempoRestante <= 0) {
        this.pararTimer();
        this.finalizou = true;
        this.cdr.detectChanges();
      }
    }, 1000);
  }

  pararTimer(): void {
    if (this.timerInterval !== null) { clearInterval(this.timerInterval); this.timerInterval = null; }
  }

  selecionar(opcao: string): void {
    if (this.resultado) return;
    this.respostaSelecionada = opcao;
  }

  responder(): void {
    if (!this.exercicioAtual || this.respondendo) return;
    const resposta = this.tipoOpcoes ? this.respostaSelecionada : this.respostaTexto;
    if (!resposta) return;

    this.respondendo = true;
    this.moduloService.responderExercicio(this.exercicioAtual.licaoId, this.exercicioAtual.id, resposta).subscribe({
      next: res => {
        this.resultado = res;
        if (res.correta) { this.totalXP += this.exercicioAtual!.xpRecompensa; this.acertos++; }
        this.respondendo = false;
        this.cdr.detectChanges();
        // Auto-avança após 1.2s no desafio rápido
        setTimeout(() => { this.proximo(); }, 1200);
      },
      error: () => { this.respondendo = false; this.cdr.detectChanges(); }
    });
  }

  proximo(): void {
    if (this.indiceAtual < this.exercicios.length - 1) {
      this.indiceAtual++;
      this.resultado = null;
      this.respostaSelecionada = '';
      this.respostaTexto = '';
      this.cdr.detectChanges();
    } else {
      this.pararTimer();
      this.finalizou = true;
      this.cdr.detectChanges();
    }
  }

  voltar(): void { this.router.navigate(['/dashboard']); }
}
