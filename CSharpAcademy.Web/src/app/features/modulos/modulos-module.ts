import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';

import { ModulosRoutingModule } from './modulos-routing-module';
import { SharedModule } from '../../shared/shared-module';
import { ModuloList } from './modulo-list/modulo-list';
import { ModuloDetail } from './modulo-detail/modulo-detail';
import { LicaoDetail } from './licao-detail/licao-detail';
import { AssistantChat } from '../assistant/assistant-chat/assistant-chat';
import { ExameView } from './exame-view/exame-view';
import { Certificado } from './certificado/certificado';

@NgModule({
  declarations: [ModuloList, ModuloDetail, LicaoDetail, AssistantChat, ExameView, Certificado],
  imports: [CommonModule, FormsModule, RouterModule, ModulosRoutingModule, SharedModule, MarkdownModule.forChild()],
})
export class ModulosModule {}
