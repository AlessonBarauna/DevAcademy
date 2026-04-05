import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { AnimacaoService } from '../../core/services/animacao';

@Component({
  selector: 'app-level-up-overlay',
  standalone: false,
  templateUrl: './level-up-overlay.html',
  styleUrl: './level-up-overlay.css',
})
export class LevelUpOverlay implements OnChanges {
  @Input() novoNivel: number | null = null;
  @Output() fechar = new EventEmitter<void>();

  visivel = false;

  readonly nivelLabels: Record<number, string> = {
    1: 'Iniciante', 2: 'Intermediário', 3: 'Avançado', 4: 'Especialista'
  };

  readonly nivelIcones: Record<number, string> = {
    1: '🌱', 2: '⚡', 3: '🔥', 4: '💎'
  };

  constructor(private animacao: AnimacaoService) {}

  ngOnChanges(): void {
    if (this.novoNivel !== null) {
      this.visivel = true;
      setTimeout(() => this.animacao.dispararConfettiModulo(), 200);
      setTimeout(() => { this.visivel = false; this.fechar.emit(); }, 4000);
    }
  }

  get label(): string { return this.nivelLabels[this.novoNivel ?? 1] ?? ''; }
  get icone(): string { return this.nivelIcones[this.novoNivel ?? 1] ?? '🎉'; }
}
