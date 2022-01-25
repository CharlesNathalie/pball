import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { SortService } from 'src/app/services/sort.service';

@Injectable({
  providedIn: 'root'
})
export class TableLeagueStatsService {
  Games: string[] = ['Games', 'Parties'];
  Name: string[] = ['Name', 'Nom'];
  Rank: string[] = ['Rank', 'No.'];
  stats: string[] = ['stats', 'statistiques'];
  Wins: string[] = ['Wins', 'Victoires'];
  Win: string[] = ['Win', 'Victoire'];


  constructor(public state: AppStateService,
    public sortService: SortService) {
  }

}
