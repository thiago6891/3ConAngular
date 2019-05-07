import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { Router, ActivatedRoute } from '@angular/router';
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

  constructor(private customerService: CustomerService, private router: Router, private fb: FormBuilder, private route: ActivatedRoute) { }
  
  private fillForm()
  {
    if (this.customer === null) {
      this.customer = new Customer();
    }

    this.formulario = this.fb.group({
      id: [this.customer.id],
      name: [
        this.customer.name, 
        [Validators.required],
        [usernameValidator(this.customerService, this.customer)]
      ],
      active: [this.customer.active],
      registerDate: [this.customer.registerDate]
    });
  }

  onSubmit() {
    this.customer.name = this.formulario.get('name').value;
    this.customer.active = this.formulario.get('active').value;
    
    if (this.customer.id > 0) {
      this.customerService.editCustomer(this.customer)
        .subscribe(() => this.router.navigateByUrl('/'));
    } else {
      this.customerService.newCustomer(this.customer)
        .subscribe(() => this.router.navigateByUrl('/'));
    }
  }
  
  ngOnInit() {
    this.customer = new Customer();

    this.fillForm();

    this.route.queryParams.subscribe(params => {
      let id = params.id;
      this.customerService.getOneCustomer(id).subscribe(customer => {
        if (customer !== undefined) {
          this.customer = customer;
        }
        this.fillForm();
      })
    });
  }
}