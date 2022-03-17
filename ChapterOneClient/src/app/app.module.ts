import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from 'src/shared/shared.module';
import { MainPageModule } from 'src/mainPage/main-page.module';
import { AdminModule } from 'src/admin/admin.module';
import { ReaderModule } from 'src/reader/reader.module';
import { UserService } from 'src/services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SubscriptionService } from 'src/services/subscription.service';
import { StatisticsService } from 'src/services/statistics.service';
import { BackupService } from 'src/services/backup.service';

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
    AdminModule,
    ReaderModule
  ],
  providers: [UserService, SubscriptionService, StatisticsService, BackupService],
  bootstrap: [AppComponent]
})
export class AppModule { }
