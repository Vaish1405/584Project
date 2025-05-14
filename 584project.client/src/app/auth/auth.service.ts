import { Injectable } from '@angular/core';
import { LoginRequest } from './login-request';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginResponse } from './login-response';
import { environment } from '../../environments/environment';
 import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _authStatus  = new BehaviorSubject<boolean>(false);
  authStatus = this._authStatus.asObservable();

  constructor(private http: HttpClient) { }
 
   private setAuthStatus(value: boolean) {
     this._authStatus.next(value);
   }

  login(loginRequest: LoginRequest) :Observable<LoginResponse> {
     let url = `${environment.baseUrl}api/Admin/login`;
     return this.http.post<LoginResponse>(url, loginRequest)
     .pipe(tap(loginResponse => {
       if (loginResponse.success) {
         localStorage.setItem("584jwtToken", loginResponse.token);
         this.setAuthStatus(true);
       }
     }))
  }
  logout() {
    localStorage.removeItem("584jwtToken");
    this.setAuthStatus(false);
  }

  isAuthenticated() :boolean{
    return localStorage.getItem("584jwtToken") != null;
  }
}
