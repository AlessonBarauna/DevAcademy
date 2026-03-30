import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { AuthService } from '../../../core/services/auth';
import { ConquistaResultDto, Licao } from '../../../core/models/modulo.model';

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
  toastsConquistas: ConquistaResultDto[] = [];

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

  exibirToastsConquistas(conquistas: ConquistaResultDto[]): void {
    conquistas.forEach((c, i) => {
      setTimeout(() => {
        this.toastsConquistas.push(c);
        this.cdr.detectChanges();
        setTimeout(() => {
          this.toastsConquistas = this.toastsConquistas.filter(t => t !== c);
          this.cdr.detectChanges();
        }, 4000);
      }, i * 600);
    });
  }

  concluirLicao(): void {
    if (!this.licaoSelecionada || this.licaoSelecionada.completada) return;
    this.concluindo = true;
    this.moduloService.concluirLicao(this.moduloId, this.licaoSelecionada.id).subscribe({
      next: result => {
        this.licaoSelecionada!.completada = true;
        const idx = this.licoes.findIndex(l => l.id === this.licaoSelecionada!.id);
        if (idx >= 0) this.licoes[idx].completada = true;
        this.concluindo = false;
        if (!result.jaConcluidaAntes) {
          this.authService.atualizarProgresso(result.xpTotal, result.novoNivel, result.streakAtual);
          this.mensagemConclusao = `+${result.xpGanho} XP! Nível ${result.novoNivel} 🔥 ${result.streakAtual} dia(s) de streak`;
          if (result.novasConquistas?.length) {
            this.exibirToastsConquistas(result.novasConquistas);
          }
        } else {
          this.mensagemConclusao = 'Lição já concluída anteriormente.';
        }
        this.cdr.detectChanges();
      },
      error: () => { this.concluindo = false; this.cdr.detectChanges(); }
    });
  }
}
