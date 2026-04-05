import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { Perfil } from './perfil/perfil';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [Perfil],
  imports: [
    CommonModule, HttpClientModule,
    RouterModule.forChild([{ path: '', component: Perfil, canActivate: [AuthGuard] }]),
  ],
})
export class PerfilModule {}
