import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ListCustomersComponent } from './list-customers/list-customers.component';
import { HttpClientModule } from '@angular/common/http';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';

import{ MatTableModule, MatInputModule, MatFormFieldModule} from '@angular/material';
import{ NoopAnimationsModule} from '@angular/platform-browser/animations';
const appRoutes: Routes = [
  { path: 'customers', component: ListCustomersComponent },
  {path:'new',component: EditCustomerComponent},
  {path:'edit',component: EditCustomerComponent},
  
  { path: '',
    redirectTo: '/customers',
    pathMatch: 'full'
  }
];
@NgModule({
  declarations: [
    AppComponent,
    ListCustomersComponent ,
    EditCustomerComponent
  ],
  imports: [
    NoopAnimationsModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatInputModule,
    MatFormFieldModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
