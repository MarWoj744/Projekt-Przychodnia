import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { WizytaService } from '../../services/wizyty.service';
import { Wizyta } from '../../models/wizyta.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-wizyta-add',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './wizyta-add.component.html',
  styleUrls: ['./wizyta-add.component.css']
})
export class WizytaAddComponent implements OnInit {
  wizyta: Partial<Wizyta> = {
    pacjent: '',
    lekarz: '',
    data: '',
    godzina: '',
    status: 'Zaplanowana',
    badanie: ''
  };

  error = '';
  success = '';

  constructor(private wizytaService: WizytaService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: any) => {
      if (params['lekarzId']) {
        this.wizyta.lekarz = params['lekarzId'];
      }
      if (params['dataOd']) {
        this.wizyta.data = params['dataOd'];
      }
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
        setTimeout(() => this.router.navigate(['/wizyty']), 1000);
      },
      error: () => this.error = 'Nie udało się dodać wizyty'
    });
  }
}
