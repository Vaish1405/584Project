import { Routes } from '@angular/router';
import { WeatherComponent } from './weather/weather.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LocationsComponent } from './locations/locations.component';
import { ReviewsComponent } from './reviews/reviews.component';

export const routes: Routes = [
    { path: "weather", component: WeatherComponent },
    { path: "navbar", component: NavBarComponent },
    { path: "locations", component: LocationsComponent},
    {path: "reviews", component: ReviewsComponent},
    { path: "", component: WeatherComponent, pathMatch: "full" }
];