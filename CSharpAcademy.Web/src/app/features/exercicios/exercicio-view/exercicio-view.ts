import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { Exercicio, RespostaExercicioResult } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-exercicio-view',
  standalone: false,
  templateUrl: './exercicio-view.html',
  styleUrl: './exercicio-view.css',
})
export class ExercicioView implements OnInit {
  exercicios: Exercicio[] = [];
  indiceAtual = 0;
  moduloId!: number;
  licaoId!: number;
  carregando = true;
  respondendo = false;
  respostaSelecionada = '';
  respostaTexto = '';
  resultado: RespostaExercicioResult | null = null;
  totalXP = 0;
  finalizou = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private moduloService: ModuloService
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.licaoId = +this.route.snapshot.params['licaoId'];
    this.moduloService.getExercicios(this.licaoId).subscribe({
      next: ex => {
        this.exercicios = ex;
        this.carregando = false;
      },
      error: () => { this.carregando = false; }
    });
  }

  get exercicioAtual(): Exercicio | null {
    return this.exercicios[this.indiceAtual] ?? null;
  }

  get opcoes(): string[] {
    try { return JSON.parse(this.exercicioAtual?.opcoesJson ?? '[]'); }
    catch { return []; }
  }

  get tipoMultiplaEscolha(): boolean {
    return this.exercicioAtual?.tipo === 'MultiplaEscolha';
  }

  get tipoVerdadeiroFalso(): boolean {
    return this.exercicioAtual?.tipo === 'VerdadeiroFalso';
  }

  selecionar(opcao: string): void {
    if (this.resultado) return;
    this.respostaSelecionada = opcao;
  }

  responder(): void {
    if (!this.exercicioAtual || this.respondendo) return;
    const resposta = this.tipoMultiplaEscolha || this.tipoVerdadeiroFalso
      ? this.respostaSelecionada
      : this.respostaTexto;
    if (!resposta) return;

    this.respondendo = true;
    this.moduloService.responderExercicio(this.licaoId, this.exercicioAtual.id, resposta).subscribe({
      next: res => {
        this.resultado = res;
        if (res.correta) this.totalXP += this.exercicioAtual!.xpRecompensa;
        this.respondendo = false;
      },
      error: () => { this.respondendo = false; }
    });
  }

  proximo(): void {
    if (this.indiceAtual < this.exercicios.length - 1) {
      this.indiceAtual++;
      this.resultado = null;
      this.respostaSelecionada = '';
      this.respostaTexto = '';
    } else {
      this.finalizou = true;
    }
  }

  voltarParaLicao(): void {
    this.router.navigate(['/modulos', this.moduloId, 'licoes']);
  }
}
