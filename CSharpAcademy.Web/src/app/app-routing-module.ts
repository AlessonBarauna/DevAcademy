import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { ModuloList } from './features/modulos/modulo-list/modulo-list';
import { ModuloDetail } from './features/modulos/modulo-detail/modulo-detail';
import { LicaoDetail } from './features/modulos/licao-detail/licao-detail';
import { ExercicioView } from './features/exercicios/exercicio-view/exercicio-view';
import { DesafioRapido } from './features/exercicios/desafio-rapido/desafio-rapido';
import { ExameView } from './features/modulos/exame-view/exame-view';
import { Certificado } from './features/modulos/certificado/certificado';
import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { Ranking } from './features/ranking/ranking/ranking';
import { Perfil } from './features/perfil/perfil/perfil';
import { LigaPage } from './features/liga/liga-page/liga-page';
import { AnalyticsPage } from './features/analytics/analytics-page/analytics-page';
import { Onboarding } from './features/auth/onboarding/onboarding';
import { PlaygroundPage } from './features/playground/playground-page/playground-page';
import { ProjetosPage } from './features/projetos/projetos-page/projetos-page';
import { GlossarioPage } from './features/glossario/glossario-page/glossario-page';
import { MetasPage } from './features/metas/metas-page/metas-page';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'registrar', component: Register },
  { path: 'dashboard', component: Dashboard, canActivate: [AuthGuard] },
  { path: 'ranking', component: Ranking, canActivate: [AuthGuard] },
  { path: 'perfil', component: Perfil, canActivate: [AuthGuard] },
  { path: 'modulos', component: ModuloList, canActivate: [AuthGuard] },
  { path: 'modulos/:moduloId', component: ModuloDetail, canActivate: [AuthGuard] },
  { path: 'modulos/:moduloId/exame', component: ExameView, canActivate: [AuthGuard] },
  { path: 'modulos/:moduloId/certificado', component: Certificado, canActivate: [AuthGuard] },
  { path: 'modulos/:moduloId/licoes', component: LicaoDetail, canActivate: [AuthGuard] },
  { path: 'modulos/:moduloId/licoes/:licaoId/exercicios', component: ExercicioView, canActivate: [AuthGuard] },
  { path: 'desafio-rapido', component: DesafioRapido, canActivate: [AuthGuard] },
  { path: 'liga', component: LigaPage, canActivate: [AuthGuard] },
  { path: 'analytics', component: AnalyticsPage, canActivate: [AuthGuard] },
  { path: 'onboarding', component: Onboarding, canActivate: [AuthGuard] },
  { path: 'playground', component: PlaygroundPage, canActivate: [AuthGuard] },
  { path: 'projetos', component: ProjetosPage, canActivate: [AuthGuard] },
  { path: 'glossario', component: GlossarioPage, canActivate: [AuthGuard] },
  { path: 'metas', component: MetasPage, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
