
import { Component, OnInit } from '@angular/core';
import { Harmonogram } from '../../models/harmonogram.model';
import { HarmonogramService } from '../../services/harmonogram.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-harmonogram',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './harmonogram.component.html',
  styleUrls: ['./harmonogram.component.css']
})
export class HarmonogramComponent implements OnInit {
  harmonogram: Harmonogram[] = [];
  error: string | null = null;
  isLekarz: boolean = false;
  lekarzName: string | null = null;
  
  aktualnyHarmonogram: Harmonogram = {
    id: 0,
    lekarzId: 0,
    dataOd: '',
    dataDo: '',
    opis: ''
  };

  edycjaTryb: boolean = false; 

  constructor(private harmonogramService: HarmonogramService) {}

  ngOnInit(): void {
    const userRole = localStorage.getItem('userRole');
    const userId = Number(localStorage.getItem('userId'));
    this.isLekarz = (userRole === 'Lekarz' || userRole === '2');
    this.lekarzName = localStorage.getItem('userName');

    console.log('userRole', userRole, 'userId', userId, 'isLekarz', this.isLekarz);
  
    if (this.isLekarz) {
      this.aktualnyHarmonogram.lekarzId = userId;
    }

    this.loadHarmonogram();
  }

  loadHarmonogram() {
  const userRole = localStorage.getItem('userRole');
  const userId = Number(localStorage.getItem('userId'));

  if (this.isLekarz) {
    this.harmonogramService.getByLekarzId(userId).subscribe({
      next: (data) => this.harmonogram = data,
      error: () => this.error = 'Błąd ładowania harmonogramu'
    });
  } else {
      this.harmonogramService.getAll().subscribe({
        next: (data) => this.harmonogram = data,
        error: () => this.error = 'Błąd ładowania harmonogramu'
      });
    }
  }

  rozpocznijEdycje(h: Harmonogram) {
    this.edycjaTryb = true;
    this.aktualnyHarmonogram = { ...h }; 
  }

  anulujEdycje() {
    this.edycjaTryb = false;
    this.aktualnyHarmonogram = {
      id: 0,
      lekarzId: 0,
      dataOd: '',
    dataDo: '',
    opis: ''
      };
  }

  zapisz() {
    if (this.isLekarz) {
    this.aktualnyHarmonogram.lekarzId = Number(localStorage.getItem('userId'));
    }

    if (this.edycjaTryb) {
      this.harmonogramService.update(this.aktualnyHarmonogram.id, this.aktualnyHarmonogram).subscribe({
        next: (response) => {
          console.log('Odpowiedź z serwera podczas aktualizacji:', response); 
          this.loadHarmonogram();
          this.anulujEdycje();
        },
        error: (error) => {
          console.error('Błąd podczas aktualizacji:', error); 
          this.error = 'Błąd podczas aktualizacji';
        }
      });
    } else {
      this.harmonogramService.create(this.aktualnyHarmonogram).subscribe({
        next: (response) => {
          console.log('Odpowiedź z serwera podczas tworzenia:', response); 
          this.loadHarmonogram();
          this.anulujEdycje();
        },
        error: (error) => {
          console.error('Błąd podczas tworzenia:', error); 
          this.error = 'Błąd podczas tworzenia';
        }
      });
    }
  }

  usun(id: number) {
    if (confirm('Na pewno chcesz usunąć ten harmonogram?')) {
      this.harmonogramService.delete(id).subscribe({
        next: () => this.loadHarmonogram(),
        error: () => this.error = 'Błąd podczas usuwania'
      });
    }
  }
}