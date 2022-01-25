import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
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
  @Input() league: League = <League>{};

  languageEnum = GetLanguageEnum();

  leagueAddForm = this.formBuilder.group({
    LeagueID: [this.league.LeagueID, [Validators.required]],
    LeagueName: [this.league.LeagueName, [Validators.required]],
    PointsToWinners: [this.league.PointsToWinners, [Validators.required]],
    PointsToLosers: [this.league.PointsToLosers, [Validators.required]],
    PlayerLevelFactor: [this.league.PlayerLevelFactor, [Validators.required]],
    PercentPointsFactor: [this.league.PercentPointsFactor, [Validators.required]],
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
    return this.leagueModifyService.GetHasError(fieldName, this.leagueAddForm);
  }

  GetErrorMessage(fieldName: 'LeagueID' | 'LeagueName' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor'): string {
    return this.leagueModifyService.GetErrorMessage(fieldName, this.leagueAddForm);
  }

  GetFormValid(): boolean {
    return this.leagueModifyService.GetFormValid(this.leagueAddForm);
  }

  OnSubmit(): void {
    this.leagueModifyService.SubmitForm(this.leagueAddForm);
  }
}
