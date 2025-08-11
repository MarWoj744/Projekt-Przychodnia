import { Component, OnInit } from '@angular/core';
import { Wizyta } from '../../models/wizyta.model';
import { WizytaService } from '../../services/wizyty.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-wizyty-anulowane',
  imports: [CommonModule],
  templateUrl: './wizyty-anulowane.component.html',
  styleUrls: ['./wizyty-anulowane.component.css']
})
export class WizytyAnulowaneComponent implements OnInit {
  wizyty: Wizyta[] = [];
  error: string | null = null;
  lekarzId = 1; 

  constructor(private wizytyService: WizytaService) {}

  ngOnInit(): void {
    this.wizytyService.getAnulowaneByLekarzId(this.lekarzId).subscribe({
      next: (data) => this.wizyty = data,
      error: () => this.error = 'Błąd ładowania anulowanych wizyt'
    });
  }
}
