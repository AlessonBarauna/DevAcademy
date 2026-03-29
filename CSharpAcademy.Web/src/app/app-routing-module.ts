import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { ModuloList } from './features/modulos/modulo-list/modulo-list';
import { LicaoDetail } from './features/modulos/licao-detail/licao-detail';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'registrar', component: Register },
  { path: 'modulos', component: ModuloList },
  { path: 'modulos/:moduloId/licoes', component: LicaoDetail },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
