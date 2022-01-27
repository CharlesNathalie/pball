import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueTabsService } from '../../services/league-tabs.service';

@Component({
  selector: 'app-league-tabs',
  templateUrl: './league-tabs.component.html',
  styleUrls: ['./league-tabs.component.css']
})
export class LeagueTabsComponent implements OnInit, OnDestroy {


  constructor(public state: AppStateService,
    public leagueTabsService: LeagueTabsService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  LeagueTabSelectedChanged(event: any) {
    this.state.LeagueTabsIndex = event.index;
    localStorage.setItem('LeagueTabsIndex', JSON.stringify(this.state.LeagueTabsIndex));
  }

}
