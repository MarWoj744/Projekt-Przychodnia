/*import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Harmonogram } from '../models/harmonogram.model';

@Injectable({
  providedIn: 'root'
})
export class HarmonogramService {
  [x: string]: any;
  private apiUrl = 'http://localhost:5120/api/Harmonogram'; 

  constructor(private http: HttpClient) {}
 getByLekarzId(lekarzId: number): Observable<Harmonogram[]> {
    return this.http.get<Harmonogram[]>(`${this.apiUrl}/Lekarz/${lekarzId}`);
  }
 
}*/
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Harmonogram } from '../models/harmonogram.model';

@Injectable({
  providedIn: 'root'
})
export class HarmonogramService {
  private apiUrl = 'https://przychodniaap-ghhbcfgzgtbjgdgt.polandcentral-01.azurewebsites.net/api/Harmonogram'; 

  constructor(private http: HttpClient) {}

  getAll(): Observable<Harmonogram[]> {
    return this.http.get<Harmonogram[]>(this.apiUrl);
  }

  getById(id: number): Observable<Harmonogram> {
    return this.http.get<Harmonogram>(`${this.apiUrl}/${id}`);
  }

  create(harmonogram: Harmonogram): Observable<Harmonogram> {
    return this.http.post<Harmonogram>(this.apiUrl, harmonogram);
  }

  update(id: number, harmonogram: Harmonogram): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, harmonogram);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
   getByLekarzId(lekarzId: number): Observable<Harmonogram[]> {
    return this.http.get<Harmonogram[]>(`${this.apiUrl}/Lekarz/${lekarzId}`);
  }
}