import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { AscDescEnum, GetAscDescEnum } from 'src/app/enums/AscDescEnum';
import { DataHelperService } from 'src/app/services/data-helper.service';
import { SortService } from 'src/app/services/sort.service';
import { TablePlayerHeadToHeadService } from '../../services/table-player-head-to-head.service';
import { DataPlayerGamesService } from 'src/app/services/data-player-games.service';

@Component({
  selector: 'app-table-player-head-to-head',
  templateUrl: './table-player-head-to-head.component.html',
  styleUrls: ['./table-player-head-to-head.component.css']
})
export class TablePlayerHeadToHeadComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tablePlayerHeadToHeadService: TablePlayerHeadToHeadService,
    public sortService: SortService,
    public dataPlayerGamesService: DataPlayerGamesService,
   public dataHelperService: DataHelperService) {
  }

  ascDescEnum = GetAscDescEnum();
  displayedColumns: string[] = ['Rank', 'Partner', 'Opponents', 'GameDate', 'Scores'];

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
    else {
      return '';
    }
  }

  GetLoggedInPlayerFullName(): string {
    let ContactID: number = 0;
    if (this.state.DemoVisible) {
      ContactID = this.state.DemoUser.ContactID;
    }
    else {
      ContactID = this.state.User.ContactID;
    }
    if (!ContactID) {
      return '';
    }
    else {
      return this.dataHelperService.GetPlayerFullName(ContactID);
    }
  }
}
