import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { WizytyService } from '../../services/wizyty.service';
import { Wizyta } from '../../models/wizyta.model';
import { ActivatedRoute } from '@angular/router';
import { RejestracjaWizytyDTO } from '../../models/rejestracja-wizyty-dto.model';

@Component({
  selector: 'app-wizyta-add',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './wizyta-add.component.html',
  styleUrls: ['./wizyta-add.component.css']
})
export class WizytaAddComponent implements OnInit {
  formModel: {
    pacjentId?: number | null;
    lekarzId?: number | null;
    recepcjonistkaId?: number | null;
    data?: string;
    godzina?: string;
    opis?: string | null;
  } = {
    pacjentId: null,
    lekarzId: null,
    recepcjonistkaId: null,
    data: '',
    godzina: '',
    opis: ''
  };

 error : string | undefined;
  success = '';

  constructor(
    private wizytaService: WizytyService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: any) => {
      if (params['lekarzId']) {
        this.formModel.lekarzId = Number(params['lekarzId']);
      }
      if (params['dataOd']) {
        this.formModel.data = params['dataOd'];
      }
    });
  }

  private buildIsoDateTime(date: string | undefined, time: string | undefined): string {
    if (!date || !time) return new Date().toISOString();
    const dt = new Date(`${date}T${time}`);
    return dt.toISOString();
  }

  onSubmit() {
 
    if (!this.formModel.pacjentId || !this.formModel.lekarzId || !this.formModel.data || !this.formModel.godzina) {
      this.error = 'Wszystkie pola oprócz recepcjonistki i opisu są wymagane';
      return;
    }

    const dto: RejestracjaWizytyDTO = {
      pacjentId: Number(this.formModel.pacjentId),
      lekarzId: Number(this.formModel.lekarzId),
      recepcjonistkaId: this.formModel.recepcjonistkaId ? Number(this.formModel.recepcjonistkaId) : null,
      dataWizyty: this.buildIsoDateTime(this.formModel.data, this.formModel.godzina),
      opis: this.formModel.opis ? this.formModel.opis : null
    };

    this.wizytaService.addWizyta(dto).subscribe({
      next: () => {
        this.success = 'Wizyta została dodana!';
        this.router.navigate(['/wizyty']);
        setTimeout(() => this.router.navigate(['/wizyty']), 800);
      },
      error: (err) => {
        console.error(err);
        if (err?.error) {
          const msg = typeof err.error === 'string' ? err.error : err.error.title || JSON.stringify(err.error);
          this.router.navigate(['/pacjent/wizyty']);
          this.error = `Błąd serwera: ${msg}`;
        } else {
          this.error = 'Nie udało się dodać wizyty';
        }
      }
    });
  }
}
