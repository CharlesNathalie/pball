import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class NoDataForUserService {
  doesNotYetBelongToALeague: string[] = ['does not yet belong to a league', 'n\'appartient pas encore à une ligue'];
  Add: string[] = ['Add', 'Ajoutez'];
  aNewLeague: string[] = ['a new league', 'une nouvelle ligue'];
  andAddYourPickleballFriends: string[] = ['and add your pickleball friends', 'et ajoutez vos amis de pickleball'];
  or: string[] = ['or', 'ou'];
  PleaseContactAKnownLeagueAdministrator: string[] = ['Please contact a known league administrator', 'Veuillez contacter un administrateur de ligue connu'];
  toBeAddedToHisLeague: string[] = ['to be added to his league', 'pour être ajouté à sa ligue'];
  YouAreCurrentlyNotAMemberOfALeague: string[] = ['You are currently not a member of a league', 'Vous n\'êtes actuellement pas membre d\'une ligue'];
 
    constructor(public state: AppStateService) {
  }
}
