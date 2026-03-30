import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../core/services/auth';
import { ModuloService } from '../../../core/services/modulo';
import { ThemeService } from '../../../core/services/theme';
import { Modulo } from '../../../core/models/modulo.model';
import { ConquistaDto, UsuarioResponseDto } from '../../../core/models/auth.model';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit, OnDestroy {
  usuario: UsuarioResponseDto | null = null;
  modulos: Modulo[] = [];
  conquistas: ConquistaDto[] = [];
  carregando = true;
  erroModulos = '';
  heatmapSemanas: { data: Date; count: number; nivel: number }[][] = [];
  private sub = new Subscription();

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
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef,
    public themeService: ThemeService
  ) {}

  get streakEmRisco(): boolean {
    const ultimoEstudo = this.usuario?.ultimoEstudo;
    if (!ultimoEstudo || !this.usuario?.streakAtual) return false;
    const hoje = new Date().toISOString().slice(0, 10);
    return ultimoEstudo < hoje;
  }

  ngOnInit(): void {
    this.authService.sincronizarPerfil();
    this.sub.add(
      this.authService.usuario$.subscribe(u => {
        this.usuario = u;
        this.cdr.detectChanges();
      })
    );
    this.moduloService.getModulos().subscribe({
      next: mods => {
        this.modulos = mods;
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: err => {
        this.carregando = false;
        this.erroModulos = err.status === 0
          ? 'Não foi possível conectar ao servidor. Verifique se o backend está rodando.'
          : `Erro ao carregar módulos (${err.status}).`;
        this.cdr.detectChanges();
      }
    });

    this.http.get<ConquistaDto[]>('/api/auth/conquistas').subscribe({
      next: c => { this.conquistas = c; this.cdr.detectChanges(); }
    });

    this.http.get<{ data: string; contagem: number }[]>('/api/auth/atividade').subscribe({
      next: atividade => {
        this.heatmapSemanas = this.buildHeatmap(atividade);
        this.cdr.detectChanges();
      }
    });
  }

  buildHeatmap(atividade: { data: string; contagem: number }[]): { data: Date; count: number; nivel: number }[][] {
    const mapaContagem = new Map<string, number>();
    for (const a of atividade) mapaContagem.set(a.data, a.contagem);

    const hoje = new Date();
    hoje.setHours(0, 0, 0, 0);
    // Começa sempre no domingo da semana de hoje, retrocede ~26 semanas
    const diasTotais = 26 * 7;
    const inicioGrid = new Date(hoje);
    inicioGrid.setDate(hoje.getDate() - diasTotais + 1);
    // Recua até o domingo anterior
    inicioGrid.setDate(inicioGrid.getDate() - inicioGrid.getDay());

    const semanas: { data: Date; count: number; nivel: number }[][] = [];
    let semanaAtual: { data: Date; count: number; nivel: number }[] = [];
    const cursor = new Date(inicioGrid);

    while (cursor <= hoje) {
      const iso = cursor.toISOString().slice(0, 10);
      const count = mapaContagem.get(iso) ?? 0;
      const nivel = count === 0 ? 0 : count === 1 ? 1 : count <= 3 ? 2 : 3;
      semanaAtual.push({ data: new Date(cursor), count, nivel });
      if (semanaAtual.length === 7) {
        semanas.push(semanaAtual);
        semanaAtual = [];
      }
      cursor.setDate(cursor.getDate() + 1);
    }
    if (semanaAtual.length > 0) semanas.push(semanaAtual);
    return semanas;
  }

  formatarDataHeatmap(data: Date): string {
    return data.toLocaleDateString('pt-BR', { weekday: 'short', day: 'numeric', month: 'short' });
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

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  irParaModulo(modulo: Modulo): void {
    this.router.navigate(['/modulos', modulo.id]);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
