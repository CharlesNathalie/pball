import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueAdminsService {
  Administrators: string[] = ['Administrators', 'Administrateurs'];
  EmailSubject: string[] = ['Email subject', 'Sujet du courriel'];
  EmailBody: string[] = ['Email body', 'corps du courriel'];

  constructor(public state: AppStateService) {
  }

}
