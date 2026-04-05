import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { AuthService } from '../../../core/services/auth';
import { AnimacaoService } from '../../../core/services/animacao';
import { ConquistaResultDto, Licao, Modulo } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-licao-detail',
  standalone: false,
  templateUrl: './licao-detail.html',
  styleUrl: './licao-detail.css',
})
export class LicaoDetail implements OnInit {
  @ViewChild('contentArea') contentArea!: ElementRef<HTMLElement>;

  licoes: Licao[] = [];
  licaoSelecionada: Licao | null = null;
  modulo: Modulo | null = null;
  moduloId!: number;
  carregando = true;
  concluindo = false;
  mensagemConclusao = '';
  toastsConquistas: ConquistaResultDto[] = [];
  sidebarAberta = false;
  scrollProgress = 0;
  levelUpNivel: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private moduloService: ModuloService,
    private authService: AuthService,
    private animacao: AnimacaoService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    const licaoIdParam = this.route.snapshot.queryParams['licaoId'];
    this.moduloService.getModulos().subscribe({
      next: mods => { this.modulo = mods.find(m => m.id === this.moduloId) ?? null; this.cdr.detectChanges(); }
    });
    this.moduloService.getLicoes(this.moduloId).subscribe({
      next: licoes => {
        this.licoes = licoes;
        if (licaoIdParam) {
          this.licaoSelecionada = licoes.find(l => l.id === +licaoIdParam) ?? licoes[0] ?? null;
        } else if (licoes.length > 0) {
          this.licaoSelecionada = licoes[0];
        }
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  get indiceAtual(): number {
    if (!this.licaoSelecionada) return -1;
    return this.licoes.findIndex(l => l.id === this.licaoSelecionada!.id);
  }

  get licaoAnterior(): Licao | null {
    const i = this.indiceAtual;
    return i > 0 ? this.licoes[i - 1] : null;
  }

  get tempoLeitura(): string {
    const texto = this.licaoSelecionada?.conteudoTeoricoMarkdown ?? '';
    const palavras = texto.trim().split(/\s+/).filter(p => p.length > 0).length;
    const minutos = Math.ceil(palavras / 200);
    return `~${minutos} min de leitura`;
  }

  get proximaLicao(): Licao | null {
    const i = this.indiceAtual;
    return i >= 0 && i < this.licoes.length - 1 ? this.licoes[i + 1] : null;
  }

  onContentScroll(event: Event): void {
    const el = event.target as HTMLElement;
    const max = el.scrollHeight - el.clientHeight;
    this.scrollProgress = max > 0 ? Math.round((el.scrollTop / max) * 100) : 0;
  }

  toggleSidebar(): void {
    this.sidebarAberta = !this.sidebarAberta;
  }

  fecharSidebar(): void {
    this.sidebarAberta = false;
  }

  selecionarLicao(licao: Licao): void {
    this.licaoSelecionada = licao;
    this.mensagemConclusao = '';
    this.sidebarAberta = false;
    setTimeout(() => this.contentArea?.nativeElement?.scrollTo({ top: 0, behavior: 'smooth' }), 0);
  }

  voltarParaModulos(): void {
    this.router.navigate(['/modulos', this.moduloId]);
  }

  irParaDashboard(): void {
    this.router.navigate(['/dashboard']);
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
          const nivelAnterior = this.authService.usuarioAtual?.nivelAtual ?? 1;
          this.authService.atualizarProgresso(result.xpTotal, result.novoNivel, result.streakAtual);
          this.mensagemConclusao = `+${result.xpGanho} XP! Nível ${result.novoNivel} 🔥 ${result.streakAtual} dia(s) de streak`;
          this.animacao.dispararConfetti();
          if (result.novoNivel > nivelAnterior) {
            this.levelUpNivel = result.novoNivel;
          }
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
