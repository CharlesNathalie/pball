import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from 'src/app/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared.module';

import { registerLocaleData } from '@angular/common';
import localeEnCa from '@angular/common/locales/en-CA';
import localeEnCaExtra from '@angular/common/locales/extra/en-CA';
import localeFrCa from '@angular/common/locales/fr-CA';
import localeFrCaExtra from '@angular/common/locales/extra/fr-CA';

registerLocaleData(localeFrCa, localeFrCaExtra);
registerLocaleData(localeEnCa, localeEnCaExtra);

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

