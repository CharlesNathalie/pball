import { Component, OnInit, OnDestroy } from '@angular/core';
import { LeagueContact } from 'src/app/models/LeagueContact.model';
import { Player } from 'src/app/models/Player.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueService } from 'src/app/services/league.service';

@Component({
  selector: 'app-league',
  templateUrl: './league.component.html',
  styleUrls: ['./league.component.css']
})
export class LeagueComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public leagueService: LeagueService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  GetLeagueHighlight(LeagueID: number): string {
    if (this.state.DemoVisible) {
      return this.state.DemoLeagueID == LeagueID ? 'highlight' : '';
    }
    else {
      return this.state.LeagueID == LeagueID ? 'highlight' : '';
    }
  }

  SetLeagueID(LeagueID: number) {
    this.leagueService.SetLeagueID(LeagueID);
  }

  GetIsSelected(LeagueID: number): string {
    if (this.state.DemoVisible) {
      return this.state.DemoLeagueID == LeagueID ? 'checked' : '';
    }
    else {
      return this.state.LeagueID == LeagueID ? 'checked' : '';
    }
  }

}
