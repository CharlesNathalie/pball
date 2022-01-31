import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { ContactService } from './contact.service';
import { DataPlayerGamesService } from './data-player-games.service';
import * as moment from 'moment';
import { DataDatePlayerStatService } from './data-date-player-stat.service';

@Injectable({
  providedIn: 'root'
})
export class ProgressService {
  all: string[] = ['all', 'tout'];
  AnalyseResultsFrom: string[] = ['Analyse results from:', 'Analyse les résultats à partir de:'];
  between: string[] = ['between', 'entre'];
  Bilingual: string[] = ['Bilingual', 'Bilingue'];
  day: string[] = ['day', 'jour'];
  EndDate: string[] = ['End Date', 'Date de fin'];
  ForgotPassword: string[] = ['Forgot password', 'Oublié votre mot de passe'];
  Games: string[] = ['Games', 'Parties'];
  Home: string[] = ['Home', 'Accueil'];
  Leagues: string[] = ['Leagues', 'Ligues'];
  Login: string[] = ['Login', 'Connexion'];
  ManageGames: string[] = ['Manage games (add, change, delete)', 'Gérer les parties (ajouter, modifier, supprimer)'];
  ManageLeagues: string[] = ['Manage leagues (add, change, delete)', 'Gérer les ligues (ajouter, modifier, supprimer)'];
  ManagePlayersWithinLeagues: string[] = ['Manage players within leagues (add, change, delete)', 'Gérer les joueurs des ligues (ajouter, modifier, supprimer)'];
  month: string[] = ['month', 'mois'];
  ProduceChartsOfResults: string[] = ['Produce charts of results', 'Produire les graphiques de résultats'];
  ProduceTablesOfResults: string[] = ['Produce tables of results', 'Produire les tableaux de résultats'];
  Profile: string[] = ['Profile', 'Profil'];
  Register: string[] = ['Register', 'S\'inscrire'];
  Progress: string[] = ['Progress', 'Progrès'];
  ScrollToViewResults: string[] = ['Scroll to view results', 'Faites défiler pour voir les résultats'];
  Start: string[] = ['Start', 'Débuter'];
  StartDate: string[] = ['Start Date', 'Date de début'];
  ShowProgressFor: string[] = ['Show progress for', 'Montrer progrès pour'];
  ToDo: string[] = ['To do', 'A faire'];
  week: string[] = ['week', 'semaine'];
  WelcomeTo: string[] = ['Welcome to', 'Bienvenue au'];
  WhatIsWorkingAtThisTime: string[] = ['What is working at this time', 'Ce que fonctionne en ce moment'];
  year: string[] = ['year', 'année'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  ProgressSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public contactService: ContactService,
    //public dataLeagueStatService: DataLeagueStatService,
    public dataPlayerGamesService: DataPlayerGamesService,
    public dataDatePlayerStatService: DataDatePlayerStatService) {
  }

  ChangedStartDate(startDate: HTMLInputElement) {
    this.state.StartDate = moment(new Date(startDate.value)).toDate();
    if (this.state.DemoVisible) {
      localStorage.setItem('DemoStartDate', JSON.stringify(this.state.StartDate));
    }
    else {
      localStorage.setItem('StartDate', JSON.stringify(this.state.StartDate));
    }
    //this.dataLeagueStatService.Run();
    this.dataPlayerGamesService.Run();
    this.dataDatePlayerStatService.Run();
  }

  ChangedEndDate(endDate: HTMLInputElement) {
    this.state.EndDate = new Date(endDate.value);
    if (this.state.DemoVisible) {
      localStorage.setItem('DemoEndDate', JSON.stringify(this.state.EndDate));
    }
    else {
      localStorage.setItem('EndDate', JSON.stringify(this.state.EndDate));
    }
    //this.dataLeagueStatService.Run();
    this.dataPlayerGamesService.Run();
    this.dataDatePlayerStatService.Run();
  }

  LoadLocalStorage() {
    if (this.state.DemoVisible) {
      if (localStorage.getItem('DemoUser') != null) {
        this.state.DemoUser = JSON.parse(localStorage.getItem('DemoUser') ?? '');
      }
      if (localStorage.getItem('DemoLeagueID') != null) {
        this.state.DemoLeagueID = JSON.parse(localStorage.getItem('DemoLeagueID') ?? '0');
      }
      if (localStorage.getItem('DemoStartDate') != null) {
        this.state.DemoStartDate = JSON.parse(localStorage.getItem('DemoStartDate') ?? '');
      }
      if (localStorage.getItem('DemoEndDate') != null) {
        this.state.DemoEndDate = JSON.parse(localStorage.getItem('DemoEndDate') ?? '');
      }
      if (localStorage.getItem('DemoVisible') != null) {
        this.state.DemoVisible = JSON.parse(localStorage.getItem('DemoVisible') ?? 'false');
      }
      if (localStorage.getItem('DemoHomeTabIndex') != null) {
        this.state.DemoHomeTabIndex = JSON.parse(localStorage.getItem('DemoHomeTabIndex') ?? '0');
      }
      if (localStorage.getItem('LeagueTabsIndex') != null) {
        this.state.LeagueTabsIndex = JSON.parse(localStorage.getItem('LeagueTabsIndex') ?? '0');
      }
      if (localStorage.getItem('ProgressTabsIndex') != null) {
        this.state.ProgressTabsIndex = JSON.parse(localStorage.getItem('ProgressTabsIndex') ?? '0');
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
      if (localStorage.getItem('HomeTabIndex') != null) {
        this.state.HomeTabIndex = JSON.parse(localStorage.getItem('HomeTabIndex') ?? '0');
      }
      if (localStorage.getItem('LeagueTabsIndex') != null) {
        this.state.LeagueTabsIndex = JSON.parse(localStorage.getItem('LeagueTabsIndex') ?? '0');
      }
      if (localStorage.getItem('ProgressTabsIndex') != null) {
        this.state.ProgressTabsIndex = JSON.parse(localStorage.getItem('ProgressTabsIndex') ?? '0');
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
    //this.dataLeagueStatService.Run();
    this.dataPlayerGamesService.Run();
    this.dataDatePlayerStatService.Run();
  }


}
