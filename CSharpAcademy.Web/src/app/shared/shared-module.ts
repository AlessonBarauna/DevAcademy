import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LevelUpOverlay } from './level-up-overlay/level-up-overlay';

@NgModule({
  declarations: [LevelUpOverlay],
  imports: [CommonModule],
  exports: [LevelUpOverlay],
})
export class SharedModule {}
