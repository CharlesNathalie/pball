import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/services/app-state.service';
import { DemoDataService } from 'src/app/services/demo-data.service';
import { GetPlayerLeaguesService } from 'src/app/services/get-player-leagues.service';
import { ShellService } from '../../services/shell.service';
import { HomeService } from '../../services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public homeService: HomeService,
    public getPlayerLeaguesService: GetPlayerLeaguesService,
    public shellService: ShellService,
    public demoDataService: DemoDataService,
    public router: Router) {
  }

  ngOnInit(): void {
    if (this.state.LeagueList.length == 0) {
      if (this.state.DemoVisible) {
        this.ShowDemo();
      }
      else {
        this.getPlayerLeaguesService.Run();
      }
    }
  }

  ngOnDestroy(): void {
  }


  HideDemo() {
    this.state.ClearDemoLocalStorage();
    this.state.ClearDemoData();
    this.getPlayerLeaguesService.Run();
  }

  ShowDemo() {
    this.state.DemoVisible = true;
    this.demoDataService.GenerateDemoData();
  }

  HomeTabSelectedChanged(event: any) {
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
