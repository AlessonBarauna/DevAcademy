import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProjetosPage } from './projetos-page/projetos-page';

@NgModule({
  declarations: [ProjetosPage],
  imports: [CommonModule, RouterModule],
})
export class ProjetosModule {}
