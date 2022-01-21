import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { Game } from 'src/app/models/Game.model';
import { PlayerGameModel } from 'src/app/models/PlayerGameModel';
import { DateService } from '../date/date.service';
import { SortService } from '../sort/sort.service';
import { DataHelperService } from './data-helper.service';

@Injectable({
  providedIn: 'root'
})
export class DataPlayerGamesService {
  WorkingOnPlayerGames: string[] = ['Working on player games', 'Travail sur les parties du joueur'];

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
    this.state.PlayerGameModelList = [];

    this.Status = `${this.WorkingOnPlayerGames[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};
    console.debug('Running player games');

    let tempGameList: Game[] = [];
    for (let i = 0, count = this.state.GameList.length; i < count; i++) {
      if (this.dateService.InRange(this.state.GameList[i].GameDate, this.state.StartDate, this.state.EndDate)) {
        if (this.state.GameList[i].Team1Player1 == this.state.User.ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
        if (this.state.GameList[i].Team1Player2 == this.state.User.ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
        if (this.state.GameList[i].Team2Player1 == this.state.User.ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
        if (this.state.GameList[i].Team2Player2 == this.state.User.ContactID) {
          tempGameList.push(this.state.GameList[i]);
        }
      }
    }
    let PlayerGameModelList: PlayerGameModel[] = [];
    if (tempGameList.length > 0) {
      for (let i = 0, count = tempGameList.length; i < count; i++) {
        let playerGameModel: PlayerGameModel = <PlayerGameModel>{};

        playerGameModel.GameID = tempGameList[i].GameID;
        playerGameModel.GameDate = tempGameList[i].GameDate;

        if (tempGameList[i].Team1Player1 == this.state.User.ContactID) {
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
        else if (tempGameList[i].Team1Player2 == this.state.User.ContactID) {
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
        else if (tempGameList[i].Team2Player1 == this.state.User.ContactID) {
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
        else if (tempGameList[i].Team2Player2 == this.state.User.ContactID) {
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

    this.state.PlayerGameModelList = this.sortService.SortPlayerGameModelList(PlayerGameModelList);

    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};

    console.debug('Player games completed');
  }
}
