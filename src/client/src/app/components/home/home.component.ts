import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/app-state.service';
import { DemoDataService } from 'src/app/services/demo-data/demo-data.service';
import { LeagueService } from 'src/app/services/league/league.service';
import { ShellService } from '../shell/shell.service';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public homeService: HomeService,
    public shellService: ShellService,
    public leagueService: LeagueService,
    public demoDataService: DemoDataService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.homeService.LoadLocalStorage();
    this.homeService.LoadLocalStorage();
    if (this.state.LeagueList.length == 0) {
      if (this.state.DemoVisible) {
        this.ShowDemo();
      }
      else {
        this.leagueService.GetPlayerLeagues();
      }
    }
  }

  ngOnDestroy(): void {
  }


  SetLeagueID(LeagueID: number) {
    this.homeService.SetLeagueID(LeagueID);
  }

  Period(time: 'day' | 'week' | 'month' | 'year' | 'all' | 'between') {
    this.homeService.Period(time);
  }

  ChangedStartDate(startDate: HTMLInputElement) {
    this.homeService.ChangedStartDate(startDate)
    this.state.DataTime = 'between';
  }

  ChangedEndDate(endDate: HTMLInputElement) {
    this.homeService.ChangedEndDate(endDate);
    this.state.DataTime = 'between';
  }

  HideDemo() {
    this.state.ClearDemoLocalStorage();
    this.state.ClearDemoData();
    this.leagueService.GetPlayerLeagues();
  }

  ShowDemo() {
    this.state.DemoVisible = true;
    this.demoDataService.GenerateDemoData();
    this.homeService.Period('year');
  }

  GetDataTimeHighlight(dataTime: 'day' | 'week' | 'month' | 'year' | 'all' | 'between'): string {
    return this.state.DataTime == dataTime ? 'highlight' : '';
  }

  GetLeagueHighlight(LeagueID: number): string {
    return this.state.LeagueID == LeagueID ? 'highlight' : '';
  }

  GetChecked(LeagueID: number): string {
    return this.state.LeagueID == LeagueID ? 'checked' : '';
  }

  DragEnd(event: any) {
    if (this.state.DemoVisible) {
      if (event.distance.x < -20) {
        if (this.state.DemoHomeTabIndex < (this.state.HomeTabCount - 1)) {
          this.state.DemoHomeTabIndex += 1;
        }
      }

      if (event.distance.x > +20) {
        if (this.state.DemoHomeTabIndex > 0) {
          this.state.DemoHomeTabIndex -= 1;
        }
      }

      localStorage.setItem('DemoHomeTabIndex', JSON.stringify(this.state.DemoHomeTabIndex));
    }
    else {
      if (event.distance.x < -20) {
        if (this.state.HomeTabIndex < (this.state.HomeTabCount - 1)) {
          this.state.HomeTabIndex += 1;
        }
      }

      if (event.distance.x > +20) {
        if (this.state.HomeTabIndex > 0) {
          this.state.HomeTabIndex -= 1;
        }
      }

      localStorage.setItem('HomeTabIndex', JSON.stringify(this.state.HomeTabIndex));
    }

    event.source.element.nativeElement.style.transform = 'none' // visually reset element to its origin
    const source: any = event.source;
    source._passiveTransform = { x: 0, y: 0 };

  }

  TabSelectedChanged(event: any) {
    if (this.state.DemoVisible) {
      this.state.DemoHomeTabIndex = event.index;
      localStorage.setItem('DemoHomeTabIndex', JSON.stringify(this.state.DemoHomeTabIndex));
    }
    else {
      this.state.HomeTabIndex = event.index;
      localStorage.setItem('HomeTabIndex', JSON.stringify(this.state.HomeTabIndex));
    }
  }
}
