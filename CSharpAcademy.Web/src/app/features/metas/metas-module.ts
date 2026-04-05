import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MetasPage } from './metas-page/metas-page';

@NgModule({
  declarations: [MetasPage],
  imports: [CommonModule, RouterModule, FormsModule],
})
export class MetasModule {}
