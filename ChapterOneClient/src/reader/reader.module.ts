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
import { BookSearchComponent } from './book-search/book-search.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BookCollectionComponent } from './book-collection/book-collection.component';
import { BookCollectionModalComponent } from './book-collection-modal/book-collection-modal.component';
import { CollectionComponent } from './collection/collection.component';
import { CardModalComponent } from './card-modal/card-modal.component';




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
    BookSearchComponent,
    BookDetailsComponent,
    BookCollectionComponent,
    BookCollectionModalComponent,
    CollectionComponent,
    CardModalComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CoreModule
  ]
})
export class ReaderModule { }
