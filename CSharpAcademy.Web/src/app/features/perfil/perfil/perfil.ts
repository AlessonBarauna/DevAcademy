import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../core/services/auth';
import { ThemeService } from '../../../core/services/theme';
import { ConquistaDto, UsuarioResponseDto } from '../../../core/models/auth.model';
import { AnalyticsModulo } from '../../../core/models/analytics.model';

@Component({
  selector: 'app-perfil',
  standalone: false,
  templateUrl: './perfil.html',
  styleUrl: './perfil.css',
})
export class Perfil implements OnInit, OnDestroy {
  usuario: UsuarioResponseDto | null = null;
  conquistas: ConquistaDto[] = [];
  analytics: AnalyticsModulo[] = [];
  carregando = true;
  heatmapSemanas: { data: Date; count: number; nivel: number }[][] = [];
  private sub = new Subscription();

  readonly radarSize = 200;
  readonly radarCenter = 100;
  readonly radarRadius = 75;

  readonly nivelLabels: Record<number, string> = {
    1: 'Iniciante', 2: 'Intermediário', 3: 'Avançado', 4: 'Especialista',
  };

  readonly xpParaProximoNivel: Record<number, number> = {
    1: 100, 2: 300, 3: 700, 4: 700,
  };

  constructor(
    private authService: AuthService,
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef,
    public themeService: ThemeService
  ) {}

  ngOnInit(): void {
    this.sub.add(
      this.authService.usuario$.subscribe(u => {
        this.usuario = u;
        this.cdr.detectChanges();
      })
    );
    this.http.get<ConquistaDto[]>('/api/auth/conquistas').subscribe({
      next: c => { this.conquistas = c; this.carregando = false; this.cdr.detectChanges(); },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });

    this.http.get<{ data: string; contagem: number }[]>('/api/auth/atividade').subscribe({
      next: atividade => { this.heatmapSemanas = this.buildHeatmap(atividade); this.cdr.detectChanges(); }
    });

    this.http.get<AnalyticsModulo[]>('/api/auth/analytics').subscribe({
      next: a => { this.analytics = a; this.cdr.detectChanges(); }
    });
  }

  buildHeatmap(atividade: { data: string; contagem: number }[]): { data: Date; count: number; nivel: number }[][] {
    const mapaContagem = new Map<string, number>();
    for (const a of atividade) mapaContagem.set(a.data, a.contagem);
    const hoje = new Date(); hoje.setHours(0, 0, 0, 0);
    const inicioGrid = new Date(hoje);
    inicioGrid.setDate(hoje.getDate() - 26 * 7 + 1);
    inicioGrid.setDate(inicioGrid.getDate() - inicioGrid.getDay());
    const semanas: { data: Date; count: number; nivel: number }[][] = [];
    let semanaAtual: { data: Date; count: number; nivel: number }[] = [];
    const cursor = new Date(inicioGrid);
    while (cursor <= hoje) {
      const iso = cursor.toISOString().slice(0, 10);
      const count = mapaContagem.get(iso) ?? 0;
      const nivel = count === 0 ? 0 : count === 1 ? 1 : count <= 3 ? 2 : 3;
      semanaAtual.push({ data: new Date(cursor), count, nivel });
      if (semanaAtual.length === 7) { semanas.push(semanaAtual); semanaAtual = []; }
      cursor.setDate(cursor.getDate() + 1);
    }
    if (semanaAtual.length > 0) semanas.push(semanaAtual);
    return semanas;
  }

  formatarDataHeatmap(data: Date): string {
    return data.toLocaleDateString('pt-BR', { weekday: 'short', day: 'numeric', month: 'short' });
  }

  get nivelLabel(): string {
    return this.nivelLabels[this.usuario?.nivelAtual ?? 1] ?? 'Iniciante';
  }

  get xpParaSubir(): number {
    return this.xpParaProximoNivel[this.usuario?.nivelAtual ?? 1] ?? 700;
  }

  get percentualXP(): number {
    const xp = this.usuario?.xp ?? 0;
    const nivelAtual = this.usuario?.nivelAtual ?? 1;
    const xpBase: Record<number, number> = { 1: 0, 2: 100, 3: 300, 4: 700 };
    const base = xpBase[nivelAtual] ?? 0;
    const limite = this.xpParaSubir;
    if (nivelAtual === 4) return 100;
    return Math.min(100, Math.round(((xp - base) / (limite - base)) * 100));
  }

  /** Ponto (x,y) de um eixo do radar dado índice e valor 0-1 */
  radarPonto(indice: number, valor: number): { x: number; y: number } {
    const n = this.analytics.length || 1;
    const angulo = (indice * 2 * Math.PI) / n - Math.PI / 2;
    return {
      x: this.radarCenter + valor * this.radarRadius * Math.cos(angulo),
      y: this.radarCenter + valor * this.radarRadius * Math.sin(angulo),
    };
  }

  /** Pontos da área preenchida */
  get radarAreaPath(): string {
    if (!this.analytics.length) return '';
    return this.analytics
      .map((a, i) => {
        const p = this.radarPonto(i, a.percentualAcerto / 100);
        return `${i === 0 ? 'M' : 'L'}${p.x.toFixed(1)},${p.y.toFixed(1)}`;
      })
      .join(' ') + ' Z';
  }

  /** Linhas da grade (círculos de referência) em 25%, 50%, 75%, 100% */
  radarGradePath(nivel: number): string {
    if (!this.analytics.length) return '';
    const v = nivel / 100;
    return this.analytics
      .map((_, i) => {
        const p = this.radarPonto(i, v);
        return `${i === 0 ? 'M' : 'L'}${p.x.toFixed(1)},${p.y.toFixed(1)}`;
      })
      .join(' ') + ' Z';
  }

  /** Posição do label de cada eixo */
  radarLabel(indice: number): { x: number; y: number } {
    return this.radarPonto(indice, 1.25);
  }

  get analyticsComDados(): AnalyticsModulo[] {
    return this.analytics.filter(a => a.totalRespostas > 0);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }
}
