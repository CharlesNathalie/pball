import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Contact } from 'src/app/models/Contact.model';
import { LoginModel } from 'src/app/models/LoginModel.model';
import { AppStateService } from 'src/app/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  LoginTxt: string[] = ['Login', 'Login (fr)'];
  LoggingIn: string[] = ['Logging in', 'Logging in (fr)'];

  private sub: Subscription = new Subscription();
  private loginModel: LoginModel = <LoginModel>{};

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  Login(loginModel: LoginModel) {
    this.loginModel = loginModel;

    this.sub ? this.sub.unsubscribe() : null;

    this.sub = this.DoLogin().subscribe();
  }

  getErrorMessage(fieldName: string, registerForm: FormGroup): string {
    switch (fieldName) {
      case "LoginEmail":
        {
          if (registerForm.controls[fieldName].hasError('required')) {
            return 'You must enter a valueaaaaaaaaaaa';
          }

          return registerForm.controls[fieldName].hasError('email') ? 'Not a valid email' : '';
        }
      default:
        return '';
    }
  }

  getFormValid(registerForm: FormGroup): boolean {
    return registerForm.valid ? true : false;
  }

  getHasError(fieldName: string, registerForm: FormGroup): boolean {
    return this.getErrorMessage(fieldName, registerForm) == '' ? false : true;
  }

  submitForm(registerForm: FormGroup): boolean {
    if (registerForm.valid) {
      return true;
    }
    return false;
  }
  
  private DoLogin() {
    let languageEnum = GetLanguageEnum();

    this.state.Status = `${this.LoggingIn[this.state.LangID]} - ${this.loginModel.LoginEmail}`;
    this.state.Working = true;

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/login`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<Contact>(url,
      JSON.stringify(this.loginModel), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLogin(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoError(e); }))));
  }

  private DoUpdateForLogin(contact: Contact) {
    this.state.Status = '';
    this.state.Working = false;
    console.debug(contact);

    this.state.Contact = contact;

    localStorage.setItem('currentContact', JSON.stringify(contact));
  }

  private DoError(e: HttpErrorResponse) {
    this.state.Status = '';
    this.state.Working = false;
    this.state.Error = <HttpErrorResponse>e;
    console.debug(e);
  }
}
