import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/app-state.service';
import { LeagueAddService } from './league-add.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-league-add',
  templateUrl: './league-add.component.html',
  styleUrls: ['./league-add.component.css']
})
export class LeagueAddComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  leagueAddForm = this.formBuilder.group({
    LeagueAddEmail: ['', [Validators.required, Validators.email]],
    Password: ['', [Validators.required]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public leagueAddService: LeagueAddService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.leagueAddService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LeagueName'): boolean {
    return this.leagueAddService.GetHasError(fieldName, this.leagueAddForm);
  }

  GetErrorMessage(fieldName: 'LeagueName'): string {
    return this.leagueAddService.GetErrorMessage(fieldName, this.leagueAddForm);
  }

  GetFormValid(): boolean {
    return this.leagueAddService.GetFormValid(this.leagueAddForm);
  }

  OnSubmit(): void {
    this.leagueAddService.SubmitForm(this.leagueAddForm);
  }
}
