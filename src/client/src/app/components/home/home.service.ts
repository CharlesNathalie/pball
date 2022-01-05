import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  HomeSomething: string[] = ['Home something', 'Home something (fr)'];

  constructor(public state: AppStateService) {
  }

}
