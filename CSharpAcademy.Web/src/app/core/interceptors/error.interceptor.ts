import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastService } from '../services/toast';

// Rotas que já tratam o erro localmente — não mostrar toast global
const SILENCIOSAS = [
  '/api/auth/login',
  '/api/auth/registrar',
  '/api/busca',
  '/api/playground/executar',
];

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private toast: ToastService) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        const silenciosa = SILENCIOSAS.some(r => req.url.includes(r));

        if (!silenciosa) {
          const mensagem = this.mensagemParaErro(err, req);
          if (mensagem) this.toast.erro(mensagem);
        }

        return throwError(() => err);
      })
    );
  }

  private mensagemParaErro(err: HttpErrorResponse, req: HttpRequest<unknown>): string | null {
    // 401 já é tratado pelo AuthInterceptor
    if (err.status === 401) return null;

    if (err.status === 0) return 'Sem conexão com o servidor. Verifique se o backend está rodando.';
    if (err.status === 403) return 'Você não tem permissão para realizar esta ação.';
    if (err.status === 404) return `Recurso não encontrado.`;
    if (err.status === 429) return 'Muitas requisições. Aguarde um momento.';
    if (err.status >= 500) return 'Erro interno no servidor. Tente novamente em instantes.';

    // Mensagem vinda do backend (campo "mensagem" no body)
    const body = err.error;
    if (body?.mensagem) return body.mensagem;

    return null;
  }
}
