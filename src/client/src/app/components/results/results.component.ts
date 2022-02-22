import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { DataHelperService } from 'src/app/services/data-helper.service';
import { ResultsService } from 'src/app/services/results.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css']
})
export class ResultsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public resultsService: ResultsService,
    public dataHelperService: DataHelperService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  Period(time: 'day' | 'week' | 'month' | 'year' | 'all' | 'between') {
    this.resultsService.Period(time);
  }

  ChangedStartDate(startDate: HTMLInputElement) {
    this.resultsService.ChangedStartDate(startDate)
    this.state.DataTime = 'between';
  }

  ChangedEndDate(endDate: HTMLInputElement) {
    this.resultsService.ChangedEndDate(endDate);
    this.state.DataTime = 'between';
  }

  
  GetDataTimeSelected(dataTime: 'day' | 'week' | 'month' | 'year' | 'all' | 'between'): boolean {
    return this.state.DataTime == dataTime ? true : false;
  }

  GetDataTimeHighlight(dataTime: 'day' | 'week' | 'month' | 'year' | 'all' | 'between'): string {
    return this.state.DataTime == dataTime ? 'highlight' : '';
  }


  GetChecked(LeagueID: number): string {
    return this.state.LeagueID == LeagueID ? 'checked' : '';
  }


}
