import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Badanie } from '../../models/badanie.model';
import { BadanieService } from '../../services/badanie.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-badanie-edit',
  imports :[CommonModule, FormsModule],
  templateUrl: './badanie-edit.component.html',
  styleUrls: ['./badanie-edit.component.css']
})
export class BadanieEditComponent implements OnInit {
  badanie: Badanie | null = null;
  error: string | null = null;

  constructor(
    private badanieService: BadanieService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.badanieService.getById(id).subscribe({
        next: (b) => this.badanie = b,
        error: () => this.error = 'Błąd ładowania badania'
      });
    }
  }

  save(): void {
    if (!this.badanie) return;

    this.badanieService.update(this.badanie).subscribe({
      next: () => this.router.navigate(['/badania']),
      error: () => this.error = 'Błąd zapisu badania'
    });
  }
}
