import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class NotificacaoService {
  private readonly CHAVE_PERMISSAO = 'notif_permissao_solicitada';

  get suportado(): boolean {
    return 'Notification' in window;
  }

  get permissaoConcedida(): boolean {
    return this.suportado && Notification.permission === 'granted';
  }

  async solicitarPermissao(): Promise<boolean> {
    if (!this.suportado) return false;
    if (Notification.permission === 'granted') return true;
    if (Notification.permission === 'denied') return false;

    const resultado = await Notification.requestPermission();
    localStorage.setItem(this.CHAVE_PERMISSAO, 'sim');
    return resultado === 'granted';
  }

  devePerguntar(): boolean {
    if (!this.suportado) return false;
    if (Notification.permission !== 'default') return false;
    return !localStorage.getItem(this.CHAVE_PERMISSAO);
  }

  mostrar(titulo: string, opcoes?: NotificationOptions): void {
    if (!this.permissaoConcedida) return;

    if ('serviceWorker' in navigator) {
      navigator.serviceWorker.ready.then(reg => {
        reg.showNotification(titulo, {
          icon: '/icons/icon-192x192.png',
          badge: '/icons/icon-96x96.png',
          ...opcoes
        });
      }).catch(() => new Notification(titulo, opcoes));
    } else {
      new Notification(titulo, opcoes);
    }
  }

  agendarLembreteDiario(streakAtual: number): void {
    if (!this.permissaoConcedida) return;

    const hoje = new Date().toDateString();
    const chave = `lembrete_${hoje}`;
    if (sessionStorage.getItem(chave)) return;
    sessionStorage.setItem(chave, '1');

    // Lembrete em 4 horas se não estudou ainda
    const ms = 4 * 60 * 60 * 1000;
    setTimeout(() => {
      const estudouHoje = localStorage.getItem('ultimo_estudo') === new Date().toDateString();
      if (!estudouHoje) {
        const msg = streakAtual > 0
          ? `🔥 Seu streak de ${streakAtual} dia(s) está em risco! Estude agora para mantê-lo.`
          : '📚 Que tal estudar C# hoje? Mantenha o hábito!';
        this.mostrar('C# Academy', {
          body: msg,
          tag: 'lembrete-diario',
        });
      }
    }, ms);
  }

  notificarNivelAcima(nomeNivel: string): void {
    this.mostrar('🎉 Você subiu de nível!', {
      body: `Parabéns! Você chegou ao nível ${nomeNivel} na C# Academy.`,
      tag: 'level-up',
    });
  }

  notificarStreakEmRisco(dias: number): void {
    this.mostrar('🔥 Streak em risco!', {
      body: `Seu streak de ${dias} dia(s) vai acabar hoje. Estude uma lição para mantê-lo!`,
      tag: 'streak-risco',
    });
  }
}
