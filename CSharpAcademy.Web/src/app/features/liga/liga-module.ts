import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LigaPage } from './liga-page/liga-page';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [LigaPage],
  imports: [
    CommonModule, HttpClientModule,
    RouterModule.forChild([{ path: '', component: LigaPage, canActivate: [AuthGuard] }]),
  ],
})
export class LigaModule {}
