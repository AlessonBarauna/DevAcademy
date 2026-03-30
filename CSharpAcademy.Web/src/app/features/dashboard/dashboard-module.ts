import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Dashboard } from './dashboard/dashboard';

@NgModule({
  declarations: [Dashboard],
  imports: [CommonModule, RouterModule],
})
export class DashboardModule {}
