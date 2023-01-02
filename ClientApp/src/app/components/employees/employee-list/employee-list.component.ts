import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: Employee[] = [];
  constructor(private employeeService : EmployeesService){
    //this.employees = [{ id: "waleamoo", name: "Wale", email: "amoowale@gmail.com", phone: 0812345678, salary: 5000, department: "Computing" }, { id: "rukky11", name: "Ruka", email: "rukky11@gmail.com", phone: 0812345678, salary: 5000, department: "Computing" }]
  }

  ngOnInit(): void {
    this.employeeService.getAllEmployees()
    .subscribe({
      next: (employees) => {
        this.employees = employees;
      }, error: (response) => {
        console.log(response);
      }
    });
    //this.employees.push()
  }
}
