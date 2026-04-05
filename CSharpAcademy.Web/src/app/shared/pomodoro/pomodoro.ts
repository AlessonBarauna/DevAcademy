import { Component, OnDestroy } from '@angular/core';

type Modo = 'foco' | 'pausa' | 'pausa-longa';

@Component({
  selector: 'app-pomodoro',
  standalone: false,
  templateUrl: './pomodoro.html',
  styleUrl: './pomodoro.css',
})
export class Pomodoro implements OnDestroy {
  aberto = false;
  editando = false;
  rodando = false;
  modo: Modo = 'foco';
  pomodoros = 0;

  // Durações em minutos (editáveis pelo usuário)
  minutosFoco = 25;
  minutosPausa = 5;
  minutosPausaLonga = 15;

  // Campos temporários durante edição
  editFoco = 25;
  editPausa = 5;
  editPausaLonga = 15;

  segundosRestantes = this.minutosFoco * 60;
  private intervalo: ReturnType<typeof setInterval> | null = null;

  get duracaoAtual(): number {
    if (this.modo === 'foco') return this.minutosFoco * 60;
    if (this.modo === 'pausa') return this.minutosPausa * 60;
    return this.minutosPausaLonga * 60;
  }

  get minutos(): string {
    return String(Math.floor(this.segundosRestantes / 60)).padStart(2, '0');
  }

  get segundos(): string {
    return String(this.segundosRestantes % 60).padStart(2, '0');
  }

  get progresso(): number {
    const total = this.duracaoAtual;
    return total > 0 ? ((total - this.segundosRestantes) / total) * 100 : 0;
  }

  get labelModo(): string {
    const mapa: Record<Modo, string> = {
      foco: '🍅 Foco',
      pausa: '☕ Pausa',
      'pausa-longa': '🌿 Pausa Longa',
    };
    return mapa[this.modo];
  }

  toggleAberto(): void {
    this.aberto = !this.aberto;
    if (!this.aberto) this.editando = false;
  }

  toggleTimer(): void {
    if (this.rodando) this.pausar();
    else this.iniciar();
  }

  abrirEdicao(): void {
    this.pausar();
    this.editFoco = this.minutosFoco;
    this.editPausa = this.minutosPausa;
    this.editPausaLonga = this.minutosPausaLonga;
    this.editando = true;
  }

  salvarEdicao(): void {
    this.minutosFoco = Math.max(1, Math.min(99, +this.editFoco || 25));
    this.minutosPausa = Math.max(1, Math.min(99, +this.editPausa || 5));
    this.minutosPausaLonga = Math.max(1, Math.min(99, +this.editPausaLonga || 15));
    this.editando = false;
    this.segundosRestantes = this.duracaoAtual;
  }

  cancelarEdicao(): void {
    this.editando = false;
  }

  private iniciar(): void {
    this.rodando = true;
    this.intervalo = setInterval(() => {
      if (this.segundosRestantes > 0) {
        this.segundosRestantes--;
      } else {
        this.concluir();
      }
    }, 1000);
  }

  private pausar(): void {
    this.rodando = false;
    if (this.intervalo) {
      clearInterval(this.intervalo);
      this.intervalo = null;
    }
  }

  private concluir(): void {
    this.pausar();
    if (this.modo === 'foco') {
      this.pomodoros++;
      const proximo: Modo = this.pomodoros % 4 === 0 ? 'pausa-longa' : 'pausa';
      this.notificar(`Pomodoro #${this.pomodoros} concluído! Hora de ${proximo === 'pausa-longa' ? 'uma pausa longa' : 'uma pausa curta'}.`);
      this.mudarModo(proximo);
    } else {
      this.notificar('Pausa encerrada! Hora de focar.');
      this.mudarModo('foco');
    }
    this.iniciar();
  }

  mudarModo(modo: Modo): void {
    this.pausar();
    this.modo = modo;
    this.segundosRestantes = this.duracaoAtual;
  }

  resetar(): void {
    this.pausar();
    this.segundosRestantes = this.duracaoAtual;
  }

  private notificar(msg: string): void {
    if ('Notification' in window && Notification.permission === 'granted') {
      new Notification('C# Academy — Pomodoro', { body: msg, icon: '/favicon.ico' });
    }
  }

  pedirPermissao(): void {
    if ('Notification' in window && Notification.permission === 'default') {
      Notification.requestPermission();
    }
  }

  ngOnDestroy(): void {
    this.pausar();
  }
}
