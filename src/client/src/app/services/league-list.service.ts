import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { League } from 'src/app/models/League.model';
import { ContactService } from 'src/app/services/contact.service';
import { DataLeagueStatService } from 'src/app/services/data-league-stats.service';
import { DataPlayerGamesService } from 'src/app/services/data-player-games.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueListService {
  Leagues: string[] = ['Leagues', 'Leagues'];
  GettingLeagues: string[] = ['Getting leagues', 'Obtenir des ligues']

  LeagueListSuccess: boolean = false;
  Skip: number = 0;
  Take: number = 100;

  private sub: Subscription = new Subscription();
  languageEnum = GetLanguageEnum();

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  leagueList: League[] = [];

  constructor(public state: AppStateService,
    public contactService: ContactService,
    public dataLeagueStatService: DataLeagueStatService,
    public dataPlayerGameService: DataPlayerGamesService,
    public httpClient: HttpClient,
    public router: Router) {
  }

  GetLeagueList() {
    this.Status = `${this.GettingLeagues[this.state.LangID ?? 0]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetLeagueList().subscribe();
  }

  Next() {
    this.Skip = this.Skip + this.Take;
    this.GetLeagueList();
  }

  Previous() {
    this.Skip = this.Skip - this.Take;
    this.GetLeagueList();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueListSuccess = false;
  }

  SetLeagueID(LeagueID: number) {
    this.state.LeagueID = LeagueID;
    if (this.state.DemoVisible)
    {
      localStorage.setItem('DemoLeagueID', JSON.stringify(this.state.LeagueID));
    }
    else{
      localStorage.setItem('LeagueID', JSON.stringify(this.state.LeagueID));
    }

    if (!this.state.DemoVisible) {
      this.contactService.GetAllPlayersForLeague();
    }
    else {
      this.dataLeagueStatService.Run();
      this.dataPlayerGameService.Run();
    }
  }
  private DoGetLeagueList() {
    const url: string = `${this.state.BaseApiUrl}${this.languageEnum[this.state.Language ?? this.languageEnum.en]}-CA/league/getleaguelist/${this.Skip}/${this.Take}`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<League[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetLeagueList(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetLeagueList(e); }))));
  }

  private DoUpdateForGetLeagueList(leagueList: League[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueListSuccess = true;
    this.leagueList = leagueList;
    console.debug(leagueList);
  }

  private DoErrorForGetLeagueList(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.LeagueListSuccess = false;
    console.debug(e);
  }

}
