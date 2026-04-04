import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { RevisaoPendente, RevisaoResultado } from '../models/revisao.model';

const API = '/api/revisao';

@Injectable({ providedIn: 'root' })
export class RevisaoService {
  private _contagem = new BehaviorSubject<number>(0);
  contagem$ = this._contagem.asObservable();

  constructor(private http: HttpClient) {}

  carregar(): void {
    this.http.get<RevisaoPendente[]>(`${API}/pendentes`).subscribe({
      next: p => this._contagem.next(p.length)
    });
  }

  getPendentes(): Observable<RevisaoPendente[]> {
    return this.http.get<RevisaoPendente[]>(`${API}/pendentes`).pipe(
      tap(p => this._contagem.next(p.length))
    );
  }

  registrar(licaoId: number, acertou: boolean): Observable<RevisaoResultado> {
    return this.http.post<RevisaoResultado>(`${API}/${licaoId}/registrar`, { acertou });
  }
}
