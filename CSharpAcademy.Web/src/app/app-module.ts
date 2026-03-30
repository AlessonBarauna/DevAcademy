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
import { MarkdownModule } from 'ngx-markdown';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';

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
    MarkdownModule.forRoot(),
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [App],
})
export class AppModule {}
