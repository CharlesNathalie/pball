import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueService } from 'src/app/services/league.service';

@Component({
  selector: 'app-league',
  templateUrl: './league.component.html',
  styleUrls: ['./league.component.css']
})
export class LeagueComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public leagueService: LeagueService,
    public router: Router) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  SetLeagueID(LeagueID: number) {
    this.leagueService.SetLeagueID(LeagueID);
  }

  GetIsSelected(LeagueID: number): boolean {
    if (this.state.DemoVisible) {
      return this.state.DemoLeagueID == LeagueID;
    }
    else {
      return this.state.LeagueID == LeagueID;
    }
  }

  ModifyTheLeague() {
    this.state.ReturnToPage = this.router.url;
    this.router.navigate([`/${ this.state.Culture }/leaguemodify`]);
  }

  AddANewLeague() {
    this.state.ReturnToPage = this.router.url;
    this.router.navigate([`/${ this.state.Culture }/leagueadd`]);
  }

  GetIsAdmin(ContactID: number): boolean {
    return this.state.LeagueContactList.find(c => c.ContactID == ContactID)?.IsLeagueAdmin ?? false;
  }
}
