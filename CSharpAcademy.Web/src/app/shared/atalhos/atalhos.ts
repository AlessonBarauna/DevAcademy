import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-atalhos',
  standalone: false,
  templateUrl: './atalhos.html',
  styleUrl: './atalhos.css',
})
export class Atalhos implements OnInit, OnDestroy {
  visivel = false;

  readonly lista = [
    { tecla: '?', descricao: 'Mostrar atalhos' },
    { tecla: 'G D', descricao: 'Ir para Dashboard' },
    { tecla: 'G M', descricao: 'Ir para Módulos' },
    { tecla: 'G R', descricao: 'Ir para Ranking' },
    { tecla: 'G P', descricao: 'Ir para Perfil' },
    { tecla: 'G A', descricao: 'Ir para Análises' },
    { tecla: 'G L', descricao: 'Ir para Liga' },
    { tecla: 'G G', descricao: 'Ir para Glossário' },
    { tecla: 'Esc', descricao: 'Fechar modal' },
  ];

  private pendente: string | null = null;
  private timer: ReturnType<typeof setTimeout> | null = null;

  constructor(private router: Router, private auth: AuthService) {}

  ngOnInit(): void {}
  ngOnDestroy(): void { if (this.timer) clearTimeout(this.timer); }

  @HostListener('document:keydown', ['$event'])
  onKeydown(e: KeyboardEvent): void {
    // Ignora se foco está em input/textarea/select
    const tag = (e.target as HTMLElement)?.tagName?.toLowerCase();
    if (['input', 'textarea', 'select'].includes(tag)) return;

    const key = e.key;

    if (key === 'Escape') { this.visivel = false; this.pendente = null; return; }
    if (key === '?') { this.visivel = !this.visivel; return; }

    if (!this.auth.estaLogado) return;

    // Sequência Go: G + letra
    if (this.pendente === 'g') {
      if (this.timer) clearTimeout(this.timer);
      this.pendente = null;
      const destinos: Record<string, string> = {
        d: '/dashboard', m: '/modulos', r: '/ranking',
        p: '/perfil', a: '/analytics', l: '/liga', g: '/glossario',
      };
      const rota = destinos[key.toLowerCase()];
      if (rota) this.router.navigate([rota]);
      return;
    }

    if (key.toLowerCase() === 'g') {
      this.pendente = 'g';
      this.timer = setTimeout(() => { this.pendente = null; }, 1500);
    }
  }

  fechar(): void { this.visivel = false; }
}
