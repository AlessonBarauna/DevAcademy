import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { RankingItem } from '../../../core/models/ranking.model';
import { ThemeService } from '../../../core/services/theme';

@Component({
  selector: 'app-ranking',
  standalone: false,
  templateUrl: './ranking.html',
  styleUrl: './ranking.css',
})
export class Ranking implements OnInit {
  itens: RankingItem[] = [];
  itensSemanal: RankingItem[] = [];
  carregando = true;
  aba: 'geral' | 'semanal' = 'geral';

  get top10(): RankingItem[] {
    return this.itens.filter(i => !i.euMesmo || i.posicao <= 10);
  }

  get euForaDoTop(): RankingItem | null {
    const eu = this.itens.find(i => i.euMesmo);
    return eu && eu.posicao > 10 ? eu : null;
  }

  readonly nivelLabels: Record<number, string> = {
    1: 'Iniciante', 2: 'Intermediário', 3: 'Avançado', 4: 'Especialista',
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef,
    public themeService: ThemeService
  ) {}

  ngOnInit(): void {
    this.http.get<RankingItem[]>('/api/ranking').subscribe({
      next: itens => { this.itens = itens; this.carregando = false; this.cdr.detectChanges(); },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
    this.http.get<RankingItem[]>('/api/ranking/semanal').subscribe({
      next: itens => { this.itensSemanal = itens; this.cdr.detectChanges(); }
    });
  }

  get itensAtivos(): RankingItem[] {
    return this.aba === 'geral' ? this.itens : this.itensSemanal;
  }

  get top10Ativo(): RankingItem[] {
    return this.itensAtivos.filter(i => !i.euMesmo || i.posicao <= 10);
  }

  get euForaDoTopAtivo(): RankingItem | null {
    const eu = this.itensAtivos.find(i => i.euMesmo);
    return eu && eu.posicao > 10 ? eu : null;
  }

  trocarAba(aba: 'geral' | 'semanal'): void {
    this.aba = aba;
    this.cdr.detectChanges();
  }

  medalha(posicao: number): string {
    return posicao === 1 ? '🥇' : posicao === 2 ? '🥈' : posicao === 3 ? '🥉' : `${posicao}º`;
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }
}
