import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { League } from 'src/app/models/League.model';
import { LeagueContact } from '../models/LeagueContact.model';
import { GetPlayerLeaguesService } from './get-player-leagues.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueContactAddService {
AddingNewLeaguePlayer: string[] = ['Adding new league player', 'L\'ajout d\'un nouveau joueur à la ligue']
LeagueContactIDIsRequired: string[] = ['LeagueContactID is required', 'LeagueContactID est requis'];
LeagueIDIsRequired: string[] = ['LeagueID is required', 'LeagueID est requis'];
ContactIDIsRequired: string[] = ['ContactID is required', 'ContactID est requis'];
IsLeagueAdminIsRequired: string[] = ['IsLeagueAdmin is required', 'IsLeagueAdmin est requis'];
ActiveIsRequired: string[] = ['Active is required', 'Active est requis'];
PlayingTodayIsRequired: string[] = ['PlayingToday is required', 'PlayingToday est requis'];

Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  LeagueContactAddSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router,
    public getPlayerLeaguesService: GetPlayerLeaguesService) {
  }

  LeagueContactAdd(leagueContact: LeagueContact) {

    this.Status = `${this.AddingNewLeaguePlayer[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoLeagueContactAdd(leagueContact).subscribe();
  }

  GetErrorMessage(fieldName: 'LeagueContactID' | 'LeagueID' | 'ContactID' | 'IsLeagueAdmin' | 'Active' | 'PlayingToday', form: FormGroup): string {
    switch (fieldName) {
      case 'LeagueContactID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LeagueContactIDIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'LeagueID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LeagueIDIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'ContactID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.ContactIDIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'IsLeagueAdmin':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.IsLeagueAdminIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'Active':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.ActiveIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'PlayingToday':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.PlayingTodayIsRequired[this.state.LangID];
          }

          return '';
        } default:
        return '';
    }
  }

  GetFormValid(form: FormGroup): boolean {
    return form.valid ? true : false;
  }

  GetHasError(fieldName: 'LeagueContactID' | 'LeagueID' | 'ContactID' | 'IsLeagueAdmin' | 'Active' | 'PlayingToday', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueContactAddSuccess = false;
  }

  SubmitLeagueContactAddForm(form: FormGroup) {
    if (form.valid) {
      let leagueContact: LeagueContact = <LeagueContact>{
        LeagueContactID: form.controls['LeagueContactID'].value,
        LeagueID: form.controls['LeagueID'].value,
        ContactID: form.controls['ContactID'].value,
        IsLeagueAdmin: form.controls['IsLeagueAdmin'].value,
        Active: form.controls['Active'].value,
        PlayingToday: form.controls['PlayingToday'].value
      };
      this.LeagueContactAdd(leagueContact);
    }
  }

  private DoLeagueContactAdd(leagueContact: LeagueContact) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/leaguecontact`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.post<League>(url,
      JSON.stringify(leagueContact), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLeagueContactAdd(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForLeagueContactAdd(e); }))));
  }

  private DoUpdateForLeagueContactAdd(leagueContact: LeagueContact) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueContactAddSuccess = true;
    this.getPlayerLeaguesService.Run();
    console.debug(leagueContact);
  }

  private DoErrorForLeagueContactAdd(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.LeagueContactAddSuccess = false;
    console.debug(e);
  }
}
