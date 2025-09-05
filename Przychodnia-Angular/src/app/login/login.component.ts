import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { CommonModule } from '@angular/common';

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
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder, 
    private http: HttpClient, 
    private router: Router,
    private authService: AuthService,
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

    this.authService.login(loginData).subscribe({
      next: (response) => {
        console.log('Logowanie nie udane:', response);
        alert(
          'Zalogowany'
        );

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
      error: (error) => {
        console.error('Błąd podczas Logowania:', error);
        alert(
          'Wystąpił błąd podczas Logowania: ' +
            (error.error || error.message || 'Nieznany błąd')
        );
      },
    });
  }
}
