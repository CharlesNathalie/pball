import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueTodayShuffleService {
  HelperToRandomChoices: string[] = ['Helper to random choices', 'Aide aux choix aléatoire'];
  ListToRandomlyPickFrom: string[] = ['List to randomly pick from', 'Liste à choisir au hazard'];
  PlayingThisTime: string[] = ['Playing this time', 'Joue cette fois'];
  NumberOfPlayersToSelect: string[] = ['Number of players to select', 'Nombre de joueur à choisir'];

  constructor(public state: AppStateService) {
  }

    ChangePlayingThisTime(playerID: number) {

  }

  ChangePlayerToShuffle(playerID: number) {

  }

  Shuffle() {
    if (this.state.DemoVisible) {
      // need to shuffle
      return;
    }
    else {
      // need to shuffle    
    }
  }

}
