import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LevelUpOverlay } from './level-up-overlay/level-up-overlay';
import { NotifBanner } from './notif-banner/notif-banner';
import { ToastComponent } from './toast/toast';

@NgModule({
  declarations: [LevelUpOverlay, NotifBanner, ToastComponent],
  imports: [CommonModule],
  exports: [LevelUpOverlay, NotifBanner, ToastComponent],
})
export class SharedModule {}
