import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { League } from 'src/app/models/League.model';
import { LeagueContactAddService } from './league-contact-add.service';
import { LeagueContact } from '../models/LeagueContact.model';
import { HighlightSpanKind } from 'typescript';
import { DemoDataService } from './demo-data.service';
import { ChartGamesPlayedService } from './chart-games-played.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueAddService {
  AddingNewLeague: string[] = ['Adding new league', 'L\'ajout d\'une nouvelle ligue en cour'];
  Cancel: string[] = ['Cancel', 'Annuler'];
  Example: string[] = ['Example', 'Exemple'];
  LeagueIDIsRequired: string[] = ['League ID is required', 'L\'identité de ligue est requis'];
  LeagueAddSuccessful: string[] = ['League added successful', 'L\'ajout de la ligue réussie'];
  LeagueAddTxt: string[] = ['Add league', 'Ajoute une ligue'];
  LeagueName: string[] = ['League name', 'Nom de la ligue'];
  LeagueNameAlreadyExist: string[] = ['League name already exist', 'Nom de la ligue existe déjà'];
  LeagueNameIsRequired: string[] = ['League name is required', 'Nom de la ligue est requis'];
  PercentPointsFactor: string[] = ['Percent points factor', 'Facteur pourcentage points'];
  PercentPointsFactorIsRequired: string[] = ['Percent points factor is required', 'Facteur pourcentage points est requis'];
  PlayerLevelFactor: string[] = ['Player level factor', 'Facteur niveau du joueur'];
  PlayerLevelFactorIsRequired: string[] = ['Player level factor is required', 'Facteur niveau du joueur est requis'];
  PointsToLosers: string[] = ['Points to losers', 'Points aux perdants']
  PointsToLosersIsRequired: string[] = ['Points to losers is required', 'Points aux perdants est requis']
  PointsToWinners: string[] = ['Points to winners', 'Points aux gagnants']
  PointsToWinnersIsRequired: string[] = ['Points to winners is required', 'Points aux gagnants est requis']
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
    public leagueContactAddService: LeagueContactAddService,
    public demoDataService: DemoDataService,
    public chartGamesPlayedService: ChartGamesPlayedService) {
  }

  LeagueAdd(league: League) {
    if (this.state.DemoVisible) {
      for (let i = 0, count = this.state.LeagueList.length; i < count; i++) {
        if (this.state.LeagueList[i].LeagueName == league.LeagueName) {
          this.Error = <HttpErrorResponse>{
            message: this.LeagueNameAlreadyExist[this.state.LangID]
          }
          return;
        }
      }

      let maxLeagueID: number = 0;
      for (let i = 0, count = this.state.LeagueList.length; i < count; i++) {
        if (maxLeagueID < this.state.LeagueList[i].LeagueID) {
          maxLeagueID = this.state.LeagueList[i].LeagueID;
        }
      }

      league.LeagueID = maxLeagueID + 1;
      this.state.LeagueList.push(league);
      this.state.DemoLeagueID = league.LeagueID;

      let leagueContact: LeagueContact = <LeagueContact>{
        LeagueContactID: 1,
        LeagueID: league.LeagueID,
        ContactID: this.state.DemoUser.ContactID,
        IsLeagueAdmin: true,
        Active: true,
        PlayingToday: true,
        Removed: false,
      };

      this.state.LeagueContactList = [];
      this.state.LeagueContactList.push(leagueContact);

      this.state.GameList = [];
      this.state.PlayerGameModelList = [];
      this.state.PlayerList = [];
      this.state.PlayerList.push(this.state.DemoUser)

      this.state.DatePlayerStatModelList = [];
      this.state.CurrentDatePlayerStatModelList = [];
      this.state.CurrentPlayerDateID = 0;

      this.demoDataService.GenerateDemoDataDemoExtraPlayerList();

      this.chartGamesPlayedService.DrawGamesPlayedChart();

      return;
    }
    else {
      this.Status = `${this.AddingNewLeague[this.state.LangID]} - ${league.LeagueName}`;
      this.Working = true;
      this.Error = <HttpErrorResponse>{};

      this.sub ? this.sub.unsubscribe() : null;
      this.sub = this.DoLeagueAdd(league).subscribe();
    }
  }

  GetErrorMessage(fieldName: 'LeagueID' | 'LeagueName' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor', form: FormGroup): string {
    switch (fieldName) {
      case 'LeagueID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LeagueIDIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'LeagueName':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LeagueNameIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'PointsToWinners':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.PointsToWinnersIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'PointsToLosers':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.PointsToLosersIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'PlayerLevelFactor':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.PlayerLevelFactorIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'PercentPointsFactor':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.PercentPointsFactorIsRequired[this.state.LangID];
          }

          return '';
        } default:
        return '';
    }
  }

  GetFormValid(form: FormGroup): boolean {
    return form.valid ? true : false;
  }

  GetHasError(fieldName: 'LeagueID' | 'LeagueName' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueAddSuccess = false;
  }

  SubmitLeagueAddForm(form: FormGroup) {
    if (form.valid) {
      let league: League = <League>{
        LeagueID: form.controls['LeagueID'].value,
        LeagueName: form.controls['LeagueName'].value,
        PointsToWinners: form.controls['PointsToWinners'].value,
        PointsToLosers: form.controls['PointsToLosers'].value,
        PlayerLevelFactor: form.controls['PlayerLevelFactor'].value,
        PercentPointsFactor: form.controls['PercentPointsFactor'].value,
      };
      this.LeagueAdd(league);
    }
  }

  private DoLeagueAdd(league: League) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/league`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
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
    this.state.LeagueID = league.LeagueID;

    let leagueContact: LeagueContact = <LeagueContact>{
      LeagueContactID: 0,
      LeagueID: league.LeagueID,
      ContactID: this.state.DemoVisible ? this.state.DemoUser.ContactID : this.state.User.ContactID,
      IsLeagueAdmin: true,
      Active: true,
      PlayingToday: true
    };
    this.leagueContactAddService.LeagueContactAdd(leagueContact);

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
