import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Wizyta } from '../models/wizyta.model';

@Injectable({
  providedIn: 'root'
})
export class WizytaService {
  private apiUrl = 'http://localhost:5120/api/Wizyta'; 

  constructor(private http: HttpClient) {}

  getAnulowaneByLekarzId(lekarzId: number): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}/Anulowane/Lekarz/${lekarzId}`);
  }
}
