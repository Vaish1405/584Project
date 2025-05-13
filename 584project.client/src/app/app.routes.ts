import { Routes } from '@angular/router';
import { WeatherComponent } from './weather/weather.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LocationsComponent } from './locations/locations.component';
import { LoginComponent } from './auth/login.component';
import { LocationReviewsComponent } from './location-reviews/location-reviews.component';

export const routes: Routes = [
    { path: "weather", component: WeatherComponent },
    { path: "navbar", component: NavBarComponent },
    { path: "locations", component: LocationsComponent},
    {path: "location-reviews/:id", component: LocationReviewsComponent},
    {path: "login", component: LoginComponent},
    { path: "", component: WeatherComponent, pathMatch: "full" }
];