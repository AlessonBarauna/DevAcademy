import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  email = '';
  senha = '';
  erro = '';
  carregando = false;

  constructor(private auth: AuthService, private router: Router, private cdr: ChangeDetectorRef) {}

  entrar(): void {
    this.erro = '';
    this.carregando = true;
    this.auth.login({ email: this.email, senha: this.senha }).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: () => {
        this.erro = 'E-mail ou senha inválidos.';
        this.carregando = false;
        this.cdr.detectChanges();
      }
    });
  }
}
