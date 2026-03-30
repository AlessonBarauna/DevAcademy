import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';

import { ModulosRoutingModule } from './modulos-routing-module';
import { ModuloList } from './modulo-list/modulo-list';
import { LicaoDetail } from './licao-detail/licao-detail';
import { AssistantChat } from '../assistant/assistant-chat/assistant-chat';

@NgModule({
  declarations: [ModuloList, LicaoDetail, AssistantChat],
  imports: [CommonModule, FormsModule, RouterModule, ModulosRoutingModule, MarkdownModule.forChild()],
})
export class ModulosModule {}
