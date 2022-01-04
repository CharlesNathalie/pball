import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Contact } from 'src/app/models/Contact.model';
import { LoginModel } from 'src/app/models/LoginModel.model';
import { AppLanguageService } from '../app/app-language.service';
import { AppLoadedService } from '../app/app-loaded.service';
import { AppStateService } from '../app/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private sub: Subscription = new Subscription();
  private loginModel: LoginModel = <LoginModel>{};

  constructor(private appLanguageService: AppLanguageService,
    private appStateService: AppStateService,
    private appLoadedService: AppLoadedService,
    private httpClient: HttpClient) {
  }

  Login(loginModel: LoginModel) {
    this.loginModel = loginModel;

    this.sub ? this.sub.unsubscribe() : null;

    this.sub = this.DoLogin().subscribe();
  }

  private DoLogin() {
    let languageEnum = GetLanguageEnum();

    this.appStateService.setStatus(`${this.appLanguageService.LoggingIn[this.appLanguageService.LangID ?? 0]} - ${this.loginModel.LoginEmail}`);
    this.appStateService.setWorking(true);

    const url: string = `${this.appLoadedService.BaseApiUrl}${languageEnum[this.appLanguageService.Language ?? languageEnum.en]}-CA/contact/login`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<Contact>(url, JSON.stringify(this.loginModel), httpOptions).pipe(map((x: any) => { this.DoUpdateForLogin(x); }), catchError(e => of(e).pipe(map(e => { this.DoError(e); }))));
  }

  private DoUpdateForLogin(contact: Contact) {
    let languageEnum = GetLanguageEnum();

    this.appStateService.setStatus('');
    this.appStateService.setWorking(false);
    console.debug(contact);

    this.appStateService.setContact(contact);
  }

  private DoError(e: HttpErrorResponse) {
    this.appStateService.setStatus('');
    this.appStateService.setWorking(false);
    this.appStateService.setError(<HttpErrorResponse>e);
    console.debug(e);
  }

}
