import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../core/services/auth';
import { ConquistaDto, UsuarioResponseDto } from '../../../core/models/auth.model';

@Component({
  selector: 'app-perfil',
  standalone: false,
  templateUrl: './perfil.html',
  styleUrl: './perfil.css',
})
export class Perfil implements OnInit, OnDestroy {
  usuario: UsuarioResponseDto | null = null;
  conquistas: ConquistaDto[] = [];
  carregando = true;
  private sub = new Subscription();

  readonly nivelLabels: Record<number, string> = {
    1: 'Iniciante', 2: 'Intermediário', 3: 'Avançado', 4: 'Especialista',
  };

  readonly xpParaProximoNivel: Record<number, number> = {
    1: 100, 2: 300, 3: 700, 4: 700,
  };

  constructor(
    private authService: AuthService,
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.sub.add(
      this.authService.usuario$.subscribe(u => {
        this.usuario = u;
        this.cdr.detectChanges();
      })
    );
    this.http.get<ConquistaDto[]>('/api/auth/conquistas').subscribe({
      next: c => { this.conquistas = c; this.carregando = false; this.cdr.detectChanges(); },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  get nivelLabel(): string {
    return this.nivelLabels[this.usuario?.nivelAtual ?? 1] ?? 'Iniciante';
  }

  get xpParaSubir(): number {
    return this.xpParaProximoNivel[this.usuario?.nivelAtual ?? 1] ?? 700;
  }

  get percentualXP(): number {
    const xp = this.usuario?.xp ?? 0;
    const nivelAtual = this.usuario?.nivelAtual ?? 1;
    const xpBase: Record<number, number> = { 1: 0, 2: 100, 3: 300, 4: 700 };
    const base = xpBase[nivelAtual] ?? 0;
    const limite = this.xpParaSubir;
    if (nivelAtual === 4) return 100;
    return Math.min(100, Math.round(((xp - base) / (limite - base)) * 100));
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  voltar(): void {
    this.router.navigate(['/dashboard']);
  }
}
