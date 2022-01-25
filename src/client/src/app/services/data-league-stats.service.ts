import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { Game } from 'src/app/models/Game.model';
import { LeagueStatsModel } from 'src/app/models/LeagueStatsModel';
import { DateService } from './date.service';
import { SortService } from './sort.service';
import { DataHelperService } from './data-helper.service';

@Injectable({
  providedIn: 'root'
})
export class DataLeagueStatService {
  WorkingOnLeagueStats: string[] = ['Working on league stats', 'Travail sur statistiques de ligue'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  WorkingTheStatSuccess: boolean = false;

  constructor(public state: AppStateService,
    public dateService: DateService,
    public sortService: SortService,
    public dataHelperService: DataHelperService) {
  }

  Run(): void {
    this.state.LeagueStatsModelList = [];

    this.Status = `${this.WorkingOnLeagueStats[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};
    console.debug('Running league stats');

    let tempGameList: Game[] = [];
    for (let i = 0, count = this.state.GameList.length; i < count; i++) {
      if (this.dateService.InRange(this.state.GameList[i].GameDate, this.state.StartDate, this.state.EndDate)) {
        tempGameList.push(this.state.GameList[i]);
      }
    }
    let LeagueStatsModelList: LeagueStatsModel[] = [];
    if (this.state.PlayerList.length > 0) {
      for (let i = 0, count = this.state.PlayerList.length; i < count; i++) {
        let leagueStatsModel: LeagueStatsModel = <LeagueStatsModel>{};
        leagueStatsModel.ContactID = this.state.PlayerList[i].ContactID;
        let init: string = '';
        if (this.state.PlayerList[i].Initial) {
          init = ` ${this.state.PlayerList[i].Initial}.`;
        }
        leagueStatsModel.FullName = this.dataHelperService.GetPlayerFullName(this.state.PlayerList[i].ContactID);
        leagueStatsModel.LastNameAndNameFirstLetter = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(this.state.PlayerList[i].ContactID);

        let game1: number = tempGameList.filter(c => c.Team1Player1 == this.state.PlayerList[i].ContactID).length;
        let game2: number = tempGameList.filter(c => c.Team1Player2 == this.state.PlayerList[i].ContactID).length;
        let game3: number = tempGameList.filter(c => c.Team2Player1 == this.state.PlayerList[i].ContactID).length;
        let game4: number = tempGameList.filter(c => c.Team2Player2 == this.state.PlayerList[i].ContactID).length;
        leagueStatsModel.NumberOfGames = game1 + game2 + game3 + game4;

        if (leagueStatsModel.NumberOfGames > 0) {
          let numberOfWins1: number = tempGameList.filter(c => (c.Team1Player1 == this.state.PlayerList[i].ContactID
            || c.Team1Player2 == this.state.PlayerList[i].ContactID) && c.Team1Scores > c.Team2Scores).length;
          let numberOfWins2: number = tempGameList.filter(c => (c.Team2Player1 == this.state.PlayerList[i].ContactID
            || c.Team2Player2 == this.state.PlayerList[i].ContactID) && c.Team2Scores > c.Team1Scores).length;

          leagueStatsModel.NumberOfWins = numberOfWins1 + numberOfWins2;

          leagueStatsModel.WinningPercentage = 100 * leagueStatsModel.NumberOfWins / leagueStatsModel.NumberOfGames;
        }

        LeagueStatsModelList.push(leagueStatsModel);
      }
    }

    this.state.LeagueStatsModelList = this.sortService.SortLeagueStatsModelList(LeagueStatsModelList);

    if (this.state.DemoVisible) {
      localStorage.setItem('DemoUser', JSON.stringify(this.state.DemoUser));
      localStorage.setItem('DemoLeagueID', JSON.stringify(this.state.DemoLeagueID));
      localStorage.setItem('DemoStartDate', JSON.stringify(this.state.DemoStartDate));
      localStorage.setItem('DemoEndDate', JSON.stringify(this.state.DemoEndDate));
      localStorage.setItem('DemoVisible', JSON.stringify(this.state.DemoVisible));
      localStorage.setItem('DemoHomeTabIndex', JSON.stringify(this.state.DemoHomeTabIndex));
    }
    else {
      localStorage.setItem('User', JSON.stringify(this.state.User));
      localStorage.setItem('LeagueID', JSON.stringify(this.state.LeagueID));
      localStorage.setItem('StartDate', JSON.stringify(this.state.StartDate));
      localStorage.setItem('EndDate', JSON.stringify(this.state.EndDate));
      localStorage.setItem('DemoVisible', JSON.stringify(this.state.DemoVisible));
      localStorage.setItem('HomeTabIndex', JSON.stringify(this.state.HomeTabIndex));
    }

    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};

    console.debug('League stats completed');
  }
}
