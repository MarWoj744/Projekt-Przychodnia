import { Component, OnInit } from '@angular/core';
import { Wizyta } from '../../models/wizyta.model';
import { WizytyService } from '../../services/wizyty.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-wizyty-anulowane',
  standalone : true,
  imports: [CommonModule],
  templateUrl: './wizyty-anulowane.component.html',
  styleUrls: ['./wizyty-anulowane.component.css']
})

export class WizytyAnulowaneComponent implements OnInit {
  wizytyAnulowane: Wizyta[] = [];
  error: string | null = null;

  constructor(private wizytyService: WizytyService) {}

  ngOnInit(): void {
    this.loadWizytyAnulowane();
  }

   loadWizytyAnulowane() {
    const userRole = localStorage.getItem('rola');
    const userId = Number(localStorage.getItem('userId'));

    if (userRole === 'Lekarz') {
    this.wizytyService.getAnulowaneByLekarzId(userId).subscribe({
      next: data => this.wizytyAnulowane = data,
      error: err => this.error = 'Błąd wczytywania anulowanych wizyt'
    });
    } else {
      this.wizytyService.getWizytyAnulowane().subscribe({
        next: data => this.wizytyAnulowane = data,
        error: err => this.error = 'Błąd wczytywania anulowanych wizyt'
      });
    }
  }
}
