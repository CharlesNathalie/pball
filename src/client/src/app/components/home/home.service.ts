import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  Bilingual: string[] = ['Bilingual', 'Bilingue'];
  ForgotPassword: string[] = ['Forgot password', 'Oublié mot de passe'];
  Login: string[] = ['Login', 'Connexion'];
  ManageGames: string[] = ['Manage games (add, change, delete)', 'Gérer les parties (ajour, changement, effacer)'];
  ManageLeagues: string[] = ['Manage leagues (add, change, delete)', 'Gérer les ligues (ajour, changement, effacer)'];
  ManagePlayersWithinLeagues: string[] = ['Manage players within leagues (add, change, delete)', 'Gérer les joueurs des ligues (ajour, changement, effacer)'];
  ProduceChartsOfResults: string[] = ['Produce charts of results', 'Produire les graphiques de résultats'];
  ProduceTablesOfResults: string[] = ['Produce tables of results', 'Produire les tableaux de résultats'];
  Profile: string[] = ['Profile', 'Profil'];
  Register: string[] = ['Register', 'S\'inscrire'];
  Start: string[]  = ['Start', 'Débutez'];
  ToDo: string[] = ['To do', 'A faire'];
  WelcomeTo: string[] = ['Welcome to', 'Bienvenu au'];
  WhatIsWorkingAtThisTime: string[] = ['What is working at this time', 'Ce que fonctionne en ce moment'];
  
  constructor(public state: AppStateService) {
  }

}
