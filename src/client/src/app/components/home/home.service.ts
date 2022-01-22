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
  AnalyseResultsFrom: string[] = ['Analyse results from:', 'Analyse les résultats à partir de:'];
  BetweenDates: string[] = ['Between dates', 'Entre les dates'];
  Bilingual: string[] = ['Bilingual', 'Bilingue'];
  EndDate: string[] = ['End Date', 'Date de fin'];
  ForgotPassword: string[] = ['Forgot password', 'Oublié votre mot de passe'];
  Leagues: string[] = ['Leagues', 'Ligues'];
  Login: string[] = ['Login', 'Connexion'];
  ManageGames: string[] = ['Manage games (add, change, delete)', 'Gérer les parties (ajouter, modifier, supprimer)'];
  ManageLeagues: string[] = ['Manage leagues (add, change, delete)', 'Gérer les ligues (ajouter, modifier, supprimer)'];
  ManagePlayersWithinLeagues: string[] = ['Manage players within leagues (add, change, delete)', 'Gérer les joueurs des ligues (ajouter, modifier, supprimer)'];
  LastMonth: string[] = ['Last month', 'Dernier mois'];
  ProduceChartsOfResults: string[] = ['Produce charts of results', 'Produire les graphiques de résultats'];
  ProduceTablesOfResults: string[] = ['Produce tables of results', 'Produire les tableaux de résultats'];
  Profile: string[] = ['Profile', 'Profil'];
  Register: string[] = ['Register', 'S\'inscrire'];
  ScrollToViewResults: string[] = ['Scroll to view results', 'Faites défiler pour voir les résultats'];
  Start: string[] = ['Start', 'Débuter'];
  StartDate: string[] = ['Start Date', 'Date de début'];
  Day: string[] = ['Today', 'Aujourd\'hui'];
  ToDo: string[] = ['To do', 'A faire'];
  LastWeek: string[] = ['Last week', 'Semaine dernière'];
  WelcomeTo: string[] = ['Welcome to', 'Bienvenue au'];
  WhatIsWorkingAtThisTime: string[] = ['What is working at this time', 'Ce que fonctionne en ce moment'];
  LastYear: string[] = ['Last year', 'L\'année dernière'];

  constructor(public state: AppStateService,
    public contactService: ContactService,
    public dataLeagueStatService: DataLeagueStatService,
    public dataPlayerGameService: DataPlayerGamesService) {
  }

  ChangedStartDate(startDate: HTMLInputElement) {
    this.state.StartDate = moment(new Date(startDate.value)).toDate();
    if (this.state.DemoVisible) {
      localStorage.setItem('DemoStartDate', JSON.stringify(this.state.StartDate));
    }
    else {
      localStorage.setItem('StartDate', JSON.stringify(this.state.StartDate));
    }
    this.dataLeagueStatService.Run();
    this.dataPlayerGameService.Run();
  }

  ChangedEndDate(endDate: HTMLInputElement) {
    this.state.EndDate = new Date(endDate.value);
    if (this.state.DemoVisible) {
      localStorage.setItem('DemoEndDate', JSON.stringify(this.state.EndDate));
    }
    else {
      localStorage.setItem('EndDate', JSON.stringify(this.state.EndDate));
    }
    this.dataLeagueStatService.Run();
    this.dataPlayerGameService.Run();
  }

  LoadLocalStorage() {
    if (this.state.DemoVisible) {
      if (localStorage.getItem('DemoUser') != null) {
        this.state.User = JSON.parse(localStorage.getItem('DemoUser') ?? '');
      }
      if (localStorage.getItem('DemoLeagueID') != null) {
        this.state.LeagueID = JSON.parse(localStorage.getItem('DemoLeagueID') ?? '0');
      }
      if (localStorage.getItem('DemoStartDate') != null) {
        this.state.StartDate = JSON.parse(localStorage.getItem('DemoStartDate') ?? '');
      }
      if (localStorage.getItem('DemoEndDate') != null) {
        this.state.EndDate = JSON.parse(localStorage.getItem('DemoEndDate') ?? '');
      }
      if (localStorage.getItem('DemoVisible') != null) {
        this.state.DemoVisible = JSON.parse(localStorage.getItem('DemoVisible') ?? 'false');
      }
    }
    else {
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
    if (this.state.DemoVisible)
    {
      localStorage.setItem('DemoStartDate', JSON.stringify(this.state.StartDate));
      localStorage.setItem('DemoEndDate', JSON.stringify(this.state.EndDate));
      }
    else{
      localStorage.setItem('StartDate', JSON.stringify(this.state.StartDate));
      localStorage.setItem('EndDate', JSON.stringify(this.state.EndDate));
      }
    this.dataLeagueStatService.Run();
    this.dataPlayerGameService.Run();
  }

  SetLeagueID(LeagueID: number) {
    this.state.LeagueID = LeagueID;
    if (this.state.DemoVisible)
    {
      localStorage.setItem('DemoLeagueID', JSON.stringify(this.state.LeagueID));
    }
    else{
      localStorage.setItem('LeagueID', JSON.stringify(this.state.LeagueID));
    }

    if (!this.state.DemoVisible) {
      this.contactService.GetAllPlayersForLeague();
    }
    else {
      this.dataLeagueStatService.Run();
      this.dataPlayerGameService.Run();
    }
  }

}
