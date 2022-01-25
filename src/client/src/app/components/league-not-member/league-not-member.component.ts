import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueNotMemberService } from '../../services/league-not-member.service';

@Component({
  selector: 'app-league-not-member',
  templateUrl: './league-not-member.component.html',
  styleUrls: ['./league-not-member.component.css']
})
export class LeagueNotMemberComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public leagueNotMemberService: LeagueNotMemberService) {
  }

  ngOnInit(): void {
 }

  ngOnDestroy(): void {
  }
}
