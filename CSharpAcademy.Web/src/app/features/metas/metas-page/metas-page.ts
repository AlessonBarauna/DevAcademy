import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface ProgressoSemana {
  licoesNaSemana: number;
  xpNaSemana: number;
  exerciciosNaSemana: number;
  inicioSemana: string;
  fimSemana: string;
}

interface Metas {
  licoes: number;
  xp: number;
  exercicios: number;
}

const CHAVE_METAS = 'metas_semanais';

const PRESETS_LICOES     = [1, 3, 5, 7, 10];
const PRESETS_XP         = [50, 100, 200, 350, 500];
const PRESETS_EXERCICIOS = [5, 10, 20, 35, 50];

@Component({
  selector: 'app-metas-page',
  standalone: false,
  templateUrl: './metas-page.html',
  styleUrl: './metas-page.css',
})
export class MetasPage implements OnInit {
  progresso: ProgressoSemana | null = null;
  carregando = true;

  metas: Metas = { licoes: 5, xp: 100, exercicios: 10 };
  editando = false;
  metasTemp: Metas = { licoes: 5, xp: 100, exercicios: 10 };

  readonly presetsLicoes     = PRESETS_LICOES;
  readonly presetsXp         = PRESETS_XP;
  readonly presetsExercicios = PRESETS_EXERCICIOS;

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.carregarMetas();
    this.carregarProgresso();
  }

  private carregarMetas(): void {
    try {
      const salvo = localStorage.getItem(CHAVE_METAS);
      if (salvo) this.metas = JSON.parse(salvo);
    } catch { /* usa padrão */ }
  }

  private carregarProgresso(): void {
    this.carregando = true;
    this.http.get<ProgressoSemana>('/api/metas/semana').subscribe({
      next: res => {
        this.progresso = res;
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.carregando = false;
        this.cdr.detectChanges();
      }
    });
  }

  // ── Edição de metas ──────────────────────────────

  abrirEdicao(): void {
    this.metasTemp = { ...this.metas };
    this.editando = true;
    this.cdr.detectChanges();
  }

  salvarMetas(): void {
    this.metas = { ...this.metasTemp };
    localStorage.setItem(CHAVE_METAS, JSON.stringify(this.metas));
    this.editando = false;
    this.cdr.detectChanges();
  }

  cancelarEdicao(): void {
    this.editando = false;
    this.cdr.detectChanges();
  }

  // ── Cálculos de progresso ────────────────────────

  pct(atual: number, meta: number): number {
    return Math.min(100, Math.round((atual / meta) * 100));
  }

  status(atual: number, meta: number): 'ok' | 'perto' | 'inicio' {
    const p = this.pct(atual, meta);
    if (p >= 100) return 'ok';
    if (p >= 60)  return 'perto';
    return 'inicio';
  }

  get diasRestantes(): number {
    if (!this.progresso) return 0;
    const fim = new Date(this.progresso.fimSemana + 'T23:59:59Z');
    const agora = new Date();
    return Math.max(0, Math.ceil((fim.getTime() - agora.getTime()) / 86400000));
  }

  get semanaLabel(): string {
    if (!this.progresso) return '';
    const fmt = (s: string) => {
      const d = new Date(s + 'T00:00:00Z');
      return d.toLocaleDateString('pt-BR', { day: '2-digit', month: '2-digit', timeZone: 'UTC' });
    };
    return `${fmt(this.progresso.inicioSemana)} – ${fmt(this.progresso.fimSemana)}`;
  }

  voltar(): void { this.router.navigate(['/dashboard']); }
}
