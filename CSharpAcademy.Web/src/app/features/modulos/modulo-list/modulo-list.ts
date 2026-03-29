import { Component, OnInit } from '@angular/core';
import { ModuloService } from '../../../core/services/modulo';
import { Modulo } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-modulo-list',
  standalone: false,
  templateUrl: './modulo-list.html',
  styleUrl: './modulo-list.css',
})
export class ModuloList implements OnInit {
  modulos: Modulo[] = [];
  carregando = true;

  constructor(private moduloService: ModuloService) {}

  ngOnInit(): void {
    this.moduloService.getModulos().subscribe({
      next: m => { this.modulos = m; this.carregando = false; },
      error: () => { this.carregando = false; }
    });
  }

  progresso(m: Modulo): number {
    if (m.totalLicoes === 0) return 0;
    return Math.round((m.licoesCompletadas / m.totalLicoes) * 100);
  }
}
