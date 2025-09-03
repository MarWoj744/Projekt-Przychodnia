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

  getWizytyByLekarzId(lekarzId: number): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}/Lekarz/${lekarzId}`);
  }

  getAnulowaneByLekarzId(lekarzId: number): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}/anulowane/Lekarz/${lekarzId}`);
  }

  getWizyty(): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}`);
  }

  getWizytyAnulowane(): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}/anulowane`);
  }

  anulujWizyte(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/anuluj`, {});
  }

  addWizyta(wizyta: Wizyta): Observable<Wizyta> {
    return this.http.post<Wizyta>(this.apiUrl, wizyta);
  }
}





