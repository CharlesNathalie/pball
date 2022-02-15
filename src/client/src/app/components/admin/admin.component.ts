import { Component, OnInit, OnDestroy } from '@angular/core';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { ChangePasswordModel } from 'src/app/models/ChangePasswordModel.model';
import { AdminService } from 'src/app/services/admin.service';
import { AppStateService } from 'src/app/services/app-state.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  constructor(public state: AppStateService,
    public adminService: AdminService) {
  }

  ngOnInit(): void {
    this.adminService.GetChangePasswordRequestList();
  }

  ngOnDestroy(): void {
  }

  GetMailTo(changePasswordModel: ChangePasswordModel) {
    let subject: string = this.adminService.PBallEmail[this.state.LangID];
    let body: string = `${ this.adminService.Hello[this.state.LangID]} ${changePasswordModel.FirstName},
    
%0d%0a%0d%0a
${ this.adminService.YourTemporaryCodeIs[this.state.LangID]} ${changePasswordModel.TempCode}
%0d%0a%0d%0a
${this.state.BaseApiUrl.replace('api/', '')}${this.languageEnum[this.state.Language]}-CA/changepassword;
%0d%0a%0d%0a
${this.state.User.FirstName} ${this.state.User.LastName}
%0d%0a%0d%0a
    `
    let to: string = changePasswordModel.LoginEmail;
    return `mailto:${to}?subject=${subject}&body=${body}`;
  }
}
