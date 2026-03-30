import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConcluirLicaoResult, Exercicio, Licao, Modulo, RespostaExercicioResult } from '../models/modulo.model';

const API = 'http://localhost:5080/api';

@Injectable({ providedIn: 'root' })
export class ModuloService {
  constructor(private http: HttpClient) {}

  getModulos(): Observable<Modulo[]> {
    return this.http.get<Modulo[]>(`${API}/modulo`);
  }

  getLicoes(moduloId: number): Observable<Licao[]> {
    return this.http.get<Licao[]>(`${API}/modulo/${moduloId}/licoes`);
  }

  getLicao(moduloId: number, licaoId: number): Observable<Licao> {
    return this.http.get<Licao>(`${API}/modulo/${moduloId}/licoes/${licaoId}`);
  }

  concluirLicao(moduloId: number, licaoId: number): Observable<ConcluirLicaoResult> {
    return this.http.post<ConcluirLicaoResult>(`${API}/modulo/${moduloId}/licoes/${licaoId}/concluir`, {});
  }

  getExercicios(licaoId: number): Observable<Exercicio[]> {
    return this.http.get<Exercicio[]>(`${API}/licao/${licaoId}/exercicios`);
  }

  responderExercicio(licaoId: number, exercicioId: number, resposta: string): Observable<RespostaExercicioResult> {
    return this.http.post<RespostaExercicioResult>(
      `${API}/licao/${licaoId}/exercicios/${exercicioId}/responder`,
      { resposta }
    );
  }
}
