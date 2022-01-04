import { Contact } from 'src/app/models/Contact.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppStateService } from 'src/app/services/app/app-state.service';
import { WebContact } from 'src/app/models/WebContact.model';

@Injectable({
  providedIn: 'root'
})
export class AppLoadedService {
  //BaseApiUrl = 'https://pball.azurewebsites.net/'; 
  BaseApiUrl = 'https://localhost:7072/api/';

  LoggedInContact?: Contact;
  WebContact?: WebContact;

  constructor(public httpClient: HttpClient,
    public appStateService: AppStateService) {
  }
}
