import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GlossarioPage } from './glossario-page/glossario-page';

@NgModule({
  declarations: [GlossarioPage],
  imports: [CommonModule, RouterModule, FormsModule],
})
export class GlossarioModule {}
