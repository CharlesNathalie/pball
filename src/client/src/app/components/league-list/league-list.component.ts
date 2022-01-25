import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueListService } from '../../services/league-list.service';

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
    this.leagueListService.GetLeagueList();
 }

  ngOnDestroy(): void {
  }

 
  GetLeagueHighlight(LeagueID: number): string {
    return this.state.LeagueID == LeagueID ? 'highlight' : '';
  }

  SetLeagueID(LeagueID: number) {
    this.leagueListService.SetLeagueID(LeagueID);
  }
}
