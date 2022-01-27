import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/services/app-state.service';
import { NoDataForUserService } from '../../services/no-data-for-user.service';

@Component({
  selector: 'app-no-data-for-user',
  templateUrl: './no-data-for-user.component.html',
  styleUrls: ['./no-data-for-user.component.css']
})
export class NoDataForUserComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public noDataForUserService: NoDataForUserService,
    public router: Router) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  AddANewLeague() {
    this.state.ReturnToPage = this.router.url;
    this.router.navigate([`/${ this.state.Culture }/leagueadd`]);
  }
}
