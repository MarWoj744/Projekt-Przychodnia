import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WykonaneBadania } from '../models/wykonane-badania.model';

@Injectable({ providedIn: 'root' })
export class WykonaneBadaniaService {

  private readonly apiBase = 'https://przychodniaap-ghhbcfgzgtbjgdgt.polandcentral-01.azurewebsites.net/api';
  private readonly url = `${this.apiBase}/WykonaneBadania`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<WykonaneBadania[]> {
    return this.http.get<WykonaneBadania[]>(this.url);
  }

  getById(id: number): Observable<WykonaneBadania> {
    return this.http.get<WykonaneBadania>(`${this.url}/${id}`);
  }

  create(payload: WykonaneBadania): Observable<WykonaneBadania> {
    return this.http.post<WykonaneBadania>(this.url, payload);
  }

  update(id: number, payload: WykonaneBadania): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }


  getByPacjent(pacjentId: number): Observable<WykonaneBadania[]> {
    return this.http.get<WykonaneBadania[]>(`${this.url}/pacjent/${pacjentId}`);
  }


  downloadPdf(id: number): Observable<Blob> {
    return this.http.get(`${this.url}/pobierz-pdf/${id}`, {
      responseType: 'blob',
    });
  }

  savePdf(id: number, filename?: string): void {
    this.downloadPdf(id).subscribe((blob) => {
      const name = filename ?? `Recepta_${id}.pdf`;
      const url = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = name;
      a.click();
      URL.revokeObjectURL(url);
    });
  }
}
