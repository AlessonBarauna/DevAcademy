import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { Perfil } from './perfil/perfil';

@NgModule({
  declarations: [Perfil],
  imports: [CommonModule, RouterModule, HttpClientModule],
})
export class PerfilModule {}
