import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { LogoffService } from './logoff.service';

@Component({
  selector: 'app-logoff',
  templateUrl: './logoff.component.html',
  styleUrls: ['./logoff.component.css']
})
export class LogoffComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public logoffService: LogoffService) {
  }

  ngOnInit(): void {
    this.logoffService.ResetLocals();
 }

  ngOnDestroy(): void {
  }

  Logoff() {
    this.logoffService.Logoff();
  }
}
