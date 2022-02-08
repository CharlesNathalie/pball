import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { Game } from 'src/app/models/Game.model';
import { League } from 'src/app/models/League.model';
import { LeagueContact } from 'src/app/models/LeagueContact.model';
import { Player } from 'src/app/models/Player.model';
import { User } from 'src/app/models/User.model';
import * as moment from 'moment';
import { SortService } from './sort.service';
import { ProgressService } from './progress.service';

@Injectable({
  providedIn: 'root'
})
export class DemoDataService {
  GeneratingDemoData: string[] = ['Generating demo data', 'Génération de données de démonstration'];
  ChangesWillNotBePermanantlySavedOnTheServer: string[] = ['Changes will not be permanantly saved on the server.', 'Les changements ne seront pas sauvegardé de manière permanante au serveur.'];
  DemoVersionRunning: string[] = ['Demo version running.', 'Version démo en cours.'];

  private NumberOfPlayers: number = 25;
  private NumberOfGames: number = 500;
  private NumberOfLeagues: number = 1;
  private NumberOfPlayersPerLeague: number = 25;

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  private firstNameList: string[] = [
    "Rouge", "Red", "Orange", "Violet", "Purple", "Brun", "Brown", "Rose", "Pink", "Bleu", "Blue",
    "Vert", "Green", "BleuCiel", "LightBlue", "BleuMarin", "DarkBlue", "Noir", "Black", "Blanc", "White",
    "Maron", "Turquoise", "RougeVin", "Jaune", "Yellow", "JauneBanane", "Gris", "Gray", "GrisPale", "LightGray",
    "GrisFoncé", "Silver", "Lime", "Aqua"
  ];

  private lastNameList: string[] = [
    "Allain", "Breau", "Boudreau", "Cormier", "Smith", "LeBlanc", "Cormier", "Vautour", "Brown", "Tremblay",
    "Martin", "Roy", "King", "Gagnon", "Lee", "Wilson", "Johnson", "MacDonald", "Taylor", "Campbell", "White",
    "Young", "Bouchard", "Scott", "Stewart", "Pelletier", "Lavoie", "More", "Miller", "Coté", "Bélanger", "Robinson",
    "Landry", "Poirier", "Thomas", "Richard", "Clarke", "Davis", "Evans", "Grant"
  ];

  private emailList: string[] = [
    "gmail.com", "yahoo.com", "gnb.ca", "canada.ca", "live.com", "nb.sympatico.ca", "umoncton.ca",
    "hotmail.com", "rogers.com", "nbed.nb.ca", "me.com"
  ];

  private initialList = [
    "A", "B", "C", "D", "E", "F",
    "G", "H", "I", "J", "K", "L",
    "M", "N", "O", "P", "Q", "R",
    "S", "T", "U", "V", "W", "X",
    "Y", "Z",
  ];

  private LeagueDayList: number[] = [];

  GenerateDemoDataSuccess: boolean = false;

  constructor(public state: AppStateService,
    public sortService: SortService,
    public progressService: ProgressService) {
  }

