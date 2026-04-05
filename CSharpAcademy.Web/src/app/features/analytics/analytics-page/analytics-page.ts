import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ChartData, ChartOptions } from 'chart.js';

interface ModuloAnalytics {
  moduloId: number;
  moduloTitulo: string;
  totalRespostas: number;
  totalCorretas: number;
  percentualAcerto: number;
}

@Component({
  selector: 'app-analytics-page',
  standalone: false,
  templateUrl: './analytics-page.html',
  styleUrl: './analytics-page.css',
})
export class AnalyticsPage implements OnInit {
  dados: ModuloAnalytics[] = [];
  carregando = true;

  barData: ChartData<'bar'> = { labels: [], datasets: [] };
  radarData: ChartData<'radar'> = { labels: [], datasets: [] };

  barOptions: ChartOptions<'bar'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      tooltip: {
        callbacks: {
          label: ctx => ` ${ctx.parsed.y}% de acerto`
        }
      }
    },
    scales: {
      y: {
        min: 0, max: 100,
        ticks: { color: '#9d9d9d', callback: v => `${v}%` },
        grid: { color: '#3e3e42' }
      },
      x: {
        ticks: { color: '#9d9d9d', maxRotation: 35 },
        grid: { color: '#3e3e42' }
      }
    }
  };

  radarOptions: ChartOptions<'radar'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: { legend: { display: false } },
    scales: {
      r: {
        min: 0, max: 100,
        ticks: { display: false },
        grid: { color: '#3e3e42' },
        pointLabels: { color: '#9d9d9d', font: { size: 11 } },
        angleLines: { color: '#3e3e42' }
      }
    }
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.http.get<ModuloAnalytics[]>('/api/auth/analytics').subscribe({
      next: dados => {
        this.dados = dados.filter(d => d.totalRespostas > 0);
        this.buildCharts();
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => { this.carregando = false; this.cdr.detectChanges(); }
    });
  }

  private buildCharts(): void {
    const labels = this.dados.map(d => this.abreviar(d.moduloTitulo));
    const valores = this.dados.map(d => d.percentualAcerto);
    const cores = valores.map(v =>
      v >= 80 ? 'rgba(0,200,150,0.7)' :
      v >= 60 ? 'rgba(220,180,0,0.7)' :
               'rgba(244,71,71,0.7)'
    );
    const bordas = cores.map(c => c.replace('0.7', '1'));

    this.barData = {
      labels,
      datasets: [{
        data: valores,
        backgroundColor: cores,
        borderColor: bordas,
        borderWidth: 1,
        borderRadius: 4
      }]
    };

    this.radarData = {
      labels,
      datasets: [{
        data: valores,
        backgroundColor: 'rgba(0,200,150,0.15)',
        borderColor: 'rgba(0,200,150,0.8)',
        pointBackgroundColor: 'rgba(0,200,150,1)',
        borderWidth: 2
      }]
    };
  }

  private abreviar(titulo: string): string {
    return titulo.length > 18 ? titulo.slice(0, 16) + '…' : titulo;
  }

  get totalRespostas(): number { return this.dados.reduce((a, d) => a + d.totalRespostas, 0); }
  get totalCorretas(): number  { return this.dados.reduce((a, d) => a + d.totalCorretas, 0); }
  get percentualGeral(): number {
    return this.totalRespostas > 0 ? Math.round((this.totalCorretas / this.totalRespostas) * 100) : 0;
  }
  get modulosAtivos(): number { return this.dados.length; }
  get melhorModulo(): ModuloAnalytics | null {
    return this.dados.length ? [...this.dados].sort((a, b) => b.percentualAcerto - a.percentualAcerto)[0] : null;
  }
  get moduloParaMelhorar(): ModuloAnalytics | null {
    const com = this.dados.filter(d => d.totalRespostas >= 3);
    return com.length ? [...com].sort((a, b) => a.percentualAcerto - b.percentualAcerto)[0] : null;
  }

  voltar(): void { this.router.navigate(['/dashboard']); }
}
