import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { Game } from '../models/Game.model';
import { DataHelperService } from 'src/app/services/data-helper.service';
import { DemoDataService } from './demo-data.service';
@Injectable({
  providedIn: 'root'
})
export class GameAddService {
  AddingNewGame: string[] = ['Adding new game', 'L\'ajout d\'une nouvelle partie en cour'];
  Cancel: string[] = ['Cancel', 'Annuler'];
  Example: string[] = ['Example', 'Exemple'];
  GameAddSuccessful: string[] = ['Game added successful', 'L\'ajout de la partie réussie'];
  GameAddTxt: string[] = ['Add game', 'Ajoute une partie'];
  GameIDIsRequired: string[] = ['Game ID is required', 'L\'identité de la partie est requis'];
  LeagueIDIsRequired: string[] = ['League ID is required', 'L\'identité de ligue est requis'];
  PleaseEnterRequiredInformation: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  required: string[] = ['required', 'requis'];
  ReturnToHomePage: string[] = ['Return to home page', 'Retour à la page d\'accueil'];
  Team1Player1: string[] = ['Team 1 player 1', 'Équipe 1 joueur 1'];
  Team1Player1IsRequired: string[] = ['Team 1 player 1 is required', 'Équipe 1 joueur 1 est requis'];
  Team1Player2: string[] = ['Team 1 player 2', 'Équipe 1 joueur 2'];
  Team1Player2IsRequired: string[] = ['Team 1 player 2 is required', 'Équipe 1 joueur 2 est requis'];
  Team2Player1: string[] = ['Team 2 player 1', 'Équipe 2 joueur 1'];
  Team2Player1IsRequired: string[] = ['Team 2 player 1 is required', 'Équipe 2 joueur 1 est requis'];
  Team2Player2: string[] = ['Team 2 player 2', 'Équipe 2 joueur 2'];
  Team2Player2IsRequired: string[] = ['Team 2 player 2 is required', 'Équipe 2 joueur 2 est requis'];
  Team1Scores: string[] = ['Team 1 scores', 'Équipe 1 pointage'];
  Team1ScoresIsRequired: string[] = ['Team 1 scores is required', 'Équipe 1 pointage est requis'];
  Team2Scores: string[] = ['Team 2 scores', 'Équipe 2 pointage'];
  Team2ScoresIsRequired: string[] = ['Team 2 scores is required', 'Équipe 2 pointage est requis'];
  GameDate: string[] = ['Game date', 'Date de la partie'];
  GameDateIsRequired: string[] = ['Game date is required', 'Date de la partie est requise'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  GameAddSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router,
    public dataHelperService: DataHelperService,
    public demoDataService: DemoDataService) {
  }

  GameAdd(game: Game) {
    if (this.state.DemoVisible) {

      let maxGameID: number = 0;
      for (let i = 0, count = this.state.GameList.length; i < count; i++) {
        if (maxGameID < this.state.GameList[i].GameID) {
          maxGameID = this.state.GameList[i].GameID;
        }
      }

      game.GameID = maxGameID + 1;

      this.state.GameList.push(game);

      return;
    }
    else {
      this.Status = `${this.AddingNewGame[this.state.LangID]} - ${this.dataHelperService.GetPlayerFullName(game.Team1Player1)}`;
      this.Working = true;
      this.Error = <HttpErrorResponse>{};

      this.sub ? this.sub.unsubscribe() : null;
      this.sub = this.DoGameAdd(game).subscribe();
    }
  }

  GetErrorMessage(fieldName: 'GameID' | 'LeagueID' | 'Team1Player1' | 'Team1Player2' | 'Team2Player1' | 'Team2Player2' | 'Team1Scores' | 'Team2Scores' | 'GameDate', form: FormGroup): string {
    switch (fieldName) {
      case 'GameID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.GameIDIsRequired[this.state.LangID];
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
      case 'Team1Player1':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.Team1Player1IsRequired[this.state.LangID];
          }

          return '';
        }
      case 'Team1Player2':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.Team1Player2IsRequired[this.state.LangID];
          }

          return '';
        }
      case 'Team2Player1':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.Team2Player1IsRequired[this.state.LangID];
          }

          return '';
        }
      case 'Team2Player2':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.Team2Player2IsRequired[this.state.LangID];
          }

          return '';
        }
      case 'Team1Scores':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.Team1ScoresIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'Team2Scores':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.Team2ScoresIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'GameDate':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.GameDateIsRequired[this.state.LangID];
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

  GetHasError(fieldName: 'GameID' | 'LeagueID' | 'Team1Player1' | 'Team1Player2' | 'Team2Player1' | 'Team2Player2' | 'Team1Scores' | 'Team2Scores' | 'GameDate', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GameAddSuccess = false;
  }

  SubmitGameAddForm(form: FormGroup) {
    if (form.valid) {
      let game: Game = <Game>{
        GameID: form.controls['GameID'].value,
        LeagueID: form.controls['LeagueID'].value,
        Team1Player1: form.controls['Team1Player1'].value,
        Team1Player2: form.controls['Team1Player2'].value,
        Team2Player1: form.controls['Team2Player1'].value,
        Team2Player2: form.controls['Team2Player2'].value,
        Team1Scores: form.controls['Team1Scores'].value,
        Team2Scores: form.controls['Team2Scores'].value,
        GameDate: form.controls['GameDate'].value,
      };
      this.GameAdd(game);
    }
  }

  private DoGameAdd(game: Game) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/game`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.post<Game>(url,
      JSON.stringify(game), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGameAdd(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGameAdd(e); }))));
  }

  private DoUpdateForGameAdd(game: Game) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GameAddSuccess = true;
    this.state.GameList.push(game);

    console.debug(game);
  }

  private DoErrorForGameAdd(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.GameAddSuccess = false;
    console.debug(e);
  }
}
