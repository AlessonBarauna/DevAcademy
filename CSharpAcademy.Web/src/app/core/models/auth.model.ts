export interface LoginDto {
  email: string;
  senha: string;
}

export interface RegistrarUsuarioDto {
  nome: string;
  email: string;
  senha: string;
}

export interface ConquistaDto {
  codigo: string;
  titulo: string;
  descricao: string;
  icone: string;
  dataConquista?: string;
}

export interface UsuarioResponseDto {
  id: number;
  nome: string;
  email: string;
  nivelAtual: number;
  xp: number;
  token: string;
  streakAtual?: number;
  streakMaximo?: number;
  ultimoEstudo?: string;
  streakFreeze?: number;
}
