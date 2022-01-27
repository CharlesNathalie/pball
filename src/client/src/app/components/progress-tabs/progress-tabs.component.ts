import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { ProgressTabsService } from 'src/app/services/progress-tabs.service';

@Component({
  selector: 'app-progress-tabs',
  templateUrl: './progress-tabs.component.html',
  styleUrls: ['./progress-tabs.component.css']
})
export class ProgressTabsComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public progressTabsService: ProgressTabsService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  ProgressTabSelectedChanged(event: any) {
    this.state.ProgressTabsIndex = event.index;
    localStorage.setItem('ProgressTabsIndex', JSON.stringify(this.state.ProgressTabsIndex));
  }

}
