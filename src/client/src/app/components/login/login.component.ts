import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { LoginModel } from 'src/app/models/LoginModel.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  loginForm = this.formBuilder.group({
    LoginEmail: ['', [Validators.required, Validators.email]],
    Password: ['', [Validators.required]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public loginService: LoginService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.loginService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LoginEmail' | 'Password'): boolean {
    return this.loginService.GetHasError(fieldName, this.loginForm);
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'Password'): string {
    return this.loginService.GetErrorMessage(fieldName, this.loginForm);
  }

  GetFormValid(): boolean {
    return this.loginService.GetFormValid(this.loginForm);
  }

  SubmitLoginForm(): void {
    this.loginService.SubmitLoginForm(this.loginForm);
  }
}
