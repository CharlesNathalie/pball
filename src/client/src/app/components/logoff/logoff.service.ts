import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/app-state.service';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User.model';

@Injectable({
  providedIn: 'root'
})
export class LogoffService {
  LoggingOff: string[] = ['Logging off', 'Déconnexion'];
  LogoffTxt: string[] = ['Logoff', 'Déconnexion'];

  LogoffSuccess: boolean = false;

  private sub: Subscription = new Subscription();
  languageEnum = GetLanguageEnum();

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router) {
  }

  Logoff() {
    this.Status = `${this.LoggingOff[this.state.LangID ?? 0]} - ${this.state.User.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.state.ClearDemoLocalStorage();
    this.state.ClearLocalStorage();
    this.state.ClearDemoData();
    this.state.ClearData();


    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoLogoff().subscribe();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LogoffSuccess = false;
  }

  private DoLogoff() {
    const url: string = `${this.state.BaseApiUrl}${this.languageEnum[this.state.Language ?? this.languageEnum.en]}-CA/contact/logoff/${this.state.User.ContactID}`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<boolean>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLogoff(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForLogoff(e); }))));
  }

  private DoUpdateForLogoff(success: boolean) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.state.User = <User>{};
    this.LogoffSuccess = true;
    this.router.navigate([`/${this.state.Culture}/home`]);
    console.debug(`${this.state.User?.LoginEmail} is now logged off`);
  }

  private DoErrorForLogoff(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.LogoffSuccess = false;
    console.debug(e);
  }

}
