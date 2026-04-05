import { ChangeDetectorRef, Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  nome = '';
  email = '';
  senha = '';
  erro = '';
  carregando = false;

  constructor(
    private auth: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  registrar(): void {
    this.erro = '';
    this.carregando = true;
    this.auth.registrar({ nome: this.nome, email: this.email, senha: this.senha }).subscribe({
      next: () => this.router.navigate(['/onboarding']),
      error: (err: HttpErrorResponse) => {
        this.erro = err.error?.mensagem ?? 'Erro ao criar conta. Verifique os dados.';
        this.carregando = false;
        this.cdr.detectChanges();
      }
    });
  }
}
