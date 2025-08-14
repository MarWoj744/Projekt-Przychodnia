import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WizytaService } from '../../services/wizyty.service';
import { Wizyta } from '../../models/wizyta.model';
@Component({
  selector: 'app-wizyty',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './wizyty.component.html',
  styleUrls: ['./wizyty.component.css']
})
export class WizytyComponent  implements OnInit {
  wizyty: Wizyta[] = [];
  error: string = '';

  constructor(private wizytaService: WizytaService) {}

  ngOnInit(): void {
    this.loadWizyty();
  }

  loadWizyty() {
    this.wizytaService.getWizyty().subscribe({
      next: data => this.wizyty = data,
      error: err => this.error = 'Błąd wczytywania wizyt'
    });
  }
}
