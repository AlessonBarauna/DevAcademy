import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LevelUpOverlay } from './level-up-overlay/level-up-overlay';
import { NotifBanner } from './notif-banner/notif-banner';
import { ToastComponent } from './toast/toast';
import { Pomodoro } from './pomodoro/pomodoro';
import { Atalhos } from './atalhos/atalhos';
import { NavProgress } from './nav-progress/nav-progress';
import { ModuloConclusao } from './modulo-conclusao/modulo-conclusao';

@NgModule({
  declarations: [LevelUpOverlay, NotifBanner, ToastComponent, Pomodoro, Atalhos, NavProgress, ModuloConclusao],
  imports: [CommonModule, FormsModule, RouterModule],
  exports: [LevelUpOverlay, NotifBanner, ToastComponent, Pomodoro, Atalhos, NavProgress, ModuloConclusao],
})
export class SharedModule {}
