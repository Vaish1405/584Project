import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LocationReviewsComponent } from './location-reviews.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { LocationReview } from '../location-review';
import { environment } from '../../environments/environment.development';

describe('LocationReviewsComponent', () => {
  let component: LocationReviewsComponent;
  let fixture: ComponentFixture<LocationReviewsComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: () => '42' // mock location ID
              }
            }
          }
        }
      ],
      declarations: [LocationReviewsComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(LocationReviewsComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch review data on init', () => {
    const mockReview: LocationReview = {
      locationId: 42,
      cityName: 'San Diego',
      country: 'USA',
      adminName: 'California',
      latitude: 32.7157,
      longitude: -117.1611,
      restaurantName: 'Ocean Breeze Diner',
      reviewScore: 4.6,
      reviewCount: '128'
    };

    fixture.detectChanges(); 

    const req = httpMock.expectOne(`${environment.baseUrl}api/LocationReviews/42`);
    expect(req.request.method).toBe('GET');

    req.flush(mockReview);

    expect(component.reviewData).toEqual(mockReview);
    expect(component.loading).toBeFalse();
  });

  it('should handle error when fetching review data', () => {
    const consoleSpy = spyOn(console, 'error');

    fixture.detectChanges(); // triggers ngOnInit

    const req = httpMock.expectOne(`${environment.baseUrl}api/LocationReviews/42`);
    req.flush('Error occurred', { status: 500, statusText: 'Server Error' });

    expect(consoleSpy).toHaveBeenCalled();
    expect(component.reviewData).toBeUndefined();
    expect(component.loading).toBeFalse();
  });
});
