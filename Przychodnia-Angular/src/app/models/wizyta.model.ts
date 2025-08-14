export interface Wizyta {
  id: number;
  lekarzId: number;
  pacjent: string;
  data: string; 
  godzina:  string, 
 status: 'Zaplanowana' | 'Zrealizowana' | 'Anulowana';
  badanie?: string;
}
