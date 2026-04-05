import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-not-found',
  standalone: false,
  templateUrl: './not-found.html',
  styleUrl: './not-found.css',
})
export class NotFound {
  readonly path = window.location.pathname;

  constructor(private router: Router, private auth: AuthService) {}

  get logado(): boolean { return this.auth.estaLogado; }

  voltar(): void {
    this.router.navigate([this.auth.estaLogado ? '/dashboard' : '/login']);
  }
}
