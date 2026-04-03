export interface RevisaoPendente {
  licaoId: number;
  licaoTitulo: string;
  moduloId: number;
  nivelRetencao: number;
  totalRevisoes: number;
  proximaRevisao: string;
}

export interface RegistrarRevisaoDto {
  acertou: boolean;
}

export interface RevisaoResultado {
  novoNivelRetencao: number;
  proximaRevisao: string;
  xpGanho: number;
  mensagem: string;
}
