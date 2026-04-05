import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { animate, query, style, transition, trigger } from '@angular/animations';
import { ThemeService } from './core/services/theme';

export const routeAnimations = trigger('routeAnimations', [
  transition('* <=> *', [
    query(':enter', [
      style({ opacity: 0, transform: 'translateY(6px)' }),
      animate('180ms ease-out', style({ opacity: 1, transform: 'translateY(0)' })),
    ], { optional: true }),
  ]),
]);

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css',
  animations: [routeAnimations],
})
export class App {
  protected readonly title = signal('CSharpAcademy.Web');

  constructor(themeService: ThemeService) {
    themeService.inicializar();
  }

  getRouteAnimation(outlet: RouterOutlet): string {
    return outlet?.activatedRouteData?.['animation'] ?? outlet?.activatedRoute?.snapshot?.url?.[0]?.path ?? '';
  }
}
