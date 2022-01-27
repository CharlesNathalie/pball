import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { ContactService } from 'src/app/services/contact.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  Home: string[] = ['Home', 'Accueil'];
  Leagues: string[] = ['Leagues', 'Ligues'];
  Progress: string[] = ['Progress', 'Progrès'];

  constructor(public state: AppStateService) {
  }

}
