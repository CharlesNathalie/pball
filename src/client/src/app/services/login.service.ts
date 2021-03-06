import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { LoginModel } from 'src/app/models/LoginModel.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { GetPlayerLeaguesService } from 'src/app/services/get-player-leagues.service';
import { User } from 'src/app/models/User.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  GotACode: string[] = ['Got a code', 'Reçu un code'];
  ChangeYourPasswordWithCode: string[] = ['Change your password with code', 'Changer votre mot de passe avec code'];
  CheckingEmailExist: string[] = ['Checking email exist', 'Vérification si courriel existe'];
  ForgotYourPassword: string[] = ['Forgot your password?', 'Oubliez votre mot de passe'];
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
  ReturnHome: string[] = ['Return home', 'Retour à la page d\'accueil'];
  ReturnToRegister: string[] = ['Return to register', 'Retour à s\'inscrire'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  LoginSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router,
    public getPlayerLeaguesService: GetPlayerLeaguesService) {
  }

  Login(loginModel: LoginModel) {

    this.Status = `${this.LoggingIn[this.state.LangID]} - ${loginModel.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    localStorage.setItem('User', '');
    localStorage.setItem('DemoUser', '');
    this.state.ClearDemoData();
    this.state.ClearData();

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

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LoginSuccess = false;
  }

  SubmitLoginForm(form: FormGroup) {
    if (form.valid) {
      let loginModel: LoginModel = <LoginModel>{ LoginEmail: form.controls['LoginEmail'].value, Password: form.controls['Password'].value };
      this.Login(loginModel);
    }
  }

  private DoLogin(loginModel: LoginModel) {
    let languageEnum = GetLanguageEnum();

    this.state.User = <User>{};
    this.state.ClearDemoData();
    this.state.ClearData();
    this.state.ClearDemoLocalStorage();
    this.state.ClearLocalStorage();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/login`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<User>(url,
      JSON.stringify(loginModel), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLogin(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForLogin(e); }))));
  }

  private DoUpdateForLogin(user: User) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.state.User = user;
    this.LoginSuccess = true;

    localStorage.setItem('User', JSON.stringify(this.state.User));
    localStorage.setItem('LeagueID', JSON.stringify(this.state.LeagueID));
    localStorage.setItem('StartDate', JSON.stringify(this.state.StartDate));
    localStorage.setItem('EndDate', JSON.stringify(this.state.EndDate));
    localStorage.setItem('DemoVisible', JSON.stringify(this.state.DemoVisible));
    localStorage.setItem('HomeTabIndex', JSON.stringify(this.state.HomeTabIndex));
    localStorage.setItem('LeagueTabsIndex', JSON.stringify(this.state.LeagueTabsIndex));
    localStorage.setItem('ResultsTabsIndex', JSON.stringify(this.state.ResultsTabsIndex));

    this.getPlayerLeaguesService.Run();
    console.debug(user);
    this.router.navigate([`/${this.state.Culture}/home`]);
  }

  private DoErrorForLogin(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.LoginSuccess = false;
    console.debug(e);
  }
}
