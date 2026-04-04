import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RevisaoService } from '../../../core/services/revisao';
import { RevisaoPendente } from '../../../core/models/revisao.model';

@Component({
  selector: 'app-revisao-page',
  standalone: false,
  templateUrl: './revisao-page.html',
  styleUrl: './revisao-page.css',
})
export class RevisaoPage implements OnInit {
  pendentes: RevisaoPendente[] = [];
  carregando = true;

  constructor(
    private revisaoService: RevisaoService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.revisaoService.getPendentes().subscribe({
      next: p => { this.pendentes = p; this.carregando = false; this.cdr.detectChanges(); },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  revisar(r: RevisaoPendente): void {
    this.router.navigate(['/modulos', r.moduloId, 'licoes'], { queryParams: { licaoId: r.licaoId } });
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }

  nivelLabel(nivel: number): string {
    const labels: Record<number, string> = { 1: 'Baixa', 2: 'Fraca', 3: 'Média', 4: 'Boa', 5: 'Alta' };
    return labels[nivel] ?? '';
  }
}
