import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Toast, ToastService } from '../../core/services/toast';

@Component({
  selector: 'app-toast',
  standalone: false,
  templateUrl: './toast.html',
  styleUrl: './toast.css',
})
export class ToastComponent implements OnInit, OnDestroy {
  toasts: Toast[] = [];
  private sub!: Subscription;

  constructor(private toastService: ToastService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.sub = this.toastService.toasts$.subscribe(t => {
      this.toasts = t;
      this.cdr.detectChanges();
    });
  }

  ngOnDestroy(): void { this.sub.unsubscribe(); }

  fechar(id: number): void { this.toastService.remover(id); }

  icone(tipo: string): string {
    return { erro: '✕', sucesso: '✓', aviso: '⚠', info: 'ℹ' }[tipo] ?? 'ℹ';
  }
}
