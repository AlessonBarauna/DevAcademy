import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { RankingItem } from '../../../core/models/ranking.model';

@Component({
  selector: 'app-ranking',
  standalone: false,
  templateUrl: './ranking.html',
  styleUrl: './ranking.css',
})
export class Ranking implements OnInit {
  itens: RankingItem[] = [];
  carregando = true;

  readonly nivelLabels: Record<number, string> = {
    1: 'Iniciante', 2: 'Intermediário', 3: 'Avançado', 4: 'Especialista',
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.http.get<RankingItem[]>('/api/ranking').subscribe({
      next: itens => {
        this.itens = itens;
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  medalha(posicao: number): string {
    return posicao === 1 ? '🥇' : posicao === 2 ? '🥈' : posicao === 3 ? '🥉' : `${posicao}º`;
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }
}
