import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { Modulo } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-modulo-list',
  standalone: false,
  templateUrl: './modulo-list.html',
  styleUrl: './modulo-list.css',
})
export class ModuloList implements OnInit {
  modulos: Modulo[] = [];
  termoBusca = '';
  carregando = true;

  get modulosFiltrados(): Modulo[] {
    const t = this.termoBusca.toLowerCase().trim();
    if (!t) return this.modulos;
    return this.modulos.filter(m =>
      m.titulo.toLowerCase().includes(t) || m.descricao.toLowerCase().includes(t)
    );
  }

  constructor(private moduloService: ModuloService, private router: Router, private cdr: ChangeDetectorRef) {}

  voltar(): void { this.router.navigate(['/dashboard']); }

  ngOnInit(): void {
    this.moduloService.getModulos().subscribe({
      next: m => { this.modulos = m; this.carregando = false; this.cdr.detectChanges(); },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  progresso(m: Modulo): number {
    if (m.totalLicoes === 0) return 0;
    return Math.round((m.licoesCompletadas / m.totalLicoes) * 100);
  }

  concluido(m: Modulo): boolean {
    return m.totalLicoes > 0 && m.licoesCompletadas >= m.totalLicoes;
  }

  nivelIcone(nivel: string): string {
    const mapa: Record<string, string> = {
      iniciante: '🌱', intermediario: '⚡', avancado: '🔥', especialista: '💎'
    };
    return mapa[nivel.toLowerCase()] ?? '📘';
  }

  navegar(m: Modulo): void {
    if (m.desbloqueado) this.router.navigate(['/modulos', m.id]);
  }
}
