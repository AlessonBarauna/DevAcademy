import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MetasPage } from './metas-page/metas-page';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [MetasPage],
  imports: [
    CommonModule, FormsModule,
    RouterModule.forChild([{ path: '', component: MetasPage, canActivate: [AuthGuard] }]),
  ],
})
export class MetasModule {}
