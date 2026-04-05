import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BaseChartDirective } from 'ng2-charts';
import { AnalyticsPage } from './analytics-page/analytics-page';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [AnalyticsPage],
  imports: [
    CommonModule, BaseChartDirective,
    RouterModule.forChild([{ path: '', component: AnalyticsPage, canActivate: [AuthGuard] }]),
  ],
})
export class AnalyticsModule {}
