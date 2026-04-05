export interface LigaItem {
  posicao: number;
  id: number;
  nome: string;
  xpSemana: number;
  streakAtual: number;
  euMesmo: boolean;
}

export interface LigaSemana {
  liga: string;
  ligaIcone: string;
  meuXpSemana: number;
  minhaPosicao: number;
  totalParticipantes: number;
  semanaInicio: string;
  semanaFim: string;
  participantes: LigaItem[];
}
