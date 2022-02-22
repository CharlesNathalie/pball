import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { SortService } from 'src/app/services/sort.service';

@Injectable({
  providedIn: 'root'
})
export class TablePartnerWinsService {
  Partner: string[] = ['Partner', 'Partenaire'];
  Games: string[] = ['Games', 'Parties'];
  Wins: string[] = ['Wins', 'Victoire'];
  PlusMinus: string[] = ['+/-', '+/-'];
  
  constructor(public state: AppStateService,
    public sortService: SortService) {
  }

  

}
