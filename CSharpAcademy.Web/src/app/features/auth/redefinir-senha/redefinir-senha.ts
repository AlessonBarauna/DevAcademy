import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-redefinir-senha',
  standalone: false,
  templateUrl: './redefinir-senha.html',
  styleUrl: '../login/login.css',
})
export class RedefinirSenha implements OnInit {
  token = '';
  novaSenha = '';
  confirmarSenha = '';
  carregando = false;
  concluido = false;
  erro = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    this.token = this.route.snapshot.queryParamMap.get('token') ?? '';
    if (!this.token) this.erro = 'Link inválido ou expirado.';
  }

  redefinir(): void {
    if (this.novaSenha !== this.confirmarSenha) {
      this.erro = 'As senhas não coincidem.';
      return;
    }
    this.erro = '';
    this.carregando = true;
    this.auth.redefinirSenha(this.token, this.novaSenha).subscribe({
      next: () => {
        this.concluido = true;
        this.carregando = false;
        setTimeout(() => this.router.navigate(['/login']), 2500);
      },
      error: (err) => {
        this.erro = err.error?.mensagem ?? 'Link inválido ou expirado.';
        this.carregando = false;
      }
    });
  }
}
