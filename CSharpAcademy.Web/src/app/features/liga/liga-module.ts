import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LigaPage } from './liga-page/liga-page';

@NgModule({
  declarations: [LigaPage],
  imports: [CommonModule, RouterModule, HttpClientModule],
})
export class LigaModule {}
