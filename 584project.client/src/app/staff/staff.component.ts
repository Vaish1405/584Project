import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Staff } from '../staff';
import { environment } from '../../environments/environment';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-staff',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './staff.component.html',
  styleUrl: './staff.component.css'
})
export class StaffComponent implements OnInit {
  public staffList: Staff[] = [];
  public groupedStaff: { [country: string]: Staff[] } = {};

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getStaff();
  }

getStaff() {
  this.http.get<Staff[]>(`${environment.baseUrl}api/staff`).subscribe({
    next: result => {
      this.staffList = result.sort((a, b) => a.country.localeCompare(b.country));
    },
    error: error => console.error(error)
  });
}
}
