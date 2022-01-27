import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueFactorsExampleService {
  AverageTeamPlayerLevel: string[] = ['Average Team Player Level', 'Moyenne du niveau des joueurs'];
  CalculationOfPoints: string[] = ['Calculation of points', 'Calcul des points'];
  Cancel: string[] = ['Cancel', 'Annuler'];
  Example: string[] = ['Example', 'Exemple'];
  LosersPoints: string[] = ['Losers points', 'Points pour les perdants'];
  PlayerLevelFactor: string[] = ['Player level factor', 'Facteur niveau de jeux du joueur']
  PercentPointsFactor: string[] = ['Percent points factor', 'Facteur de poucentage de points']
  PointsToLosers: string[] = ['Points to losers', 'Points au perdants']
  PointsToWinners: string[] = ['Points to winners', 'Points aux gagnants']
  Team1Player1Level: string[] = ['Team 1 Player 1 level', 'Niveau de l\'équipe 1 joueur 1']
  Team1Player2Level: string[] = ['Team 1 Player 2 level', 'Niveau de l\'équipe 1 joueur 2']
  Team2Player1Level: string[] = ['Team 2 Player 1 level', 'Niveau de l\'équipe 2 joueur 1']
  Team2Player2Level: string[] = ['Team 2 Player 2 level', 'Niveau de l\'équipe 2 joueur 2']
  TeamPercentageOfPoints: string[] = ['Team percentage of points', 'Poucentage de points pour l\'équipe'];
  WinnersPoints: string[] = ['Winners points', 'Points pour les gagnants'];

  constructor(public state: AppStateService) {
  }

  
}
