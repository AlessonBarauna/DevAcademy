import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth';
import { ModuloService } from '../../../core/services/modulo';
import { Modulo } from '../../../core/models/modulo.model';
import { UsuarioResponseDto } from '../../../core/models/auth.model';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  usuario: UsuarioResponseDto | null = null;
  modulos: Modulo[] = [];
  carregando = true;

  readonly nivelLabels: Record<number, string> = {
    1: 'Iniciante',
    2: 'Intermediário',
    3: 'Avançado',
    4: 'Especialista',
  };

  readonly xpParaProximoNivel: Record<number, number> = {
    1: 100,
    2: 300,
    3: 700,
    4: 700,
  };

  constructor(
    private authService: AuthService,
    private moduloService: ModuloService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.usuario = this.authService.usuarioAtual;
    this.moduloService.getModulos().subscribe({
      next: mods => {
        this.modulos = mods;
        this.carregando = false;
      },
      error: () => { this.carregando = false; }
    });
  }

  get totalLicoes(): number {
    return this.modulos.reduce((acc, m) => acc + m.totalLicoes, 0);
  }

  get totalCompletadas(): number {
    return this.modulos.reduce((acc, m) => acc + m.licoesCompletadas, 0);
  }

  get percentualGeral(): number {
    if (this.totalLicoes === 0) return 0;
    return Math.round((this.totalCompletadas / this.totalLicoes) * 100);
  }

  get nivelLabel(): string {
    return this.nivelLabels[this.usuario?.nivelAtual ?? 1] ?? 'Iniciante';
  }

  get xpParaSubir(): number {
    return this.xpParaProximoNivel[this.usuario?.nivelAtual ?? 1] ?? 700;
  }

  get percentualXP(): number {
    const xp = this.usuario?.xp ?? 0;
    const limite = this.xpParaSubir;
    const nivelAtual = this.usuario?.nivelAtual ?? 1;
    const xpBase: Record<number, number> = { 1: 0, 2: 100, 3: 300, 4: 700 };
    const base = xpBase[nivelAtual] ?? 0;
    if (nivelAtual === 4) return 100;
    return Math.min(100, Math.round(((xp - base) / (limite - base)) * 100));
  }

  irParaModulo(modulo: Modulo): void {
    this.router.navigate(['/modulos', modulo.id, 'licoes']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
