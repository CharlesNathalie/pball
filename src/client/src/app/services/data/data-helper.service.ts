import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { Player } from 'src/app/models/Player.model';

@Injectable({
  providedIn: 'root'
})
export class DataHelperService {

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
}
