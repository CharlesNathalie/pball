import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Game } from 'src/app/models/Game.model';
import { LeagueGamesModel } from 'src/app/models/LeagueGamesModel.model';
import { DataLeagueStatService } from '../data/data-league-stats.service';
import { DataPlayerGamesService } from '../data/data-player-games.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  GettingGames: string[] = ['Getting all leagues games between dates', 'Getting all leagues games between dates (fr)'];
  LeagueIDIsRequired: string[] = ['LeagueID is required', 'LeagueID est requis'];
  StartDateIsRequired: string[] = ['Start date is required', 'Date de départ est requis'];
  EndDateIsRequired: string[] = ['End date is required', 'Date de fin est requis'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  GetAllLeagueGamesSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public dataLeagueStatService: DataLeagueStatService,
    public dataPlayerGameService: DataPlayerGamesService) {
  }

  GetAllLeagueGames() {
    this.Status = `${this.GettingGames[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetAllLeagueGames().subscribe();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetAllLeagueGamesSuccess = false;

  }

  private DoGetAllLeagueGames() {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/game/getallleaguegames/${this.state.LeagueID}`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<Game[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetAllLeagueGames(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetAllLeagueGames(e); }))));
  }

  private DoUpdateForGetAllLeagueGames(gameList: Game[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetAllLeagueGamesSuccess = true;
    this.state.GameList = gameList;
    this.dataLeagueStatService.Run();
    this.dataPlayerGameService.Run();
    console.debug(gameList);
  }

  private DoErrorForGetAllLeagueGames(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.state.GameList = [];
    this.GetAllLeagueGamesSuccess = false;
    console.debug(e);
  }
}
