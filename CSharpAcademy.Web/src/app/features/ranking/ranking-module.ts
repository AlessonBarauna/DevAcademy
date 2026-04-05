import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { Ranking } from './ranking/ranking';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [Ranking],
  imports: [
    CommonModule, HttpClientModule,
    RouterModule.forChild([{ path: '', component: Ranking, canActivate: [AuthGuard] }]),
  ],
})
export class RankingModule {}
