import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { Player } from 'src/app/models/Player.model';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class DataHelperService {

  MonthAbbrList: string[][] =
    [
      ['Jan.', 'janv.'],
      ['Feb.', 'févr.'],
      ['Mar.', 'mars'],
      ['Apr.', 'avr.'],
      ['May.', 'mai'],
      ['Jun.', 'juin'],
      ['Jul.', 'juill.'],
      ['Aug.', 'août'],
      ['Sep.', 'sept.'],
      ['Oct.', 'oct.'],
      ['Nov.', 'nov.'],
      ['Dec.', 'dec.'],
    ];

  constructor(public state: AppStateService) {
  }

  GetPlayerFullName(playerID: number): string {
    let playerArr: Player[] = this.state.PlayerList.filter(c => c.ContactID == playerID);
    if (playerArr.length > 0) {
      let init: string = '';
      if (playerArr[0].Initial) {
        init = ` ${playerArr[0].Initial}.`;
      }
      return `${playerArr[0].LastName}, ${playerArr[0].FirstName}${init}`;
    }

    return '';
  }

  GetPlayerLastNameAndFirstNameInit(playerID: number): string {
    let playerArr: Player[] = this.state.PlayerList.filter(c => c.ContactID == playerID);
    if (playerArr.length > 0) {
      return `${playerArr[0].LastName}, ${playerArr[0].FirstName[0]}`;
    }

    return '';
  }

  GetDateFormat(gameDate: Date): string {
    let mDate = moment(gameDate);
    if (mDate.isValid()) {
      let month = mDate.month();
      return '' + mDate.year() + '-' + this.MonthAbbrList[month][this.state.LangID] + '-' + mDate.date();
    }

    return '';
  }
}
