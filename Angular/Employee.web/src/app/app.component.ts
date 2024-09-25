import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { first, Observable } from 'rxjs';
import { Employee } from '../models/employee.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';



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

  OnFormSubmit(){
    const addEmployeeRequest ={
      firstName : this.employeeForm.value.firstName,
      lastName : this.employeeForm.value.lastName,
      email : this.employeeForm.value.email,
      phone : this.employeeForm.value.phone,
    }

    this.http.post('https://localhost:7014/api/Employee', addEmployeeRequest)
    .subscribe({
      next: (value) =>{
        console.log(value);
        this.employees$ = this.GetEmployees();
        this.employeeForm.reset();
      }
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

}
