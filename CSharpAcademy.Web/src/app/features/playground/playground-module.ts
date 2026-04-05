import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PlaygroundPage } from './playground-page/playground-page';

@NgModule({
  declarations: [PlaygroundPage],
  imports: [CommonModule, FormsModule, RouterModule],
})
export class PlaygroundModule {}
