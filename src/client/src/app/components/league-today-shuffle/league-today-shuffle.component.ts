import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Player } from 'src/app/models/Player.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { DemoDataService } from 'src/app/services/demo-data.service';
import { LeagueTodayShuffleService } from 'src/app/services/league-today-shuffle.service';
import { SearchPlayersService } from 'src/app/services/search-players.service';

@Component({
  selector: 'app-league-today-shuffle',
  templateUrl: './league-today-shuffle.component.html',
  styleUrls: ['./league-today-shuffle.component.css']
})
export class LeagueTodayShuffleComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  constructor(public state: AppStateService,
    public leagueTodayShuffleService: LeagueTodayShuffleService,
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

  SetPlayingThisTime(playerID: number) {
    this.leagueTodayShuffleService.ChangePlayingThisTime(playerID);
  }

  SetPlayerToShuffle(playerID: number) {
    this.leagueTodayShuffleService.ChangePlayerToShuffle(playerID);
  }
}
