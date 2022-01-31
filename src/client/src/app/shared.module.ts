import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/app-material.module';
import { ChartAveragePlayerLevelOfOpponentsComponent } from './components/chart-average-player-level-of-opponents/chart-average-player-level-of-opponents.component';
import { ChartAveragePlayerLevelOfPartnersComponent } from './components/chart-average-player-level-of-partners/chart-average-player-level-of-partners.component';
import { ChartGamesPercentWonComponent } from './components/chart-games-percent-won/chart-games-percent-won.component';
import { ChartGamesPlayedComponent } from './components/chart-games-played/chart-games-played.component';
import { ChartGamesWonComponent } from './components/chart-games-won/chart-games-won.component';
import { ChartPointsComponent } from './components/chart-points/chart-points.component';
import { ChartTotalNumberOfOpponentsComponent } from './components/chart-total-number-of-opponents/chart-total-number-of-opponents.component';
import { ChartTotalNumberOfPartnersComponent } from './components/chart-total-number-of-partners/chart-total-number-of-partners.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { HomeComponent } from './components/home/home.component';
import { LeagueAddComponent } from './components/league-add/league-add.component';
import { LeagueAdminsComponent } from './components/league-admins/league-admins.component';
import { LeagueFactorsExampleComponent } from './components/league-factors-example/league-factors-example.component';
import { LeagueMembersComponent } from './components/league-members/league-members.component';
import { LeagueModifyComponent } from './components/league-modify/league-modify.component';
import { LeagueTabsComponent } from './components/league-tabs/league-tabs.component';
import { LeagueComponent } from './components/league/league.component';
import { LoginComponent } from './components/login/login.component';
import { NoDataForUserComponent } from './components/no-data-for-user/no-data-for-user.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ProgressTabsComponent } from './components/progress-tabs/progress-tabs.component';
import { ProgressComponent } from './components/progress/progress.component';
import { RegisterComponent } from './components/register/register.component';
import { ShellComponent } from './components/shell/shell.component';
import { StateJsonComponent } from './components/state-json/state-json.component';
import { TableLeagueFactorExampleComponent } from './components/table-league-factor-example/table-league-factor-example.component';
import { TableLeagueStatsComponent } from './components/table-league-stats/table-league-stats.component';
import { TablePlayerGamesComponent } from './components/table-player-games/table-player-games.component';

@NgModule({
  declarations: [
    ChartAveragePlayerLevelOfPartnersComponent,
    ChartAveragePlayerLevelOfOpponentsComponent,
    ChartGamesPlayedComponent,
    ChartGamesPercentWonComponent,
    ChartGamesWonComponent,
    ChartPointsComponent,
    ChartTotalNumberOfOpponentsComponent,
    ChartTotalNumberOfPartnersComponent,
    ForgotPasswordComponent,
    HomeComponent,
    LeagueComponent,
    LeagueAddComponent,
    LeagueAdminsComponent,
    LeagueFactorsExampleComponent,
    LeagueMembersComponent,
    LeagueModifyComponent,
    LeagueTabsComponent,
    LoginComponent,
    NoDataForUserComponent,
    PageNotFoundComponent,
    ProfileComponent,
    ProgressComponent,
    ProgressTabsComponent,
    RegisterComponent,
    ShellComponent,
    StateJsonComponent,
    TableLeagueFactorExampleComponent,
    TableLeagueStatsComponent,
    TablePlayerGamesComponent,
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

    ChartAveragePlayerLevelOfPartnersComponent,
    ChartAveragePlayerLevelOfOpponentsComponent,
    ChartGamesPlayedComponent,
    ChartGamesPercentWonComponent,
    ChartGamesWonComponent,
    ChartPointsComponent,
    ChartTotalNumberOfOpponentsComponent,
    ChartTotalNumberOfPartnersComponent,
    ForgotPasswordComponent,
    HomeComponent,
    LeagueComponent,
    LeagueAddComponent,
    LeagueAdminsComponent,
    LeagueFactorsExampleComponent,
    LeagueMembersComponent,
    LeagueModifyComponent,
    LeagueTabsComponent,
    LoginComponent,  
    NoDataForUserComponent,
    PageNotFoundComponent,
    ProfileComponent,
    ProgressComponent,
    ProgressTabsComponent,
    RegisterComponent,
    ShellComponent,
    StateJsonComponent,
    TableLeagueFactorExampleComponent,
    TableLeagueStatsComponent,
    TablePlayerGamesComponent,
  ]
})
export class SharedModule { }
