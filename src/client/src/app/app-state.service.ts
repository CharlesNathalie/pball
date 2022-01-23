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

  //BaseApiUrl = 'https://pball.azurewebsites.net/api/'; 
  BaseApiUrl = 'https://localhost:7072/api/';

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

  // Reality var
  EndDate: Date = new Date();
  LeagueID: number = 0;
  StartDate: Date = new Date(2020, 1, 1);
  User: User = <User>{};

  GameList: Game[] = [];
  LeagueContactList: LeagueContact[] = [];
  LeagueList: League[] = [];
  LeagueStatsModelList: LeagueStatsModel[] = [];
  LeagueStatsModelSortProp: 'FullName' | 'NumberOfGames' | 'NumberOfWins' | 'WinningPercentage' = 'NumberOfGames';
  LeagueStatsModelSortAscDesc: AscDescEnum = AscDescEnum.Descending;
  PlayerGameModelList: PlayerGameModel[] = [];
  PlayerGameModelSortProp: 'Partner' | 'Opponents' | 'GameDate' | 'Scores' = 'GameDate';
  PlayerGameModelSortAscDesc: AscDescEnum = AscDescEnum.Descending;
  PlayerList: Player[] = [];
  
  DemoVisible: boolean = false;
  DataTime: 'day' | 'week' | 'month' | 'year' | 'all' | 'between' = 'year';

  DemoHomeTabIndex: number = 0;
  HomeTabIndex: number = 0;
  HomeTabCount: number = 4;

  constructor() {

  }

  ClearDemoLocalStorage()
  {
    localStorage.removeItem('DemoUser');
    localStorage.removeItem('DemoLeagueID');
    localStorage.removeItem('DemoVisible');
    localStorage.removeItem('DemoStartDate');
    localStorage.removeItem('DemoEndDate');
    localStorage.removeItem('DemoHomeTabIndex');
  }

  ClearLocalStorage()
  {
    localStorage.removeItem('User');
    localStorage.removeItem('LeagueID');
    localStorage.removeItem('DemoVisible');
    localStorage.removeItem('StartDate');
    localStorage.removeItem('EndDate');
    localStorage.removeItem('HomeTabIndex');
  }

  ClearDemoData() {
    this.DemoVisible = false;
    this.DemoEndDate = new Date();
    this.GameList = [];
    this.LeagueContactList = [];
    this.DemoLeagueID = 0;
    this.LeagueList = [];
    this.LeagueStatsModelList = [];
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
