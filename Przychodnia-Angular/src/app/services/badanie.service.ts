import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Badanie } from '../models/badanie.model';

@Injectable({
  providedIn: 'root'
})
export class BadanieService {
  private apiUrl = 'http://localhost:5120/api/Badanie'; 

  constructor(private http: HttpClient) {}

  getById(id: number): Observable<Badanie> {
    return this.http.get<Badanie>(`${this.apiUrl}/${id}`);
  }

  update(badanie: Badanie): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${badanie.id}`, badanie);
  }
}
