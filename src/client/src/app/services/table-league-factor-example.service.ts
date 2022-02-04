import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class TableLeagueFactorExampleService {
  ExamplePoints: string[] = ['Example points', 'Exemple points'];
  Points1: string[] = ['Points 1', 'Points 1'];
  Points2: string[] = ['Points 2', 'Points 2'];
  Team1: string[] = ['Team 1', 'Équipe 1'];
  Team2: string[] = ['Team 2', 'Équipe 2'];



  constructor(public state: AppStateService) {
  }

}
