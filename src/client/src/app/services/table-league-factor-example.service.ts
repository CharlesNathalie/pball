import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class TableLeagueFactorExampleService {
  ExamplePoints: string[] = ['Example points', 'Exemple points'];
  T1P1: string[] = ['T1P1', 'E1J1'];
  T1P2: string[] = ['T1P2', 'E1J2'];
  T2P1: string[] = ['T2P1', 'E2J1'];
  T2P2: string[] = ['T2P2', 'E2J2'];
  T1S: string[] = ['T1S', 'E1P'];
  T2S: string[] = ['T2S', 'E2P'];



  constructor(public state: AppStateService) {
  }

}
