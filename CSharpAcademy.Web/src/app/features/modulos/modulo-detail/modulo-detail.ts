import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { Licao, Modulo } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-modulo-detail',
  standalone: false,
  templateUrl: './modulo-detail.html',
  styleUrl: './modulo-detail.css',
})
export class ModuloDetail implements OnInit {
  modulo: Modulo | null = null;
  licoes: Licao[] = [];
  carregando = true;
  moduloId!: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private moduloService: ModuloService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.moduloService.getModulos().subscribe({
      next: mods => {
        this.modulo = mods.find(m => m.id === this.moduloId) ?? null;
        this.cdr.detectChanges();
      }
    });
    this.moduloService.getLicoes(this.moduloId).subscribe({
      next: licoes => {
        this.licoes = licoes;
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  get progresso(): number {
    if (!this.modulo || this.modulo.totalLicoes === 0) return 0;
    return Math.round((this.modulo.licoesCompletadas / this.modulo.totalLicoes) * 100);
  }

  get xpTotal(): number {
    return this.licoes.reduce((acc, l) => acc + l.xpRecompensa, 0);
  }

  get proximaLicao(): Licao | null {
    return this.licoes.find(l => !l.completada) ?? this.licoes[0] ?? null;
  }

  entrarNaLicao(licao: Licao): void {
    this.router.navigate(['/modulos', this.moduloId, 'licoes'], {
      queryParams: { licaoId: licao.id }
    });
  }

  comecar(): void {
    if (this.proximaLicao) {
      this.entrarNaLicao(this.proximaLicao);
    }
  }

  irParaExame(): void {
    this.router.navigate(['/modulos', this.moduloId, 'exame']);
  }

  voltar(): void {
    this.router.navigate(['/modulos']);
  }
}
