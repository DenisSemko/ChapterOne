import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from 'src/admin/dashboard/dashboard.component';
import { DefaultComponent } from 'src/admin/default/default.component';
import { AuthGuard } from 'src/guard/auth.guard';
import { AboutUsComponent } from 'src/mainPage/about-us/about-us.component';
import { HomeComponent } from 'src/mainPage/home/home.component';
import { LoginComponent } from 'src/mainPage/login/login.component';
import { RegistrationComponent } from 'src/mainPage/registration/registration.component';

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
      path: 'account',
      component: DashboardComponent,
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
