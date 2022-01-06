import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Contact } from 'src/app/models/Contact.model';
import { AppStateService } from 'src/app/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LogoffService {
  LoggingOff: string[] = ['Logging off', 'Déconnexion'];
  LogoffTxt: string[] = ['Logoff', 'Déconnexion'];

  logoffSuccess: boolean = false;

  private sub: Subscription = new Subscription();
  languageEnum = GetLanguageEnum();

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  Logoff() {
    this.Status = `${this.LoggingOff[this.state.LangID ?? 0]} - ${this.state.Contact.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoLogoff().subscribe();
  }

  ResetLocals()
  {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{}; 
    this.logoffSuccess = false;
  }

  private DoLogoff() {
    const url: string = `${this.state.BaseApiUrl}${this.languageEnum[this.state.Language ?? this.languageEnum.en]}-CA/contact/logoff/${this.state.Contact.ContactID}`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.Contact.Token}`,
      })
    };

    return this.httpClient.get<Contact>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLogoff(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForLogoff(e); }))));
  }

  private DoUpdateForLogoff(success: boolean) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    console.debug(`${this.state.Contact?.LoginEmail} is now logged off`);

    this.state.Contact = <Contact>{};
    this.logoffSuccess = true;

    localStorage.setItem('currentContact', '');
  }

  private DoErrorForLogoff(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.logoffSuccess = false;
    console.debug(e);
  }

}
