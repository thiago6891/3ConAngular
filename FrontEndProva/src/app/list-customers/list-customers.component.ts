import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-list-customers',
  templateUrl: './list-customers.component.html',
  styleUrls: ['./list-customers.component.css']
})
export class ListCustomersComponent implements OnInit {
  displayedColumns: string[] = ['name', 'active', 'registerDate', 'id'];
  dataSource :any;
  constructor(private customerService:CustomerService) { }
 
  ngOnInit() {
    this.customerService.getCustomers().subscribe(res=> this.dataSource =res);
  }

}
