import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { LoginModel } from 'src/app/models/LoginModel.model';
import { AppStateService } from 'src/app/app-state.service';
import { LoginService } from './login.service';

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
    public loginService: LoginService) {
  }

  ngOnInit(): void {
    this.loginService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  Login() {
    let loginModel: LoginModel = <LoginModel>{ LoginEmail: '9741a@gmail.com', Password: '2445a' };
    this.loginService.Login(loginModel);
  }

  LoginError() {
    let loginModel: LoginModel = <LoginModel>{ LoginEmail: '9741a@gmail.com', Password: '2445aaaa' };
    this.loginService.Login(loginModel);
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

  OnSubmit(): void {
    this.loginService.SubmitForm(this.loginForm);
  }
}
