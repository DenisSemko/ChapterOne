import { Component, EventEmitter, Inject, LOCALE_ID, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-header-admin',
  templateUrl: './header-admin.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class HeaderAdminComponent implements OnInit {

  @Output() toggleSideBarForMe: EventEmitter<any> = new EventEmitter();

  constructor(private router: Router, private userService: UserService, private translate: TranslateService, private toastr: ToastrService, @Inject(LOCALE_ID) protected localeId: string) {
    translate.setDefaultLang('en');
  }

  ngOnInit(): void {
  }

  toggleSideBar(){
    this.toggleSideBarForMe.emit();
  }

  profileClick() {
    this.router.navigateByUrl('admin/my-account');
  }

  statisticsClick() {
    this.router.navigateByUrl('admin/statistics');
  }

  logout() {
    this.userService.logout();
  }

  useLanguage(language: string): void {
    this.translate.use(language);
  }

}
