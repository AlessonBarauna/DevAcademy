import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BaseChartDirective } from 'ng2-charts';
import { AnalyticsPage } from './analytics-page/analytics-page';

@NgModule({
  declarations: [AnalyticsPage],
  imports: [CommonModule, RouterModule, BaseChartDirective],
})
export class AnalyticsModule {}
