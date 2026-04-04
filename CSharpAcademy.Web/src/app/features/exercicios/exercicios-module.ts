import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ExercicioView } from './exercicio-view/exercicio-view';
import { DesafioRapido } from './desafio-rapido/desafio-rapido';

@NgModule({
  declarations: [ExercicioView, DesafioRapido],
  imports: [CommonModule, FormsModule, RouterModule],
})
export class ExerciciosModule {}
