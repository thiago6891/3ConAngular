import { Observable } from 'rxjs';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AsyncValidatorFn, ValidationErrors, AbstractControl } from '@angular/forms';
import { Directive } from '@angular/core';
import { CustomerService } from './services/customer.service';
import { map } from 'rxjs/operators';

export function usernameValidator(customerService: CustomerService): AsyncValidatorFn {
    return (control: AbstractControl): 
        Promise<ValidationErrors | null> | Observable<ValidationErrors | null> =>
    {
        return customerService.getCustomers().pipe(map(customers => {
            var name = control.value.trim();
            
            if (name.toLowerCase() === '3con') {
                return { usernameNotAllowed: true };
            }
            
            if (customers && customers.find(c => c.name === name) !== undefined) {
                return { usernameTaken: true };
            }
            
            return null;
        }));
    }
}

@Directive({
    selector: '[usernameValidator]',
    providers: [{
        provide: NG_ASYNC_VALIDATORS,
        useExisting: UsernameValidatorDirective,
        multi: true}]
})
export class UsernameValidatorDirective implements AsyncValidator {
    constructor(private customerService: CustomerService) {}
    
    validate(control: AbstractControl): 
        Promise<ValidationErrors | null> | Observable<ValidationErrors | null>
    {
        return usernameValidator(this.customerService)(control);
    }
}