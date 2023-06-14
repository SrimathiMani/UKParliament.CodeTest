
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ManageViewComponent } from './people/manage-view/manage-view.component';
import { ListComponent } from './people/list/list.component';
import { AddEditComponent } from './people/add-edit/add-edit.component';
import { dateFormatddMMYYYY } from './_helpers/dateFormat.pipe';

import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { AlertDialogComponent } from './people/alert-dialog/alert-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatSortModule,
    MatDialogModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule,
    RouterModule.forRoot([
      //{ path: '', component: HomeComponent, pathMatch: 'full' }
      { path: '', component: ManageViewComponent }
    ]),
    
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    ManageViewComponent,
    ListComponent,
    AddEditComponent,
    dateFormatddMMYYYY,
    AlertDialogComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { };
