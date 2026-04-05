import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Onboarding } from './onboarding/onboarding';

const routes: Routes = [
  { path: '', component: Onboarding },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
