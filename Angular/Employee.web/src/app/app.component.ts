import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { first, Observable } from 'rxjs';
import { Employee } from '../models/employee.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';


// meta data
// this is called as component decorator
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe, HttpClientModule, FormsModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  http = inject(HttpClient)

  employeeForm = new FormGroup({
    firstName : new FormControl<string>(''),
    lastName : new FormControl<string>(''),
    email : new FormControl<string | null>(''),
    phone : new FormControl<string>(''),

  })

  employees$ = this.GetEmployees();
  isEditMode = false;
  currentEmployeeId : string | null = null;

  OnFormSubmit(){
    const addEmployeeRequest ={
      firstName : this.employeeForm.value.firstName,
      lastName : this.employeeForm.value.lastName,
      email : this.employeeForm.value.email,
      phone : this.employeeForm.value.phone,
    }


    if(this.isEditMode && this.currentEmployeeId){
      this.http.put(`https://localhost:7014/api/Employee/${this.currentEmployeeId}`, addEmployeeRequest)
      .subscribe({
        next: (value) => {
          console.log(value);
          this.ResetForm();
          this.employees$ = this.GetEmployees();
        }
      })
    }
    else{
      this.http.post('https://localhost:7014/api/Employee', addEmployeeRequest)
      .subscribe({
        next: (value) =>{
          console.log(value);
          this.ResetForm();
          this.employees$ = this.GetEmployees();
        }
      });
    }
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

  private GetEmployees(): Observable<Employee[]>{
    return this.http.get<Employee[]>('https://localhost:7014/api/Employee');
  }

  private ResetForm(){
    this.employeeForm.reset();
    this.isEditMode = false;
    this.currentEmployeeId = null;
  }
}
