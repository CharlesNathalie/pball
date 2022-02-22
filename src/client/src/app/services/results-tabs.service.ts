import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class ResultsTabsService {
  Leaders: string[] = ['Leaders', 'Meneurs'];
  Games: string[] = ['Games', 'Parties'];
  Partners: string[] = ['Partners', 'Partenaires'];
  Charts: string[] = ['Charts', 'Graphiques'];
 
  constructor(public state: AppStateService) {
  }

}
