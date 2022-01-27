import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { ContactService } from './contact.service';
import { DataLeagueStatService } from './data-league-stats.service';
import { DataPlayerGamesService } from './data-player-games.service';
import { DataPlayerPointsService } from './data-player-points.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueService {
  Leagues: string[] = ['Leagues', 'Ligues'];
  AddANewLeague: string[] = ['Add a new league', 'Ajoutez une nouvelle ligue'];
  ModifyTheLeague: string[] = ['Modify the league', 'Modifier la ligue'];

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
    public dataPlayerGamesService: DataPlayerGamesService,
    public dataPlayerPointsService: DataPlayerPointsService) {
  }

  SetLeagueID(LeagueID: number) {
    this.state.LeagueID = LeagueID;
    if (this.state.DemoVisible)
    {
      localStorage.setItem('DemoLeagueID', JSON.stringify(this.state.DemoLeagueID));
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
      this.dataPlayerPointsService.Run();
    }

    if (this.state.DemoVisible)
    {
      this.state.CurrentLeague = this.state.LeagueList.filter(c => c.LeagueID == this.state.DemoLeagueID)[0];
    }
    else{
      this.state.CurrentLeague = this.state.LeagueList.filter(c => c.LeagueID == this.state.LeagueID)[0];
    }
  }
}
