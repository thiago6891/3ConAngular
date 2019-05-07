import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../customer';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  configUrl = 'http://localhost:5000/api/';

  constructor(private http: HttpClient) { }

  getCustomers(): Observable<any> {
    return this.http.get(this.configUrl + 'Customers/GetAll');
  }
  
  getOneCustomer(id: number): Observable<any> {
    return this.http.get(this.configUrl + `Customers?id=${id}`);
  }
  
  newCustomer(customer: Customer): Observable<any> {
    return this.http.post(this.configUrl + 'Customers', customer);
  }
  
  editCustomer(customer: Customer) {
    return this.http.put(this.configUrl + 'Customers', customer);
  }
}