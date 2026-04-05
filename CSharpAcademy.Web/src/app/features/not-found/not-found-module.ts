import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NotFound } from './not-found';

@NgModule({
  declarations: [NotFound],
  imports: [
    CommonModule,
    RouterModule.forChild([{ path: '', component: NotFound }]),
  ],
})
export class NotFoundModule {}
