import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
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
    public shellService: ShellService) {
  }

  ngOnInit(): void {
 }

  ngOnDestroy(): void {
  }

}
