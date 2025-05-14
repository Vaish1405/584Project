import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { environment } from '../../environments/environment.development';
import { LoginRequest } from './login-request';
import { LoginResponse } from './login-response';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
    localStorage.clear();
  });

  afterEach(() => {
    httpMock.verify(); // Ensures no outstanding requests
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should login and store token on success', (done) => {
    const mockRequest: LoginRequest = { userName: 'admin', password: 'test123' };
    const mockResponse: LoginResponse = {
      success: true,
      message: 'Login successful',
      token: 'fake-token'
    };

    service.authStatus.subscribe(status => {
      if (status) {
        expect(localStorage.getItem('584jwtToken')).toBe('fake-token');
        done();
      }
    });

    service.login(mockRequest).subscribe(response => {
      expect(response.success).toBeTrue();
    });

    const req = httpMock.expectOne(`${environment.baseUrl}api/Admin/login`);
    expect(req.request.method).toBe('POST');
    req.flush(mockResponse);
  });

  it('should not store token if login fails', (done) => {
    const mockRequest: LoginRequest = { userName: 'admin', password: 'wrong' };
    const mockResponse: LoginResponse = {
      success: false,
      message: 'Invalid credentials',
      token: ''
    };

    service.authStatus.subscribe(status => {
      expect(status).toBeFalse();
      expect(localStorage.getItem('584jwtToken')).toBeNull();
      done();
    });

    service.login(mockRequest).subscribe();

    const req = httpMock.expectOne(`${environment.baseUrl}api/Admin/login`);
    req.flush(mockResponse);
  });

  it('should logout and clear token', () => {
    localStorage.setItem('584jwtToken', 'some-token');
    service.logout();

    expect(localStorage.getItem('584jwtToken')).toBeNull();

    service.authStatus.subscribe(status => {
      expect(status).toBeFalse();
    });
  });

  it('should return true if authenticated', () => {
    localStorage.setItem('584jwtToken', 'some-token');
    expect(service.isAuthenticated()).toBeTrue();
  });

  it('should return false if not authenticated', () => {
    expect(service.isAuthenticated()).toBeFalse();
  });
});
