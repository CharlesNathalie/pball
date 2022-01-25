import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { SortService } from 'src/app/services/sort.service';

@Injectable({
  providedIn: 'root'
})
export class TablePlayerGamesService {
  Partner: string[] = ['Partner', 'Partenaire'];
  Opponents: string[] = ['Opponents', 'Adversaire'];
  GameDate: string[] = ['Game date', 'Date de la partie'];
  Scores: string[] = ['Scores', 'Pointages'];
  GamesWon: string[] = ['Games won', 'parties gagnées'];
  
  constructor(public state: AppStateService,
    public sortService: SortService) {
  }

  

}
