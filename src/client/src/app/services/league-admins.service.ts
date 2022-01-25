import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueAdminsService {
  Administrators: string[] = ['Administrators', 'Administrateurs'];

  constructor(public state: AppStateService) {
  }

}
