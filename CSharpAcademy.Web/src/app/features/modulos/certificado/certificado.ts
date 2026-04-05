import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface CertificadoData {
  nomeAluno: string;
  moduloTitulo: string;
  moduloDescricao: string;
  nivel: string;
  totalLicoes: number;
  xpGanho: number;
  dataConclusao: string;
  emitidoEm: string;
}

@Component({
  selector: 'app-certificado',
  standalone: false,
  templateUrl: './certificado.html',
  styleUrl: './certificado.css',
})
export class Certificado implements OnInit {
  certificado: CertificadoData | null = null;
  carregando = true;
  erro = '';
  moduloId!: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.http.get<CertificadoData>(`/api/certificado/${this.moduloId}`).subscribe({
      next: c => { this.certificado = c; this.carregando = false; this.cdr.detectChanges(); },
      error: err => {
        this.erro = err.status === 400
          ? 'Conclua todas as lições do módulo para obter o certificado.'
          : 'Erro ao carregar certificado.';
        this.carregando = false;
        this.cdr.detectChanges();
      }
    });
  }

  imprimir(): void { window.print(); }

  voltar(): void { this.router.navigate(['/modulos', this.moduloId]); }

  irParaDashboard(): void { this.router.navigate(['/dashboard']); }

  nivelIcone(nivel: string): string {
    const mapa: Record<string, string> = {
      Iniciante: '🌱', Intermediario: '⚡', Avancado: '🔥', Especialista: '💎'
    };
    return mapa[nivel] ?? '📘';
  }
}
