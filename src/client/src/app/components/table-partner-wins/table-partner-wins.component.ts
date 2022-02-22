import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { AscDescEnum, GetAscDescEnum } from 'src/app/enums/AscDescEnum';
import { DataHelperService } from 'src/app/services/data-helper.service';
import { SortService } from 'src/app/services/sort.service';
import { TablePartnerWinsService } from '../../services/table-partner-wins.service';
import { DataPlayerGamesService } from 'src/app/services/data-player-games.service';

@Component({
  selector: 'app-table-partner-wins',
  templateUrl: './table-partner-wins.component.html',
  styleUrls: ['./table-partner-wins.component.css']
})
export class TablePartnerWinsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tablePartnerWinsService: TablePartnerWinsService,
    public sortService: SortService,
    public dataPlayerGamesService: DataPlayerGamesService,
   public dataHelperService: DataHelperService) {
  }

  ascDescEnum = GetAscDescEnum();
  displayedColumns: string[] = ['Rank', 'Partner', 'Games', 'Wins', 'PlusMinus'];

  ngOnInit(): void {
    // if (this.state.DemoVisible) {
    //   if (this.state.DemoLeagueID > 0) {
    //     if (this.state.PartnerWinsModelList.length == 0) {
    //       //this.dataPlayerGamesService.Run();
    //     }
    //   }
    // }
    // else {
    //   if (this.state.LeagueID > 0) {
    //     if (this.state.PartnerWinsModelList.length == 0) {
    //       //this.dataPlayerGamesService.Run();
    //     }
    //   }
    // }
  }

  ngOnDestroy(): void {
  }

  DoSortPartnerWins(prop: 'Partner' | 'Games' | 'Wins' | 'PlusMinus') {
    if (this.state.PartnerWinsModelSortProp == prop) {
      if (this.state.PartnerWinsModelSortAscDesc == AscDescEnum.Ascending) {
        this.state.PartnerWinsModelSortAscDesc = AscDescEnum.Descending;
      }
      else {
        this.state.PartnerWinsModelSortAscDesc = AscDescEnum.Ascending;
      }
    }
    this.state.PartnerWinsModelSortProp = prop;
    this.state.PartnerWinsModelList = this.sortService.SortPartnerWinsModelList(this.state.PartnerWinsModelList);
  }

  GetSortingClass(prop: 'Partner' | 'Games' | 'Wins' | 'PlusMinus') {
    if (this.state.PartnerWinsModelSortProp == prop) {
      switch (prop) {
        case 'Partner':
          return this.state.PartnerWinsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'Games':
          return this.state.PartnerWinsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'Wins':
          return this.state.PartnerWinsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
        case 'PlusMinus':
          return this.state.PartnerWinsModelSortAscDesc == this.ascDescEnum.Ascending ? 'sortedAsc' : 'sortedDesc';
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
