import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Player } from 'src/app/models/Player.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { DemoDataService } from 'src/app/services/demo-data.service';
import { LeagueTodayService } from 'src/app/services/league-today.service';
import { SearchPlayersService } from 'src/app/services/search-players.service';

@Component({
  selector: 'app-league-today',
  templateUrl: './league-today.component.html',
  styleUrls: ['./league-today.component.css']
})
export class LeagueTodayComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  constructor(public state: AppStateService,
    public leagueTodayService: LeagueTodayService,
    public demoDataService: DemoDataService,
    public httpClient: HttpClient,
    public searchPlayersService: SearchPlayersService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  GetIsPlayingToday(ContactID: number): boolean {
    return this.state.LeagueContactList.find(c => c.ContactID == ContactID)?.PlayingToday ?? false;
  }

  SetPlayerPlayingTodayState(playerID: number) {
    this.leagueTodayService.ChangeLeagueMemberPlayingToday(playerID);
  }
}
