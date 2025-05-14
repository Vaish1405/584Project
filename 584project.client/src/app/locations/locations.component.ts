import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Locations } from '../locations'; 
import { environment } from '../../environments/environment';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-locations',
  imports: [RouterLink, CommonModule],
  templateUrl: './locations.component.html',
  styleUrl: './locations.component.css'
})
export class LocationsComponent implements OnInit {
  public locations: Locations[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getLocations();
  }
  
  getLocations() {
      this.http.get<Locations[]>(`${environment.baseUrl}api/Locations`).subscribe({
        next: result => this.locations = result,
        error: error => console.error(error)
      });
    }
}