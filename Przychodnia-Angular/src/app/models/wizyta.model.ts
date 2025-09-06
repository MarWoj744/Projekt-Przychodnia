export interface Wizyta {
  id: number;
  lekarz: string;
  pacjent: string;
  data: string; 
  godzina:  string, 
 status: 'Zaplanowana' | 'Zrealizowana' | 'Anulowana';
  opis?: string;
}
