import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface AuthResponse {
  token: string;
  refreshToken: string;
  userId: number;
  login: string;
  imie: string;
  nazwisko: string;
  email: string;
  rola: string;
  tokenExpiration: string;
  isActive: boolean;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder, 
    private http: HttpClient, 
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      login: ['', Validators.required],
      haslo: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'Proszę uzupełnić wymagane pola';
      return;
    }

    const loginData = {
      login: this.loginForm.value.login,
      haslo: this.loginForm.value.haslo
    };

    this.http.post<AuthResponse>('api/auth/login', loginData).subscribe({
      next: (response) => {
        if (response && response.token) {
          localStorage.setItem('jwtToken', response.token);
          localStorage.setItem('refreshToken', response.refreshToken);
          localStorage.setItem('userLogin', response.login);
          localStorage.setItem('userRole', response.rola);

          this.router.navigate(['/']);
        } else {
          this.errorMessage = 'Niepoprawna odpowiedź z serwera.';
        }
      },
      error: (err) => {
        this.errorMessage = err.error || 'Błąd podczas logowania. Sprawdź dane.';
      }
    });
  }
}
