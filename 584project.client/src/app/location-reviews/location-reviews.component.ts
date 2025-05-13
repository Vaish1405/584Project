import { Component, OnInit } from '@angular/core';
import { LocationReview } from '../location-review';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../environments/environment.development';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-location-reviews',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './location-reviews.component.html',
  styleUrl: './location-reviews.component.css'
})
export class LocationReviewsComponent implements OnInit {
  constructor(private route: ActivatedRoute, private http: HttpClient) {} 
  locationId!: number;
  reviewData!: LocationReview;
  loading = true;

  ngOnInit(): void {
    let locationId = this.route.snapshot.paramMap.get('id');
    this.http.get<LocationReview>(`${environment.baseUrl}api/LocationReviews/${locationId}`)
      .subscribe({
        next: (data) => {
          this.reviewData = data;
          this.loading = false;
        },
        error: (err) => {
          console.error('Error fetching review data:', err);
          this.loading = false;
        }
      });
  }
}
