import { Component, OnInit, OnDestroy } from '@angular/core';
import { Player } from 'src/app/models/Player.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { DemoDataService } from 'src/app/services/demo-data.service';
import { LeagueAdminsService } from 'src/app/services/league-admins.service';

@Component({
  selector: 'app-league-admins',
  templateUrl: './league-admins.component.html',
  styleUrls: ['./league-admins.component.css']
})
export class LeagueAdminsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public leagueAdminsService: LeagueAdminsService,
    public demoDataService: DemoDataService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  GetIsAdmin(ContactID: number): boolean {
    return this.state.LeagueContactList.find(c => c.ContactID == ContactID)?.IsLeagueAdmin ?? false;
  }

  GetMailTo(player: Player) {
    let subject: string = this.leagueAdminsService.PBallEmail[this.state.LangID];
    let body: string = this.leagueAdminsService.Hello[this.state.LangID];
    let to: string = player.LoginEmail;
    return `mailto:${to}?subject=${subject}&body=${body} ${player.FirstName},<br><br>`;
  }

  GetMailToForAllAdmin() {
    let subject: string = this.leagueAdminsService.PBallEmail[this.state.LangID];
    let body: string = this.leagueAdminsService.Hello[this.state.LangID];

    let to: string = '';
    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      if (this.state.LeagueContactList[i].IsLeagueAdmin) {
        to += `${this.state.PlayerList.find(c => c.ContactID == this.state.LeagueContactList[i].ContactID)?.LoginEmail};`;
      }
    }
    return `mailto:${to}?subject=${subject}&body=${body} ${this.leagueAdminsService.Administrators[this.state.LangID]},`;
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

  SelectionClicked(playerID: number) {
      this.leagueAdminsService.ChangeLeagueAdmin(playerID);
  }

  IsUser(playerID: number)
  {
    if (this.state.DemoVisible)
    {
      return playerID == this.state.DemoUser.ContactID;
    }
    
    return playerID == this.state.User.ContactID;
  }

}
