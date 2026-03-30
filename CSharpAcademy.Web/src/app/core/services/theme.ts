import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

type Tema = 'dark' | 'light';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private _tema = new BehaviorSubject<Tema>(this.carregarTema());
  tema$ = this._tema.asObservable();

  get temaAtual(): Tema {
    return this._tema.value;
  }

  get isLight(): boolean {
    return this._tema.value === 'light';
  }

  toggle(): void {
    const novo: Tema = this._tema.value === 'dark' ? 'light' : 'dark';
    this.aplicar(novo);
  }

  inicializar(): void {
    this.aplicar(this._tema.value);
  }

  private aplicar(tema: Tema): void {
    document.documentElement.setAttribute('data-theme', tema);
    localStorage.setItem('tema', tema);
    this._tema.next(tema);
  }

  private carregarTema(): Tema {
    const salvo = localStorage.getItem('tema') as Tema | null;
    return salvo === 'light' ? 'light' : 'dark';
  }
}
