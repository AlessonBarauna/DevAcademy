import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuloService } from '../../../core/services/modulo';
import { AuthService } from '../../../core/services/auth';
import { Modulo } from '../../../core/models/modulo.model';

@Component({
  selector: 'app-certificado',
  standalone: false,
  templateUrl: './certificado.html',
  styleUrl: './certificado.css',
})
export class Certificado implements OnInit {
  modulo: Modulo | null = null;
  nomeUsuario = '';
  dataHoje = new Date().toLocaleDateString('pt-BR', { day: 'numeric', month: 'long', year: 'numeric' });
  moduloId!: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private moduloService: ModuloService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.moduloService.getModulos().subscribe(mods => {
      this.modulo = mods.find(m => m.id === this.moduloId) ?? null;
    });
    this.authService.usuario$.subscribe(u => {
      this.nomeUsuario = u?.nome ?? '';
    });
  }

  imprimir(): void {
    window.print();
  }

  voltar(): void {
    this.router.navigate(['/modulos', this.moduloId]);
  }
}
