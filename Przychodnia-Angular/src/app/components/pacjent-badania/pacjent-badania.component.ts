import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PacjentService } from '../../services/pacjent.service';
import { WykonaneBadaniaService } from '../../services/wykonane-badania.service';
import { WykonaneBadania } from '../../models/wykonane-badania.model';
import { Subscription, of, switchMap } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-pacjent-badania',
  imports: [CommonModule],
  templateUrl: './pacjent-badania.component.html'
})
export class PacjentBadaniaComponent implements OnInit, OnDestroy {
  badania: WykonaneBadania[] = [];
  sub?: Subscription;

  constructor(private pacjentSvc: PacjentService, private badaniaSvc: WykonaneBadaniaService) {}

  ngOnInit(): void {
    this.sub = this.pacjentSvc.pacjent$.pipe(
      switchMap(p => p ? this.badaniaSvc.getByPacjent(p.id) : of([]))
    ).subscribe(list => {
      this.badania = [...list].sort((a,b) => +new Date(b.data) - +new Date(a.data));
    });
  }

  pobierzPdf(b: WykonaneBadania) {
    this.badaniaSvc.savePdf(b.id);
  }

  ngOnDestroy(): void { this.sub?.unsubscribe(); }
}
