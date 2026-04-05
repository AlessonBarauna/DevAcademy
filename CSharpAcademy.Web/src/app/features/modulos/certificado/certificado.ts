import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import html2canvas from 'html2canvas';

interface CertificadoData {
  nomeAluno: string;
  moduloTitulo: string;
  moduloDescricao: string;
  nivel: string;
  totalLicoes: number;
  xpGanho: number;
  dataConclusao: string;
  emitidoEm: string;
}

@Component({
  selector: 'app-certificado',
  standalone: false,
  templateUrl: './certificado.html',
  styleUrl: './certificado.css',
})
export class Certificado implements OnInit {
  @ViewChild('certCard') certCard!: ElementRef<HTMLElement>;

  certificado: CertificadoData | null = null;
  carregando = true;
  erro = '';
  moduloId!: number;
  linkCopiado = false;
  baixando = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.moduloId = +this.route.snapshot.params['moduloId'];
    this.http.get<CertificadoData>(`/api/certificado/${this.moduloId}`).subscribe({
      next: c => { this.certificado = c; this.carregando = false; this.cdr.detectChanges(); },
      error: err => {
        this.erro = err.status === 400
          ? 'Conclua todas as lições do módulo para obter o certificado.'
          : 'Erro ao carregar certificado.';
        this.carregando = false;
        this.cdr.detectChanges();
      }
    });
  }

  imprimir(): void { window.print(); }

  voltar(): void { this.router.navigate(['/modulos', this.moduloId]); }

  irParaDashboard(): void { this.router.navigate(['/dashboard']); }

  get urlCertificado(): string {
    return window.location.href;
  }

  copiarLink(): void {
    navigator.clipboard.writeText(this.urlCertificado).then(() => {
      this.linkCopiado = true;
      this.cdr.detectChanges();
      setTimeout(() => { this.linkCopiado = false; this.cdr.detectChanges(); }, 2500);
    });
  }

  compartilharLinkedIn(): void {
    if (!this.certificado) return;
    const texto = encodeURIComponent(
      `Concluí o módulo "${this.certificado.moduloTitulo}" na C# Academy! 🎓\n` +
      `${this.certificado.totalLicoes} lições • ${this.certificado.xpGanho} XP conquistados\n\n` +
      `#CSharp #dotNET #Programação #DesenvolvimentoSoftware`
    );
    const url = encodeURIComponent(this.urlCertificado);
    window.open(`https://www.linkedin.com/sharing/share-offsite/?url=${url}&summary=${texto}`, '_blank');
  }

  compartilharWhatsApp(): void {
    if (!this.certificado) return;
    const texto = encodeURIComponent(
      `🎓 Concluí o módulo *${this.certificado.moduloTitulo}* na C# Academy!\n` +
      `${this.certificado.totalLicoes} lições • ${this.certificado.xpGanho} XP\n\n` +
      `Veja meu certificado: ${this.urlCertificado}`
    );
    window.open(`https://wa.me/?text=${texto}`, '_blank');
  }

  async baixarImagem(): Promise<void> {
    if (!this.certCard) return;
    this.baixando = true;
    this.cdr.detectChanges();
    try {
      const canvas = await html2canvas(this.certCard.nativeElement, {
        scale: 2,
        backgroundColor: '#1e1e1e',
        useCORS: true,
      });
      const link = document.createElement('a');
      link.download = `certificado-${this.certificado?.moduloTitulo ?? 'csharp'}.png`;
      link.href = canvas.toDataURL('image/png');
      link.click();
    } finally {
      this.baixando = false;
      this.cdr.detectChanges();
    }
  }

  nivelIcone(nivel: string): string {
    const mapa: Record<string, string> = {
      Iniciante: '🌱', Intermediario: '⚡', Avancado: '🔥', Especialista: '💎'
    };
    return mapa[nivel] ?? '📘';
  }
}
