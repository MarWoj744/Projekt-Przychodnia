import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { Badanie } from '../models/badanie.model';
import { Wizyta } from '../models/wizyta.model';
import { Pacjent } from '../models/pacjent.model';

@Injectable({
  providedIn: 'root'
})
export class PacjentService {
  private apiUrl = 'https://przychodniaap-ghhbcfgzgtbjgdgt.polandcentral-01.azurewebsites.net/api';
  private pacjentSubject = new BehaviorSubject<Pacjent | null>(null);
  pacjent$: Observable<Pacjent | null> = this.pacjentSubject.asObservable();

  constructor(private http: HttpClient) {}

  getPacjent(): Pacjent | null {
    return this.pacjentSubject.value;
  }
  getBadanieById(id: number): Observable<Badanie> {
    return this.http.get<Badanie>(`${this.apiUrl}/Badanie/${id}`);
  }

getWizytyByPacjentId(pacjentId: number): Observable<Wizyta[]> {
  return this.http.get<Wizyta[]>(`${this.apiUrl}/Wizyta/pacjent/${pacjentId}`);
}

}