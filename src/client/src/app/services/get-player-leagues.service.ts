import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { League } from 'src/app/models/League.model';
import { ContactService } from './contact.service';

@Injectable({
  providedIn: 'root'
})
export class GetPlayerLeaguesService {
  GettingUserLeagues: string[] = ['Getting user leagues', 'Getting user league (fr)'];
  UserNotLoggedIn: string[] = ['User not logged in', 'L\'utilisateur n\'a aucune connexion'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  GetPlayerLeaguesSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public contactService: ContactService) {
  }

  Run() {
    if (this.state.DemoVisible || this.state.User.ContactID == 0) {
      this.Error = new HttpErrorResponse({ error: this.UserNotLoggedIn });
      return;
    }

    this.Status = `${this.GettingUserLeagues[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoRun().subscribe();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetPlayerLeaguesSuccess = false;
  }

  private DoRun() {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/league/getplayerleagues`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<League[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetPlayerLeagues(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetPlayerLeagues(e); }))));
  }

  private DoUpdateForGetPlayerLeagues(leagueList: League[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetPlayerLeaguesSuccess = true;
    this.state.LeagueList = leagueList;
    if (this.state.LeagueList.length > 0)
    {
      let leagueArr: League[] = this.state.LeagueList.filter(c => c.LeagueID == this.state.LeagueID);
      if (!leagueArr.length)
      {
        this.state.LeagueID = this.state.LeagueList[0].LeagueID;
      }
      this.contactService.GetAllPlayersForLeague()
    }   
    console.debug(leagueList);
  }

  private DoErrorForGetPlayerLeagues(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.state.LeagueList = [];
    this.GetPlayerLeaguesSuccess = false;
    console.debug(e);
  }
}