  GenerateDemoData() {
    this.Status = `${this.GeneratingDemoData[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.state.PlayerList = [];
    this.state.LeagueList = [];
    this.state.LeagueContactList = [];
    this.state.GameList = [];

    this.GenerateDemoDataPlayerList();
    this.GenerateDemoDataDemoExtraPlayerList();
    this.GenerateDemoDataLeagueList();
    this.GenerateDemoDataLeagueContactList();
    this.GenerateDemoDataGameList();

    this.state.DemoLeagueID = 1;
    this.state.CurrentLeague = this.state.LeagueList[0];
    this.state.DemoVisible = true;
    if (this.state.DemoIsAdmin)
    {
      this.state.DemoUser = <User>{ ...this.state.PlayerList.find(c => c.ContactID == this.state.LeagueContactList.find(c => c.IsLeagueAdmin == true)?.ContactID) };
    }
    else{
      this.state.DemoUser = <User>{ ...this.state.PlayerList.find(c => c.ContactID == this.state.LeagueContactList.find(c => c.IsLeagueAdmin == false)?.ContactID) };
    }

    localStorage.setItem('DemoUser', JSON.stringify(this.state.DemoUser));
    localStorage.setItem('DemoLeagueID', JSON.stringify(this.state.DemoLeagueID));
    localStorage.setItem('DemoVisible', JSON.stringify(true));
    localStorage.setItem('DemoHomeTabIndex', JSON.stringify(this.state.DemoHomeTabIndex));
    localStorage.setItem('DemoIsAdmin', JSON.stringify(this.state.DemoIsAdmin));
  
    this.progressService.Period('year');
  }

  GenerateDemoDataGameList() {
    for (let j = 0; j < this.NumberOfLeagues; j++) {
      let gameID: number = 1;
      for (let i = 0; i < this.NumberOfGames; i++) {
        let gameContactIDList: number[] = [];
        let gameContactLevelList: number[] = [];

        let done: boolean = false;
        while (!done) {
          let ContactID: number = this.state.LeagueContactList[Math.floor(Math.random() * (this.NumberOfPlayersPerLeague))].ContactID;
          let exist: number = gameContactIDList.filter(c => c == ContactID).length;
          if (exist == 0) {
            gameContactIDList.push(ContactID);
            gameContactLevelList.push(this.state.PlayerList.filter(c => c.ContactID == ContactID)[0].PlayerLevel);
          }

          if (gameContactIDList.length > 3) {
            done = true;
          }
        }

        let Team1AvgLevel: number = (gameContactLevelList[0] + gameContactLevelList[1]) / 2.0;
        let Team2AvgLevel: number = (gameContactLevelList[2] + gameContactLevelList[3]) / 2.0;

        let Team1ChangeOfWinning: number = 100 * Team1AvgLevel / (Team1AvgLevel + Team2AvgLevel);

        let Team1Won: boolean = Math.floor(Math.random() * (100 + 1)) > Team1ChangeOfWinning ? true : false;

        let To9Point: boolean = Math.floor(Math.random() * (100 + 1)) > 60 ? true : false;

        let Score1: number = 1;
        let Score2: number = 1;

        if (Team1Won) {
          if (To9Point) {
            Score1 = 9;
            Score2 = Math.floor(Math.random() * (7 + 1));
          }
          else {
            Score1 = 11;
            Score2 = Math.floor(Math.random() * (9 + 1));
          }
        }
        else {
          if (To9Point) {
            Score2 = 9;
            Score1 = Math.floor(Math.random() * (7 + 1));
          }
          else {
            Score2 = 11;
            Score1 = Math.floor(Math.random() * (9 + 1));
          }
        }

        let GameDate: Date = new Date();
        let doneDate: boolean = false;
        let backNumberOfDays: number = 0;
        while (!doneDate) {
          backNumberOfDays = Math.floor(Math.random() * 400) * (-1);

          if (moment(new Date()).add(backNumberOfDays, 'days').day() == this.LeagueDayList[j]) {
            doneDate = true;
          }
        }

        this.state.GameList.push(<Game>
          {
            GameID: gameID,
            LeagueID: this.state.LeagueList[j].LeagueID,
            Team1Player1: gameContactIDList[0],
            Team1Player2: gameContactIDList[1],
            Team2Player1: gameContactIDList[2],
            Team2Player2: gameContactIDList[3],
            GameDate: moment(new Date()).add(backNumberOfDays, 'days').toDate(),
            Team1Scores: Score1,
            Team2Scores: Score2,
            Removed: false,
            LastUpdateContactID: gameContactIDList[0],
            LastUpdateDate_UTC: GameDate,
          });

        gameID += 1;
      }

    }
  }

  GenerateDemoDataLeagueList() {
    let dayENList: string[] = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    let dayFRList: string[] = ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi'];

    let morningAfternoonNightEN: string[] = ['morning', 'afternoon', 'night'];
    let morningAfternoonNightFR: string[] = ['matin', 'après-midi', 'soir'];

    for (let i = 0; i < this.NumberOfLeagues; i++) {
      this.state.LeagueList.push(<League>{
        LeagueID: i + 1,
        LeagueName: '',
        PointsToWinners: 3,
        PointsToLosers: 1,
        PercentPointsFactor: 0.5,
        PlayerLevelFactor: 0.5,
        Removed: false,
        LastUpdateContactID: this.state.PlayerList[0].ContactID,
        LastUpdateDate_UTC: new Date(),
      });

      let done: boolean = false;
      while (!done) {
        this.LeagueDayList[i] = Math.floor(Math.random() * (dayENList.length));

        if (this.state.LangID == 1) // fr
        {
          this.state.LeagueList[i].LeagueName = `${dayFRList[this.LeagueDayList[i]]} ${morningAfternoonNightFR[Math.floor(Math.random() * (morningAfternoonNightFR.length))]} pickleball`;
        }
        else {
          this.state.LeagueList[i].LeagueName = `${dayENList[this.LeagueDayList[i]]} ${morningAfternoonNightEN[Math.floor(Math.random() * (morningAfternoonNightEN.length))]} pickleball`;
        }

        if (i == 0) {
          done = true;
        }

        if (i == 1 && this.LeagueDayList[0] != this.LeagueDayList[1]) {
          done = true;
        }
      }
    }
  }

  GenerateDemoDataLeagueContactList() {
    let leagueContactID: number = 1;
    let maxLeagueAdminCount: number = 3;
    let IsLeagueAdmin: boolean = true;

    for (let j = 0; j < this.NumberOfLeagues; j++) {
      for (let i = 0; i < this.NumberOfPlayersPerLeague; i++) {
        let done: boolean = false;
        let Active: boolean = Math.floor(Math.random() * 10) > 3;
        let PlayingToday: boolean = Math.floor(Math.random() * 10) > 3;
        while (!done) {
          let ContactID: number = this.state.PlayerList[Math.floor(Math.random() * this.state.PlayerList.length)].ContactID;
          let count = this.state.LeagueContactList.filter(c => c.ContactID == ContactID && c.LeagueID == this.state.LeagueList[j].LeagueID).length;
          if (count == 0) {
            this.state.LeagueContactList.push(<LeagueContact>
              {
                LeagueContactID: leagueContactID,
                ContactID: ContactID,
                IsLeagueAdmin: IsLeagueAdmin,
                Active: Active,
                PlayingToday: Active ? PlayingToday : false,
                LeagueID: this.state.LeagueList[j].LeagueID,
                Removed: false,
                LastUpdateContactID: ContactID,
                LastUpdateDate_UTC: new Date(),
              });

            done = true;
          }
        }

        leagueContactID += 1;

        if (IsLeagueAdmin && leagueContactID > maxLeagueAdminCount) {
          IsLeagueAdmin = false;
        }
      }
    }

  }

  GenerateDemoDataPlayerList() {
    for (let i = 0; i < this.NumberOfPlayers; i++) {
      let done: boolean = false;
      while (!done) {
        let firstName: string = this.firstNameList[Math.floor(Math.random() * (this.firstNameList.length))];
        let lastName: string = this.lastNameList[Math.floor(Math.random() * (this.lastNameList.length))];
        let count: number = this.state.PlayerList.filter(c => c.FirstName == firstName && c.LastName == lastName).length;
        if (count == 0) {
          this.state.PlayerList.push(<Player>{

            ContactID: i + 1,
            FirstName: firstName,
            LastName: lastName,
            PlayerLevel: +1 + (Math.floor(Math.random() * 4)) + (Math.random() * 1),
          });

          done = true;
        }
      }
    }

    for (let i = 0, count = this.state.PlayerList.length; i < count; i++) {
      let initial: boolean = Math.floor(Math.random() * 4) > 3 ? true : false;

      this.state.PlayerList[i].LoginEmail = `${this.state.PlayerList[i].FirstName}.${this.state.PlayerList[i].LastName}@${this.emailList[Math.floor(Math.random() * (this.emailList.length + 1))]}`;
      this.state.PlayerList[i].Removed = false;
      this.state.PlayerList[i].LastUpdateContactID = this.state.PlayerList[i].ContactID;
      this.state.PlayerList[i].LastUpdateDate_UTC = new Date();
      this.state.PlayerList[i].Initial = initial == true ? this.initialList[Math.floor(Math.random() * (this.initialList.length))] : '';
    }

    this.state.PlayerList = this.sortService.SortPlayerList(this.state.PlayerList);
  }

  GenerateDemoDataDemoExtraPlayerList() {
    for (let i = 0; i < this.NumberOfPlayers; i++) {
      let done: boolean = false;
      while (!done) {
        let firstName: string = this.firstNameList[Math.floor(Math.random() * (this.firstNameList.length))];
        let lastName: string = this.lastNameList[Math.floor(Math.random() * (this.lastNameList.length))];
        let count: number = this.state.PlayerList.filter(c => c.FirstName == firstName && c.LastName == lastName).length;
        let count2: number = this.state.DemoExtraPlayerList.filter(c => c.FirstName == firstName && c.LastName == lastName).length;
        if (count == 0 && count2 == 0) {
          this.state.DemoExtraPlayerList.push(<Player>{

            ContactID: this.state.PlayerList.length + i + 1,
            FirstName: firstName,
            LastName: lastName,
            PlayerLevel: +1 + (Math.floor(Math.random() * 4)) + (Math.random() * 1),
          });

          done = true;
        }
      }
    }

    for (let i = 0, count = this.state.DemoExtraPlayerList.length; i < count; i++) {
      let initial: boolean = Math.floor(Math.random() * 4) > 3 ? true : false;

      this.state.DemoExtraPlayerList[i].LoginEmail = `${this.state.DemoExtraPlayerList[i].FirstName}.${this.state.DemoExtraPlayerList[i].LastName}@${this.emailList[Math.floor(Math.random() * (this.emailList.length + 1))]}`;
      this.state.DemoExtraPlayerList[i].Removed = false;
      this.state.DemoExtraPlayerList[i].LastUpdateContactID = this.state.DemoExtraPlayerList[i].ContactID;
      this.state.DemoExtraPlayerList[i].LastUpdateDate_UTC = new Date();
      this.state.DemoExtraPlayerList[i].Initial = initial == true ? this.initialList[Math.floor(Math.random() * (this.initialList.length))] : '';
    }

    this.state.DemoExtraPlayerList = this.sortService.SortPlayerList(this.state.DemoExtraPlayerList);
  }
}
