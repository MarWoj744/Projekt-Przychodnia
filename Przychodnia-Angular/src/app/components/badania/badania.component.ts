import { Component, OnInit } from '@angular/core';
import { BadanieService } from '../../services/badanie.service';
import { Badanie } from '../../models/badanie.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BadanieEditComponent } from '../badanie-edit/badanie-edit.component';

@Component({
  selector: 'app-badania',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, BadanieEditComponent],
  templateUrl: './badania.component.html',
  styleUrls: ['./badania.component.css']
})
export class BadaniaComponent implements OnInit {
  badania: Badanie[] = [];
  selectedBadanie: Badanie | null = null;

  isPacjent = false;
  userId?: number;

  constructor(
    private badanieService: BadanieService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const roleRaw =
      localStorage.getItem('userRole') ??
      localStorage.getItem('rola') ??
      '';

    const role = roleRaw.toLowerCase();
    this.isPacjent = role === 'pacjent' || role === '0';

    const idStr =
      localStorage.getItem('userId') ??
      localStorage.getItem('pacjentId') ??
      '';

    this.userId = idStr ? Number(idStr) : undefined;

    console.log('[BadaniaComponent] roleRaw=', roleRaw, 'isPacjent=', this.isPacjent, 'userId=', this.userId);

    this.loadBadania();
  }

  loadBadania(): void {
    this.badanieService.getAll().subscribe((data) => {
      this.badania = data;
    });
  }

  add(): void {
    if (this.isPacjent) return; // pacjent nie może dodawać
    this.selectedBadanie = { id: 0, nazwa: '', cennik: 0, specjalizacja: '' };
  }

  edit(b: Badanie): void {
    if (this.isPacjent) return; // pacjent nie może edytować
    this.selectedBadanie = { ...b };
  }

  delete(id: number): void {
    if (this.isPacjent) return; // pacjent nie może usuwać
    if (confirm('Czy na pewno chcesz usunąć to badanie?')) {
      this.badanieService.delete(id).subscribe(() => {
        this.loadBadania();
      });
    }
  }

  onSave(): void {
    this.loadBadania();
    this.selectedBadanie = null;
  }

  onCancel(): void {
    this.selectedBadanie = null;
  }

  umowWizyte(b: Badanie): void {
    if (!this.userId) {
      alert('Brak identyfikatora pacjenta w localStorage.');
      return;
    }
    this.router.navigate(['/recepcja/wizyty/dodaj'], {
      queryParams: { badanieId: b.id, pacjentId: this.userId }
    });
  }
}
