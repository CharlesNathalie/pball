import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  CurrentlyInDemoMode: string[] = ['Currently in demo mode', 'Maintenant en mode demo'];
  HideDemo: string[] = ['Hide demo', 'Cacher demo'];
  Home: string[] = ['Home', 'Accueil'];
  Leagues: string[] = ['Leagues', 'Ligues'];
  Login: string[] = ['Login', 'Connexion'];
  or: string[] = ['or', 'ou'];
  Results: string[] = ['Results', 'Résultats'];
  Register: string[] = ['Register', 'S\'inscrire'];
  ShowDemoAsLeagueAdmin: string[] = ['Show demo as league admin', 'Montrer demo comme admin de la ligue'];
  ShowDemoAsNormalUser: string[] = ['Show demo as normal user', 'Montrer demo comme utilisateur normal'];
  ViewLeaguesInformation: string[] = ['View leagues information', 'Voir information sur les ligues'];
  ViewResults: string[] = ['View results', 'Voir résultats'];
  YouAreAnAdministrator: string[] = ['You are an administrator', 'Vous êtes un administrateur'];
  YouAreANormalUser: string[] = ['You are a normal user', 'Vous êtes un utilisateur normal'];
  
  constructor(public state: AppStateService) {
  }

}
