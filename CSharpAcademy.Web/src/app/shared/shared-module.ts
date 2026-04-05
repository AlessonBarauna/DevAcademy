import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LevelUpOverlay } from './level-up-overlay/level-up-overlay';
import { NotifBanner } from './notif-banner/notif-banner';
import { ToastComponent } from './toast/toast';
import { Pomodoro } from './pomodoro/pomodoro';

@NgModule({
  declarations: [LevelUpOverlay, NotifBanner, ToastComponent, Pomodoro],
  imports: [CommonModule, FormsModule],
  exports: [LevelUpOverlay, NotifBanner, ToastComponent, Pomodoro],
})
export class SharedModule {}
