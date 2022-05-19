import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { CoreModule } from 'src/core/core.module';
import { DefaultComponent } from './default/default.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HeaderAdminComponent } from './header-admin/header-admin.component';
import { SidebarAdminComponent } from './sidebar-admin/sidebar-admin.component';
import { ProfileComponent } from './profile/profile.component';
import { StatisticComponent } from './statistic/statistic.component';
import { UsersActionsComponent } from './users-actions/users-actions.component';
import { UsersActionDeleteComponent } from './users-action-delete/users-action-delete.component';
import { UsersActionAddAdminComponent } from './users-action-add-admin/users-action-add-admin.component';
import { AddBookComponent } from './add-book/add-book.component';



@NgModule({
  declarations: [
  
    DefaultComponent,
       DashboardComponent,
       HeaderAdminComponent,
       SidebarAdminComponent,
       ProfileComponent,
       StatisticComponent,
       UsersActionsComponent,
       UsersActionDeleteComponent,
       UsersActionAddAdminComponent,
       AddBookComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CoreModule
  ]
})
export class AdminModule { }
