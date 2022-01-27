import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { Router } from '@angular/router';
import { League } from 'src/app/models/League.model';

@Injectable({
  providedIn: 'root'
})
export class LeagueNotMemberService {

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public router: Router) {
  }
}
