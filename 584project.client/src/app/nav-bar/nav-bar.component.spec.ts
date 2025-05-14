import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { WeatherForecast } from '../weather-forecast';
import { environment } from '../../environments/environment.development';
import { WeatherComponent } from '../weather/weather.component';

describe('WeatherComponent', () => {
  let component: WeatherComponent;
  let fixture: ComponentFixture<WeatherComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [WeatherComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(WeatherComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch weather forecasts on init', () => {
    const mockForecasts: WeatherForecast[] = [
      {
        date: '2025-05-13',
        temperatureC: 25,
        temperatureF: 77,
        summary: 'Sunny'
      },
      {
        date: '2025-05-14',
        temperatureC: 22,
        temperatureF: 71.6,
        summary: 'Cloudy'
      }
    ];
    

    fixture.detectChanges(); // triggers ngOnInit -> getForecasts()

    const req = httpMock.expectOne(`${environment.baseUrl}weatherforecast`);
    expect(req.request.method).toBe('GET');

    req.flush(mockForecasts);

    expect(component.forecasts).toEqual(mockForecasts);
  });

  it('should handle error when fetching forecasts', () => {
    const consoleSpy = spyOn(console, 'error');

    fixture.detectChanges(); // triggers ngOnInit -> getForecasts()

    const req = httpMock.expectOne(`${environment.baseUrl}weatherforecast`);
    req.flush('Error occurred', { status: 500, statusText: 'Server Error' });

    expect(consoleSpy).toHaveBeenCalled();
    expect(component.forecasts).toEqual([]);
  });
});
