import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { ModuloList } from './features/modulos/modulo-list/modulo-list';
import { LicaoDetail } from './features/modulos/licao-detail/licao-detail';
import { ExercicioView } from './features/exercicios/exercicio-view/exercicio-view';
import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { Ranking } from './features/ranking/ranking/ranking';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'registrar', component: Register },
  { path: 'dashboard', component: Dashboard, canActivate: [AuthGuard] },
  { path: 'ranking', component: Ranking, canActivate: [AuthGuard] },
  { path: 'modulos', component: ModuloList, canActivate: [AuthGuard] },
  { path: 'modulos/:moduloId/licoes', component: LicaoDetail, canActivate: [AuthGuard] },
  { path: 'modulos/:moduloId/licoes/:licaoId/exercicios', component: ExercicioView, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
