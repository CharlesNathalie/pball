import { formatDate } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { TableMostGamesPlayedService } from './table-most-games-played.service';

@Component({
  selector: 'app-table-most-games-played',
  templateUrl: './table-most-games-played.component.html',
  styleUrls: ['./table-most-games-played.component.css']
})
export class TableMostGamesPlayedComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  displayedColumns: string[] = ['FullName', 'NumberOfGames'];

  getMostGamesPlayedForm = this.formBuilder.group({
    LeagueID: ['0', [Validators.required]],
    StartDate: [formatDate(new Date(2020, 1, 1), 'yyyy-MM-dd', 'en-CA'), [Validators.required]],
    EndDate: [formatDate(new Date(2023, 1, 1), 'yyyy-MM-dd', 'en'), [Validators.required]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public tableMostGamesPlayedService: TableMostGamesPlayedService) {
  }

  ngOnInit(): void {
    this.tableMostGamesPlayedService.ResetLocals();
 }

  ngOnDestroy(): void {
  }

  
  GetHasError(fieldName: 'LeagueID' | 'StartDate' | 'EndDate'): boolean {
    return this.tableMostGamesPlayedService.GetHasError(fieldName, this.getMostGamesPlayedForm);
  }

  GetErrorMessage(fieldName: 'LeagueID' | 'StartDate' | 'EndDate'): string {
    return this.tableMostGamesPlayedService.GetErrorMessage(fieldName, this.getMostGamesPlayedForm);
  }

  GetFormValid(): boolean {
    return this.tableMostGamesPlayedService.GetFormValid(this.getMostGamesPlayedForm);
  }

  OnSubmit(): void {
    this.tableMostGamesPlayedService.SubmitForm(this.getMostGamesPlayedForm);
  }
}
