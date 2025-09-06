import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WizytyService } from '../../services/wizyty.service';
import { Wizyta } from '../../models/wizyta.model';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-wizyty',
  standalone: true,
  imports: [CommonModule,FormsModule, RouterModule],
  templateUrl: './wizyty.component.html',
  styleUrls: ['./wizyty.component.css']
})

export class WizytyComponent  implements OnInit {
  wizyty: Wizyta[] = [];
  error: string = '';

  constructor(private wizytaService: WizytyService) {}

  ngOnInit(): void {
    this.loadWizyty();
  }

  loadWizyty() {
  const userRole = localStorage.getItem('rola');
  const userId = Number(localStorage.getItem('userId'));

  if (userRole === 'Lekarz') {
    this.wizytaService.getWizytyByLekarzId(userId).subscribe({
      next: data => this.wizyty = data,
      error: err => this.error = 'Błąd wczytywania wizyt'
    });
  } else {
      this.wizytaService.getWizyty().subscribe({
        next: data => this.wizyty = data,
        error: err => this.error = 'Błąd wczytywania wizyt'
      });
    }
  }

  anulujWizyte(id: number) {
  this.wizytaService.anulujWizyte(id).subscribe({
    next: () => this.loadWizyty(),
    error: (err) => {
    console.error('Błąd anulowania wizyty:', err);
    this.error = 'Nie udało się anulować wizyty'
    }});
  }
}
