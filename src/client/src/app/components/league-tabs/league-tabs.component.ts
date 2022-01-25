import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueTabsService } from '../../services/league-tabs.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-league-tabs',
  templateUrl: './league-tabs.component.html',
  styleUrls: ['./league-tabs.component.css']
})
export class LeagueTabsComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  leagueAddForm = this.formBuilder.group({
    LeagueID: ['0', [Validators.required]],
    LeagueName: ['', [Validators.required]],
    PointsToWinners: ['', [Validators.required]],
    PointsToLosers: ['', [Validators.required]],
    PlayerLevelFactor: ['', [Validators.required]],
    PercentPointsFactor: ['', [Validators.required]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public leagueAddService: LeagueTabsService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.leagueAddService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LeagueID' | 'LeagueName' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor'): boolean {
    return this.leagueAddService.GetHasError(fieldName, this.leagueAddForm);
  }

  GetErrorMessage(fieldName: 'LeagueID' | 'LeagueName' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor'): string {
    return this.leagueAddService.GetErrorMessage(fieldName, this.leagueAddForm);
  }

  GetFormValid(): boolean {
    return this.leagueAddService.GetFormValid(this.leagueAddForm);
  }

  OnSubmit(): void {
    this.leagueAddService.SubmitForm(this.leagueAddForm);
  }
}
