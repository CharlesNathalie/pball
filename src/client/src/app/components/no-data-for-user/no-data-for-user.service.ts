import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class NoDataForUserService {
  TheUser: string[] = ['The user', 'L\'utilisateur'];
  doesNotYetBelongToALeague: string[] = ['does not yet belong to a league', 'n\'appartient pas encore à une ligue'];

    constructor(public state: AppStateService) {
  }
}
