import { Component, OnInit } from '@angular/core';
import { Harmonogram } from '../../models/harmonogram.model';
import { HarmonogramService } from '../../services/harmonogram.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-harmonogram',
  imports: [CommonModule],
  templateUrl: './harmonogram.component.html',
  styleUrls: ['./harmonogram.component.css']
})
export class HarmonogramComponent implements OnInit {
  harmonogram: Harmonogram[] = [];
  error: string | null = null;
  lekarzId = 1; 

  constructor(private harmonogramService: HarmonogramService) {}

  ngOnInit(): void {
    this.harmonogramService.getByLekarzId(this.lekarzId).subscribe({
      next: (data) => this.harmonogram = data,
      error: () => this.error = 'Błąd ładowania harmonogramu'
    });
  }
}
