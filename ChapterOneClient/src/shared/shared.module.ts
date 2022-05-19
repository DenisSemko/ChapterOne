import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatCardModule } from '@angular/material/card'; 
import {MatFormFieldModule} from '@angular/material/form-field';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { MatInputModule } from '@angular/material/input';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatSelectModule} from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import {MatMenuModule} from '@angular/material/menu'
import {MatListModule} from '@angular/material/list';
import {MatSidenavModule} from '@angular/material/sidenav';
import { NgxEchartsModule } from "ngx-echarts";
import {MatRadioModule} from '@angular/material/radio';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularD3CloudModule } from 'angular-d3-cloud'
import {MatTabsModule} from '@angular/material/tabs';
import {MatChipsModule} from '@angular/material/chips';
import {MatAutocompleteModule} from '@angular/material/autocomplete';




@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatSelectModule,
    MatNativeDateModule,
    MatDividerModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule,
    MatTableModule,
    FlexLayoutModule,
    MatMenuModule,
    MatRadioModule,
    MatGridListModule,
    NgbModule,
    MatCheckboxModule,
    MatAutocompleteModule,
    MatSidenavModule,
    MatTabsModule,
    MatChipsModule,
    BrowserAnimationsModule,
    MatTooltipModule,
    AngularD3CloudModule,
    MatListModule,
    NgxEchartsModule.forRoot({
      echarts: () => import("echarts")
    }),
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right'
    }),
  ],
  exports: [
    RouterModule,
    MatIconModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    ToastrModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule,
    MatDividerModule,
    MatButtonModule,
    MatTableModule,
    FlexLayoutModule,
    MatSidenavModule,
    MatRadioModule,
    MatGridListModule,
    MatCheckboxModule,
    MatAutocompleteModule,
    MatMenuModule,
    MatListModule,
    BrowserAnimationsModule,
    MatTooltipModule,
    MatTabsModule,
    MatChipsModule,
    NgxEchartsModule,
    NgbModule,
    AngularD3CloudModule
  ]
})
export class SharedModule { }
