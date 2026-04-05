import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DesafioRapido } from './desafio-rapido/desafio-rapido';
import { SharedModule } from '../../shared/shared-module';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [DesafioRapido],
  imports: [
    CommonModule, FormsModule, SharedModule,
    RouterModule.forChild([{ path: '', component: DesafioRapido, canActivate: [AuthGuard] }]),
  ],
})
export class ExerciciosModule {}
