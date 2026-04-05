import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PlaygroundPage } from './playground-page/playground-page';
import { AuthGuard } from '../../core/guards/auth.guard';

@NgModule({
  declarations: [PlaygroundPage],
  imports: [
    CommonModule, FormsModule,
    RouterModule.forChild([{ path: '', component: PlaygroundPage, canActivate: [AuthGuard] }]),
  ],
})
export class PlaygroundModule {}
