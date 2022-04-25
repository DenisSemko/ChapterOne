import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from 'src/admin/dashboard/dashboard.component';
import { DefaultComponent } from 'src/admin/default/default.component';
import { ProfileComponent } from 'src/admin/profile/profile.component';
import { StatisticComponent } from 'src/admin/statistic/statistic.component';
import { AuthGuard } from 'src/guard/auth.guard';
import { AboutUsComponent } from 'src/mainPage/about-us/about-us.component';
import { HomeComponent } from 'src/mainPage/home/home.component';
import { LoginComponent } from 'src/mainPage/login/login.component';
import { RegistrationComponent } from 'src/mainPage/registration/registration.component';
import { BookDetailsComponent } from 'src/reader/book-details/book-details.component';
import { DashboardReaderComponent } from 'src/reader/dashboard-reader/dashboard-reader.component';
import { DefaultReaderComponent } from 'src/reader/default-reader/default-reader.component';
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
      path: 'book/:id',
        component: BookDetailsComponent,
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
