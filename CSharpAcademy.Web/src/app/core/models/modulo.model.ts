export interface Modulo {
  id: number;
  titulo: string;
  descricao: string;
  ordem: number;
  nivelMinimo: string;
  totalLicoes: number;
  licoesCompletadas: number;
}

export interface Licao {
  id: number;
  moduloId: number;
  titulo: string;
  descricao: string;
  conteudoTeoricoMarkdown: string;
  ordem: number;
  xpRecompensa: number;
  completada: boolean;
}

export interface Exercicio {
  id: number;
  licaoId: number;
  enunciado: string;
  tipo: string;
  opcoesJson: string;
  ordem: number;
  xpRecompensa: number;
}

export interface RespostaExercicioResult {
  correta: boolean;
  explicacao: string | null;
  respostaCorreta: string | null;
  vidasRestantes: number;
  minutosParaRecarga: number;
}

export interface ConquistaResultDto {
  codigo: string;
  titulo: string;
  descricao: string;
  icone: string;
}

export interface ConcluirLicaoResult {
  xpGanho: number;
  novoNivel: number;
  xpTotal: number;
  jaConcluidaAntes: boolean;
  streakAtual: number;
  novasConquistas: ConquistaResultDto[];
}
