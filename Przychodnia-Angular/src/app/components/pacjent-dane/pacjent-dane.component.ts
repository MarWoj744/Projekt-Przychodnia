// src/app/components/pacjent-dane/pacjent-dane.component.ts
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Pacjent } from '../../models/pacjent.model';
import { PacjentService } from '../../services/pacjent.service';

@Component({
  selector: 'app-pacjent-dane',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pacjent-dane.component.html',
  styleUrls: ['./pacjent-dane.component.css'],
})
export class PacjentDaneComponent implements OnInit {
  pacjent$!: Observable<Pacjent | null>; // <- bez inicjalizatora

  constructor(private pacjentSvc: PacjentService) {}

  ngOnInit(): void {
    this.pacjent$ = this.pacjentSvc.pacjent$; // <- inicjalizacja po DI
  }
}
