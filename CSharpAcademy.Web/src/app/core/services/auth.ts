import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginDto, RegistrarUsuarioDto, UsuarioResponseDto } from '../models/auth.model';

const API = '/api';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private _usuario = new BehaviorSubject<UsuarioResponseDto | null>(this.carregarUsuario());
  usuario$ = this._usuario.asObservable();

  constructor(private http: HttpClient) {}

  get usuarioAtual(): UsuarioResponseDto | null {
    return this._usuario.value;
  }

  get token(): string | null {
    return this.usuarioAtual?.token ?? null;
  }

  get estaLogado(): boolean {
    return !!this.token;
  }

  login(dto: LoginDto): Observable<UsuarioResponseDto> {
    return this.http.post<UsuarioResponseDto>(`${API}/auth/login`, dto).pipe(
      tap(u => this.salvarUsuario(u))
    );
  }

  registrar(dto: RegistrarUsuarioDto): Observable<UsuarioResponseDto> {
    return this.http.post<UsuarioResponseDto>(`${API}/auth/registrar`, dto).pipe(
      tap(u => this.salvarUsuario(u))
    );
  }

  atualizarProgresso(xp: number, nivelAtual: number, streakAtual?: number): void {
    const u = this._usuario.value;
    if (!u) return;
    const hoje = new Date().toISOString().slice(0, 10);
    const atualizado = { ...u, xp, nivelAtual, ultimoEstudo: hoje, ...(streakAtual !== undefined && { streakAtual }) };
    localStorage.setItem('usuario', JSON.stringify(atualizado));
    this._usuario.next(atualizado);
  }

  sincronizarPerfil(): void {
    this.http.get<any>(`${API}/auth/perfil`).subscribe({
      next: perfil => {
        const u = this._usuario.value;
        if (!u) return;
        const atualizado = { ...u, xp: perfil.xp, nivelAtual: perfil.nivelAtual, streakAtual: perfil.streakAtual, streakMaximo: perfil.streakMaximo, ultimoEstudo: perfil.ultimoEstudo, streakFreeze: perfil.streakFreeze };
        localStorage.setItem('usuario', JSON.stringify(atualizado));
        this._usuario.next(atualizado);
      }
    });
  }

  logout(): void {
    localStorage.removeItem('usuario');
    this._usuario.next(null);
  }

  private salvarUsuario(u: UsuarioResponseDto): void {
    localStorage.setItem('usuario', JSON.stringify(u));
    this._usuario.next(u);
  }

  private carregarUsuario(): UsuarioResponseDto | null {
    const json = localStorage.getItem('usuario');
    return json ? JSON.parse(json) : null;
  }
}
