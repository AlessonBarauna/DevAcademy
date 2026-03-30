import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  ChatMessage,
  ChatRequestDto,
  ChatResponseDto,
  CustomExerciseDto,
  FeedbackDto,
  GerarExercicioRequestDto
} from '../models/assistant.model';

const API = '/api/assistant';

@Injectable({ providedIn: 'root' })
export class AssistantService {
  constructor(private http: HttpClient) {}

  fazerPergunta(request: ChatRequestDto): Observable<ChatResponseDto> {
    return this.http.post<ChatResponseDto>(`${API}/perguntar`, request);
  }

  getHistorico(licaoId: number, pagina = 1): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(`${API}/historico/${licaoId}?pagina=${pagina}`);
  }

  avaliarResposta(idMensagem: number, feedback: FeedbackDto): Observable<void> {
    return this.http.post<void>(`${API}/${idMensagem}/avaliar`, feedback);
  }

  gerarExercicioCustomizado(request: GerarExercicioRequestDto): Observable<CustomExerciseDto> {
    return this.http.post<CustomExerciseDto>(`${API}/gerar-exercicio`, request);
  }
}
