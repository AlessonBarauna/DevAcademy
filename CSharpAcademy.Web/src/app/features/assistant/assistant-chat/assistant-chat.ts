import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { AssistantService } from '../../../core/services/assistant';
import { AuthService } from '../../../core/services/auth';
import { ChatMessage, ChatRequestDto, CustomExerciseDto, FeedbackDto } from '../../../core/models/assistant.model';

@Component({
  selector: 'app-assistant-chat',
  standalone: false,
  templateUrl: './assistant-chat.html',
  styleUrl: './assistant-chat.css',
})
export class AssistantChat implements OnInit {
  @Input() licaoId!: number;
  @ViewChild('chatContainer') chatContainer!: ElementRef;

  mostrarChat = false;
  mensagens: ChatMessage[] = [];
  pergunta = '';
  carregando = false;
  idioma = 'pt-BR';
  tema: 'light' | 'dark' = 'light';

  exercicioModal: CustomExerciseDto | null = null;
  mostrarExercicioModal = false;

  constructor(
    private assistantService: AssistantService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {}

  abrirChat(): void {
    this.mostrarChat = true;
    if (this.mensagens.length === 0) {
      this.carregarHistorico();
    }
    setTimeout(() => this.scrollParaBaixo(), 200);
  }

  fecharChat(): void {
    this.mostrarChat = false;
  }

  carregarHistorico(): void {
    if (!this.licaoId) return;
    this.assistantService.getHistorico(this.licaoId).subscribe({
      next: msgs => {
        this.mensagens = msgs;
        setTimeout(() => this.scrollParaBaixo(), 100);
      }
    });
  }

  enviarPergunta(): void {
    if (!this.pergunta.trim() || this.carregando) return;

    const texto = this.pergunta.trim();
    this.pergunta = '';
    this.carregando = true;

    const request: ChatRequestDto = {
      pergunta: texto,
      licaoId: this.licaoId,
      idioma: this.idioma
    };

    this.assistantService.fazerPergunta(request).subscribe({
      next: response => {
        if (response.sucesso) {
          this.mensagens.push({
            id: response.idMensagem,
            pergunta: texto,
            resposta: response.resposta,
            estrelas: null,
            data: new Date(),
            usouCache: response.usouCache
          });
          setTimeout(() => this.scrollParaBaixo(), 100);
        }
        this.carregando = false;
      },
      error: () => { this.carregando = false; }
    });
  }

  avaliarResposta(idMensagem: number, estrelas: number): void {
    const usuario = this.authService.usuarioAtual;
    if (!usuario) return;

    const feedback: FeedbackDto = {
      usuarioId: usuario.id,
      estrelas,
      respostaAjudou: estrelas >= 4,
      respostaClara: true,
      respostaCompleta: true
    };

    this.assistantService.avaliarResposta(idMensagem, feedback).subscribe({
      next: () => {
        const msg = this.mensagens.find(m => m.id === idMensagem);
        if (msg) msg.estrelas = estrelas;
      }
    });
  }

  gerarExercicio(topico: string): void {
    this.assistantService.gerarExercicioCustomizado({
      licaoId: this.licaoId,
      topicoPergunta: topico
    }).subscribe({
      next: exercicio => {
        this.exercicioModal = exercicio;
        this.mostrarExercicioModal = true;
      }
    });
  }

  fecharExercicioModal(): void {
    this.mostrarExercicioModal = false;
    this.exercicioModal = null;
  }

  trocarTema(): void {
    this.tema = this.tema === 'light' ? 'dark' : 'light';
  }

  handleKeydown(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.enviarPergunta();
    }
  }

  private scrollParaBaixo(): void {
    if (this.chatContainer) {
      this.chatContainer.nativeElement.scrollTop =
        this.chatContainer.nativeElement.scrollHeight;
    }
  }

  estrelaRange = [1, 2, 3, 4, 5];
}
