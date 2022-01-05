import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { PageNotFoundService } from './pagenotfound.service';

@Component({
  selector: 'app-pagenotfound',
  templateUrl: './pagenotfound.component.html',
  styleUrls: ['./pagenotfound.component.css']
})
export class PageNotFoundComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public pageNotFoundService: PageNotFoundService) {
  }

  ngOnInit(): void {
 }

  ngOnDestroy(): void {
  }

}
