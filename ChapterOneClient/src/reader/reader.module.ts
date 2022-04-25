import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { CoreModule } from 'src/core/core.module';
import { DashboardReaderComponent } from './dashboard-reader/dashboard-reader.component';
import { DefaultReaderComponent } from './default-reader/default-reader.component';
import { HeaderReaderComponent } from './header-reader/header-reader.component';
import { SidebarReaderComponent } from './sidebar-reader/sidebar-reader.component';
import { ProfileReaderComponent } from './profile-reader/profile-reader.component';
import { SubscriptionDetailsComponent } from './subscription-details/subscription-details.component';
import { ModalSubscriptionComponent } from './modal-subscription/modal-subscription.component';
import { BookCardComponent } from './book-card/book-card.component';
import { BookDetailsComponent } from './book-details/book-details.component';




@NgModule({
  declarations: [
    DashboardReaderComponent,
    DefaultReaderComponent,
    HeaderReaderComponent,
    SidebarReaderComponent,
    ProfileReaderComponent,
    SubscriptionDetailsComponent,
    ModalSubscriptionComponent,
    BookCardComponent,
    BookDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CoreModule
  ]
})
export class ReaderModule { }
