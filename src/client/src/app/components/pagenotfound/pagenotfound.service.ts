import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PageNotFoundService {
  PageNotFound: string[] = ['Page not found', 'Page not found (fr)'];

  constructor() {
  }

}