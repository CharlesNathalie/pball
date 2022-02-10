import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { GameAddService } from '../../services/game-add.service';
import { Router } from '@angular/router';
import { DataHelperService } from 'src/app/services/data-helper.service';
import { DemoDataService } from 'src/app/services/demo-data.service';

@Component({
  selector: 'app-game-add',
  templateUrl: './game-add.component.html',
  styleUrls: ['./game-add.component.css']
})
export class GameAddComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  gameAddForm = this.formBuilder.group({
    GameID: ['0', [Validators.required]],
    LeagueID: [this.state.DemoVisible ? this.state.DemoLeagueID : this.state.LeagueID, [Validators.required]],
    Team1Player1: [this.state.DemoVisible ? this.state.DemoUser.ContactID : this.state.User.ContactID, [Validators.required]],
    Team1Player2: ['', [Validators.required]],
    Team2Player1: ['', [Validators.required]],
    Team2Player2: ['', [Validators.required]],
    Team1Scores: ['5', [Validators.required]],
    Team2Scores: ['5', [Validators.required]],
    GameDate: ['', [Validators.required]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public gameAddService: GameAddService,
    public router: Router,
    public dataHelperService: DataHelperService,
    public demoDataService: DemoDataService) {
  }

  ngOnInit(): void {
    this.gameAddService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'GameID' | 'LeagueID' | 'Team1Player1' | 'Team1Player2' | 'Team2Player1' | 'Team2Player2' | 'Team1Scores' | 'Team2Scores' | 'GameDate'): boolean {
    return this.gameAddService.GetHasError(fieldName, this.gameAddForm);
  }

  GetErrorMessage(fieldName: 'GameID' | 'LeagueID' | 'Team1Player1' | 'Team1Player2' | 'Team2Player1' | 'Team2Player2' | 'Team1Scores' | 'Team2Scores' | 'GameDate'): string {
    return this.gameAddService.GetErrorMessage(fieldName, this.gameAddForm);
  }

  GetFormValid(): boolean {
    return this.gameAddService.GetFormValid(this.gameAddForm);
  }

  SubmitGameAddForm(): void {
    this.gameAddService.SubmitGameAddForm(this.gameAddForm);
    //this.router.navigate([`/${ this.state.Culture }/home`]);
  }

   GetIsPlayingToday(ContactID: number): boolean {
    return this.state.LeagueContactList.find(c => c.ContactID == ContactID)?.PlayingToday ?? false;
  }

}
