import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoginModel } from 'src/app/models/LoginModel.model';
import { AppLanguageService } from 'src/app/services/app/app-language.service';
import { AppLoadedService } from 'src/app/services/app/app-loaded.service';
import { AppStateService } from 'src/app/services/app/app-state.service';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  constructor(public appStateService: AppStateService,
    public appLanguageService: AppLanguageService,
    public appLoadedService: AppLoadedService,
    private loginService: LoginService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  Login() {
    let loginModel: LoginModel = <LoginModel>{ LoginEmail: '3785a@gmail.com', Password: '494a' };
    this.loginService.Login(loginModel);
  }
}
