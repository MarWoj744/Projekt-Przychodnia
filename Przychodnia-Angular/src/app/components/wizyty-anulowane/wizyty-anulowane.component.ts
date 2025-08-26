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
[x: string]: any;
    wizytyAnulowane: Wizyta[] = [];
  error: string | null = null;
  lekarzId = 1; 

  constructor(private wizytyService: WizytaService) {}

  
   ngOnInit(): void {
    this.loadWizytyAnulowane();
  }

  loadWizytyAnulowane() {
    this.wizytyService.getWizytyAnulowane().subscribe({
      next: data => this.wizytyAnulowane = data,
      error: err => this.error = 'Błąd wczytywania anulowanych wizyt'
    });
  }
}
