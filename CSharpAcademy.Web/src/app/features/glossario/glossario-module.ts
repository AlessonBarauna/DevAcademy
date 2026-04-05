import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GlossarioPage } from './glossario-page/glossario-page';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [GlossarioPage],
  imports: [
    CommonModule, FormsModule,
    RouterModule.forChild([{ path: '', component: GlossarioPage, canActivate: [AuthGuard] }]),
  ],
})
export class GlossarioModule {}
