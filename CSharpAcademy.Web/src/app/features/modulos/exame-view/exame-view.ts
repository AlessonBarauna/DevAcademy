import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Exercicio } from '../../../core/models/modulo.model';

interface ExameInicio {
  moduloId: number;
  titulo: string;
  totalQuestoes: number;
  duracaoSegundos: number;
  exercicios: Exercicio[];
}

interface ExameQuestao extends Exercicio {
  respostaUsuario: string | null;
  correta: boolean | null;
  respostaCorreta: string | null;
}

type Fase = 'carregando' | 'intro' | 'questao' | 'resultado';

@Component({
  selector: 'app-exame-view',
  standalone: false,
  templateUrl: './exame-view.html',
  styleUrl: './exame-view.css',
})
export class ExameView implements OnInit, OnDestroy {
  moduloId!: number;
  tituloModulo = '';
  fase: Fase = 'carregando';
  questoes: ExameQuestao[] = [];
  indiceAtual = 0;
  respostaDigitada = '';
  opcaoSelecionada = '';
  tempoRestante = 300;
  private timer: ReturnType<typeof setInterval> | null = null;
  enviando = false;

  get questaoAtual(): ExameQuestao | null {
    return this.questoes[this.indiceAtual] ?? null;
  }

  get totalQuestoes(): number { return this.questoes.length; }

  get acertos(): number {
    return this.questoes.filter(q => q.correta === true).length;
  }

  get percentual(): number {
    if (this.totalQuestoes === 0) return 0;
    return Math.round((this.acertos / this.totalQuestoes) * 100);
  }

  get nota(): string {
    const p = this.percentual;
    if (p >= 90) return 'A';
    if (p >= 75) return 'B';
    if (p >= 60) return 'C';
    return 'F';
  }

  get notaLabel(): string {
    const p = this.percentual;
    if (p >= 90) return 'Excelente!';
    if (p >= 75) return 'Bom!';
    if (p >= 60) return 'Suficiente';
    return 'Tente novamente';
  }

  get tempoFormatado(): string {
    const m = Math.floor(this.tempoRestante / 60);
    const s = this.tempoRestante % 60;
    return `${m}:${s.toString().padStart(2, '0')}`;
  }

  get isMultiplaEscolha(): boolean {
    return this.questaoAtual?.tipo === 'MultiplaEscolha';
  }

  get opcoes(): string[] {
    try { return JSON.parse(this.questaoAtual?.opcoesJson ?? '[]'); }
    catch { return []; }
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.http.get<ExameInicio>(`/api/exame/${this.moduloId}/iniciar`).subscribe({
      next: dados => {
        this.tituloModulo = dados.titulo;
        this.tempoRestante = dados.duracaoSegundos;
        this.questoes = dados.exercicios.map(e => ({
          ...e,
          respostaUsuario: null,
          correta: null,
          respostaCorreta: null,
        }));
        this.fase = 'intro';
        this.cdr.detectChanges();
      },
      error: () => this.router.navigate(['/modulos', this.moduloId])
    });
  }

  iniciarExame(): void {
    this.fase = 'questao';
    this.indiceAtual = 0;
    this.limparResposta();
    this.iniciarTimer();
  }

  private iniciarTimer(): void {
    this.timer = setInterval(() => {
      this.tempoRestante--;
      if (this.tempoRestante <= 0) this.finalizarPorTempo();
      this.cdr.detectChanges();
    }, 1000);
  }

  private finalizarPorTempo(): void {
    this.pararTimer();
    // marca restantes como erradas sem resposta
    for (let i = this.indiceAtual; i < this.questoes.length; i++) {
      if (this.questoes[i].correta === null) this.questoes[i].correta = false;
    }
    this.fase = 'resultado';
  }

  responder(): void {
    if (this.enviando) return;
    const q = this.questaoAtual;
    if (!q) return;

    const resposta = this.isMultiplaEscolha ? this.opcaoSelecionada : this.respostaDigitada;
    if (!resposta.trim()) return;

    this.enviando = true;
    this.http.post<{ correta: boolean; respostaCorreta: string | null }>(
      `/api/licao/${q.licaoId}/exercicios/${q.id}/responder`,
      { resposta }
    ).subscribe({
      next: res => {
        q.respostaUsuario = resposta;
        q.correta = res.correta;
        q.respostaCorreta = res.respostaCorreta;
        this.enviando = false;
        this.cdr.detectChanges();
      },
      error: () => { this.enviando = false; this.cdr.detectChanges(); }
    });
  }

  avancar(): void {
    if (this.indiceAtual < this.totalQuestoes - 1) {
      this.indiceAtual++;
      this.limparResposta();
    } else {
      this.pararTimer();
      this.fase = 'resultado';
    }
    this.cdr.detectChanges();
  }

  private limparResposta(): void {
    this.respostaDigitada = '';
    this.opcaoSelecionada = '';
  }

  private pararTimer(): void {
    if (this.timer) { clearInterval(this.timer); this.timer = null; }
  }

  voltar(): void {
    this.router.navigate(['/modulos', this.moduloId]);
  }

  irParaDashboard(): void {
    this.router.navigate(['/dashboard']);
  }

  ngOnDestroy(): void {
    this.pararTimer();
  }
}
