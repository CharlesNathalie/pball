import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Player } from 'src/app/models/Player.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { DemoDataService } from 'src/app/services/demo-data.service';
import { LeagueMembersService } from 'src/app/services/league-members.service';
import { SearchPlayersService } from 'src/app/services/search-players.service';

@Component({
  selector: 'app-league-members',
  templateUrl: './league-members.component.html',
  styleUrls: ['./league-members.component.css']
})
export class LeagueMembersComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  constructor(public state: AppStateService,
    public leagueMembersService: LeagueMembersService,
    public demoDataService: DemoDataService,
    public httpClient: HttpClient,
    public searchPlayersService: SearchPlayersService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  GetIsActive(ContactID: number): boolean {
    return this.state.LeagueContactList.find(c => c.ContactID == ContactID)?.Active ?? false;
  }

  GetMailTo(player: Player) {
    let subject: string = this.leagueMembersService.PBallEmail[this.state.LangID];
    let body: string = this.leagueMembersService.Hello[this.state.LangID];
    let to: string = player.LoginEmail;
    return `mailto:${to}?subject=${subject}&body=${body} ${player.FirstName},<br><br>`;
  }

  GetMailToForAllMembers() {
    let subject: string = this.leagueMembersService.PBallEmail[this.state.LangID];
    let body: string = this.leagueMembersService.Hello[this.state.LangID];

    let to: string = '';
    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      to += `${this.state.PlayerList.find(c => c.ContactID == this.state.LeagueContactList[i].ContactID)?.LoginEmail};`;
    }
    return `mailto:${to}?subject=${subject}&body=${body} ${this.leagueMembersService.Members[this.state.LangID]},`;
  }

  GetMailToForAllActiveMembers() {
    let subject: string = this.leagueMembersService.PBallEmail[this.state.LangID];
    let body: string = this.leagueMembersService.Hello[this.state.LangID];

    let to: string = '';
    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      if (this.state.LeagueContactList[i].Active) {
        to += `${this.state.PlayerList.find(c => c.ContactID == this.state.LeagueContactList[i].ContactID)?.LoginEmail};`;
      }
    }
    return `mailto:${to}?subject=${subject}&body=${body} ${this.leagueMembersService.Members[this.state.LangID]},`;
  }

  UserIsAdminOfLeague(): boolean {
    if (this.state.DemoVisible) {
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        if (this.state.LeagueContactList[i].ContactID == this.state.DemoUser.ContactID) {
          if (this.state.LeagueContactList[i].IsLeagueAdmin) {
            return true;
          }
        }
      }
    }
    else {
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        if (this.state.LeagueContactList[i].ContactID == this.state.User.ContactID) {
          if (this.state.LeagueContactList[i].IsLeagueAdmin) {
            return true;
          }
        }
      }
    }
    return false;
  }

  AddPlayerToLeague(player: Player) {
    this.leagueMembersService.AddPlayerToLeague(player);
  }

  SetPlayerActiveState(playerID: number) {
    this.leagueMembersService.ChangeLeagueMemberActive(playerID);
  }

  IsUser(playerID: number) {
    if (this.state.DemoVisible) {
      return playerID == this.state.DemoUser.ContactID;
    }

    return playerID == this.state.User.ContactID;
  }

  InactiveExist(): boolean {
    if (this.state.DemoVisible) {
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        if (!this.state.LeagueContactList[i].Active) {
          return true;
        }
      }
    }
    else {
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        if (!this.state.LeagueContactList[i].Active) {
          return true;
        }
      }
    }
    return false;
  }

  Search(searchTerms: string) {
    if (searchTerms.length) {
      this.searchPlayersService.SearchPlayers(searchTerms);
    }
    else{
      this.state.SearchPlayerList = [];
    }
  }

}
