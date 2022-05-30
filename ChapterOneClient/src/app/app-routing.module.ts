import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddBookComponent } from 'src/admin/add-book/add-book.component';
import { DashboardComponent } from 'src/admin/dashboard/dashboard.component';
import { DefaultComponent } from 'src/admin/default/default.component';
import { ProfileComponent } from 'src/admin/profile/profile.component';
import { StatisticComponent } from 'src/admin/statistic/statistic.component';
import { AuthGuard } from 'src/guard/auth.guard';
import { AboutUsComponent } from 'src/mainPage/about-us/about-us.component';
import { HomeComponent } from 'src/mainPage/home/home.component';
import { LoginComponent } from 'src/mainPage/login/login.component';
import { RegistrationComponent } from 'src/mainPage/registration/registration.component';
import { BookCollectionComponent } from 'src/reader/book-collection/book-collection.component';
import { BookDetailsComponent } from 'src/reader/book-details/book-details.component';
import { BookSearchComponent } from 'src/reader/book-search/book-search.component';
import { CollectionComponent } from 'src/reader/collection/collection.component';
import { DashboardReaderComponent } from 'src/reader/dashboard-reader/dashboard-reader.component';
import { DefaultReaderComponent } from 'src/reader/default-reader/default-reader.component';
import { DeliveryComponent } from 'src/reader/delivery/delivery.component';
import { PaymentComponent } from 'src/reader/payment/payment.component';
import { ProfileReaderComponent } from 'src/reader/profile-reader/profile-reader.component';
import { SubscriptionDetailsComponent } from 'src/reader/subscription-details/subscription-details.component';

const routes: Routes = [
  {
    path: 'about',
    component: AboutUsComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegistrationComponent
  },
  {
    path: 'admin',
    component: DefaultComponent,
    children: [{
      path: 'dashboard',
      component: DashboardComponent,
      canActivate:[AuthGuard]
    },
    {
      path: 'my-account',
        component: ProfileComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'statistics',
        component: StatisticComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'manage-books',
        component: AddBookComponent,
        canActivate:[AuthGuard]
    }]
  },
  {
    path: 'reader',
    component: DefaultReaderComponent,
    children: [{
      path: 'dashboard',
      component: DashboardReaderComponent,
      canActivate:[AuthGuard]
    },
    {
      path: 'my-account',
        component: ProfileReaderComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'subscription-details',
        component: SubscriptionDetailsComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'book-search',
        component: BookSearchComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'book/:id',
        component: BookDetailsComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'my-book-collection',
        component: BookCollectionComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'book-collection/:id',
        component: CollectionComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'payment/book',
        component: PaymentComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'payment/subscription',
        component: PaymentComponent,
        canActivate:[AuthGuard]
    },
    {
      path: 'delivery',
        component: DeliveryComponent,
        canActivate:[AuthGuard]
    }]
  },
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
