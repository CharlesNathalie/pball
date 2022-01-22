import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { LeagueContact } from 'src/app/models/LeagueContact.model';
import { GameService } from '../game/game.service';

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

  GetLeagueContactsSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public gameService: GameService) {
  }

  GetLeaguePlayers() {
    if (this.state.DemoVisible || this.state.User.ContactID == 0) {
      this.Error = new HttpErrorResponse({ error: this.UserNotLoggedIn });
      return;
    }

    this.Status = `${this.GettingLeagueContacts[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetLeaguePlayers().subscribe();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetLeagueContactsSuccess = false;

  }

  private DoGetLeaguePlayers() {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/leaguecontact/getleaguecontacts/${this.state.LeagueID}`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<LeagueContact[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetLeaguePlayers(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetLeaguePlayers(e); }))));
  }

  private DoUpdateForGetLeaguePlayers(leagueContactList: LeagueContact[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetLeagueContactsSuccess = true;
    this.state.LeagueContactList = leagueContactList;
    console.debug(leagueContactList);
    this.gameService.GetAllLeagueGames()
  }

  private DoErrorForGetLeaguePlayers(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.GetLeagueContactsSuccess = false;
    this.state.LeagueContactList = [];
    console.debug(e);
  }

}
