import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { LigaSemana } from '../../../core/models/liga.model';

@Component({
  selector: 'app-liga-page',
  standalone: false,
  templateUrl: './liga-page.html',
  styleUrl: './liga-page.css',
})
export class LigaPage implements OnInit {
  liga: LigaSemana | null = null;
  carregando = true;

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.http.get<LigaSemana>('/api/liga/semana').subscribe({
      next: l => { this.liga = l; this.carregando = false; this.cdr.detectChanges(); },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }

  ligaCorClasse(liga: string): string {
    const map: Record<string, string> = {
      Bronze: 'liga-bronze',
      Prata: 'liga-prata',
      Ouro: 'liga-ouro',
      Diamante: 'liga-diamante'
    };
    return map[liga] ?? '';
  }

  medalha(posicao: number): string {
    return posicao === 1 ? '🥇' : posicao === 2 ? '🥈' : posicao === 3 ? '🥉' : `${posicao}º`;
  }

  xpPercent(xp: number, participantes: { xpSemana: number }[]): number {
    const max = Math.max(...participantes.map(p => p.xpSemana), 1);
    return Math.round((xp / max) * 100);
  }

  proximaLiga(liga: string): string {
    const map: Record<string, string> = {
      Bronze: 'Prata (Nível 2)',
      Prata: 'Ouro (Nível 3)',
      Ouro: 'Diamante (Nível 4)',
      Diamante: ''
    };
    return map[liga] ?? '';
  }
}
