import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './components/admin/admin.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { HomeComponent } from './components/home/home.component';
import { LeagueAddComponent } from './components/league-add/league-add.component';
import { LeagueFactorsExampleComponent } from './components/league-factors-example/league-factors-example.component';
import { LeagueModifyComponent } from './components/league-modify/league-modify.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProfileComponent } from './components/profile/profile.component';
import { RegisterComponent } from './components/register/register.component';
import { ShellComponent } from './components/shell/shell.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'en-CA' },
  {
    path: 'en-CA', component: ShellComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'home' },
      { path: 'admin', component: AdminComponent },
      { path: 'changepassword', component: ChangePasswordComponent },
      { path: 'forgotpassword', component: ForgotPasswordComponent },
      { path: 'home', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'leagueadd', component: LeagueAddComponent },
      { path: 'leaguemodify', component: LeagueModifyComponent },
      { path: 'leaguefactorsexample', component: LeagueFactorsExampleComponent },
    ]
  },
  {
    path: 'fr-CA', component: ShellComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'home' },
      { path: 'admin', component: AdminComponent },
      { path: 'changepassword', component: ChangePasswordComponent },
      { path: 'forgotpassword', component: ForgotPasswordComponent },
      { path: 'home', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'leagueadd', component: LeagueAddComponent },
      { path: 'leaguemodify', component: LeagueModifyComponent },
      { path: 'leaguefactorsexample', component: LeagueFactorsExampleComponent },
    ]
  },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule,
    RouterModule.forRoot(routes,
      { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
