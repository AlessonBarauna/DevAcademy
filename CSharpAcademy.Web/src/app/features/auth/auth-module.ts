import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AuthRoutingModule } from './auth-routing-module';
import { Login } from './login/login';
import { Register } from './register/register';
import { Onboarding } from './onboarding/onboarding';

@NgModule({
  declarations: [Login, Register, Onboarding],
  imports: [CommonModule, FormsModule, RouterModule, AuthRoutingModule],
})
export class AuthModule {}
