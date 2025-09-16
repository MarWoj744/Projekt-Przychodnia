import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Wizyta } from '../models/wizyta.model';
import { RejestracjaWizytyDTO } from '../models/rejestracja-wizyty-dto.model';

@Injectable({
  providedIn: 'root'
})

export class WizytyService {
  private apiUrl = 'https://przychodniaap-ghhbcfgzgtbjgdgt.polandcentral-01.azurewebsites.net/api/Wizyta'; 

  constructor(private http: HttpClient) {}

  getWizytyByLekarzId(lekarzId: number): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}/Lekarz/${lekarzId}`);
  }

  getWizytyByPacjentId(pacjentId: number): Observable<Wizyta[]> {
    return this.http.get<Wizyta[]>(`${this.apiUrl}/pacjent/${pacjentId}`);
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

  addWizyta(wizyta: RejestracjaWizytyDTO): Observable<RejestracjaWizytyDTO> {
    return this.http.post<RejestracjaWizytyDTO>(`${this.apiUrl}`, wizyta);
  }
}





