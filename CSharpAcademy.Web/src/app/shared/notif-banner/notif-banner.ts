import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { NotificacaoService } from '../../core/services/notificacao';

@Component({
  selector: 'app-notif-banner',
  standalone: false,
  templateUrl: './notif-banner.html',
  styleUrl: './notif-banner.css',
})
export class NotifBanner implements OnInit {
  visivel = false;
  respondido = false;

  constructor(
    private notif: NotificacaoService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.visivel = this.notif.devePerguntar();
  }

  async permitir(): Promise<void> {
    this.respondido = true;
    await this.notif.solicitarPermissao();
    this.visivel = false;
    this.cdr.detectChanges();
  }

  dispensar(): void {
    localStorage.setItem('notif_permissao_solicitada', 'sim');
    this.visivel = false;
    this.cdr.detectChanges();
  }
}
