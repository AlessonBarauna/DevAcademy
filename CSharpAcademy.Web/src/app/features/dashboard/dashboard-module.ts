import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { Dashboard } from './dashboard/dashboard';

@NgModule({
  declarations: [Dashboard],
  imports: [CommonModule, RouterModule, HttpClientModule],
})
export class DashboardModule {}
