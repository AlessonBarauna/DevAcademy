import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MissaoDiaria } from '../models/missao.model';

@Injectable({ providedIn: 'root' })
export class MissaoService {
  constructor(private http: HttpClient) {}

  getHoje(): Observable<MissaoDiaria[]> {
    return this.http.get<MissaoDiaria[]>('/api/missoes/hoje');
  }
}
