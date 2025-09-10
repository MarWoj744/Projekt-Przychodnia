import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { Subscription } from 'rxjs';

import { WizytyService } from '../../services/wizyty.service';
import { Wizyta } from '../../models/wizyta.model';

@Component({
  standalone: true,
  selector: 'app-pacjent-wizyty',
  imports: [CommonModule, DatePipe],
  templateUrl: './pacjent-wizyty.component.html'
})
export class PacjentWizytyComponent implements OnInit, OnDestroy {
  loading = false;
  error?: string;

  wizyty: Wizyta[] = [];
  wizytyNadchodzace: Wizyta[] = [];

  private sub?: Subscription;

  constructor(private wizytySvc: WizytyService) {}

  ngOnInit(): void {
    const pacjentId = this.readPatientIdFromLS();
    if (pacjentId == null) {
      this.error = 'Brak identyfikatora pacjenta w localStorage.';
      return;
    }

    this.loading = true;
    this.sub = this.wizytySvc.getWizytyByPacjentId(pacjentId).subscribe({
      next: (list) => {
        const sorted = [...(list ?? [])].sort(
          (a, b) => +new Date(a.data) - +new Date(b.data)
        );
        this.wizyty = sorted;

        const now = Date.now();
        this.wizytyNadchodzace = sorted.filter(
          (w) => +new Date(w.data) >= now
        );

        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Nie udało się pobrać wizyt.';
        this.loading = false;
      },
    });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  trackById = (_: number, w: Wizyta) => w.id;

  private readPatientIdFromLS(): number | null {
    const raw = localStorage.getItem('userId'); // upewnij się, że tak nazywasz klucz po logowaniu
    if (!raw) return null;
    const n = Number(raw);
    return Number.isFinite(n) ? n : null;
  }
}
