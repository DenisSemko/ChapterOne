import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { CoreModule } from 'src/core/core.module';
import { DashboardReaderComponent } from './dashboard-reader/dashboard-reader.component';
import { DefaultReaderComponent } from './default-reader/default-reader.component';
import { HeaderReaderComponent } from './header-reader/header-reader.component';
import { SidebarReaderComponent } from './sidebar-reader/sidebar-reader.component';
import { ProfileReaderComponent } from './profile-reader/profile-reader.component';



@NgModule({
  declarations: [
    DashboardReaderComponent,
    DefaultReaderComponent,
    HeaderReaderComponent,
    SidebarReaderComponent,
    ProfileReaderComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CoreModule
  ]
})
export class ReaderModule { }
