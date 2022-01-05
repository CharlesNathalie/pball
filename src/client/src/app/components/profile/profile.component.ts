import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { ProfileService } from './profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public profleService: ProfileService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }
}
