import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { LeagueGamesModel } from 'src/app/models/LeagueGamesModel.model';
import { MostGamesPlayedModel } from 'src/app/models/MostGamesPlayedModel';

@Injectable({
  providedIn: 'root'
})
export class TableMostGamesPlayedService {
  AllLeague: string[] = ['All leagues', 'All leagues (fr)'];
  EndDate: string[] = ['End date', 'End date (fr)'];
  EndDateIsRequired: string[] = ['EndDate is required', 'EndDate est requis'];
  GettingMostGamesPlayed: string[] = ['Getting most games played', 'Getting most games played (fr)'];
  LeagueIDIsRequired: string[] = ['LeagueID is required', 'LeagueID est requis'];
  PleaseEnterRequiredInformation: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  Refresh: string[] = ['Refresh', 'Refresh (fr)'];
  required: string[] = ['required', 'requis'];
  StartDate: string[] = ['Start date', 'Start date (fr)'];
  StartDateIsRequired: string[] = ['StartDate is required', 'StartDate est requis'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  getMostGamesPlayedSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  GetMostGamesPlayed(leagueGamesModel: LeagueGamesModel) {
    this.Status = `${this.GettingMostGamesPlayed[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetMostGamesPlayed(leagueGamesModel).subscribe();
  }

  GetErrorMessage(fieldName: 'LeagueID' | 'StartDate' | 'EndDate', form: FormGroup): string {
    switch (fieldName) {
      case 'LeagueID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LeagueIDIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'StartDate':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.StartDateIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'EndDate':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.EndDateIsRequired[this.state.LangID];
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

  GetHasError(fieldName: 'LeagueID' | 'StartDate' | 'EndDate', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.getMostGamesPlayedSuccess = false;

  }

  SubmitForm(form: FormGroup) {
    if (form.valid) {
      let leagueGamesModel: LeagueGamesModel = <LeagueGamesModel>{
        LeagueID: form.controls['LeagueID'].value,
        StartDate: form.controls['StartDate'].value,
        EndDate: form.controls['EndDate'].value,
      };
      this.GetMostGamesPlayed(leagueGamesModel);
    }
  }

  private DoGetMostGamesPlayed(leagueGamesModel: LeagueGamesModel) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/game/getmostgamesplayedbetweendates`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<MostGamesPlayedModel[]>(url,
      JSON.stringify(leagueGamesModel), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetMostGamesPlayed(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetMostGamesPlayed(e); }))));
  }

  private DoUpdateForGetMostGamesPlayed(mostGamesPlayedModelList: MostGamesPlayedModel[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.getMostGamesPlayedSuccess = true;
    this.state.MostGamesPlayedModelList = mostGamesPlayedModelList;
    console.debug(mostGamesPlayedModelList);
  }

  private DoErrorForGetMostGamesPlayed(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.getMostGamesPlayedSuccess = false;
    this.state.MostGamesPlayedModelList = [];
    console.debug(e);
  }

}
