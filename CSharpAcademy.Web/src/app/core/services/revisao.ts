import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RevisaoPendente, RevisaoResultado } from '../models/revisao.model';

const API = '/api/revisao';

@Injectable({ providedIn: 'root' })
export class RevisaoService {
  constructor(private http: HttpClient) {}

  getPendentes(): Observable<RevisaoPendente[]> {
    return this.http.get<RevisaoPendente[]>(`${API}/pendentes`);
  }

  registrar(licaoId: number, acertou: boolean): Observable<RevisaoResultado> {
    return this.http.post<RevisaoResultado>(`${API}/${licaoId}/registrar`, { acertou });
  }
}
