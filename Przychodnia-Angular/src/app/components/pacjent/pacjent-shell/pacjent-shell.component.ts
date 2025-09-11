// pacjent-shell.component.ts
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';

import { Pacjent } from '../../../models/pacjent.model';          
import { PacjentService } from '../../../services/pacjent.service';

@Component({
  selector: 'app-pacjent-shell',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './pacjent-shell.component.html',
  styleUrls: ['./pacjent-shell.component.css'],
})
export class PacjentShellComponent {
  pacjent$: Observable<Pacjent | null>;

  constructor(private pacjentSvc: PacjentService) {
    this.pacjent$ = this.pacjentSvc.pacjent$;
  }
}
