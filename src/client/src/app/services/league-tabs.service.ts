import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueTabsService {
  Members: string[] = ['Members', 'Membres'];
  Today: string[] = ['Today', 'Aujourd\'hui'];
  Games: string[] = ['Games', 'Parties'];

  constructor(public state: AppStateService) {
  }

}
