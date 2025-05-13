import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationReviewsComponent } from './location-reviews.component';

describe('LocationReviewsComponent', () => {
  let component: LocationReviewsComponent;
  let fixture: ComponentFixture<LocationReviewsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LocationReviewsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LocationReviewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
