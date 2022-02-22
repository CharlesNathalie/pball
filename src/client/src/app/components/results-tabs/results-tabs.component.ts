import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { ResultsTabsService } from 'src/app/services/results-tabs.service';

@Component({
  selector: 'app-results-tabs',
  templateUrl: './results-tabs.component.html',
  styleUrls: ['./results-tabs.component.css']
})
export class ResultsTabsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public resultsTabsService: ResultsTabsService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  ResultsTabSelectedChanged(event: any) {
    this.state.ResultsTabsIndex = event.index;
    localStorage.setItem('ResultsTabsIndex', JSON.stringify(this.state.ResultsTabsIndex));
  }

}
