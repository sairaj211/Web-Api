import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { first, Observable, BehaviorSubject } from 'rxjs';
import { Employee } from '../models/employee.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Validator } from '@angular/forms';
import { CommonModule } from '@angular/common';

// meta data
// this is called as component decorator
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe, HttpClientModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  http = inject(HttpClient)

  employeeForm = new FormGroup({
    firstName : new FormControl<string>('', [Validators.required]),
    lastName : new FormControl<string>('', [Validators.required]),
    email : new FormControl<string | null>('', [Validators.required, Validators.email]),
    phone : new FormControl<string>('', [
      Validators.required,
      Validators.minLength(10),
      Validators.pattern(/^\d+$/)
    ])
  })

  employees$ = this.GetEmployees();
  isEditMode = false;
  currentEmployeeId : string | null = null;
  validationErrors: string[] = [];

  trackByFn(index: number, item: any): number {
    return item.id; // or any unique identifier for your items
  }

  OnFormSubmit(){
    const addEmployeeRequest ={
      firstName : this.employeeForm.value.firstName,
      lastName : this.employeeForm.value.lastName,
      email : this.employeeForm.value.email,
      phone : this.employeeForm.value.phone,
    }

    console.log('Request Body:', addEmployeeRequest); // Log the request body

    const requestBody = this.isEditMode && this.currentEmployeeId
      ?  this.http.put(`https://localhost:7014/api/Employee/${this.currentEmployeeId}`, addEmployeeRequest)
      : this.http.post('https://localhost:7014/api/Employee', addEmployeeRequest);

     // console.log(this.currentEmployeeId);

      requestBody.subscribe({
        next: (value) => {
          console.log(value);
          this.validationErrors = [];
          this.ResetForm();
          this.employees$ = this.GetEmployees();
        },
        error: (err) => {
          if (err.status === 400 && err.error && err.error.errors) {
            this.HandleValidationErrors(err.error.errors);
            console.log(this.validationErrors);
          }
          else{
            console.log("An error occured: ", err);
          }
        }
      });
    }


  OnEdit(employee : Employee){
    console.log('Editing Employee ID:', employee.id);
    this.isEditMode = true;
    this.currentEmployeeId = employee.id;

    this.employeeForm.setValue({
      firstName: employee.firstName,
      lastName: employee.lastName,
      email: employee.email,
      phone: employee.phone
    });
  }

  OnDelete(id: string, event: MouseEvent) {
    // Confirm deletion (optional)
    event.stopPropagation();
    if (confirm('Are you sure you want to delete this employee?')) {
        this.http.delete(`https://localhost:7014/api/Employee/${id}`)
        .subscribe({
            next: () => {
                alert('Item Deleted!');
                this.ResetForm();
                this.employees$ = this.GetEmployees(); // Refresh employee list
            },
            error: (err) => {
                console.error('Delete error', err);
                alert('Failed to delete the employee.');
            }
        });
    }
}

  OnCancelEdit() {
    this.ResetForm();
  }

  private GetEmployees(): Observable<Employee[]>{
    return this.http.get<Employee[]>('https://localhost:7014/api/Employee');
  }

  private HandleValidationErrors(errors: Record<string, string[]>){
    this.validationErrors = [];

    Object.keys(errors).forEach(key => {
      const messages = errors[key];
      this.validationErrors.push(...messages);
    });
  }

  private ResetForm(){
    this.employeeForm.reset();
    this.isEditMode = false;
    this.currentEmployeeId = null;
    this.validationErrors = [];
  }
}
