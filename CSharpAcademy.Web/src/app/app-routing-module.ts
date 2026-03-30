import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { ModuloList } from './features/modulos/modulo-list/modulo-list';
import { LicaoDetail } from './features/modulos/licao-detail/licao-detail';
import { ExercicioView } from './features/exercicios/exercicio-view/exercicio-view';
import { Dashboard } from './features/dashboard/dashboard/dashboard';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'registrar', component: Register },
  { path: 'dashboard', component: Dashboard },
  { path: 'modulos', component: ModuloList },
  { path: 'modulos/:moduloId/licoes', component: LicaoDetail },
  { path: 'modulos/:moduloId/licoes/:licaoId/exercicios', component: ExercicioView },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
