import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { first, Observable } from 'rxjs';
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

  OnFormSubmit(){
    const addEmployeeRequest ={
      firstName : this.employeeForm.value.firstName,
      lastName : this.employeeForm.value.lastName,
      email : this.employeeForm.value.email,
      phone : this.employeeForm.value.phone,
    }

    const requestBody = this.isEditMode && this.currentEmployeeId
      ?  this.http.put(`https://localhost:7014/api/Employee/${this.currentEmployeeId}`, addEmployeeRequest)
      : this.http.post('https://localhost:7014/api/Employee', addEmployeeRequest);

      requestBody.subscribe({
        next: (value) => {
          console.log(value);
          this.ResetForm();
          this.employees$ = this.GetEmployees();
          this.validationErrors = [];
        },
        error: (err) => {
          if (err.status === 400) {
            this.HandleValidationErrors(err.error.errors);
          }
          else{
            console.log("An error occured: ", err);
          }
        }
      });
    }


  OnEdit(employee : Employee){
    this.isEditMode = true;
    this.currentEmployeeId = employee.id;
    this.employeeForm.setValue({
      firstName: employee.firstName,
      lastName: employee.lastName,
      email: employee.email,
      phone: employee.phone
    });
  }

  OnDelete(id: string){
    this.http.delete(`https://localhost:7014/api/Employee/${id}`)
    .subscribe({
      next: (value) =>{
        alert('Item Deleted!');
        this.employees$ = this.GetEmployees();
      }
    });
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
