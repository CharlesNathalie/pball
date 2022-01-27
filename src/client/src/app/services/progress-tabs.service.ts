import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class ProgressTabsService {
 
  constructor(public state: AppStateService) {
  }

}
