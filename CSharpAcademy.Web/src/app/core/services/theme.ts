import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

type Tema = 'dark' | 'light';

export interface CorDestaque {
  nome: string;
  valor: string;
  valorEscuro: string;
}

export const CORES_DESTAQUE: CorDestaque[] = [
  { nome: 'Teal',    valor: '#4ec9b0', valorEscuro: '#3aab94' },
  { nome: 'Azul',    valor: '#4d9ef7', valorEscuro: '#3a8adf' },
  { nome: 'Roxo',    valor: '#c586c0', valorEscuro: '#a96ea3' },
  { nome: 'Verde',   valor: '#89d185', valorEscuro: '#6ab566' },
  { nome: 'Laranja', valor: '#f5a623', valorEscuro: '#d9901a' },
  { nome: 'Rosa',    valor: '#f48fb1', valorEscuro: '#d9709a' },
];

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private _tema = new BehaviorSubject<Tema>(this.carregarTema());
  tema$ = this._tema.asObservable();

  readonly cores = CORES_DESTAQUE;
  private _corAtual = new BehaviorSubject<CorDestaque>(this.carregarCor());
  cor$ = this._corAtual.asObservable();

  get temaAtual(): Tema { return this._tema.value; }
  get isLight(): boolean { return this._tema.value === 'light'; }
  get corAtual(): CorDestaque { return this._corAtual.value; }

  toggle(): void {
    const novo: Tema = this._tema.value === 'dark' ? 'light' : 'dark';
    this.aplicar(novo);
  }

  get usandoTemaDoSistema(): boolean {
    return !localStorage.getItem('tema');
  }

  inicializar(): void {
    this.aplicar(this._tema.value);
    this.aplicarCor(this._corAtual.value);

    // Ouve mudanças de tema do sistema em tempo real (só quando o usuário não salvou preferência)
    window.matchMedia('(prefers-color-scheme: light)').addEventListener('change', e => {
      if (!localStorage.getItem('tema')) {
        this.aplicar(e.matches ? 'light' : 'dark');
      }
    });
  }

  definirCor(cor: CorDestaque): void {
    localStorage.setItem('cor-destaque', cor.nome);
    this._corAtual.next(cor);
    this.aplicarCor(cor);
  }

  private aplicar(tema: Tema): void {
    document.documentElement.setAttribute('data-theme', tema);
    localStorage.setItem('tema', tema);
    this._tema.next(tema);
  }

  private aplicarCor(cor: CorDestaque): void {
    const root = document.documentElement;
    root.style.setProperty('--teal', cor.valor);
    root.style.setProperty('--teal-dark', cor.valorEscuro);
  }

  private carregarTema(): Tema {
    const salvo = localStorage.getItem('tema') as Tema | null;
    if (salvo === 'light' || salvo === 'dark') return salvo;
    // Sem preferência salva — respeita a preferência do sistema operacional
    return window.matchMedia('(prefers-color-scheme: light)').matches ? 'light' : 'dark';
  }

  private carregarCor(): CorDestaque {
    const nome = localStorage.getItem('cor-destaque');
    return CORES_DESTAQUE.find(c => c.nome === nome) ?? CORES_DESTAQUE[0];
  }
}
