import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService, RegisterDTO } from '../auth.service';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, RouterModule, CommonModule, HttpClientModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        imie: ['', Validators.required],
        nazwisko: ['', Validators.required],
        pesel: [
          '',
          [Validators.required, Validators.pattern(/^\d{11}$/)],
        ],
        adres: ['', Validators.required],
        telefon: [
          '',
          [Validators.required, Validators.pattern(/^\d{9}$/)],
        ],
        email: ['', [Validators.required, Validators.email]],
        login: ['', Validators.required],
        haslo: ['', [Validators.required, Validators.minLength(6)]],
        potwierdzHaslo: ['', Validators.required],
        rola: ['Pacjent', Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('haslo')?.value;
    const confirmPassword = form.get('potwierdzHaslo')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      alert('Proszę poprawnie wypełnić formularz!');
      return;
    }

    const formValue = this.registerForm.value;

    const registerData: RegisterDTO = {
      imie: formValue.imie ?? '',
      nazwisko: formValue.nazwisko ?? '',
      pesel: formValue.pesel ?? '',
      adres: formValue.adres ?? '',
      telefon: formValue.telefon ?? '',
      email: formValue.email ?? '',
      login: formValue.login ?? '',
      haslo: formValue.haslo ?? '',
      potwierdzHaslo: formValue.potwierdzHaslo ?? '',
      rola: formValue.rola,
    };

    this.authService.register(registerData).subscribe({
      next: (response) => {
        console.log('Rejestracja udana:', response);
        alert(
          'Rejestracja zakończona sukcesem. Możesz się teraz zalogować.'
        );
        this.router.navigate(['/login']); 
      },
      error: (error) => {
        console.error('Błąd podczas rejestracji:', error);
        alert(
          'Wystąpił błąd podczas rejestracji: ' +
            (error.error || error.message || 'Nieznany błąd')
        );
      },
    });
  }
}
