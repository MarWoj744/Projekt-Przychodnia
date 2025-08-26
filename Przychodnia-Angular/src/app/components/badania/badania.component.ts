import { Component, OnInit } from '@angular/core';
import { BadanieService } from '../../services/badanie.service';
import { Badanie } from '../../models/badanie.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BadanieEditComponent } from "../badanie-edit/badanie-edit.component";
@Component({
  selector: 'app-badania',
  standalone:true,
   imports: [CommonModule, FormsModule, RouterModule, BadanieEditComponent],
  templateUrl: './badania.component.html',
  styleUrls: ['./badania.component.css']
})
export class BadaniaComponent implements OnInit {
   badania: Badanie[] = [];
  selectedBadanie: Badanie | null = null;

  constructor(private badanieService: BadanieService) {}

  ngOnInit(): void {
    this.loadBadania();
  }

  loadBadania(): void {
    this.badanieService.getAll().subscribe(data => {
      this.badania = data;
    });
  }
  add(): void {
    this.selectedBadanie = 
    { id: 0, 
      nazwa: '', 
      cennik: 0, 
      specjalizacja: '' };
  }

  edit(b: Badanie): void {
    this.selectedBadanie = { ...b };
  }

  delete(id: number): void {
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
}


