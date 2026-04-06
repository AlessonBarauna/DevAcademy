import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-esqueci-senha',
  standalone: false,
  templateUrl: './esqueci-senha.html',
  styleUrl: '../login/login.css',
})
export class EsqueciSenha {
  email = '';
  carregando = false;
  enviado = false;
  erro = '';

  constructor(private auth: AuthService) {}

  enviar(): void {
    if (!this.email) return;
    this.erro = '';
    this.carregando = true;
    this.auth.esqueciSenha(this.email).subscribe({
      next: () => { this.enviado = true; this.carregando = false; },
      error: () => { this.erro = 'Erro ao enviar e-mail. Tente novamente.'; this.carregando = false; }
    });
  }
}
