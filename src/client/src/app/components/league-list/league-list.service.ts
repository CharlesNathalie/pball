import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/app-state.service';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User.model';
import { League } from 'src/app/models/League.model';

@Injectable({
  providedIn: 'root'
})
export class LeagueListService {
  Leagues: string[] = ['Leagues', 'Leagues'];
  GettingLeagues: string[] = ['Getting leagues', 'Obtenir des ligues']

  LeagueListSuccess: boolean = false;

  private sub: Subscription = new Subscription();
  languageEnum = GetLanguageEnum();

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  leagueList: League[] = [];

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router) {
  }

  LeagueList() {
    this.Status = `${this.GettingLeagues[this.state.LangID ?? 0]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoLeagueList().subscribe();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueListSuccess = false;
  }

  private DoLeagueList() {
    const url: string = `${this.state.BaseApiUrl}${this.languageEnum[this.state.Language ?? this.languageEnum.en]}-CA/league/getall`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<League[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForLeagueList(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForLeagueList(e); }))));
  }

  private DoUpdateForLeagueList(leagueList: League[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.LeagueListSuccess = true;
    this.leagueList = leagueList;
    console.debug(leagueList);
  }

  private DoErrorForLeagueList(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.LeagueListSuccess = false;
    console.debug(e);
  }

}
