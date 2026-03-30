export interface LoginDto {
  email: string;
  senha: string;
}

export interface RegistrarUsuarioDto {
  nome: string;
  email: string;
  senha: string;
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
}
