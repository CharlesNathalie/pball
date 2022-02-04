import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from '../enums/LanguageEnum';
import { LeagueContact } from '../models/LeagueContact.model';

@Injectable({
  providedIn: 'root'
})
export class LeagueAdminsService {
  Administrators: string[] = ['Administrators', 'Administrateurs'];
  ChangeingLeagueAdmin: string[] = ['Changing league administrator', 'Changement d\'administrateur de ligue'];
  EmailAllAdministrators: string[] = ['Email all administrators', 'Envoyer un courriel à tous les administrateurs'];
  Hello: string[] = ['Hello', 'Bonjour'];
  ManageAdministrators: string[] = ['Manage administrators', 'Gérer les administrateurs'];
  Note: string[] = ['Note', 'Notez'];
  OneCannotRemoveItself: string[] = ['One cannot remove itself', 'On ne peut s\'enlever soit-même'];
  PBallEmail: string[] = ['PBall email', 'Courriel de PBall'];
  YouNeedToAskAnotherAdministrator: string[] = ['You need to ask another administrator', 'Vous devez demander à un autre administrateur'];  
  
  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  ChangeLeagueAdminSuccess: boolean = false;

  private sub: Subscription = new Subscription();
  
  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  IsAdminTryingToRemoveHimself(playerID: number)
  {
    if (this.state.DemoVisible)
    {
      return playerID == this.state.DemoUser.ContactID;
    }
    
    return playerID == this.state.User.ContactID;
  }

  ChangeLeagueAdmin(playerID: number) {
    if (this.IsAdminTryingToRemoveHimself(playerID))
    {
      this.Working = false;
      this.Error = <HttpErrorResponse>{
        message: 'Should not delete the last administrator of the league'
      };
      return;
    }

    if (this.state.DemoVisible) {
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        if (this.state.LeagueContactList[i].ContactID == playerID) {
          this.state.LeagueContactList[i].IsLeagueAdmin = !this.state.LeagueContactList[i].IsLeagueAdmin;
        }
      }
        
      return;
    }
    else{
      this.Status = `${this.ChangeingLeagueAdmin[this.state.LangID]} - ${this.state.PlayerList.find(c => c.ContactID == playerID)?.LoginEmail ?? ''}`;
      this.Working = true;
      this.Error = <HttpErrorResponse>{};

      this.sub ? this.sub.unsubscribe() : null;
      this.sub = this.DoChangeLeagueAdmin(playerID).subscribe();
      }
  }

  private DoChangeLeagueAdmin(playerID: number) {
    let languageEnum = GetLanguageEnum();

    let leaguecontact: LeagueContact = <LeagueContact>{};

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/leaguecontact`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      if (this.state.LeagueContactList[i].ContactID == playerID) {
        this.state.LeagueContactList[i].IsLeagueAdmin = !this.state.LeagueContactList[i].IsLeagueAdmin;
        leaguecontact = this.state.LeagueContactList[i];
      }
    }

    return this.httpClient.put<LeagueContact>(url,
      JSON.stringify(leaguecontact), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForChangeLeagueAdmin(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForChangeLeagueAdmin(e); }))));
  }

  private DoUpdateForChangeLeagueAdmin(leagueContact: LeagueContact) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.ChangeLeagueAdminSuccess = true;
    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      if (this.state.LeagueContactList[i].ContactID == leagueContact.ContactID) {
        this.state.LeagueContactList[i].IsLeagueAdmin = leagueContact.IsLeagueAdmin;
        break;
      }
    }

    console.debug(leagueContact);
  }

  private DoErrorForChangeLeagueAdmin(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.ChangeLeagueAdminSuccess = false;
    console.debug(e);
  }

}
