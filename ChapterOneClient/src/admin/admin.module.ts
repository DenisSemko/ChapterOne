import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { CoreModule } from 'src/core/core.module';
import { DefaultComponent } from './default/default.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HeaderAdminComponent } from './header-admin/header-admin.component';
import { SidebarAdminComponent } from './sidebar-admin/sidebar-admin.component';
import { ProfileComponent } from './profile/profile.component';



@NgModule({
  declarations: [
  
    DefaultComponent,
       DashboardComponent,
       HeaderAdminComponent,
       SidebarAdminComponent,
       ProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CoreModule
  ]
})
export class AdminModule { }
