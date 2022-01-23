import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/app-state.service';
import { Router } from '@angular/router';
import { LeagueService } from 'src/app/services/league/league.service';
import { User } from 'src/app/models/User.model';
import { League } from 'src/app/models/League.model';

@Injectable({
  providedIn: 'root'
})
export class LeagueAddService {
  Cancel: string[] = ['Cancel', 'Annuler'];
  LeagueName: string[] = ['League name', 'Nom de la ligue'];
  LeagueAddSuccessful: string[] = ['League added successful', 'L\'ajout de la ligue réussie'];
  LeagueNameIsRequired: string[] = ['League name is required', 'Nom de la ligue est requis'];
  AddingNewLeague: string[] = ['Adding new league', 'L\'ajout d\'une nouvelle ligue en cour'];
  LeagueAddTxt: string[] = ['Add league', 'Ajoute une ligue'];
  // No_account: string[] = ['No account?', 'Pas de compte?']
  PleaseEnterRequiredInformation: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  required: string[] = ['required', 'requis'];
  ReturnToHomePage: string[] = ['Return to home page', 'Retour à la page d\'accueil'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  LeagueAddSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router,
    public leagueService: LeagueService) {
  }

  LeagueAdd(league: League) {

    this.Status = `${this.AddingNewLeague[this.state.LangID]} - ${league.LeagueName}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoLeagueAdd(league).subscribe();
  }

  GetErrorMessage(fieldName: 'LeagueName', form: FormGroup): string {
    switch (fieldName) {
      case 'LeagueName':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LeagueNameIsRequired[this.state.LangID];
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

  GetHasError(fieldName: 'LeagueName', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueAddSuccess = false;
  }

  SubmitForm(form: FormGroup) {
    if (form.valid) {
      let league: League = <League>{ LeagueName: form.controls['LeagueName'].value };
      this.LeagueAdd(league);
    }
  }

  private DoLeagueAdd(league: League) {
    let languageEnum = GetLanguageEnum();

    this.state.User = <User>{};
    this.state.ClearDemoData();
    this.state.ClearData();
    this.state.ClearDemoLocalStorage();
    this.state.ClearLocalStorage();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/league`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<League>(url,
      JSON.stringify(league), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLeagueAdd(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForLeagueAdd(e); }))));
  }

  private DoUpdateForLeagueAdd(league: League) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueAddSuccess = true;

    console.debug(league);
  }

  private DoErrorForLeagueAdd(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.LeagueAddSuccess = false;
    console.debug(e);
  }
}
