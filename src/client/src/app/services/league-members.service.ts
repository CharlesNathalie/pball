import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from '../enums/LanguageEnum';
import { LeagueContact } from '../models/LeagueContact.model';
import { Player } from '../models/Player.model';
import { SortService } from './sort.service';

@Injectable({
  providedIn: 'root'
})
export class LeagueMembersService {
  Active: string[] = ['Active', 'Actif'];
  AddingPlayerToLeague: string[] = ['Adding player to league', 'L\'ajout d\'un joueur à la ligue'];
  ChangingLeagueMember: string[] = ['Changing league member', 'Changement de membre de ligue'];
  EmailAllMembers: string[] = ['Email all members', 'Courriel à tous les membres'];
  EmailAllActiveMembers: string[] = ['Email all active members', 'Courriel à tous les membres actifs'];
  Hello: string[] = ['Hello', 'Bonjour'];
  Inactive: string[] = ['Inactive', 'Inactif'];
  IncludeAnotherMember: string[] = ['Include another member', 'Inclure un autre membre'];
  ManageMembers: string[] = ['Manage members', 'Gérer les membres'];
  Members: string[] = ['Members', 'Membres'];
  Note: string[] = ['Note', 'Notez'];
  OneCannotRemoveItself: string[] = ['One cannot remove itself', 'On ne peut s\'enlever soit-même'];
  PBallEmail: string[] = ['PBall email', 'Courriel de PBall'];
  SearchResultOnlyProvidesNamesNotAlreadyInLeague: string[] = ['Search result only provides names not already in league', 'Le résultat de la recherche ne fournit que des noms qui ne sont pas déjà dans la ligue'];
  SelectedActive: string[] = ['Selected active', 'Sélection actif'];
  YouNeedToAskAnotherAdministrator: string[] = ['You need to ask another administrator', 'Vous devez demander à un autre administrateur'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  ChangeLeagueMemberSuccess: boolean = false;
  AddPlayerToLeagueSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient,
    public sortService: SortService) {
  }

  ChangeLeagueMemberActive(playerID: number) {
    if (this.state.DemoVisible) {
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        if (this.state.LeagueContactList[i].ContactID == playerID) {
          this.state.LeagueContactList[i].Active = !this.state.LeagueContactList[i].Active;
        }
      }

      return;
    }
    else {
      this.Status = `${this.ChangingLeagueMember[this.state.LangID]} - ${this.state.PlayerList.find(c => c.ContactID == playerID)?.LoginEmail ?? ''}`;
      this.Working = true;
      this.Error = <HttpErrorResponse>{};

      this.sub ? this.sub.unsubscribe() : null;
      this.sub = this.DoChangeLeagueMemberActive(playerID).subscribe();
    }
  }

  private DoChangeLeagueMemberActive(playerID: number) {
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
        this.state.LeagueContactList[i].Active = !this.state.LeagueContactList[i].Active;
        leaguecontact = this.state.LeagueContactList[i];
      }
    }

    return this.httpClient.put<LeagueContact>(url,
      JSON.stringify(leaguecontact), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForChangeLeagueMemberActive(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForChangeLeagueMemberActive(e); }))));
  }

  private DoUpdateForChangeLeagueMemberActive(leagueContact: LeagueContact) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.ChangeLeagueMemberSuccess = true;
    for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
      if (this.state.LeagueContactList[i].ContactID == leagueContact.ContactID) {
        this.state.LeagueContactList[i].Active = leagueContact.Active;
        break;
      }
    }

    console.debug(leagueContact);
  }

  private DoErrorForChangeLeagueMemberActive(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.ChangeLeagueMemberSuccess = false;
    console.debug(e);
  }

  AddPlayerToLeague(player: Player) {
    if (this.state.DemoVisible) {
      let alreadyExist: boolean = false;
      let MaxLeagueContactID: number = 0;
      for (let i = 0, count = this.state.LeagueContactList.length; i < count; i++) {
        MaxLeagueContactID = this.state.LeagueContactList[i].LeagueContactID;
        if (this.state.LeagueContactList[i].ContactID == player.ContactID) {
          alreadyExist = true;
        }
      }

      if (!alreadyExist) {
        let leagueContact: LeagueContact = <LeagueContact>{
          LeagueContactID: MaxLeagueContactID + 1,
          LeagueID: this.state.DemoLeagueID,
          ContactID: player.ContactID,
          IsLeagueAdmin: false,
          Active: true,
          PlayingToday: true,
          Removed: false
        };

        let DemoPlayerIndex: number = 0;
        for (let j = 0, count = this.state.DemoExtraPlayerList.length; j < count; j++) {
          if (this.state.DemoExtraPlayerList[j].ContactID == player.ContactID) {
            DemoPlayerIndex = j;
            break;
          }
        }
        this.state.DemoExtraPlayerList.splice(DemoPlayerIndex, 1);
        this.state.PlayerList.push(player);
        this.state.PlayerList = this.sortService.SortPlayerList(this.state.PlayerList);
        this.state.LeagueContactList.push(leagueContact);
      }

      return;
    }
    else {
      this.Status = `${this.AddingPlayerToLeague[this.state.LangID]} - ${this.state.PlayerList.find(c => c.ContactID == player.ContactID)?.LoginEmail ?? ''}`;
      this.Working = true;
      this.Error = <HttpErrorResponse>{};

      this.state.PlayerList.push(player);

      let leagueContact: LeagueContact = <LeagueContact>{
        LeagueContactID: 0,
        LeagueID: this.state.LeagueID,
        ContactID: player.ContactID,
        IsLeagueAdmin: false,
        Active: true,
        PlayingToday: true,
        Removed: false
      };

      this.sub ? this.sub.unsubscribe() : null;
      this.sub = this.DoAddPlayerToLeague(leagueContact).subscribe();
    }
  }


  private DoAddPlayerToLeague(leagueContact: LeagueContact) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/leaguecontact`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.post<LeagueContact>(url,
      JSON.stringify(leagueContact), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForAddPlayerToLeague(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForAddPlayerToLeague(e); }))));
  }

  private DoUpdateForAddPlayerToLeague(leagueContact: LeagueContact) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.AddPlayerToLeagueSuccess = true;
    this.state.LeagueContactList.push(leagueContact);

    this.state.PlayerList = this.sortService.SortPlayerList(this.state.PlayerList);

    console.debug(leagueContact);
  }

  private DoErrorForAddPlayerToLeague(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.AddPlayerToLeagueSuccess = false;

    this.state.PlayerList.pop();

    console.debug(e);
  }


}
