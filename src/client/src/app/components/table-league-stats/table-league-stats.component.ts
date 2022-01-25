import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { AscDescEnum, GetAscDescEnum } from 'src/app/enums/AscDescEnum';
import { DataLeagueStatService } from 'src/app/services/data-league-stats.service';
import { SortService } from 'src/app/services/sort.service';
import { TableLeagueStatsService } from '../../services/table-league-stats.service';

@Component({
  selector: 'app-table-league-stats',
  templateUrl: './table-league-stats.component.html',
  styleUrls: ['./table-league-stats.component.css']
})
export class TableLeagueStatsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tableLeagueStatsService: TableLeagueStatsService,
    public sortService: SortService,
    public dataLeagueStatService: DataLeagueStatService) {
  }

  ascDescEnum = GetAscDescEnum();
  displayedColumns: string[] = ['Rank', 'FullName', 'NumberOfGames', 'NumberOfWins', 'WinningPercentage'];

  ngOnInit(): void {
    if (this.state.LeagueID > 0) {
      if (this.state.LeagueStatsModelList.length == 0) {
        this.dataLeagueStatService.Run();
      }
    }
  }

  ngOnDestroy(): void {
  }

  DoSortLeagueStats(prop: 'FullName' | 'NumberOfGames' | 'NumberOfWins' | 'WinningPercentage') {
    if (this.state.LeagueStatsModelSortProp == prop) {
      if (this.state.LeagueStatsModelSortAscDesc == AscDescEnum.Ascending) {
        this.state.LeagueStatsModelSortAscDesc = AscDescEnum.Descending;
      }
      else {
        this.state.LeagueStatsModelSortAscDesc = AscDescEnum.Ascending;
      }
    }
    this.state.LeagueStatsModelSortProp = prop;
    this.state.LeagueStatsModelList = this.sortService.SortLeagueStatsModelList(this.state.LeagueStatsModelList);
  }

  GetSortingClass(prop: 'FullName' | 'NumberOfGames' | 'NumberOfWins' | 'WinningPercentage') {
    if (this.state.LeagueStatsModelSortProp == prop) {
      switch (prop) {
        case 'FullName':
          return this.state.LeagueStatsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'NumberOfGames':
          return this.state.LeagueStatsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'NumberOfWins':
          return this.state.LeagueStatsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'WinningPercentage':
          return this.state.LeagueStatsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        default:
          return '';
      }
    }
    else{
      return '';
    }
  }

  GetCurrentLeagueName()
  {
    if (this.state.DemoVisible)
    {
      return this.state.LeagueList.filter(c => c.LeagueID == this.state.DemoLeagueID)[0].LeagueName;
    }

    return this.state.LeagueList.filter(c => c.LeagueID == this.state.LeagueID)[0].LeagueName;
  }
}
