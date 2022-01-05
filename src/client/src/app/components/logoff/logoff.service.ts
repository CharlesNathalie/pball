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
  LoggingOff: string[] = ['Logging off', 'Logging off (fr)'];
  Logoff: string[] = ['Logoff', 'Logoff (fr)'];

  private sub: Subscription = new Subscription();
  languageEnum = GetLanguageEnum();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  LogOff() {
    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoLogoff().subscribe();
  }

  private DoLogoff() {
    this.state.Status = `${this.LoggingOff[this.state.LangID ?? 0]} - ${this.state.Contact.LoginEmail}`;
    this.state.Working = true;

    const url: string = `${this.state.BaseApiUrl}${this.languageEnum[this.state.Language ?? this.languageEnum.en]}-CA/contact/logoff/${this.state.Contact.ContactID}`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.state.Contact.Token,
      })
    };

    return this.httpClient.get<Contact>(url)
      .pipe(map((x: any) => { this.DoUpdateForLogoff(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoError(e); }))));
  }

  private DoUpdateForLogoff(success: boolean) {
    this.state.Status = '';
    this.state.Working = false;
    console.debug(`${this.state.Contact?.LoginEmail} is now logged off`);

    this.state.Contact = <Contact>{};

    localStorage.setItem('currentContact', '');
  }

  private DoError(e: HttpErrorResponse) {
    this.state.Status = '';
    this.state.Working = false;
    this.state.Error = <HttpErrorResponse>e;
    console.debug(e);
  }

}
