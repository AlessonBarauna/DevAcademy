import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Licao, Modulo } from '../models/modulo.model';

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
}
