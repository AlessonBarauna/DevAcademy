import { Component, signal } from '@angular/core';
import { ThemeService } from './core/services/theme';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('CSharpAcademy.Web');

  constructor(themeService: ThemeService) {
    themeService.inicializar();
  }
}
