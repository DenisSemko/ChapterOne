import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from 'src/shared/shared.module';
import { MainPageModule } from 'src/mainPage/main-page.module';
import { AdminModule } from 'src/admin/admin.module';
import { UserService } from 'src/services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    MainPageModule,
    AdminModule
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
