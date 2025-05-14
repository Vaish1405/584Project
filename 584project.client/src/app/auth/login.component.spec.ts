import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './auth.service';
import { of, throwError } from 'rxjs';
import { Router } from '@angular/router';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    const authSpy = jasmine.createSpyObj('AuthService', ['login']);
    const routeSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      declarations: [LoginComponent],
      imports: [ReactiveFormsModule],
      providers: [
        { provide: AuthService, useValue: authSpy },
        { provide: Router, useValue: routeSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with empty values', () => {
    expect(component.form).toBeDefined();
    expect(component.form.controls['userName'].value).toBe('');
    expect(component.form.controls['password'].value).toBe('');
  });

  it('should call AuthService.login and navigate on success', () => {
    authServiceSpy.login.and.returnValue(of({
      success: true,
      message: 'Login successful',
      token: 'fake-jwt-token'
    }));
    

    component.form.controls['userName'].setValue('testuser');
    component.form.controls['password'].setValue('password');
    component.onSubmit();

    expect(authServiceSpy.login).toHaveBeenCalledWith({
      userName: 'testuser',
      password: 'password'
    });
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/']);
  });

  it('should log error on failed login', () => {
    const consoleSpy = spyOn(console, 'error');
    authServiceSpy.login.and.returnValue(throwError(() => new Error('Login failed')));

    component.form.controls['userName'].setValue('baduser');
    component.form.controls['password'].setValue('wrongpass');
    component.onSubmit();

    expect(consoleSpy).toHaveBeenCalledWith(jasmine.any(Error));
  });
});
