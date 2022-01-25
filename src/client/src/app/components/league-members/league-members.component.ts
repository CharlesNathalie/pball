import { Component, OnInit, OnDestroy } from '@angular/core';
import { LeagueContact } from 'src/app/models/LeagueContact.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueMembersService } from 'src/app/services/league-members.service';

@Component({
  selector: 'app-league-members',
  templateUrl: './league-members.component.html',
  styleUrls: ['./league-members.component.css']
})
export class LeagueMembersComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public leagueMembersService: LeagueMembersService) {
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
}
