import { Injectable } from '@angular/core';
import { GetLanguageEnum, LanguageEnum } from 'src/app/enums/LanguageEnum';
import { AscDescEnum } from '../enums/AscDescEnum';
import { DatePlayerStatModel } from '../models/DatePlayerStatModel.model';
import { Game } from '../models/Game.model';
import { League } from '../models/League.model';
import { LeagueContact } from '../models/LeagueContact.model';
import { LeaguePointExampleModel } from '../models/LeaguePointExampleModel.model';
//import { LeagueStatsModel } from '../models/LeagueStatsModel';
import { Player } from '../models/Player.model';
import { PlayerGameModel } from '../models/PlayerGameModel';
import { PlayerStatModel } from '../models/PlayerStatModel.model';
import { User } from '../models/User.model';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  Version: string[] = ['Version: alpha-1.0.0.0', 'Version: alpha-1.0.0.0'];

  BaseApiUrl = 'https://pball.azurewebsites.net/api/'; 
  //BaseApiUrl = 'https://localhost:7072/api/';

  languageEnum = GetLanguageEnum();
  Language: LanguageEnum = LanguageEnum.en;
  LangID: number = 0;
  Culture: string = 'en-CA';

  CurrentCols: string = '1';
  Screen: 'Small' | 'Large' = 'Small';

  // Demo var
  DemoEndDate: Date = new Date();
  DemoLeagueID: number = 0;
  DemoStartDate: Date = new Date(2020, 1, 1);
  DemoUser: User = <User>{};
  DemoIsAdmin: boolean = true;
  
  // Reality var
  EndDate: Date = new Date();
  LeagueID: number = 0;
  StartDate: Date = new Date(2020, 1, 1);
  User: User = <User>{};

  CurrentLeague: League = <League>{};
  LeagueTabsIndex: number = 0;

  ProgressTabsIndex: number = 0;

  ReturnToPage: string = '';
  ReturnToPage2: string = '';

  GameList: Game[] = [];
  LeagueContactList: LeagueContact[] = [];
  LeagueList: League[] = [];
  PlayerGameModelList: PlayerGameModel[] = [];
  PlayerGameModelSortProp: 'Partner' | 'Opponents' | 'GameDate' | 'Scores' = 'GameDate';
  PlayerGameModelSortAscDesc: AscDescEnum = AscDescEnum.Descending;
  PlayerList: Player[] = [];
  PlayerListSortProp: 'FullName' = 'FullName';
  PlayerListSortAscDesc: AscDescEnum = AscDescEnum.Ascending;

  DemoExtraPlayerList: Player[] = [];
  
  DemoVisible: boolean = false;
  DataTime: 'day' | 'week' | 'month' | 'year' | 'all' | 'between' = 'year';

  DemoHomeTabIndex: number = 0;
  HomeTabIndex: number = 0;

  LeaguePointExampleModelList: LeaguePointExampleModel[] = [];
  DatePlayerStatModelList: DatePlayerStatModel[] = [];

  CurrentDatePlayerStatModelList: PlayerStatModel[] = [];
  CurrentPlayerDateID: number = 0;

  PlayerStatModelSortAscDesc: AscDescEnum = AscDescEnum.Descending;
  PlayerStatModelSortProp: 'FullName' | 'GamesPlayed' | 'Points' | 'GamesWon' 
  | 'WinningPercentage' | 'TotalNumberOfPartners' | 'TotalNumberOfOpponents'
  | 'AveragePlayerLevelOfPartners' | 'AveragePlayerLevelOfOpponents' | '' = '';

  SearchPlayerList: Player[] = [];
  
  constructor() {

  }

  ClearDemoLocalStorage() {
    localStorage.removeItem('DemoUser');
    localStorage.removeItem('DemoLeagueID');
    localStorage.removeItem('DemoVisible');
    localStorage.removeItem('DemoStartDate');
    localStorage.removeItem('DemoEndDate');
    localStorage.removeItem('DemoHomeTabIndex');
    localStorage.removeItem('DemoIsAdmin');
    localStorage.removeItem('LeagueTabsIndex');
    localStorage.removeItem('ProgressTabsIndex');
  }

  ClearLocalStorage() {
    localStorage.removeItem('User');
    localStorage.removeItem('LeagueID');
    localStorage.removeItem('DemoVisible');
    localStorage.removeItem('StartDate');
    localStorage.removeItem('EndDate');
    localStorage.removeItem('HomeTabIndex');
    localStorage.removeItem('LeagueTabsIndex');
    localStorage.removeItem('ProgressTabsIndex');
  }

  ClearDemoData() {
    this.DemoVisible = false;
    this.DemoEndDate = new Date();
    this.GameList = [];
    this.LeagueContactList = [];
    this.DemoLeagueID = 0;
    this.LeagueList = [];
    this.DatePlayerStatModelList = [];
    this.CurrentDatePlayerStatModelList = [];
    this.PlayerGameModelList = [];
    this.PlayerList = [];
    this.DemoStartDate = new Date(2020, 1, 1);
    this.DemoUser = <User>{};
  }

  ClearData() {
    this.DemoVisible = false;
    this.EndDate = new Date();
    this.GameList = [];
    this.LeagueContactList = [];
    this.LeagueID = 0;
    this.LeagueList = [];
    this.DatePlayerStatModelList = [];
    this.CurrentDatePlayerStatModelList = [];
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
