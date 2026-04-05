import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { Dashboard } from './dashboard/dashboard';
import { SharedModule } from '../../shared/shared-module';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [Dashboard],
  imports: [
    CommonModule, FormsModule, HttpClientModule, SharedModule,
    RouterModule.forChild([{ path: '', component: Dashboard, canActivate: [AuthGuard] }]),
  ],
})
export class DashboardModule {}
