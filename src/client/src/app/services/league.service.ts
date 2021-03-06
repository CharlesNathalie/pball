import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { ContactService } from './contact.service';
import { DataPlayerGamesService } from './data-player-games.service';
import { DataDatePlayerStatService } from './data-date-player-stat.service';
import { ChartAllService } from './chart-all.service';

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
    public dataPlayerGamesService: DataPlayerGamesService,
    public dataDatePlayerStatService: DataDatePlayerStatService,
    public chartAAllService: ChartAllService) {
  }

  SetLeagueID(LeagueID: number) {
    if (this.state.DemoVisible) {
      this.state.DemoLeagueID = LeagueID;
      localStorage.setItem('DemoLeagueID', JSON.stringify(this.state.DemoLeagueID));
      this.dataPlayerGamesService.Run();
      this.dataDatePlayerStatService.Run();
      this.state.CurrentLeague = this.state.LeagueList.filter(c => c.LeagueID == this.state.DemoLeagueID)[0];
    }
    else {
      this.state.LeagueID = LeagueID;
      localStorage.setItem('LeagueID', JSON.stringify(this.state.LeagueID));
      this.contactService.GetAllPlayersForLeague();
      this.state.CurrentLeague = this.state.LeagueList.filter(c => c.LeagueID == this.state.LeagueID)[0];
    }

    this.chartAAllService.RedrawDrawAllCharts();
  }
}
