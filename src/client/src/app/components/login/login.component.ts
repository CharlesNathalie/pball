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
    LoginEmail: ['', Validators.required],
    Password: ['', Validators.required],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public loginService: LoginService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  Login() {
    let loginModel: LoginModel = <LoginModel>{ LoginEmail: '3785a@gmail.com', Password: '494a' };
    this.loginService.Login(loginModel);
  }

  LoginError() {
    let loginModel: LoginModel = <LoginModel>{ LoginEmail: '3785a@gmail.com', Password: '494aaaa' };
    this.loginService.Login(loginModel);
  }

  getHasError(fieldName: string): boolean {
    return this.loginService.getHasError(fieldName, this.loginForm);
  }

  getErrorMessage(fieldName: string): string {
    return this.loginService.getErrorMessage(fieldName, this.loginForm);
  }

  getFormValid(): boolean {
    return this.loginService.getFormValid(this.loginForm);
  }

  onSubmit(): void {
    this.loginService.submitForm(this.loginForm);
    }
}
