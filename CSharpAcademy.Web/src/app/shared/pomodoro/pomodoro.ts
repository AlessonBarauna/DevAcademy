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
  rodando = false;
  modo: Modo = 'foco';
  pomodoros = 0;

  private readonly duracoes: Record<Modo, number> = {
    foco: 25 * 60,
    pausa: 5 * 60,
    'pausa-longa': 15 * 60,
  };

  segundosRestantes = this.duracoes['foco'];
  private intervalo: ReturnType<typeof setInterval> | null = null;

  get minutos(): string {
    return String(Math.floor(this.segundosRestantes / 60)).padStart(2, '0');
  }

  get segundos(): string {
    return String(this.segundosRestantes % 60).padStart(2, '0');
  }

  get progresso(): number {
    return ((this.duracoes[this.modo] - this.segundosRestantes) / this.duracoes[this.modo]) * 100;
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
  }

  toggleTimer(): void {
    if (this.rodando) {
      this.pausar();
    } else {
      this.iniciar();
    }
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
      const proximoModo: Modo = this.pomodoros % 4 === 0 ? 'pausa-longa' : 'pausa';
      this.notificar(`Pomodoro #${this.pomodoros} concluído! Hora de ${proximoModo === 'pausa-longa' ? 'uma pausa longa' : 'uma pausa curta'}.`);
      this.mudarModo(proximoModo);
    } else {
      this.notificar('Pausa encerrada! Hora de focar.');
      this.mudarModo('foco');
    }
    this.iniciar();
  }

  mudarModo(modo: Modo): void {
    this.pausar();
    this.modo = modo;
    this.segundosRestantes = this.duracoes[modo];
  }

  resetar(): void {
    this.pausar();
    this.segundosRestantes = this.duracoes[this.modo];
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
