import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export type ToastTipo = 'erro' | 'sucesso' | 'aviso' | 'info';

export interface Toast {
  id: number;
  mensagem: string;
  tipo: ToastTipo;
  saindo?: boolean;
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private _toasts = new BehaviorSubject<Toast[]>([]);
  readonly toasts$ = this._toasts.asObservable();

  private nextId = 0;

  mostrar(mensagem: string, tipo: ToastTipo = 'info', duracaoMs = 4000): void {
    const id = ++this.nextId;
    const toast: Toast = { id, mensagem, tipo };
    this._toasts.next([...this._toasts.value, toast]);
    setTimeout(() => this.remover(id), duracaoMs);
  }

  erro(mensagem: string): void    { this.mostrar(mensagem, 'erro', 5000); }
  sucesso(mensagem: string): void { this.mostrar(mensagem, 'sucesso', 3000); }
  aviso(mensagem: string): void   { this.mostrar(mensagem, 'aviso', 4000); }
  info(mensagem: string): void    { this.mostrar(mensagem, 'info', 4000); }

  remover(id: number): void {
    // Marca como saindo para disparar a animação de saída
    const lista = this._toasts.value.map(t => t.id === id ? { ...t, saindo: true } : t);
    this._toasts.next(lista);
    // Remove após a animação terminar (250ms = duração do slideOut)
    setTimeout(() => {
      this._toasts.next(this._toasts.value.filter(t => t.id !== id));
    }, 260);
  }
}
