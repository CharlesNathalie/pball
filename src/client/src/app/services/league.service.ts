import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { ContactService } from './contact.service';
import { DataLeagueStatService } from './data-league-stats.service';
import { DataPlayerGamesService } from './data-player-games.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueService {
  Leagues: string[] = ['Leagues', 'Ligues'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  LeagueSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router,
    public contactService: ContactService,
    public dataLeagueStatService: DataLeagueStatService,
    public dataPlayerGamesService: DataPlayerGamesService) {
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
      this.dataPlayerGamesService.Run();
    }
  }
}
