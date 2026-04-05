import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

export interface TarefaProjeto {
  id: number;
  descricao: string;
  concluida: boolean;
}

export interface ProjetoItem {
  id: number;
  titulo: string;
  descricao: string;
  dificuldade: string;
  xpRecompensa: number;
  totalTarefas: number;
  tarefasConcluidas: number;
  percentual: number;
  concluido: boolean;
  tarefas: TarefaProjeto[];
}

@Component({
  selector: 'app-projetos-page',
  standalone: false,
  templateUrl: './projetos-page.html',
  styleUrl: './projetos-page.css',
})
export class ProjetosPage implements OnInit {
  projetos: ProjetoItem[] = [];
  carregando = true;
  projetoAberto: number | null = null;
  concluindo: Set<string> = new Set();

  constructor(
    private http: HttpClient,
    public router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.carregar();
  }

  carregar(): void {
    this.http.get<ProjetoItem[]>('/api/projeto').subscribe({
      next: p => {
        this.projetos = p;
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  toggleProjeto(id: number): void {
    this.projetoAberto = this.projetoAberto === id ? null : id;
    this.cdr.detectChanges();
  }

  concluirTarefa(projeto: ProjetoItem, tarefa: TarefaProjeto): void {
    if (tarefa.concluida) return;
    const chave = `${projeto.id}-${tarefa.id}`;
    if (this.concluindo.has(chave)) return;
    this.concluindo.add(chave);

    this.http.post<{ projetoConcluido: boolean; xpGanho: number; totalConcluidas: number; total: number }>(
      `/api/projeto/${projeto.id}/tarefa/${tarefa.id}/concluir`, {}
    ).subscribe({
      next: res => {
        tarefa.concluida = true;
        projeto.tarefasConcluidas = res.totalConcluidas;
        projeto.percentual = res.total > 0 ? Math.round(res.totalConcluidas / res.total * 100) : 0;
        projeto.concluido = res.projetoConcluido;
        this.concluindo.delete(chave);
        this.cdr.detectChanges();
      },
      error: () => { this.concluindo.delete(chave); this.cdr.detectChanges(); }
    });
  }

  get totalConcluidos(): number {
    return this.projetos.filter(p => p.concluido).length;
  }

  get xpTotal(): number {
    return this.projetos.filter(p => p.concluido).reduce((a, p) => a + p.xpRecompensa, 0);
  }

  corDificuldade(d: string): string {
    return d === 'Iniciante' ? 'dif-ini' : d === 'Intermediário' ? 'dif-int' : 'dif-av';
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }
}
