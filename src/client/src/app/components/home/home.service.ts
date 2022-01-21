import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { ContactService } from 'src/app/services/contact/contact.service';
import * as moment from 'moment';
import { DataLeagueStatService } from 'src/app/services/data/data-league-stats.service';
import { DataPlayerGamesService } from 'src/app/services/data/data-player-games.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  All: string[] = ['All', 'Tout'];
  AllInit: string[] = ['All', 'Tout'];
  BetweenDates: string[] = ['Between dates', 'Entre les dates'];
  Bilingual: string[] = ['Bilingual', 'Bilingue'];
  EndDate: string[] = ['End Date', 'Date de fin'];
  ForgotPassword: string[] = ['Forgot password', 'Oublié votre mot de passe'];
  Leagues: string[] = ['Leagues', 'Ligues'];
  Login: string[] = ['Login', 'Connexion'];
  ManageGames: string[] = ['Manage games (add, change, delete)', 'Gérer les parties (ajouter, modifier, supprimer)'];
  ManageLeagues: string[] = ['Manage leagues (add, change, delete)', 'Gérer les ligues (ajouter, modifier, supprimer)'];
  ManagePlayersWithinLeagues: string[] = ['Manage players within leagues (add, change, delete)', 'Gérer les joueurs des ligues (ajouter, modifier, supprimer)'];
  Month: string[] = ['Month', 'Mois'];
  MonthInit: string[] = ['Month', 'Mois'];
  ProduceChartsOfResults: string[] = ['Produce charts of results', 'Produire les graphiques de résultats'];
  ProduceTablesOfResults: string[] = ['Produce tables of results', 'Produire les tableaux de résultats'];
  Profile: string[] = ['Profile', 'Profil'];
  Register: string[] = ['Register', 'S\'inscrire'];
  ScrollToViewResults: string[] = ['Scroll to view results', 'Faites défiler pour voir les résultats'];
  Start: string[] = ['Start', 'Débuter'];
  StartDate: string[] = ['Start Date', 'Date de début'];
  Today: string[] = ['Today', 'Aujourd\'hui'];
  TodayInit: string[] = ['Day', 'Auj.'];
  ToDo: string[] = ['To do', 'A faire'];
  Week: string[] = ['Week', 'Semaine'];
  WeekInit: string[] = ['Week', 'Sem.'];
  WelcomeTo: string[] = ['Welcome to', 'Bienvenue au'];
  WhatIsWorkingAtThisTime: string[] = ['What is working at this time', 'Ce que fonctionne en ce moment'];
  Year: string[] = ['Year', 'Année'];
  YearInit: string[] = ['Year', 'Ann.'];

  constructor(public state: AppStateService,
    public contactService: ContactService,
    public dataLeagueStatService: DataLeagueStatService,
    public dataPlayerGameService: DataPlayerGamesService) {
  }

  ChangedStartDate(startDate: HTMLInputElement) {
    this.state.StartDate = moment(new Date(startDate.value)).toDate();
    localStorage.setItem('StartDate', JSON.stringify(this.state.StartDate));
    this.dataLeagueStatService.Run();
    this.dataPlayerGameService.Run();
  }

  ChangedEndDate(endDate: HTMLInputElement) {
    this.state.EndDate = new Date(endDate.value);
    localStorage.setItem('EndDate', JSON.stringify(this.state.EndDate));
    this.dataLeagueStatService.Run();
    this.dataPlayerGameService.Run();
  }

  LoadLocalStorage() {
    if (localStorage.getItem('User') != null) {
      this.state.User = JSON.parse(localStorage.getItem('User') ?? '');
    }
    if (localStorage.getItem('LeagueID') != null) {
      this.state.LeagueID = JSON.parse(localStorage.getItem('LeagueID') ?? '0');
    }
    if (localStorage.getItem('StartDate') != null) {
      this.state.StartDate = JSON.parse(localStorage.getItem('StartDate') ?? '');
    }
    if (localStorage.getItem('EndDate') != null) {
      this.state.EndDate = JSON.parse(localStorage.getItem('EndDate') ?? '');
    }
    if (localStorage.getItem('DemoVisible') != null) {
      this.state.DemoVisible = JSON.parse(localStorage.getItem('DemoVisible') ?? 'false');
    }
  }

  Period(time: 'day' | 'week' | 'month' | 'year' | 'all' | 'between'): void {
    this.state.EndDate = new Date();
    this.state.StartDate = new Date();
    if (time == 'all') {
      this.state.StartDate.setFullYear(this.state.StartDate.getFullYear() - 10);
      this.state.DataTime = 'all';
    }
    else if (time == 'year') {
      this.state.StartDate.setFullYear(this.state.StartDate.getFullYear() - 1);
      this.state.DataTime = 'year';
    }
    else if (time == 'month') {
      this.state.StartDate.setMonth(this.state.StartDate.getMonth() - 1);
      this.state.DataTime = 'month';
    }
    else if (time == 'week') {
      this.state.StartDate.setDate(this.state.StartDate.getDate() - 7);
      this.state.DataTime = 'week';
    }
    else {
      this.state.StartDate.setDate(this.state.StartDate.getDate() - 1);
      this.state.DataTime = 'day';
    }
    localStorage.setItem('StartDate', JSON.stringify(this.state.StartDate));
    localStorage.setItem('EndDate', JSON.stringify(this.state.EndDate));
    this.dataLeagueStatService.Run();
    this.dataPlayerGameService.Run();
  }

  SetLeagueID(LeagueID: number) {
    this.state.LeagueID = LeagueID;
    localStorage.setItem('LeagueID', JSON.stringify(this.state.LeagueID));

    if (!this.state.DemoVisible)
    {
      this.contactService.GetAllPlayersForLeague();
    }
    else{
      this.dataLeagueStatService.Run();
      this.dataPlayerGameService.Run();
    }
  }

}
