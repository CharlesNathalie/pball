import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { AscDescEnum, GetAscDescEnum } from 'src/app/enums/AscDescEnum';
import { DataLeagueStatService } from 'src/app/services/data/data-league-stats.service';
import { DataPlayerGamesService } from 'src/app/services/data/data-player-games.service';
import { SortService } from 'src/app/services/sort/sort.service';
import { TablePlayerGamesService } from './table-player-games.service';

@Component({
  selector: 'app-table-player-games',
  templateUrl: './table-player-games.component.html',
  styleUrls: ['./table-player-games.component.css']
})
export class TablePlayerGamesComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tablePlayerGamesService: TablePlayerGamesService,
    public sortService: SortService,
    public dataPlayerGameService: DataPlayerGamesService) {
  }

  ascDescEnum = GetAscDescEnum();
  displayedColumns: string[] = ['Rank', 'Partner', 'Opponents', 'GameDate', 'Scores'];

  ngOnInit(): void {
    if (this.state.LeagueID > 0) {
      if (this.state.PlayerGameModelList.length == 0) {
        this.dataPlayerGameService.Run();
      }
    }
  }

  ngOnDestroy(): void {
  }

  DoSortPlayerGames(prop: 'Partner' | 'Opponents' | 'GameDate' | 'Scores') {
    if (this.state.PlayerGameModelSortProp == prop) {
      if (this.state.PlayerGameModelSortAscDesc == AscDescEnum.Ascending) {
        this.state.PlayerGameModelSortAscDesc = AscDescEnum.Descending;
      }
      else {
        this.state.PlayerGameModelSortAscDesc = AscDescEnum.Ascending;
      }
    }
    this.state.PlayerGameModelSortProp = prop;
    this.state.PlayerGameModelList = this.sortService.SortPlayerGameModelList(this.state.PlayerGameModelList);
  }

  GetSortingClass(prop: 'Partner' | 'Opponents' | 'GameDate' | 'Scores') {
    if (this.state.PlayerGameModelSortProp == prop) {
      switch (prop) {
        case 'Partner':
          return this.state.PlayerGameModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'Opponents':
          return this.state.PlayerGameModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'GameDate':
          return this.state.PlayerGameModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'Scores':
          return this.state.PlayerGameModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        default:
          return '';
      }
    }
    else{
      return '';
    }
  }
}
