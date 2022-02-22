import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { Game } from 'src/app/models/Game.model';
import { PlayerGameModel } from 'src/app/models/PlayerGameModel';
import { DateService } from './date.service';
import { SortService } from './sort.service';
import { DataHelperService } from './data-helper.service';
import { AscDescEnum } from '../enums/AscDescEnum';
import { PartnerWinsModel } from '../models/PartnerWins.model';

@Injectable({
  providedIn: 'root'
})
export class DataPlayerGamesService {
  WorkingOnPlayerGames: string[] = ['Working on player games', 'Travail sur les parties du joueur'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  DataPlayerGamesSuccess: boolean = false;

  constructor(public state: AppStateService,
    public dateService: DateService,
    public sortService: SortService,
    public dataHelperService: DataHelperService) {
  }

  Run(): void {
    this.state.PlayerGameModelList = [];

    this.Status = `${this.WorkingOnPlayerGames[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};
    console.debug('Running player games');

    let ContactID: number = 0;
    if (this.state.DemoVisible) {
      ContactID = this.state.DemoUser.ContactID;
    }
    else {
      ContactID = this.state.User.ContactID;
    }
    let tempGameList: Game[] = [];
    for (let i = 0, count = this.state.GameList.length; i < count; i++) {
      if (this.dateService.InRange(this.state.GameList[i].GameDate, this.state.StartDate, this.state.EndDate)) {
        if (this.state.GameList[i].Team1Player1 == ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
        if (this.state.GameList[i].Team1Player2 == ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
        if (this.state.GameList[i].Team2Player1 == ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
        if (this.state.GameList[i].Team2Player2 == ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
      }
    }

    let PlayerGameModelList: PlayerGameModel[] = [];
    let PartnerWinsModelList: PartnerWinsModel[] = [];
    if (tempGameList.length > 0) {
      for (let i = 0, count = tempGameList.length; i < count; i++) {
        let playerGameModel: PlayerGameModel = <PlayerGameModel>{};

        playerGameModel.GameID = tempGameList[i].GameID;
        playerGameModel.GameDate = tempGameList[i].GameDate;

        if (tempGameList[i].Team1Player1 == ContactID) {
          playerGameModel.PartnerID = tempGameList[i].Team1Player2;
          playerGameModel.PartnerFullName = this.dataHelperService.GetPlayerFullName(playerGameModel.PartnerID);
          playerGameModel.PartnerLastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.PartnerID);
          playerGameModel.Scores = tempGameList[i].Team1Scores;
          playerGameModel.Opponent1ID = tempGameList[i].Team2Player1;
          playerGameModel.Opponent1FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent1ID);
          playerGameModel.Opponent1LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent1ID);
          playerGameModel.Opponent2ID = tempGameList[i].Team2Player2;
          playerGameModel.Opponent2FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent2ID);
          playerGameModel.Opponent2LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent2ID);
          playerGameModel.OpponentScores = tempGameList[i].Team2Scores;
        }
        else if (tempGameList[i].Team1Player2 == ContactID) {
          playerGameModel.PartnerID = tempGameList[i].Team1Player1;
          playerGameModel.PartnerFullName = this.dataHelperService.GetPlayerFullName(playerGameModel.PartnerID);
          playerGameModel.PartnerLastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.PartnerID);
          playerGameModel.Scores = tempGameList[i].Team1Scores;
          playerGameModel.Opponent1ID = tempGameList[i].Team2Player1;
          playerGameModel.Opponent1FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent1ID);
          playerGameModel.Opponent1LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent1ID);
          playerGameModel.Opponent2ID = tempGameList[i].Team2Player2;
          playerGameModel.Opponent2FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent2ID);
          playerGameModel.Opponent2LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent2ID);
          playerGameModel.OpponentScores = tempGameList[i].Team2Scores;
        }
        else if (tempGameList[i].Team2Player1 == ContactID) {
          playerGameModel.PartnerID = tempGameList[i].Team2Player2;
          playerGameModel.PartnerFullName = this.dataHelperService.GetPlayerFullName(playerGameModel.PartnerID);
          playerGameModel.PartnerLastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.PartnerID);
          playerGameModel.Scores = tempGameList[i].Team2Scores;
          playerGameModel.Opponent1ID = tempGameList[i].Team1Player1;
          playerGameModel.Opponent1FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent1ID);
          playerGameModel.Opponent1LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent1ID);
          playerGameModel.Opponent2ID = tempGameList[i].Team1Player2;
          playerGameModel.Opponent2FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent2ID);
          playerGameModel.Opponent2LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent2ID);
          playerGameModel.OpponentScores = tempGameList[i].Team1Scores;
        }
        else if (tempGameList[i].Team2Player2 == ContactID) {
          playerGameModel.PartnerID = tempGameList[i].Team2Player1;
          playerGameModel.PartnerFullName = this.dataHelperService.GetPlayerFullName(playerGameModel.PartnerID);
          playerGameModel.PartnerLastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.PartnerID);
          playerGameModel.Scores = tempGameList[i].Team2Scores;
          playerGameModel.Opponent1ID = tempGameList[i].Team1Player1;
          playerGameModel.Opponent1FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent1ID);
          playerGameModel.Opponent1LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent1ID);
          playerGameModel.Opponent2ID = tempGameList[i].Team1Player2;
          playerGameModel.Opponent2FullName = this.dataHelperService.GetPlayerFullName(playerGameModel.Opponent2ID);
          playerGameModel.Opponent2LastNameAndFirstNameInit = this.dataHelperService.GetPlayerLastNameAndFirstNameInit(playerGameModel.Opponent2ID);
          playerGameModel.OpponentScores = tempGameList[i].Team1Scores;
        }

        PlayerGameModelList.push(playerGameModel);
      }
    }

    for (let i = 0, count1 = PlayerGameModelList.length; i < count1; i++) {
      let found: boolean = false;
      for (let j = 0, count2 = PartnerWinsModelList.length; j < count2; j++) {
        if (PartnerWinsModelList[j].PartnerID == PlayerGameModelList[i].PartnerID) {
          PartnerWinsModelList[j].Games += 1;
          PartnerWinsModelList[j].Wins += PlayerGameModelList[i].Scores > PlayerGameModelList[i].OpponentScores ? 1 : 0;
          PartnerWinsModelList[j].PlusMinus += PlayerGameModelList[i].Scores;
          PartnerWinsModelList[j].PlusMinus -= PlayerGameModelList[i].OpponentScores;
          found = true;
          break;
        }
      }

      if (!found) {
        PartnerWinsModelList.push({
          PartnerID: PlayerGameModelList[i].PartnerID,
          Partner: PlayerGameModelList[i].PartnerFullName,
          Games: 1,
          Wins: PlayerGameModelList[i].Scores > PlayerGameModelList[i].OpponentScores ? 1 : 0,
          PlusMinus: PlayerGameModelList[i].Scores - PlayerGameModelList[i].OpponentScores,
        });
      }
    }

    this.state.PartnerWinsModelList = this.sortService.SortPartnerWinsModelList(PartnerWinsModelList);
    this.state.PlayerGameModelList = this.sortService.SortPlayerGameModelList(PlayerGameModelList);

    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};

    console.debug('Player games completed');
  }
}
