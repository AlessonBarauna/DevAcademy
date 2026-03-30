import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { AuthService } from '../../../core/services/auth';
import { Licao } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-licao-detail',
  standalone: false,
  templateUrl: './licao-detail.html',
  styleUrl: './licao-detail.css',
})
export class LicaoDetail implements OnInit {
  licoes: Licao[] = [];
  licaoSelecionada: Licao | null = null;
  moduloId!: number;
  carregando = true;
  concluindo = false;
  mensagemConclusao = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private moduloService: ModuloService,
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.moduloService.getLicoes(this.moduloId).subscribe({
      next: licoes => {
        this.licoes = licoes;
        if (licoes.length > 0) this.licaoSelecionada = licoes[0];
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  selecionarLicao(licao: Licao): void {
    this.licaoSelecionada = licao;
    this.mensagemConclusao = '';
  }

  irParaExercicios(): void {
    if (!this.licaoSelecionada) return;
    this.router.navigate(['/modulos', this.moduloId, 'licoes', this.licaoSelecionada.id, 'exercicios']);
  }

  concluirLicao(): void {
    if (!this.licaoSelecionada || this.licaoSelecionada.completada) return;
    this.concluindo = true;
    this.moduloService.concluirLicao(this.moduloId, this.licaoSelecionada.id).subscribe({
      next: result => {
        this.licaoSelecionada!.completada = true;
        const idx = this.licoes.findIndex(l => l.id === this.licaoSelecionada!.id);
        if (idx >= 0) this.licoes[idx].completada = true;
        this.mensagemConclusao = result.jaConcluidaAntes
          ? 'Lição já concluída anteriormente.'
          : `+${result.xpGanho} XP ganhos! Nível atual: ${result.novoNivel}`;
        this.concluindo = false;
        if (!result.jaConcluidaAntes) {
          this.authService.atualizarProgresso(result.xpTotal, result.novoNivel);
        }
        this.cdr.detectChanges();
      },
      error: () => { this.concluindo = false; this.cdr.detectChanges(); }
    });
  }
}
