import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { Modulo } from '../../core/models/modulo.model';
import { AnimacaoService } from '../../core/services/animacao';

@Component({
  selector: 'app-modulo-conclusao',
  standalone: false,
  templateUrl: './modulo-conclusao.html',
  styleUrl: './modulo-conclusao.css',
})
export class ModuloConclusao implements OnChanges {
  @Input() modulo: Modulo | null = null;
  @Input() xpTotal: number = 0;
  @Output() fechar = new EventEmitter<void>();
  @Output() irParaModulos = new EventEmitter<void>();

  visivel = false;

  constructor(private animacao: AnimacaoService) {}

  ngOnChanges(): void {
    if (this.modulo !== null) {
      this.visivel = true;
      setTimeout(() => this.animacao.dispararConfettiModulo(), 200);
    }
  }

  fecharOverlay(): void {
    this.visivel = false;
    this.fechar.emit();
  }

  navegarParaModulos(): void {
    this.visivel = false;
    this.irParaModulos.emit();
  }
}
