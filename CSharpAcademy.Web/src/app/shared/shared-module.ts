import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LevelUpOverlay } from './level-up-overlay/level-up-overlay';
import { NotifBanner } from './notif-banner/notif-banner';

@NgModule({
  declarations: [LevelUpOverlay, NotifBanner],
  imports: [CommonModule],
  exports: [LevelUpOverlay, NotifBanner],
})
export class SharedModule {}
