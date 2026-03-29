export interface ChatMessage {
  id: number;
  pergunta: string;
  resposta: string;
  estrelas: number | null;
  data: Date;
  usouCache: boolean;
}

export interface ChatRequestDto {
  pergunta: string;
  licaoId: number;
  exercicioId?: number;
  idioma: string;
}

export interface ChatResponseDto {
  idMensagem: number;
  resposta: string;
  sucesso: boolean;
  mensagem?: string;
  tipo?: string;
  usouCache: boolean;
  idioma: string;
  sugerirExercicio: boolean;
}

export interface FeedbackDto {
  usuarioId: number;
  estrelas: number;
  comentario?: string;
  respostaAjudou: boolean;
  respostaClara: boolean;
  respostaCompleta: boolean;
}

export interface CustomExerciseDto {
  enunciado: string;
  tipo: string;
  opcoes: string[];
  respostaCorreta: string;
  explicacao: string;
}

export interface GerarExercicioRequestDto {
  licaoId: number;
  topicoPergunta: string;
}
