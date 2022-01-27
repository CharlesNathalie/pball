import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { DataHelperService } from 'src/app/services/data-helper.service';
import { ProgressService } from 'src/app/services/progress.service';

@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrls: ['./progress.component.css']
})
export class ProgressComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public progressService: ProgressService,
    public dataHelperService: DataHelperService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  Period(time: 'day' | 'week' | 'month' | 'year' | 'all' | 'between') {
    this.progressService.Period(time);
  }

  ChangedStartDate(startDate: HTMLInputElement) {
    this.progressService.ChangedStartDate(startDate)
    this.state.DataTime = 'between';
  }

  ChangedEndDate(endDate: HTMLInputElement) {
    this.progressService.ChangedEndDate(endDate);
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
