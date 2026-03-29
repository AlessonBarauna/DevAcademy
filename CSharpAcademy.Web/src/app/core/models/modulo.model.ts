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
