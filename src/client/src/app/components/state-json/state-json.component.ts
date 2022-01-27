import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Component({
  selector: 'app-state-json',
  templateUrl: './state-json.component.html',
  styleUrls: ['./state-json.component.css']
})
export class StateJsonComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  GetObjectKeysOfState(): string[] {
    return Object.keys(this.state);
  }

  GetJsonOfStateKey(key: any): any {
    return Object.values(this.state);
  }
}
