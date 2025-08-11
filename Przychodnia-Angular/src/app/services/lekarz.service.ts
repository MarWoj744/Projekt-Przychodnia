import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Badanie } from '../models/badanie.model';
import { Harmonogram } from '../models/harmonogram.model';
import { Wizyta } from '../models/wizyta.model';

@Injectable({
  providedIn: 'root'
})
export class LekarzService {
  private apiUrl = 'http://localhost:5120/api/Lekarz';

  constructor(private http: HttpClient) {}

  getBadanieById(id: number): Observable<Badanie> {
    return this.http.get<Badanie>(`${this.apiUrl}/Badanie/${id}`);
  }

  getHarmonogramByLekarzId(lekarzId: number): Observable<Harmonogram[]> {
    return this.http.get<Harmonogram[]>(`${this.apiUrl}/${lekarzId}/Harmonogram`);
  }

  getAnulowaneWizytyByLekarzId(lekarzId: number): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}/${lekarzId}/Wizyty/Anulowane`);
  }


getWizytyByLekarzId(lekarzId: number): Observable<Wizyta[]> {
  return this.http.get<Wizyta[]>(`${this.apiUrl}/${lekarzId}/Wizyty`);
}

}
