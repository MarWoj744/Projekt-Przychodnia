import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { WizytyService } from '../../../services/wizyty.service';
import { LekarzService } from '../../../services/lekarz.service';
import { Lekarz } from '../../../models/lekarz.model';
import { RejestracjaWizytyDTO } from '../../../models/rejestracja-wizyty-dto.model';

@Component({
  selector: 'app-pacjent-wizyta-add',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pacjent-wizyta-add.component.html',
  styleUrls: ['./pacjent-wizyta-add.component.css']
})
export class PacjentWizytaAddComponent implements OnInit {
  // model formularza — przechowuje ID i osobno datę/godzinę
  formModel: {
    pacjentId?: number | null;
    lekarzId?: number | null;
    badanieId?: number | null;
    data?: string;   // "YYYY-MM-DD"
    godzina?: string; // "HH:MM"
  } = {
    pacjentId: null,
    lekarzId: null,
    badanieId: null,
    data: '',
    godzina: ''
  };

  lekarze: Lekarz[] = [];
  error = '';
  success = '';
  userId?: number;

  constructor(
    private wizytaService: WizytyService,
    private lekarzService: LekarzService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // pobierz userId z localStorage (jeśli zapisane)
    const idStr = localStorage.getItem('userId') ?? localStorage.getItem('pacjentId') ?? '';
    this.userId = idStr ? Number(idStr) : undefined;

    if (this.userId) {
      this.formModel.pacjentId = this.userId;
    }

    // opcjonalne parametry (np. badanieId)
    this.route.queryParams.subscribe((params: any) => {
      if (params['badanieId']) {
        this.formModel.badanieId = Number(params['badanieId']);
      }
      if (params['lekarzId']) {
        this.formModel.lekarzId = Number(params['lekarzId']);
      }
      if (params['data']) {
        this.formModel.data = params['data']; // oczekujemy "YYYY-MM-DD"
      }
    });

    // pobierz listę lekarzy
    this.lekarzService.getAll().subscribe({
      next: (list) => (this.lekarze = list),
      error: () => (this.error = 'Nie udało się pobrać listy lekarzy')
    });
  }

  private buildIsoDateTime(date: string | undefined, time: string | undefined): string {
    if (!date || !time) return new Date().toISOString();
    const dt = new Date(`${date}T${time}`);
    return dt.toISOString();
  }

  onSubmit() {
    this.error = '';
    this.success = '';

    if (!this.formModel.pacjentId || !this.formModel.lekarzId || !this.formModel.data || !this.formModel.godzina) {
      this.error = 'Wszystkie pola oprócz badania są wymagane';
      return;
    }

    const dto: RejestracjaWizytyDTO = {
      pacjentId: Number(this.formModel.pacjentId),
      lekarzId: Number(this.formModel.lekarzId),
      recepcjonistkaId: null, // jeśli nie masz tej informacji, zostaw null
      dataWizyty: this.buildIsoDateTime(this.formModel.data, this.formModel.godzina),
      opis: this.formModel.badanieId ? String(this.formModel.badanieId) : null
    };

    this.wizytaService.addWizyta(dto).subscribe({
      next: () => {
        this.success = 'Wizyta została dodana!';
        setTimeout(() => this.router.navigate(['/pacjent/wizyty']), 800);
      },
      error: (err) => {
        console.error(err);
        if (err?.error) {
          const msg = typeof err.error === 'string' ? err.error : err.error.title || JSON.stringify(err.error);
          this.error = `Błąd serwera: ${msg}`;
        } else {
          this.error = 'Nie udało się dodać wizyty';
        }
      }
    });
  }
}
