import { Component, OnInit, OnDestroy } from '@angular/core';
import { LeagueContact } from 'src/app/models/LeagueContact.model';
import { Player } from 'src/app/models/Player.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueAdminsService } from 'src/app/services/league-admins.service';

@Component({
  selector: 'app-league-admins',
  templateUrl: './league-admins.component.html',
  styleUrls: ['./league-admins.component.css']
})
export class LeagueAdminsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public leagueAdminsService: LeagueAdminsService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  GetIsAdmin(ContactID: number): boolean {
    let leagueContactArr: LeagueContact[] = this.state.LeagueContactList.filter(c => c.ContactID == ContactID);

    if (leagueContactArr.length > 0) {
      if (leagueContactArr[0].IsLeagueAdmin) {
        return true;
      }
    }

    return false;
  }

  GetMailTo(player: Player)
  {
    return `mailto:${player.LoginEmail}?subject=${ this.leagueAdminsService.EmailSubject[this.state.LangID] }&body=${ this.leagueAdminsService.EmailBody[this.state.LangID] }`;
  }
}
