import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { PageNotFoundService } from '../../services/page-not-found.service';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.css']
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
