import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PacjentService } from '../../services/pacjent.service';
import { WizytyService } from '../../services/wizyty.service';
import { Wizyta } from '../../models/wizyta.model';
import { Subscription, of, switchMap } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-pacjent-wizyty',
  imports: [CommonModule],
  templateUrl: './pacjent-wizyty.component.html'
})
export class PacjentWizytyComponent implements OnInit, OnDestroy {
  wizyty: Wizyta[] = [];
  wizytyNadchodzace: Wizyta[] = [];
  sub?: Subscription;

  constructor(private pacjentSvc: PacjentService, private wizytySvc: WizytyService) {}

  ngOnInit(): void {
    this.sub = this.pacjentSvc.pacjent$.pipe(
      switchMap(p => p ? this.wizytySvc.getWizytyByPacjentId(p.id) : of([]))
    ).subscribe(list => {
      const sorted = [...list].sort((a,b) => +new Date(a.data) - +new Date(b.data));
      this.wizyty = sorted;
      const now = Date.now();
      this.wizytyNadchodzace = sorted.filter(w => +new Date(w.data) >= now);
    });
  }
  ngOnDestroy(): void { this.sub?.unsubscribe(); }
}
