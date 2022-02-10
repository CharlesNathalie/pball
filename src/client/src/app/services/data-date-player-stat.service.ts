import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { Game } from 'src/app/models/Game.model';
import { DateService } from './date.service';
import { SortService } from './sort.service';
import { DataHelperService } from './data-helper.service';
import { PlayerStatModel } from '../models/PlayerStatModel.model';
import { Player } from '../models/Player.model';
import { GamePoints } from '../models/GamePoints.model';
import { PlayerPlayerListModel } from '../models/PlayerPlayerListModel.model';
import { Router } from '@angular/router';
import { ChartAllService } from './chart-all.service';

@Injectable({
  providedIn: 'root'
})
export class DataDatePlayerStatService {
  WorkingOnPlayerPoints: string[] = ['Working on player points', 'Travail sur les points du joueur'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  DataPlayerPointsSuccess: boolean = false;

  constructor(public state: AppStateService,
    public dateService: DateService,
    public sortService: SortService,
    public dataHelperService: DataHelperService,
    public router: Router,
    public chartAllService: ChartAllService) {
  }

  Run(): void {
    this.state.DatePlayerStatModelList = [];

    this.Status = `${this.WorkingOnPlayerPoints[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};
    console.debug('Running player points');

    let ContactID: number = 0;
    if (this.state.DemoVisible) {
      ContactID = this.state.DemoUser.ContactID;
    }
    else {
      ContactID = this.state.User.ContactID;
    }

    let DailyPlayerStatModelList: PlayerStatModel[] = [];
    let WeeklyPlayerStatModelList: PlayerStatModel[] = [];
    let MonthlyPlayerStatModelList: PlayerStatModel[] = [];
    let PlayerPartnerList: PlayerPlayerListModel[] = [];
    let PlayerOpponentList: PlayerPlayerListModel[] = [];

    for (let j = 0, count = this.state.PlayerList.length; j < count; j++) {
      DailyPlayerStatModelList.push({
        PlayerID: this.state.PlayerList[j].ContactID,
        Points: 0,
        GamesPlayed: 0,
        GamesWon: 0,
        TotalNumberOfPartners: 0,
        TotalNumberOfOpponents: 0,
        AveragePlayerLevelOfPartners: 0.0,
        AveragePlayerLevelOfOpponents: 0.0
      });
      WeeklyPlayerStatModelList.push({
        PlayerID: this.state.PlayerList[j].ContactID,
        Points: 0,
        GamesPlayed: 0,
        GamesWon: 0,
        TotalNumberOfPartners: 0,
        TotalNumberOfOpponents: 0,
        AveragePlayerLevelOfPartners: 0.0,
        AveragePlayerLevelOfOpponents: 0.0
      });
      MonthlyPlayerStatModelList.push({
        PlayerID: this.state.PlayerList[j].ContactID,
        Points: 0,
        GamesPlayed: 0,
        GamesWon: 0,
        TotalNumberOfPartners: 0,
        TotalNumberOfOpponents: 0,
        AveragePlayerLevelOfPartners: 0.0,
        AveragePlayerLevelOfOpponents: 0.0
      });
      PlayerPartnerList.push({ PlayerID: this.state.PlayerList[j].ContactID, PlayerList: [] });
      PlayerOpponentList.push({ PlayerID: this.state.PlayerList[j].ContactID, PlayerList: [] });
    }

    let tempGameList: Game[] = [];
    for (let i = 0, count = this.state.GameList.length; i < count; i++) {
      if (this.dateService.InRange(this.state.GameList[i].GameDate, this.state.StartDate, this.state.EndDate)) {
        tempGameList.push(this.state.GameList[i]);
      }
    }

    tempGameList = this.sortService.SortGameByDateAscendingList(tempGameList);

    let oldDate: Date = new Date(2000, 1, 1);

    for (let i = 0, count = tempGameList.length; i < count; i++) {
      if (this.dataHelperService.GetDateFormat(oldDate) != this.dataHelperService.GetDateFormat(tempGameList[i].GameDate)) {
        let playerStatModelList: PlayerStatModel[] = [];
        for (let j = 0, count = this.state.PlayerList.length; j < count; j++) {
          playerStatModelList.push({
            PlayerID: this.state.PlayerList[j].ContactID,
            Points: 0,
            GamesPlayed: 0,
            GamesWon: 0,
            TotalNumberOfPartners: 0,
            TotalNumberOfOpponents: 0,
            AveragePlayerLevelOfPartners: 0.0,
            AveragePlayerLevelOfOpponents: 0.0
          });
        }

        this.state.DatePlayerStatModelList.push({ Date: tempGameList[i].GameDate, PlayerStatModelList: playerStatModelList });
        oldDate = tempGameList[i].GameDate;
      }
    }

    for (let i = 0, count = this.state.DatePlayerStatModelList.length; i < count; i++) {
      for (let j = 0, count = tempGameList.length; j < count; j++) {
        if (this.dataHelperService.GetDateFormat(this.state.DatePlayerStatModelList[i].Date) == this.dataHelperService.GetDateFormat(tempGameList[j].GameDate)) {
          let T1P1: Player = this.state.PlayerList.find(c => c.ContactID == tempGameList[j].Team1Player1) ?? <Player>{};
          let T1P2: Player = this.state.PlayerList.find(c => c.ContactID == tempGameList[j].Team1Player2) ?? <Player>{};
          let T2P1: Player = this.state.PlayerList.find(c => c.ContactID == tempGameList[j].Team2Player1) ?? <Player>{};
          let T2P2: Player = this.state.PlayerList.find(c => c.ContactID == tempGameList[j].Team2Player2) ?? <Player>{};

          if (!(T1P1 && T1P2 && T2P1 && T2P2)) {
            continue;
          }

          // T1P1
          if (!(PlayerPartnerList.find(c => c.PlayerID == T1P1.ContactID)?.PlayerList.find(c => c.ContactID == T1P2.ContactID))) {
            PlayerPartnerList.find(c => c.PlayerID == T1P1.ContactID)?.PlayerList.push(T1P2);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T1P1.ContactID)?.PlayerList.find(c => c.ContactID == T2P1.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T1P1.ContactID)?.PlayerList.push(T2P1);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T1P1.ContactID)?.PlayerList.find(c => c.ContactID == T2P2.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T1P1.ContactID)?.PlayerList.push(T2P2);
          }

          // T1P2
          if (!(PlayerPartnerList.find(c => c.PlayerID == T1P2.ContactID)?.PlayerList.find(c => c.ContactID == T1P1.ContactID))) {
            PlayerPartnerList.find(c => c.PlayerID == T1P2.ContactID)?.PlayerList.push(T1P1);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T1P2.ContactID)?.PlayerList.find(c => c.ContactID == T2P1.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T1P2.ContactID)?.PlayerList.push(T2P1);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T1P2.ContactID)?.PlayerList.find(c => c.ContactID == T2P2.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T1P2.ContactID)?.PlayerList.push(T2P2);
          }

          // T2P1
          if (!(PlayerPartnerList.find(c => c.PlayerID == T2P1.ContactID)?.PlayerList.find(c => c.ContactID == T2P2.ContactID))) {
            PlayerPartnerList.find(c => c.PlayerID == T2P1.ContactID)?.PlayerList.push(T2P2);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T2P1.ContactID)?.PlayerList.find(c => c.ContactID == T1P1.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T2P1.ContactID)?.PlayerList.push(T1P1);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T2P1.ContactID)?.PlayerList.find(c => c.ContactID == T1P2.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T2P1.ContactID)?.PlayerList.push(T1P2);
          }

          // T2P2
          if (!(PlayerPartnerList.find(c => c.PlayerID == T2P2.ContactID)?.PlayerList.find(c => c.ContactID == T2P1.ContactID))) {
            PlayerPartnerList.find(c => c.PlayerID == T2P2.ContactID)?.PlayerList.push(T2P1);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T2P2.ContactID)?.PlayerList.find(c => c.ContactID == T1P1.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T2P2.ContactID)?.PlayerList.push(T1P1);
          }
          if (!(PlayerOpponentList.find(c => c.PlayerID == T2P2.ContactID)?.PlayerList.find(c => c.ContactID == T1P2.ContactID))) {
            PlayerOpponentList.find(c => c.PlayerID == T2P2.ContactID)?.PlayerList.push(T1P2);
          }


          let gamePoints: GamePoints = this.GetGamePoints(tempGameList[j], T1P1, T1P2, T2P1, T2P2);

          DailyPlayerStatModelList.filter(c => c.PlayerID == T1P1.ContactID)[0].Points += gamePoints.Team1Player1Points;
          DailyPlayerStatModelList.filter(c => c.PlayerID == T1P2.ContactID)[0].Points += gamePoints.Team1Player2Points;
          DailyPlayerStatModelList.filter(c => c.PlayerID == T2P1.ContactID)[0].Points += gamePoints.Team2Player1Points;
          DailyPlayerStatModelList.filter(c => c.PlayerID == T2P2.ContactID)[0].Points += gamePoints.Team2Player2Points;
          DailyPlayerStatModelList.filter(c => c.PlayerID == T1P1.ContactID)[0].GamesPlayed += 1;
          DailyPlayerStatModelList.filter(c => c.PlayerID == T1P2.ContactID)[0].GamesPlayed += 1;
          DailyPlayerStatModelList.filter(c => c.PlayerID == T2P1.ContactID)[0].GamesPlayed += 1;
          DailyPlayerStatModelList.filter(c => c.PlayerID == T2P2.ContactID)[0].GamesPlayed += 1;

          if (tempGameList[j].Team1Scores > tempGameList[j].Team2Scores) {
            DailyPlayerStatModelList.filter(c => c.PlayerID == T1P1.ContactID)[0].GamesWon += 1;
            DailyPlayerStatModelList.filter(c => c.PlayerID == T1P2.ContactID)[0].GamesWon += 1;
          }
          else {
            DailyPlayerStatModelList.filter(c => c.PlayerID == T2P1.ContactID)[0].GamesWon += 1;
            DailyPlayerStatModelList.filter(c => c.PlayerID == T2P2.ContactID)[0].GamesWon += 1;
          }

          for (let k = 0, count = this.state.PlayerList.length; k < count; k++) {
            this.state.DatePlayerStatModelList[i].PlayerStatModelList.filter(c => c.PlayerID == this.state.PlayerList[k].ContactID).forEach((player) => {
              DailyPlayerStatModelList.filter(c => c.PlayerID == this.state.PlayerList[k].ContactID).forEach((totPlayer) => {
                player.Points = totPlayer.Points;
                player.GamesPlayed = totPlayer.GamesPlayed;
                player.GamesWon = totPlayer.GamesWon;
                PlayerPartnerList.filter(c => c.PlayerID == player.PlayerID).forEach((pp) => {
                  player.TotalNumberOfPartners = pp.PlayerList.length;
                  let PlayerLevelList: number[] = [];
                  pp.PlayerList.forEach((c) => {
                    PlayerLevelList.push(c.PlayerLevel);
                  });
                  const sum = PlayerLevelList.reduce((a, b) => a + b, 0);
                  player.AveragePlayerLevelOfPartners = (sum / PlayerLevelList.length) || 0;
                });
                PlayerOpponentList.filter(c => c.PlayerID == player.PlayerID).forEach((pp) => {
                  player.TotalNumberOfOpponents = pp.PlayerList.length;
                  let PlayerLevelList: number[] = [];
                  pp.PlayerList.forEach((c) => {
                    PlayerLevelList.push(c.PlayerLevel);
                  });
                  const sum = PlayerLevelList.reduce((a, b) => a + b, 0);
                  player.AveragePlayerLevelOfOpponents = (sum / PlayerLevelList.length) || 0;
                });
              });
            });
          }
        }
      }
    }

    this.state.CurrentPlayerDateID = this.state.DatePlayerStatModelList.length - 1;
    this.state.CurrentDatePlayerStatModelList = this.sortService.SortPlayerStatModelList(this.state.DatePlayerStatModelList[this.state.DatePlayerStatModelList.length - 1].PlayerStatModelList);

    this.chartAllService.RedrawDrawAllCharts();

    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};

    console.debug('Player points completed');
  }

  GetGamePoints(game: Game, T1P1: Player, T1P2: Player, T2P1: Player, T2P2: Player): GamePoints {
    let T1P1Points: number = 0;
    let T1P2Points: number = 0;
    let T2P1Points: number = 0;
    let T2P2Points: number = 0;


    if (game.Team1Scores > game.Team2Scores) {
      T1P1Points = this.state.CurrentLeague.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T1P1', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T1P1', game.Team1Scores, game.Team2Scores);
      T1P2Points = this.state.CurrentLeague.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T1P2', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T1P2', game.Team1Scores, game.Team2Scores);
      T2P1Points = this.state.CurrentLeague.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T2P1', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T2P1', game.Team1Scores, game.Team2Scores);
      T2P2Points = this.state.CurrentLeague.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T2P2', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T2P2', game.Team1Scores, game.Team2Scores);
    }
    else {
      T1P1Points = this.state.CurrentLeague.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T1P1', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T1P1', game.Team1Scores, game.Team2Scores);
      T1P2Points = this.state.CurrentLeague.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T1P2', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T1P2', game.Team1Scores, game.Team2Scores);
      T2P1Points = this.state.CurrentLeague.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T2P1', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T2P1', game.Team1Scores, game.Team2Scores);
      T2P2Points = this.state.CurrentLeague.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T2P2', T1P1, T1P2, T2P1, T2P2) + this.GetPointsFromPercentPointsFactor('T2P2', game.Team1Scores, game.Team2Scores);
    }

    return <GamePoints>{
      Team1Player1Points: T1P1Points,
      Team1Player2Points: T1P2Points,
      Team2Player1Points: T2P1Points,
      Team2Player2Points: T2P2Points,
    };
  }

  private GetPointsFromPlayerLevelFactor(player: 'T1P1' | 'T1P2' | 'T2P1' | 'T2P2', T1P1: Player, T1P2: Player, T2P1: Player, T2P2: Player): number {
    let Team1PlayerAvg: number = (+T1P1.PlayerLevel + +T1P2.PlayerLevel) / 2.0;
    let Team2PlayerAvg: number = (+T2P1.PlayerLevel + +T2P2.PlayerLevel) / 2.0;

    switch (player) {
      case 'T1P1':
        {
          let points: number = (5 - Team1PlayerAvg) * this.state.CurrentLeague.PlayerLevelFactor;
          return points;
        }
      case 'T1P2':
        {
          let points: number = (5 - Team1PlayerAvg) * this.state.CurrentLeague.PlayerLevelFactor;
          return points;
        }
      case 'T2P1':
        {
          let points: number = (5 - Team2PlayerAvg) * this.state.CurrentLeague.PlayerLevelFactor;
          return points;
        }
      case 'T2P2':
        {
          let points: number = (5 - Team2PlayerAvg) * this.state.CurrentLeague.PlayerLevelFactor;
          return points;
        }
      default:
        {
          return 0;
        }
    }
  }

  private GetPointsFromPercentPointsFactor(player: 'T1P1' | 'T1P2' | 'T2P1' | 'T2P2', T1Scores: number, T2Scores: number): number {
    let Team1PercentagePoint: number = T1Scores / (T1Scores + T2Scores);
    let Team2PercentagePoint: number = T2Scores / (T1Scores + T2Scores);

    switch (player) {
      case 'T1P1':
        {
          let points: number = Team1PercentagePoint * this.state.CurrentLeague.PercentPointsFactor;
          return points;
        }
      case 'T1P2':
        {
          let points: number = Team1PercentagePoint * this.state.CurrentLeague.PercentPointsFactor;
          return points;
        }
      case 'T2P1':
        {
          let points: number = Team2PercentagePoint * this.state.CurrentLeague.PercentPointsFactor;
          return points;
        }
      case 'T2P2':
        {
          let points: number = Team2PercentagePoint * this.state.CurrentLeague.PercentPointsFactor;
          return points;
        }
      default:
        {
          return 0;
        }
    }
  }
}


