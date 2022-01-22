import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Player } from 'src/app/models/Player.model';
import { LeagueContactService } from '../league-contact/league-contact.service';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  GettingGames: string[] = ['Getting all contacts for league', 'Getting all contacts for league (fr)'];
  LeagueIDIsRequired: string[] = ['LeagueID is required', 'LeagueID est requis'];
  UserNotLoggedIn: string[] = ['User not logged in', 'L\'utilisateur n\'a aucune connexion'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  GetAllContactsForLeagueSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public leagueContactService: LeagueContactService) {
  }

  GetAllPlayersForLeague() {
    if (this.state.DemoVisible || this.state.User.ContactID == 0) {
      this.Error = new HttpErrorResponse({ error: this.UserNotLoggedIn });
      return;
    }

    this.Status = `${this.GettingGames[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetAllPlayersForLeague().subscribe();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetAllContactsForLeagueSuccess = false;
  }

  private DoGetAllPlayersForLeague() {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/getallplayersforleague/${this.state.LeagueID}`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<Player[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetAllPlayersForLeague(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetAllPlayersForLeague(e); }))));
  }

  private DoUpdateForGetAllPlayersForLeague(playerList: Player[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetAllContactsForLeagueSuccess = true;
    this.state.PlayerList = playerList;
    this.leagueContactService.GetLeaguePlayers();
    console.debug(playerList);
  }

  private DoErrorForGetAllPlayersForLeague(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.GetAllContactsForLeagueSuccess = false;
    this.state.PlayerList = [];
    console.debug(e);
  }
}
