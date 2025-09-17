import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WizytyService } from '../../../services/wizyty.service';
import { Wizyta } from '../../../models/wizyta.model';
import { Observable, Subscription, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

type WizytaVM = Omit<Wizyta, 'data'> & { data: Date };

@Component({
  standalone: true,
  selector: 'app-pacjent-wizyty',
  imports: [CommonModule],
  templateUrl: './pacjent-wizyty.component.html',
  styleUrl: './pacjent-wizyty.component.css'
})
export class PacjentWizytyComponent implements OnInit, OnDestroy {
  loading = false;
  error?: string;

  wizyty: WizytaVM[] = [];
  wizytyNadchodzace: WizytaVM[] = [];

  private sub?: Subscription;

  constructor(private wizytySvc: WizytyService,private router: Router) {}

  ngOnInit(): void {
    const userId = Number(localStorage.getItem('userId'));
    if (!userId) {
      this.error = 'Brak identyfikatora pacjenta w localStorage.';
      return;
    }

    this.loading = true;
    this.sub = this.wizytySvc.getWizytyByPacjentId(userId)
      .pipe(
        map(list =>
          (list ?? []).map(w => ({
            ...w,
            data: new Date((w as any).data)
          })) as WizytaVM[]
        ),
        catchError(err => {
          console.error(err);
          this.error = 'Nie udało się pobrać wizyt.';
          return of([] as WizytaVM[]);
        })
      )
      .subscribe(list => {
        this.loading = false;
        const sorted = [...list].sort((a, b) => +a.data - +b.data);
        this.wizyty = sorted;

        const now = Date.now();
        this.wizytyNadchodzace = sorted.filter(w => +w.data >= now);
      });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  statusText(status: Wizyta['status']): string {
    switch (status) {
      case 'Zaplanowana': return 'Zaplanowana';
      case 'Zrealizowana': return 'Zrealizowana';
      case 'Anulowana':   return 'Anulowana';
      default:            return String(status);
    }
  }

  statusClass(status: Wizyta['status']): string {
    switch (status) {
      case 'Zaplanowana': return 'badge badge--plan';
      case 'Zrealizowana': return 'badge badge--done';
      case 'Anulowana':   return 'badge badge--cancel';
      default:            return 'badge';
    }
  }

  canCancel(w: WizytaVM): boolean {
    if (w.status !== 'Zaplanowana') return false;
    const hoursToVisit = (+w.data - Date.now()) / (1000 * 60 * 60);
    return hoursToVisit >= 24;
  }

  cancelReason(w: WizytaVM): string {
    if (w.status !== 'Zaplanowana') return 'Wizyta nie jest zaplanowana.';
    const hoursToVisit = (+w.data - Date.now()) / (1000 * 60 * 60);
    if (hoursToVisit < 24) return 'Mniej niż 24 godziny do wizyty.';
    return '';
  }

  isCancelable(w: WizytaVM): boolean { return this.canCancel(w); }
  cantCancelReason(w: WizytaVM): string { return this.cancelReason(w); }

  trackById(index: number, item: WizytaVM): number {
    return item.id;
  }
doctorName(w: any): string {
  if (typeof w.lekarz === 'string' && w.lekarz.trim()) return w.lekarz;

  const fromSplit = [w.lekarzImie, w.lekarzNazwisko].filter(Boolean).join(' ').trim();
  if (fromSplit) return fromSplit;

  const fromObj = [w?.lekarz?.imie, w?.lekarz?.nazwisko].filter(Boolean).join(' ').trim();
  if (fromObj) return fromObj;

  return '-';
}
  anuluj(w: WizytaVM): void {
    if (!this.canCancel(w)) return;
    if (!confirm('Czy na pewno chcesz anulować tę wizytę?')) return;

    this.wizytySvc.anulujWizyte(w.id).subscribe({
      next: () => {
        const userId = Number(localStorage.getItem('userId'));
        if (!userId) return;

        this.loading = true;
        this.wizytySvc
          .getWizytyByPacjentId(userId)
          .pipe(map(list => (list ?? []).map(x => ({ ...x, data: new Date((x as any).data) })) as WizytaVM[]))
          .subscribe(fresh => {
            this.loading = false;
            const sorted = [...fresh].sort((a, b) => +a.data - +b.data);
            this.wizyty = sorted;

            const now = Date.now();
            this.wizytyNadchodzace = sorted.filter(v => +v.data >= now);
          });
      },
      error: err => {
        console.error(err);
        alert('udało się anulować wizytę.');
        this.ngOnInit();
      }
    });
  }
}
