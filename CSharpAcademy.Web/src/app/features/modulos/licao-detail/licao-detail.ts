import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { Licao } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-licao-detail',
  standalone: false,
  templateUrl: './licao-detail.html',
  styleUrl: './licao-detail.css',
})
export class LicaoDetail implements OnInit {
  licoes: Licao[] = [];
  licaoSelecionada: Licao | null = null;
  moduloId!: number;
  carregando = true;

  constructor(private route: ActivatedRoute, private moduloService: ModuloService) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.moduloService.getLicoes(this.moduloId).subscribe({
      next: licoes => {
        this.licoes = licoes;
        if (licoes.length > 0) this.licaoSelecionada = licoes[0];
        this.carregando = false;
      },
      error: () => { this.carregando = false; }
    });
  }

  selecionarLicao(licao: Licao): void {
    this.licaoSelecionada = licao;
  }
}
