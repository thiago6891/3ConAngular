import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { Customer } from '../customer';
import { usernameValidator } from '../usernameValidator';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit {
  formulario: FormGroup;
  customer: Customer = new Customer();

  constructor(private customerService: CustomerService, private router: Router, private fb: FormBuilder) { }
  
  onSubmit() {
    this.customer.name = this.formulario.get('name').value;
    
    this.customerService.newCustomer(this.customer)
      .subscribe(() => this.router.navigateByUrl('/'));
  }
  
  ngOnInit() {
    this.formulario = this.fb.group({
      id: [this.customer.id],
      name: [
        this.customer.name, 
        [Validators.required],
        [usernameValidator(this.customerService)]
      ],
      active: [this.customer.active],
      registerDate: [this.customer.registerDate]
    });
  }
}