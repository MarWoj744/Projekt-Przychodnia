import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { WykonaneBadania } from '../../../models/wykonane-badania.model';
import { WykonaneBadaniaService } from '../../../services/wykonane-badania.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-pacjent-badania',
  standalone: true,
   imports: [CommonModule],
  templateUrl: './pacjent-badania.component.html',
  styleUrls: ['./pacjent-badania.component.css'],
})
export class PacjentBadaniaComponent implements OnInit, OnDestroy {
  badania: WykonaneBadania[] = [];
  loading = false;
  error?: string;

  private sub?: Subscription;

  constructor(private badaniaSvc: WykonaneBadaniaService) {}

  ngOnInit(): void {
    const idStr = localStorage.getItem('userId');
    const userId = idStr ? Number(idStr) : NaN;

    if (Number.isNaN(userId)) {
      this.error = 'Brak identyfikatora pacjenta w localStorage.';
      return;
    }

    this.loadBadania(userId);
  }

  private loadBadania(pacjentId: number): void {
    this.loading = true;
    this.sub = this.badaniaSvc.getByPacjent(pacjentId).subscribe({
      next: (list) => {
        this.badania = [...list].sort(
          (a, b) => new Date(b.data).getTime() - new Date(a.data).getTime()
        );
        this.loading = false;
      },
      error: () => {
        this.error = 'Nie udało się pobrać badań.';
        this.loading = false;
      },
    });
  }

  pobierzPdf(b: WykonaneBadania): void {
    this.badaniaSvc.savePdf(b.id);
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
}
