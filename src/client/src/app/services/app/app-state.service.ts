import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AppState } from 'src/app/models/AppState.model';
import { Contact } from 'src/app/models/Contact.model';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  private readonly _appState = new BehaviorSubject<AppState>({});

  readonly appState$ = this._appState.asObservable();

  get appState(): AppState {
    return this._appState.getValue();
  }

  private set appState(appState: AppState) {
    this._appState.next(appState);
  }

  setContact(contact: Contact) {
    this.appState.Contact = contact; 
  }

  setStatus(status: string) {
    this.appState.Status = status; 
  }

  setWorking(working: boolean) {
    this.appState.Working = working; 
  }

  setError(error: HttpErrorResponse) {
    this.appState.Error = error; 
  }
}
