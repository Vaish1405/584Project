import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LocationsComponent } from './locations.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { environment } from '../../environments/environment.development';
import { Locations } from '../locations';

describe('LocationsComponent', () => {
  let component: LocationsComponent;
  let fixture: ComponentFixture<LocationsComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [LocationsComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(LocationsComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch locations on init', () => {
    const mockLocations: Locations[] = [
      {
        locationId: 1,
        cityName: 'San Francisco',
        country: 'USA',
        latitude: 37.7749,
        longitude: -122.4194,
        restaurantName: 'Bay Grill'
      },
      {
        locationId: 2,
        cityName: 'Paris',
        country: 'France',
        latitude: 48.8566,
        longitude: 2.3522,
        restaurantName: 'Le Gourmet'
      }
    ];    

    fixture.detectChanges(); // triggers ngOnInit and getLocations

    const req = httpMock.expectOne(`${environment.baseUrl}api/Locations`);
    expect(req.request.method).toBe('GET');

    req.flush(mockLocations);

    expect(component.locations.length).toBe(2);
    expect(component.locations).toEqual(mockLocations);
  });

  it('should handle HTTP error gracefully', () => {
    const consoleSpy = spyOn(console, 'error');

    fixture.detectChanges(); // triggers ngOnInit and getLocations

    const req = httpMock.expectOne(`${environment.baseUrl}api/Locations`);
    req.flush('Error loading locations', { status: 500, statusText: 'Server Error' });

    expect(consoleSpy).toHaveBeenCalled();
    expect(component.locations.length).toBe(0);
  });
});
