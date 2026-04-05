import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProjetosPage } from './projetos-page/projetos-page';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [ProjetosPage],
  imports: [
    CommonModule,
    RouterModule.forChild([{ path: '', component: ProjetosPage, canActivate: [AuthGuard] }]),
  ],
})
export class ProjetosModule {}
