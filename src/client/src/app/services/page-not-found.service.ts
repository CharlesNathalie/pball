import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PageNotFoundService {
  PageNotFound: string[] = ['Page not found', 'Page introuvable'];
  ReturnHome: string[] = ['Return home', 'Retour à la page d\'accueil'];

  constructor() {
  }

}
