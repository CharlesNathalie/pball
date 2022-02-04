import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from '../enums/LanguageEnum';
import { Player } from '../models/Player.model';

@Injectable({
  providedIn: 'root'
})
export class SearchPlayersService {
 SearchingPlayer: string[] = ['Searching players', 'Recherche de joueurs'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  SearchPlayersSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  SearchPlayers(searchTerms: string) {
    this.state.SearchPlayerList = [];

    if (this.state.DemoVisible) {
      let termList: string[] = searchTerms.split(' ');
      for (let i = 0, count = this.state.DemoExtraPlayerList.length; i < count; i++) {
        if (termList.length == 1)
        {
          let a = this.state.DemoExtraPlayerList[i].FirstName.search(termList[0]);
          
          if (this.state.DemoExtraPlayerList[i].FirstName.search(termList[0]) > -1
          || this.state.DemoExtraPlayerList[i].LastName.search(termList[0]) > -1
          || this.state.DemoExtraPlayerList[i].LoginEmail.search(termList[0]) > -1)
          {
            this.state.SearchPlayerList.push(this.state.DemoExtraPlayerList[i]);
          }
        }

        if (termList.length == 2)
        {
          if ((this.state.DemoExtraPlayerList[i].FirstName.search(termList[0]) > -1
          || this.state.DemoExtraPlayerList[i].LastName.search(termList[0]) > -1
          || this.state.DemoExtraPlayerList[i].LoginEmail.search(termList[0]) > -1)
          && (this.state.DemoExtraPlayerList[i].FirstName.search(termList[1]) > -1
          || this.state.DemoExtraPlayerList[i].LastName.search(termList[1]) > -1
          || this.state.DemoExtraPlayerList[i].LoginEmail.search(termList[1]) > -1))
          {
            this.state.SearchPlayerList.push(this.state.DemoExtraPlayerList[i]);
          }
        }

        if (this.state.SearchPlayerList.length > 9) {
          break;
        }
      }
        
      return;
    }
    else{
      this.Status = `${this.SearchingPlayer[this.state.LangID]}`;
      this.Working = true;
      this.Error = <HttpErrorResponse>{};

      this.sub ? this.sub.unsubscribe() : null;
      this.sub = this.DoSearchPlayer(searchTerms).subscribe();
      }
  }

  private DoSearchPlayer(searchTerms: string) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/searchcontacts/${ this.state.LeagueID }/${ searchTerms }`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<Player[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForSearchPlayers(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForSearchPlayers(e); }))));
  }

  private DoUpdateForSearchPlayers(playerList: Player[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.SearchPlayersSuccess = true;
    for (let i = 0, count = playerList.length; i < count; i++) {
        this.state.SearchPlayerList.push(playerList[i]);
    }

    console.debug(playerList);
  }

  private DoErrorForSearchPlayers(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.SearchPlayersSuccess = false;
    console.debug(e);
  }

}
