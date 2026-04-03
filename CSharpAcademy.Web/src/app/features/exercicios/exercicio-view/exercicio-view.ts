import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { Exercicio, Licao, RespostaExercicioResult } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-exercicio-view',
  standalone: false,
  templateUrl: './exercicio-view.html',
  styleUrl: './exercicio-view.css',
})
export class ExercicioView implements OnInit, OnDestroy {
  exercicios: Exercicio[] = [];
  licoes: Licao[] = [];
  indiceAtual = 0;
  moduloId!: number;
  licaoId!: number;
  carregando = true;
  respondendo = false;
  respostaSelecionada = '';
  respostaTexto = '';
  resultado: RespostaExercicioResult | null = null;
  totalXP = 0;
  acertos = 0;
  finalizou = false;
  tempoTotal = 60;
  tempoRestante = 60;
  vidasRestantes = 5;
  minutosParaRecarga = 0;
  private timerInterval: ReturnType<typeof setInterval> | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private moduloService: ModuloService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.licaoId = +this.route.snapshot.params['licaoId'];
    this.moduloService.getExercicios(this.licaoId).subscribe({
      next: ex => {
        this.exercicios = ex;
        this.carregando = false;
        this.cdr.detectChanges();
        if (ex.length > 0) this.iniciarTimer();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
    this.moduloService.getLicoes(this.moduloId).subscribe({
      next: licoes => { this.licoes = licoes; this.cdr.detectChanges(); }
    });
  }

  ngOnDestroy(): void {
    this.pararTimer();
  }

  get timerPercent(): number {
    return (this.tempoRestante / this.tempoTotal) * 100;
  }

  get timerCor(): string {
    if (this.tempoRestante > 30) return 'verde';
    if (this.tempoRestante > 10) return 'amarelo';
    return 'vermelho';
  }

  iniciarTimer(): void {
    this.pararTimer();
    this.tempoRestante = this.tempoTotal;
    this.timerInterval = setInterval(() => {
      this.tempoRestante--;
      this.cdr.detectChanges();
      if (this.tempoRestante <= 0) {
        this.pararTimer();
        this.onTimeout();
      }
    }, 1000);
  }

  pararTimer(): void {
    if (this.timerInterval !== null) {
      clearInterval(this.timerInterval);
      this.timerInterval = null;
    }
  }

  private onTimeout(): void {
    if (this.resultado || this.finalizou) return;
    // Timeout: conta como erro, avança automaticamente
    this.resultado = { correta: false, respostaCorreta: null, explicacao: 'Tempo esgotado!', vidasRestantes: this.vidasRestantes, minutosParaRecarga: this.minutosParaRecarga };
    this.cdr.detectChanges();
  }

  get exercicioAtual(): Exercicio | null {
    return this.exercicios[this.indiceAtual] ?? null;
  }

  get opcoes(): string[] {
    try { return JSON.parse(this.exercicioAtual?.opcoesJson ?? '[]'); }
    catch { return []; }
  }

  get coracoes(): boolean[] {
    return Array.from({ length: 5 }, (_, i) => i < this.vidasRestantes);
  }

  get tipoMultiplaEscolha(): boolean {
    return this.exercicioAtual?.tipo === 'MultiplaEscolha';
  }

  get tipoVerdadeiroFalso(): boolean {
    return this.exercicioAtual?.tipo === 'VerdadeiroFalso';
  }

  get tipoPreencherEspacos(): boolean {
    return this.exercicioAtual?.tipo === 'PreencherEspacos';
  }

  get tipoCorrigirCodigo(): boolean {
    return this.exercicioAtual?.tipo === 'CorrigirCodigo';
  }

  /** Divide o enunciado em partes para destacar o ____ inline */
  get enunciadoPartes(): string[] {
    return this.exercicioAtual?.enunciado.split('____') ?? [];
  }

  get percentualAcerto(): number {
    if (this.exercicios.length === 0) return 0;
    return Math.round((this.acertos / this.exercicios.length) * 100);
  }

  get estrelas(): number {
    const p = this.percentualAcerto;
    if (p === 100) return 3;
    if (p >= 60) return 2;
    if (p >= 30) return 1;
    return 0;
  }

  get proximaLicao(): Licao | null {
    const idx = this.licoes.findIndex(l => l.id === this.licaoId);
    return idx >= 0 && idx < this.licoes.length - 1 ? this.licoes[idx + 1] : null;
  }

  selecionar(opcao: string): void {
    if (this.resultado) return;
    this.respostaSelecionada = opcao;
  }

  responder(): void {
    if (!this.exercicioAtual || this.respondendo) return;
    this.pararTimer();
    const resposta = this.tipoMultiplaEscolha || this.tipoVerdadeiroFalso
      ? this.respostaSelecionada
      : this.respostaTexto;
    if (!resposta) return;

    this.respondendo = true;
    this.moduloService.responderExercicio(this.licaoId, this.exercicioAtual.id, resposta).subscribe({
      next: res => {
        this.resultado = res;
        this.vidasRestantes = res.vidasRestantes;
        this.minutosParaRecarga = res.minutosParaRecarga;
        if (res.correta) {
          this.totalXP += this.exercicioAtual!.xpRecompensa;
          this.acertos++;
        }
        this.respondendo = false;
        this.cdr.detectChanges();
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
      this.iniciarTimer();
    } else {
      this.pararTimer();
      this.finalizou = true;
    }
  }

  voltarParaLicao(): void {
    this.router.navigate(['/modulos', this.moduloId, 'licoes'], {
      queryParams: { licaoId: this.licaoId }
    });
  }

  irParaProximaLicao(): void {
    if (this.proximaLicao) {
      this.router.navigate(['/modulos', this.moduloId, 'licoes'], {
        queryParams: { licaoId: this.proximaLicao.id }
      });
    }
  }
}
