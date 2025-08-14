export interface Wizyta {
  id: number;
  lekarzId: number;
  pacjent: string;
  data: string; 
  godzina:  string, 
 status: 'Zaplata' | 'Zrealizowana' | 'Anulowana';
  badanie?: string;
}
