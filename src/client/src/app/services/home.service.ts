import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  Home: string[] = ['Home', 'Accueil'];
  Leagues: string[] = ['Leagues', 'Ligues'];
  Login: string[] = ['Login', 'Connexion'];
  Progress: string[] = ['Progress', 'Progrès'];
  HideDemo: string[] = ['Hide demo', 'Cacher demo'];
  Register: string[] = ['Register', 'S\'inscrire'];
  ShowDemoAsLeagueAdmin: string[] = ['Show demo as league admin', 'Montrer demo comme admin de la ligue'];
  ShowDemoAsNormalUser: string[] = ['Show demo as normal user', 'Montrer demo comme utilisateur normal'];

  constructor(public state: AppStateService) {
  }

}
