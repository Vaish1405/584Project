import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
 
 export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
   let token = localStorage.getItem("584jwtToken");
   
   if (token) {
     req = req.clone({
       setHeaders: {
         Authorization: `Bearer ${token}`
       }
     });
   }
   return next(req).pipe(catchError(error => {
    if(error instanceof HttpErrorResponse && error.status === 401) {
      const returnUrl = router.url;
      router.navigate(["/login"], { queryParams: { returnUrl } });
    }
    return throwError(() => error);
  }));
 };