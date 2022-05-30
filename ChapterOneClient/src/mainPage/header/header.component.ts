import { Component, EventEmitter, HostListener, OnInit, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['../mainStyles.component.scss']
})
export class HeaderComponent implements OnInit {

  navbarfixed:boolean = false;
  isMainPage:boolean = true;

  constructor(private translate: TranslateService) {
    translate.setDefaultLang('en');
   }

  ngOnInit(): void {
  }

  @HostListener('window:scroll', ['$event']) onscroll() {
    if(window.scrollY > 100)
    {
      if(window.location.href.includes("login") || window.location.href.includes("register")) {
        this.isMainPage = false;
      } else {
        this.isMainPage = true;
      }
      this.navbarfixed = true;
    } else {
      this.navbarfixed = false;
    }
  }

  useLanguage(language: string): void {
    this.translate.use(language);
  }


}
