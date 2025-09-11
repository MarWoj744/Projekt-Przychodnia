import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { catchError, of } from 'rxjs';
import { Pacjent } from '../../../models/pacjent.model';

@Component({
  selector: 'app-pacjent-dane',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './pacjent-dane.component.html',
  styleUrls: ['./pacjent-dane.component.css'],
})
export class PacjentDaneComponent implements OnInit {
  private apiUrl = 'http://localhost:5120/api';

  loading = false;
  error?: string;
  pacjent: Pacjent | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const role = localStorage.getItem('userRole');
    const idStr = localStorage.getItem('userId');

    if (!idStr) {
      this.error = 'Brak identyfikatora pacjenta w localStorage.';
      return;
    }
    // if (role !== 'Pacjent') {
    //   this.error = 'Zalogowana rola nie jest pacjentem.';
    //   return;
    // }

    const id = Number(idStr);
    if (!Number.isFinite(id) || id <= 0) {
      this.error = 'Nieprawidłowy identyfikator pacjenta.';
      return;
    }

    this.loading = true;
    this.http
      .get<Pacjent>(`${this.apiUrl}/Pacjent/${id}`)
      .pipe(
        catchError(err => {
          console.error(err);
          this.error = 'Nie udało się pobrać danych pacjenta.';
          return of(null);
        })
      )
      .subscribe(p => {
        this.pacjent = p;
        this.loading = false;
      });
  }
}
