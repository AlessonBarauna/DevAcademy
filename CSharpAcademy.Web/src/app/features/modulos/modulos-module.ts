import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';

import { SharedModule } from '../../shared/shared-module';
import { ModuloList } from './modulo-list/modulo-list';
import { ModuloDetail } from './modulo-detail/modulo-detail';
import { LicaoDetail } from './licao-detail/licao-detail';
import { AssistantChat } from '../assistant/assistant-chat/assistant-chat';
import { ExameView } from './exame-view/exame-view';
import { Certificado } from './certificado/certificado';
import { ExercicioView } from '../exercicios/exercicio-view/exercicio-view';
import { AuthGuard } from '../../core/guards/auth.guard';

const guard = [AuthGuard];

@NgModule({
  declarations: [ModuloList, ModuloDetail, LicaoDetail, AssistantChat, ExameView, Certificado, ExercicioView],
  imports: [
    CommonModule, FormsModule, SharedModule, MarkdownModule.forChild(),
    RouterModule.forChild([
      { path: '',                                              component: ModuloList,    canActivate: guard },
      { path: ':moduloId',                                    component: ModuloDetail,  canActivate: guard },
      { path: ':moduloId/exame',                              component: ExameView,     canActivate: guard },
      { path: ':moduloId/certificado',                        component: Certificado,   canActivate: guard },
      { path: ':moduloId/licoes',                             component: LicaoDetail,   canActivate: guard },
      { path: ':moduloId/licoes/:licaoId/exercicios',         component: ExercicioView, canActivate: guard },
    ]),
  ],
})
export class ModulosModule {}
