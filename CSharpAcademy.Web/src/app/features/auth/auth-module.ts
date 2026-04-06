import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AuthRoutingModule } from './auth-routing-module';
import { Login } from './login/login';
import { Register } from './register/register';
import { Onboarding } from './onboarding/onboarding';
import { EsqueciSenha } from './esqueci-senha/esqueci-senha';
import { RedefinirSenha } from './redefinir-senha/redefinir-senha';

@NgModule({
  declarations: [Login, Register, Onboarding, EsqueciSenha, RedefinirSenha],
  imports: [CommonModule, FormsModule, RouterModule, AuthRoutingModule],
})
export class AuthModule {}
