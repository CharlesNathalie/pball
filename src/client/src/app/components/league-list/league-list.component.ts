import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { LeagueListService } from './league-list.service';

@Component({
  selector: 'app-league-list',
  templateUrl: './league-list.component.html',
  styleUrls: ['./league-list.component.css']
})
export class LeagueListComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public leagueListService: LeagueListService) {
  }

  ngOnInit(): void {
    this.leagueListService.ResetLocals();
    this.leagueListService.LeagueList();
 }

  ngOnDestroy(): void {
  }

  LeagueList() {
    this.leagueListService.LeagueList();
  }
}
