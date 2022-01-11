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
  Cant_access_your_account: string[] = ['Can\'t access your account?', 'Dans l\'impossibilité d\'accéder à votre compte'];
  CheckingEmailExist: string[] = ['Checking email exist', 'Vérification si courriel existe'];
  InvalidLoginEmail: string[] = ['Invalid login email', 'Courriel de connexion est invalide'];
  LoginEmail: string[] = ['Login email', 'Courriel de connexion'];
  LoginSuccessful: string[] = ['Login successful', 'Connexion réussie'];
  LoginEmailIsRequired: string[] = ['Login email is required', 'Courriel de connexion est requis'];
  Password: string[] = ['Password', 'Mot de passe'];
  LoggingIn: string[] = ['Logging in', 'Connexion en cours'];
  LoginTxt: string[] = ['Login', 'Connexion'];
  No_account: string[] = ['No account?', 'Pas de compte?']
  PleaseEnterRequiredInformation: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  required: string[] = ['required', 'requis'];
  ReturnToHomePage: string[] = ['Return to home page', 'Retour à la page d\'accueil'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  loginSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  Login(loginModel: LoginModel) {
    this.Status = `${this.LoggingIn[this.state.LangID]} - ${loginModel.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoLogin(loginModel).subscribe();
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'Password', form: FormGroup): string {
    switch (fieldName) {
      case 'LoginEmail':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LoginEmailIsRequired[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('email')) {
            return this.InvalidLoginEmail[this.state.LangID];
          }

          return '';
        }
      case 'Password':
        {
          return '';
        }
      default:
        return '';
    }
  }

  GetFormValid(form: FormGroup): boolean {
    return form.valid ? true : false;
  }

  GetHasError(fieldName: 'LoginEmail' | 'Password', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals()
  {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{}; 
    this.loginSuccess = false;
  }

  SubmitForm(form: FormGroup) {
    if (form.valid) {
      let loginModel: LoginModel = <LoginModel>{ LoginEmail: form.controls['LoginEmail'].value, Password: form.controls['Password'].value };
      this.Login(loginModel);
    }
  }

  private DoLogin(loginModel: LoginModel) {
    let languageEnum = GetLanguageEnum();

    localStorage.setItem('currentContact', '');
    this.state.Contact = <Contact>{};

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/login`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<Contact>(url,
      JSON.stringify(loginModel), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLogin(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForLogin(e); }))));
  }

  private DoUpdateForLogin(contact: Contact) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    console.debug(contact);

    this.loginSuccess = true;
    this.state.Contact = contact;

    localStorage.setItem('currentContact', JSON.stringify(contact));
  }

  private DoErrorForLogin(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.loginSuccess = false;
    console.debug(e);
  }
}
