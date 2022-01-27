import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { AscDescEnum } from 'src/app/enums/AscDescEnum';
import { IDNumbOrTextSort } from 'src/app/models/IDNumbSort';
import { LeagueStatsModel } from 'src/app/models/LeagueStatsModel';
import { PlayerGameModel } from 'src/app/models/PlayerGameModel';
import * as moment from 'moment';
import { Player } from '../models/Player.model';
import { Game } from '../models/Game.model';

@Injectable({
  providedIn: 'root'
})
export class SortService {
  Junk: string[] = ['Junk', 'Junk (fr)'];

  constructor(public state: AppStateService) {
  }

  SortLeagueStatsModelList(arr: LeagueStatsModel[]): LeagueStatsModel[] {
    if (typeof (arr) == "undefined" || !arr || arr?.length == 0 || arr == null) return [];

    let LeagueStatsModelSorted: LeagueStatsModel[] = [];
    let arr2: IDNumbOrTextSort[] = [];
    let sortable: IDNumbOrTextSort[] = [];

    for (let i = 0, count = arr?.length; i < count; i++) {
      let numbOrText: number | string = '';
      switch (this.state.LeagueStatsModelSortProp) {
        case 'FullName':
          {
            numbOrText = arr[i].FullName;
          }
          break;
        case 'NumberOfGames':
          {
            numbOrText = arr[i].NumberOfGames;
          }
          break;
        case 'NumberOfWins':
          {
            numbOrText = arr[i].NumberOfWins;
          }
          break;
        case 'WinningPercentage':
          {
            numbOrText = arr[i].WinningPercentage;
          }
          break;
        default:
          break;
      }

      sortable.push(<IDNumbOrTextSort>{
        ID: arr[i].ContactID,
        NumbOrText: numbOrText,
      });
    }

    if (this.state.LeagueStatsModelSortAscDesc === AscDescEnum.Ascending) {
      arr2 = sortable.sort(this.PredicateAscBy('NumbOrText'));
    }
    else {
      arr2 = sortable.sort(this.PredicateDescBy('NumbOrText'));
    }

    for (let i = 0, count = sortable?.length; i < count; i++) {
      for (let j = 0; j < arr?.length; j++) {
        if (arr2[i].ID == arr[j].ContactID) {
          LeagueStatsModelSorted.push(arr[j]);
          break;
        }
      }
    }

    return LeagueStatsModelSorted;
  }

  SortPlayerGameModelList(arr: PlayerGameModel[]): PlayerGameModel[] {
    if (typeof (arr) == "undefined" || !arr || arr?.length == 0 || arr == null) return [];

    let PlayerGameModelSorted: PlayerGameModel[] = [];
    let arr2: IDNumbOrTextSort[] = [];
    let sortable: IDNumbOrTextSort[] = [];

    for (let i = 0, count = arr?.length; i < count; i++) {
      let numbOrText: number | string = '';
      switch (this.state.PlayerGameModelSortProp) {
        case 'Partner':
          {
            numbOrText = arr[i].PartnerFullName;
          }
          break;
        case 'Opponents':
          {
            numbOrText = `${arr[i].Opponent1FullName}-${arr[i].Opponent2FullName}`;
          }
          break;
        case 'GameDate':
          {
            numbOrText = moment(arr[i].GameDate).format('yyyy-MM-DD');
          }
          break;
        case 'Scores':
          {
            numbOrText = `${arr[i].Scores < 10 ? '0' + arr[i].Scores : arr[i].Scores} -- ${arr[i].OpponentScores < 10 ? '0' + arr[i].OpponentScores : arr[i].OpponentScores}`;
          }
          break;
        default:
          break;
      }

      sortable.push(<IDNumbOrTextSort>{
        ID: arr[i].GameID,
        NumbOrText: numbOrText,
      });
    }

    if (this.state.PlayerGameModelSortAscDesc === AscDescEnum.Ascending) {
      arr2 = sortable.sort(this.PredicateAscBy('NumbOrText'));
    }
    else {
      arr2 = sortable.sort(this.PredicateDescBy('NumbOrText'));
    }

    for (let i = 0, count = sortable?.length; i < count; i++) {
      for (let j = 0; j < arr?.length; j++) {
        if (arr2[i].ID == arr[j].GameID) {
          PlayerGameModelSorted.push(arr[j]);
          break;
        }
      }
    }

    return PlayerGameModelSorted;
  }

  SortPlayerList(arr: Player[]): Player[] {
    if (typeof (arr) == "undefined" || !arr || arr?.length == 0 || arr == null) return [];

    let PlayerListSorted: Player[] = [];
    let arr2: IDNumbOrTextSort[] = [];
    let sortable: IDNumbOrTextSort[] = [];

    for (let i = 0, count = arr?.length; i < count; i++) {
      let numbOrText: number | string = '';
      switch (this.state.PlayerListSortProp) {
        case 'FullName':
          {
            numbOrText = arr[i].LastName + arr[i].FirstName + arr[i].Initial;
          }
          break;
        default:
          break;
      }

      sortable.push(<IDNumbOrTextSort>{
        ID: arr[i].ContactID,
        NumbOrText: numbOrText,
      });
    }

    if (this.state.PlayerListSortAscDesc === AscDescEnum.Ascending) {
      arr2 = sortable.sort(this.PredicateAscBy('NumbOrText'));
    }
    else {
      arr2 = sortable.sort(this.PredicateDescBy('NumbOrText'));
    }

    for (let i = 0, count = sortable?.length; i < count; i++) {
      for (let j = 0; j < arr?.length; j++) {
        if (arr2[i].ID == arr[j].ContactID) {
          PlayerListSorted.push(arr[j]);
          break;
        }
      }
    }

    return PlayerListSorted;
  }

  SortGameByDateAscendingList(arr: Game[]): Game[] {
    if (typeof (arr) == "undefined" || !arr || arr?.length == 0 || arr == null) return [];

    let GameSorted: Game[] = [];
    let arr2: IDNumbOrTextSort[] = [];
    let sortable: IDNumbOrTextSort[] = [];

    for (let i = 0, count = arr?.length; i < count; i++) {
      let numbOrText: number | string = '';
      numbOrText = moment(arr[i].GameDate).format('yyyy-MM-DD');

      sortable.push(<IDNumbOrTextSort>{
        ID: arr[i].GameID,
        NumbOrText: numbOrText,
      });
    }

      arr2 = sortable.sort(this.PredicateAscBy('NumbOrText'));

    for (let i = 0, count = sortable?.length; i < count; i++) {
      for (let j = 0; j < arr?.length; j++) {
        if (arr2[i].ID == arr[j].GameID) {
          GameSorted.push(arr[j]);
          break;
        }
      }
    }

    return GameSorted;
  }

  private PredicateAscBy(prop: any) {
    return function (a: any, b: any) {
      if (a[prop] > b[prop]) {
        return 1;
      } else if (a[prop] < b[prop]) {
        return -1;
      }
      return 0;
    }
  }

  private PredicateDescBy(prop: any) {
    return function (a: any, b: any) {
      if (a[prop] < b[prop]) {
        return 1;
      } else if (a[prop] > b[prop]) {
        return -1;
      }
      return 0;
    }
  }

}
