import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Harmonogram } from '../models/harmonogram.model';

@Injectable({
  providedIn: 'root'
})
export class HarmonogramService {
  private apiUrl = 'http://localhost:5120/api/Harmonogram'; 

  constructor(private http: HttpClient) {}

  getByLekarzId(lekarzId: number): Observable<Harmonogram[]> {
    return this.http.get<Harmonogram[]>(`${this.apiUrl}/Lekarz/${lekarzId}`);
  }
}
