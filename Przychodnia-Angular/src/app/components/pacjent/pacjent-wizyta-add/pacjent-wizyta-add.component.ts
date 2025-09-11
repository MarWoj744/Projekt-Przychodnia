import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Wizyta } from '../../../models/wizyta.model';
import { WizytyService } from '../../../services/wizyty.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Lekarz } from '../../../models/lekarz.model';
import { LekarzService } from '../../../services/lekarz.service';

@Component({
  selector: 'app-pacjent-wizyta-add',
  imports: [CommonModule, FormsModule],
  templateUrl: './pacjent-wizyta-add.component.html',
  styleUrl: './pacjent-wizyta-add.component.css'
})

export class PacjentWizytaAddComponent implements OnInit {
  wizyta: Partial<Wizyta> = {
    pacjent: '',
    lekarz: '',
    data: '',
    godzina: '',
    status: 'Zaplanowana',
    badanie: ''
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
    const idStr = localStorage.getItem('userId') ?? localStorage.getItem('pacjentId') ?? '';
    this.userId = idStr ? Number(idStr) : undefined;

    if (this.userId) {
      this.wizyta.pacjent = String(this.userId);
    }

    this.route.queryParams.subscribe((params: any) => {
      if (params['badanieId']) {
        this.wizyta.badanie = params['badanieId'];
      }
    });

    this.lekarzService.getAll().subscribe({
      next: (list) => (this.lekarze = list),
      error: () => (this.error = 'Nie udało się pobrać listy lekarzy')
    });
  }

  onSubmit() {
    if (!this.wizyta.pacjent || !this.wizyta.lekarz || !this.wizyta.data || !this.wizyta.godzina) {
      this.error = 'Wszystkie pola oprócz badania są wymagane';
      return;
    }

    this.wizytaService.addWizyta(this.wizyta as Wizyta).subscribe({
      next: () => {
        this.success = 'Wizyta została dodana!';
        setTimeout(() => this.router.navigate(['/pacjent/wizyty']), 1000);
      },
      error: () => this.error = 'Nie udało się dodać wizyty'
    });
  }
}
