import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { AscDescEnum, GetAscDescEnum } from 'src/app/enums/AscDescEnum';
import { SortService } from 'src/app/services/sort.service';
import { TableLeagueStatsService } from '../../services/table-league-stats.service';
import { DataDatePlayerStatService } from 'src/app/services/data-date-player-stat.service';
import { DataHelperService } from 'src/app/services/data-helper.service';

@Component({
  selector: 'app-table-league-stats',
  templateUrl: './table-league-stats.component.html',
  styleUrls: ['./table-league-stats.component.css']
})
export class TableLeagueStatsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tableLeagueStatsService: TableLeagueStatsService,
    public dataHelperService: DataHelperService,
    public sortService: SortService,
    public dataDatePlayerStatService: DataDatePlayerStatService) {
  }

  ascDescEnum = GetAscDescEnum();
  displayedColumns: string[] = ['Rank', 'FullName', 'GamesPlayed', 'GamesWon', 'WinningPercentage', 'Points'];

  ngOnInit(): void {
    if (this.state.DemoVisible) {
      if (this.state.DemoLeagueID > 0) {
        if (this.state.PlayerGameModelList.length == 0) {
          //this.dataPlayerGamesService.Run();
        }
      }
    }
    else {
      if (this.state.LeagueID > 0) {
        if (this.state.PlayerGameModelList.length == 0) {
          //this.dataPlayerGamesService.Run();
        }
      }
    }
  }

  ngOnDestroy(): void {
  }

  DoSortLeagueStats(prop: 'FullName' | 'GamesPlayed' | 'GamesWon' | 'WinningPercentage' | 'Points') {
    if (this.state.PlayerStatModelSortProp == prop) {
      if (this.state.PlayerStatModelSortAscDesc == AscDescEnum.Ascending) {
        this.state.PlayerStatModelSortAscDesc = AscDescEnum.Descending;
      }
      else {
        this.state.PlayerStatModelSortAscDesc = AscDescEnum.Ascending;
      }
    }
    this.state.PlayerStatModelSortProp = prop;
    this.state.CurrentDatePlayerStatModelList = this.sortService.SortPlayerStatModelList(this.state.DatePlayerStatModelList[this.state.CurrentPlayerDateID].PlayerStatModelList);
  }

  GetSortingClass(prop: 'FullName' | 'GamesPlayed' | 'GamesWon' | 'WinningPercentage' | 'Points') {
    if (this.state.PlayerStatModelSortProp == prop) {
      switch (prop) {
        case 'FullName':
          return this.state.PlayerStatModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'GamesPlayed':
          return this.state.PlayerStatModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'GamesWon':
          return this.state.PlayerStatModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'WinningPercentage':
          return this.state.PlayerStatModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'Points':
          return this.state.PlayerStatModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        default:
          return '';
      }
    }
    else {
      return '';
    }
  }

  GetCurrentLeagueName() {
    if (this.state.DemoVisible) {
      return this.state.LeagueList.filter(c => c.LeagueID == this.state.DemoLeagueID)[0].LeagueName;
    }

    return this.state.LeagueList.filter(c => c.LeagueID == this.state.LeagueID)[0].LeagueName;
  }

  GetPlayerFullName(PlayerID: number): string {
    return this.dataHelperService.GetPlayerFullName(PlayerID);
  }

  GetPlayerLastNameAndFirstNameInit(PlayerID: number): string {
    return this.dataHelperService.GetPlayerLastNameAndFirstNameInit(PlayerID);
  }

  GetPreviousStatDate(): Date {
    if (this.state.CurrentPlayerDateID > 0) {
      return new Date(); // this should not happen
    }

    return this.state.DatePlayerStatModelList[this.state.CurrentPlayerDateID - 1].Date;
  }

  GetCurrentStatDate(): Date {
    return this.state.DatePlayerStatModelList[this.state.CurrentPlayerDateID].Date;
  }

  GetNextStatDate(): Date {
    if (this.state.CurrentPlayerDateID < this.state.DatePlayerStatModelList.length - 2) {
      return new Date(); // this.should not happen
    }

    return this.state.DatePlayerStatModelList[this.state.CurrentPlayerDateID + 1].Date;
  }

  SetCurrentStatDate(CurrentPlayerDateID: number) {
    if (CurrentPlayerDateID < 0) {
      this.state.CurrentPlayerDateID = 0;
    }
    else if (CurrentPlayerDateID > this.state.DatePlayerStatModelList.length - 1) {
      this.state.CurrentPlayerDateID = this.state.DatePlayerStatModelList.length - 1;
    }
    else {
      this.state.CurrentPlayerDateID = CurrentPlayerDateID;
    }

    this.state.CurrentDatePlayerStatModelList = this.sortService.SortPlayerStatModelList(this.state.DatePlayerStatModelList[this.state.CurrentPlayerDateID].PlayerStatModelList);
  }
}
