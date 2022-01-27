import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { Game } from 'src/app/models/Game.model';
import { DateService } from './date.service';
import { SortService } from './sort.service';
import { DataHelperService } from './data-helper.service';
import { PlayerPointsModel } from '../models/PlayerPointsModel.model';
import { Player } from '../models/Player.model';
import { GamePoints } from '../models/GamePoints.model';
import { DatePlayerPointsModel } from '../models/DatePlayerPointsModel.model';

@Injectable({
  providedIn: 'root'
})
export class DataPlayerPointsService {
  WorkingOnPlayerPoints: string[] = ['Working on player points', 'Travail sur les points du joueur'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  DataPlayerPointsSuccess: boolean = false;

  constructor(public state: AppStateService,
    public dateService: DateService,
    public sortService: SortService,
    public dataHelperService: DataHelperService) {
  }

  Run(): void {
    this.state.DatePlayerPointsModelList = [];

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

    let TotalPlayerPointsModelList: PlayerPointsModel[] = [];

    for (let j = 0, count = this.state.PlayerList.length; j < count; j++) {
      TotalPlayerPointsModelList.push({ PlayerID: this.state.PlayerList[j].ContactID, Points: 0 });
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
        let playerPointsModelList: PlayerPointsModel[] = [];
        for (let j = 0, count = this.state.PlayerList.length; j < count; j++) {
          playerPointsModelList.push({ PlayerID: this.state.PlayerList[j].ContactID, Points: 0 });
        }

        this.state.DatePlayerPointsModelList.push({ Date: tempGameList[i].GameDate, PlayerPointsModelList: playerPointsModelList });
        oldDate = tempGameList[i].GameDate;
      }
    }

    for (let i = 0, count = this.state.DatePlayerPointsModelList.length; i < count; i++) {
      for (let j = 0, count = tempGameList.length; j < count; j++) {
        if (this.dataHelperService.GetDateFormat(this.state.DatePlayerPointsModelList[i].Date) == this.dataHelperService.GetDateFormat(tempGameList[j].GameDate)) {
          let T1P1: Player = this.state.PlayerList.filter(c => c.ContactID == tempGameList[j].Team1Player1)[0] ?? <Player>{};
          let T1P2: Player = this.state.PlayerList.filter(c => c.ContactID == tempGameList[j].Team1Player2)[0] ?? <Player>{};
          let T2P1: Player = this.state.PlayerList.filter(c => c.ContactID == tempGameList[j].Team2Player1)[0] ?? <Player>{};
          let T2P2: Player = this.state.PlayerList.filter(c => c.ContactID == tempGameList[j].Team2Player2)[0] ?? <Player>{};

          if (!(T1P1 && T1P2 && T2P1 && T2P2)) {
            continue;
          }

          let gamePoints: GamePoints = this.GetGamePoints(tempGameList[j], T1P1, T1P2, T2P1, T2P2);

          TotalPlayerPointsModelList.filter(c => c.PlayerID == T1P1.ContactID)[0].Points += gamePoints.Team1Player1Points;
          TotalPlayerPointsModelList.filter(c => c.PlayerID == T1P2.ContactID)[0].Points += gamePoints.Team1Player2Points;
          TotalPlayerPointsModelList.filter(c => c.PlayerID == T2P1.ContactID)[0].Points += gamePoints.Team2Player1Points;
          TotalPlayerPointsModelList.filter(c => c.PlayerID == T2P2.ContactID)[0].Points += gamePoints.Team2Player2Points;

          for (let k = 0, count = this.state.PlayerList.length; k < count; k++) {
            this.state.DatePlayerPointsModelList[i].PlayerPointsModelList.filter(c => c.PlayerID == this.state.PlayerList[k].ContactID)[0].Points =
              TotalPlayerPointsModelList.filter(c => c.PlayerID == this.state.PlayerList[k].ContactID)[0].Points;
          }
        }
      }
    }

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


