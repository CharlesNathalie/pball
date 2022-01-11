import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/app-material.module';
import { ChartAllComponent } from './components/chart-all/chart-all.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/pagenotfound/pagenotfound.component';
import { ProfileComponent } from './components/profile/profile.component';
import { RegisterComponent } from './components/register/register.component';
import { ShellComponent } from './components/shell/shell.component';
import { TableAllComponent } from './components/table-all/table-all.component';
import { TableMostGamesPlayedComponent } from './components/table-most-games-played/table-most-games-played.component';
import { TablePlayerComponent } from './components/table-player/table-player.component';

@NgModule({
  declarations: [
    ChartAllComponent,
    ForgotPasswordComponent,
    HomeComponent,
    LoginComponent,
    PageNotFoundComponent,
    ProfileComponent,
    RegisterComponent,
    ShellComponent,
    TableAllComponent,
    TableMostGamesPlayedComponent,
    TablePlayerComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
    ReactiveFormsModule,
    RouterModule,

    ChartAllComponent,
    ForgotPasswordComponent,
    HomeComponent,
    LoginComponent,  
    PageNotFoundComponent,
    ProfileComponent,
    RegisterComponent,
    ShellComponent,
    TableAllComponent,
    TableMostGamesPlayedComponent,
    TablePlayerComponent,
  ]
})
export class SharedModule { }
