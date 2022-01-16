import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
