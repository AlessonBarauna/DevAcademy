import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login',     component: Login },
  { path: 'registrar', component: Register },

  { path: 'onboarding', loadChildren: () => import('./features/auth/auth-module').then(m => m.AuthModule), canActivate: [AuthGuard] },
  { path: 'dashboard',  loadChildren: () => import('./features/dashboard/dashboard-module').then(m => m.DashboardModule) },
  { path: 'modulos',    loadChildren: () => import('./features/modulos/modulos-module').then(m => m.ModulosModule) },
  { path: 'desafio-rapido', loadChildren: () => import('./features/exercicios/exercicios-module').then(m => m.ExerciciosModule) },
  { path: 'ranking',    loadChildren: () => import('./features/ranking/ranking-module').then(m => m.RankingModule) },
  { path: 'perfil',     loadChildren: () => import('./features/perfil/perfil-module').then(m => m.PerfilModule) },
  { path: 'liga',       loadChildren: () => import('./features/liga/liga-module').then(m => m.LigaModule) },
  { path: 'analytics',  loadChildren: () => import('./features/analytics/analytics-module').then(m => m.AnalyticsModule) },
  { path: 'playground', loadChildren: () => import('./features/playground/playground-module').then(m => m.PlaygroundModule) },
  { path: 'projetos',   loadChildren: () => import('./features/projetos/projetos-module').then(m => m.ProjetosModule) },
  { path: 'glossario',  loadChildren: () => import('./features/glossario/glossario-module').then(m => m.GlossarioModule) },
  { path: 'metas',      loadChildren: () => import('./features/metas/metas-module').then(m => m.MetasModule) },

  { path: '**', loadChildren: () => import('./features/not-found/not-found-module').then(m => m.NotFoundModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
