import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginDto, RegistrarUsuarioDto, UsuarioResponseDto } from '../models/auth.model';

const API = 'http://localhost:5080/api';

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
