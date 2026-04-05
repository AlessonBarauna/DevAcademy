import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { AuthModule } from './features/auth/auth-module';
import { ModulosModule } from './features/modulos/modulos-module';
import { ExerciciosModule } from './features/exercicios/exercicios-module';
import { DashboardModule } from './features/dashboard/dashboard-module';
import { RankingModule } from './features/ranking/ranking-module';
import { PerfilModule } from './features/perfil/perfil-module';
import { SharedModule } from './shared/shared-module';
import { LigaModule } from './features/liga/liga-module';
import { AnalyticsModule } from './features/analytics/analytics-module';
import { MarkdownModule } from 'ngx-markdown';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';

@NgModule({
  declarations: [App],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    AuthModule,
    ModulosModule,
    ExerciciosModule,
    DashboardModule,
    RankingModule,
    PerfilModule,
    SharedModule,
    LigaModule,
    AnalyticsModule,
    MarkdownModule.forRoot(),
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    provideCharts(withDefaultRegisterables())
  ],
  bootstrap: [App],
})
export class AppModule {}
