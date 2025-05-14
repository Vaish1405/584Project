import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field'
import { Router, RouterLink } from '@angular/router';
import { MatInputModule } from '@angular/material/input'
import { LoginRequest } from './login-request';
import { AuthService } from './auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [MatFormFieldModule, MatInputModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  returnUrl: string = '/';

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.form = new FormGroup({
      userName: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  
  onSubmit() {
    const loginRequest = <LoginRequest>{
      userName: this.form.controls['userName'].value,
      password: this.form.controls['password'].value
    };
  
    this.authService.login(loginRequest).subscribe({
      next: result => {
        if (result.success) {
          this.router.navigateByUrl(this.returnUrl);
        }
      },
      error: error => console.error(error)
    });
  }
  
}