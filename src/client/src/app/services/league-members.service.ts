import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueMembersService {
  Members: string[] = ['Members', 'Membres'];

  constructor(public state: AppStateService) {
  }

}
