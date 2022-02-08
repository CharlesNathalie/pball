import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from '../enums/LanguageEnum';
import { LeagueContact } from '../models/LeagueContact.model';
import { Player } from '../models/Player.model';
import { SortService } from './sort.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueTodayService {
  ChangingLeagueMemberPlayingToday: string[] = ['Changing league member playing today', 'Changement de membre de ligue qui joue aujourd\'hui'];
  NotPlayingToday: string[] = ['Not playing today', 'Ne joue pas aujourd\'hui'];
  PlayingToday: string[] = ['Playing today', 'Joue aujourd\'hui'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  ChangeLeagueMemberPlayingTodaySuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public sortService: SortService) {
  }

  ChangeLeagueMemberPlayingToday(playerID: number) {
    if (this.state.DemoVisible) {
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        if (this.state.LeagueContactList[i].ContactID == playerID) {
          this.state.LeagueContactList[i].PlayingToday = !this.state.LeagueContactList[i].PlayingToday;
        }
      }

      return;
    }
    else {
      this.Status = `${this.ChangingLeagueMemberPlayingToday[this.state.LangID]} - ${this.state.PlayerList.find(c => c.ContactID == playerID)?.LoginEmail ?? ''}`;
      this.Working = true;
      this.Error = <HttpErrorResponse>{};

      this.sub ? this.sub.unsubscribe() : null;
      this.sub = this.DoChangeLeagueMemberPlayingToday(playerID).subscribe();
    }
  }

  private DoChangeLeagueMemberPlayingToday(playerID: number) {
    let languageEnum = GetLanguageEnum();

    let leaguecontact: LeagueContact = <LeagueContact>{};

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/leaguecontact`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      if (this.state.LeagueContactList[i].ContactID == playerID) {
        this.state.LeagueContactList[i].PlayingToday = !this.state.LeagueContactList[i].PlayingToday;
        leaguecontact = this.state.LeagueContactList[i];
      }
    }

    return this.httpClient.put<LeagueContact>(url,
      JSON.stringify(leaguecontact), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForChangeLeagueMemberPlayingToday(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForChangeLeagueMemberPlayingToday(e); }))));
  }

  private DoUpdateForChangeLeagueMemberPlayingToday(leagueContact: LeagueContact) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.ChangeLeagueMemberPlayingTodaySuccess = true;
    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      if (this.state.LeagueContactList[i].ContactID == leagueContact.ContactID) {
        this.state.LeagueContactList[i].PlayingToday = leagueContact.PlayingToday;
        break;
      }
    }

    console.debug(leagueContact);
  }

  private DoErrorForChangeLeagueMemberPlayingToday(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.ChangeLeagueMemberPlayingTodaySuccess = false;
    console.debug(e);
  }

}
