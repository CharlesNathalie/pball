import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { LeagueContact } from 'src/app/models/LeagueContact.model';

@Injectable({
  providedIn: 'root'
})
export class LeagueContactService {
  GettingLeagueContacts: string[] = ['Getting league contacts', 'Getting league contacts (fr)'];
  LeagueIDIsRequired: string[] = ['LeagueID is required', 'LeagueID est requis'];
  UserNotLoggedIn: string[] = ['User not logged in', 'L\'utilisateur n\'a aucune connexion'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  getLeagueContactsSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  GetLeagueContacts(leagueID: number) {
    if (this.state.DemoVisible || this.state.User.ContactID == 0) {
      this.Error = new HttpErrorResponse({ error: this.UserNotLoggedIn });
      return;
    }

   this.Status = `${this.GettingLeagueContacts[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetLeagueContacts(leagueID).subscribe();
  }

  GetErrorMessage(fieldName: 'LeagueID', form: FormGroup): string {
    switch (fieldName) {
      case 'LeagueID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LeagueIDIsRequired[this.state.LangID];
          }

          return '';
        }
      default:
        return '';
    }
  }

  GetFormValid(form: FormGroup): boolean {
    return form.valid ? true : false;
  }

  GetHasError(fieldName: 'LeagueID', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals()
  {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{}; 
    this.getLeagueContactsSuccess = false;

  }

  SubmitForm(form: FormGroup) {
    if (form.valid) {
      let LeagueID: number = form.controls['LeagueID'].value;
      this.GetLeagueContacts(LeagueID);
    }
  }

  private DoGetLeagueContacts(leagueID: number) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/leaguecontact/getleaguecontacts/${ leagueID }`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.get<LeagueContact[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetLeagueContacts(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetLeagueContacts(e); }))));
  }

  private DoUpdateForGetLeagueContacts(leagueContactList: LeagueContact[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.getLeagueContactsSuccess = true;

    this.state.LeagueContactList = leagueContactList;

    console.debug(leagueContactList);
  }

  private DoErrorForGetLeagueContacts(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.getLeagueContactsSuccess = false;
    console.debug(e);
  }

}
