import { TestBed } from '@angular/core/testing';
import { HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { authInterceptor } from './auth.interceptor';
import { Router } from '@angular/router';

describe('authInterceptor', () => {
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(() => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      providers: [
        { provide: Router, useValue: mockRouter }
      ]
    });
  });

  it('should add Authorization header when token exists', (done) => {
    localStorage.setItem('584jwtToken', 'fake-token');

    const req = new HttpRequest('GET', '/test');

    const next = (reqWithHeader: any) => {
      expect(reqWithHeader.headers.get('Authorization')).toBe('Bearer fake-token');
      localStorage.removeItem('584jwtToken');
      return of(new HttpResponse({ status: 200, body: {} }));
    };

    const result$ = authInterceptor(req, next);
    result$.subscribe(() => done());
  });

  it('should redirect to /login on 401 error', (done) => {
    const req = new HttpRequest('GET', '/test');

    const next = (_: any) => {
      const httpError = new HttpErrorResponse({
        status: 401,
        statusText: 'Unauthorized',
        url: '/test'
      });
      return throwError(() => httpError);
    };

    const result$ = authInterceptor(req, next);
    result$.subscribe({
      next: () => {},
      error: () => {
        expect(mockRouter.navigate).toHaveBeenCalledWith(['/login']);
        done();
      }
    });
  });
});
