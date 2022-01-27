import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueModifyService } from '../../services/league-modify.service';
import { Router } from '@angular/router';
import { League } from 'src/app/models/League.model';

@Component({
  selector: 'app-league-modify',
  templateUrl: './league-modify.component.html',
  styleUrls: ['./league-modify.component.css']
})
export class LeagueModifyComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  leagueModifyForm: FormGroup = this.formBuilder.group({
    LeagueID: [this.state.CurrentLeague.LeagueID, [Validators.required]],
    LeagueName: [this.state.CurrentLeague.LeagueName, [Validators.required]],
    PointsToWinners: [this.state.CurrentLeague.PointsToWinners, [Validators.required]],
    PointsToLosers: [this.state.CurrentLeague.PointsToLosers, [Validators.required]],
    PlayerLevelFactor: [this.state.CurrentLeague.PlayerLevelFactor, [Validators.required]],
    PercentPointsFactor: [this.state.CurrentLeague.PercentPointsFactor, [Validators.required]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public leagueModifyService: LeagueModifyService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.leagueModifyService.ResetLocals();
    }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LeagueID' | 'LeagueName' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor'): boolean {
    return this.leagueModifyService.GetHasError(fieldName, this.leagueModifyForm);
  }

  GetErrorMessage(fieldName: 'LeagueID' | 'LeagueName' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor'): string {
    return this.leagueModifyService.GetErrorMessage(fieldName, this.leagueModifyForm);
  }

  GetFormValid(): boolean {
    return this.leagueModifyService.GetFormValid(this.leagueModifyForm);
  }

  OnSubmit(): void {
    this.leagueModifyService.SubmitForm(this.leagueModifyForm);
  }
}
