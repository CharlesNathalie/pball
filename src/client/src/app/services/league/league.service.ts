import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { League } from 'src/app/models/League.model';

@Injectable({
  providedIn: 'root'
})
export class LeagueService {
  GettingAllLeagues: string[] = ['Getting all leagues', 'Getting all league (fr)'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  getAllLeaguesSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  GetAllLeagues() {
    this.Status = `${this.GettingAllLeagues[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetAllLeagues().subscribe();
  }

  ResetLocals()
  {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{}; 
    this.getAllLeaguesSuccess = false;

  }

  private DoGetAllLeagues() {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/leaguecontact/getallleagues }`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.get<League[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetAllLeagues(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetAllLeagues(e); }))));
  }

  private DoUpdateForGetAllLeagues(leagueList: League[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.getAllLeaguesSuccess = true;

    this.state.LeagueList = leagueList;

    console.debug(leagueList);
  }

  private DoErrorForGetAllLeagues(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.getAllLeaguesSuccess = false;
    console.debug(e);
  }
}
