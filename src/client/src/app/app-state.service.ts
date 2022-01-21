import { Injectable } from '@angular/core';
import { GetLanguageEnum, LanguageEnum } from 'src/app/enums/LanguageEnum';
import { AscDescEnum } from './enums/AscDescEnum';
import { Game } from './models/Game.model';
import { League } from './models/League.model';
import { LeagueContact } from './models/LeagueContact.model';
import { LeagueStatsModel } from './models/LeagueStatsModel';
import { Player } from './models/Player.model';
import { PlayerGameModel } from './models/PlayerGameModel';
import { User } from './models/User.model';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  Version: string[] = ['Version: alpha-1.0.0.0', 'Version: alpha1.0.0.0'];

  BaseApiUrl = 'https://pball.azurewebsites.net/api/'; 
  //BaseApiUrl = 'https://localhost:7072/api/';

  languageEnum = GetLanguageEnum();
  Language: LanguageEnum = LanguageEnum.en;
  LangID: number = 0;
  Culture: string = 'en-CA';

  CurrentCols: string = '1';
  Screen: 'Small' | 'Large' = 'Small';

  EndDate: Date = new Date();
  GameList: Game[] = [];
  LeagueContactList: LeagueContact[] = [];
  LeagueID: number = 0;
  LeagueList: League[] = [];
  LeagueStatsModelList: LeagueStatsModel[] = [];
  LeagueStatsModelSortProp: 'FullName' | 'NumberOfGames' | 'NumberOfWins' | 'WinningPercentage' = 'NumberOfGames';
  LeagueStatsModelSortAscDesc: AscDescEnum = AscDescEnum.Descending;
  PlayerGameModelList: PlayerGameModel[] = [];
  PlayerGameModelSortProp: 'Partner' | 'Opponents' | 'GameDate' | 'Scores' = 'GameDate';
  PlayerGameModelSortAscDesc: AscDescEnum = AscDescEnum.Descending;
  PlayerList: Player[] = [];
  DemoVisible: boolean = false;
  StartDate: Date = new Date(2020, 1, 1);
  User: User = <User>{};
  DataTime: 'day' | 'week' | 'month' | 'year' | 'all' | 'between' = 'month';

  constructor() {

  }

  ClearLocalStorage()
  {
    localStorage.removeItem('User');
    localStorage.removeItem('LeagueID');
    localStorage.removeItem('DemoVisible');
    localStorage.removeItem('StartDate');
    localStorage.removeItem('EndDate');
  }

  ClearData() {
    this.DemoVisible = false;
    this.EndDate = new Date();
    this.GameList = [];
    this.LeagueContactList = [];
    this.LeagueID = 0;
    this.LeagueList = [];
    this.LeagueStatsModelList = [];
    this.PlayerGameModelList = [];
    this.PlayerList = [];
    this.StartDate = new Date(2020, 1, 1);
    this.User = <User>{};  
  }
  
  SetLanguage(Language: LanguageEnum) {
    if (Language == LanguageEnum.fr) {
      this.Language = LanguageEnum.fr;
      this.LangID = LanguageEnum.fr - 1;
      this.Culture = 'fr-CA';
    }
    else {
      this.Language = LanguageEnum.en;
      this.LangID = LanguageEnum.en - 1;
      this.Culture = 'en-CA';
    }
  }
}
