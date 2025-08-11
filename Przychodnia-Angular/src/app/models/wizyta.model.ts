export interface Wizyta {
  id: number;
  lekarzId: number;
  pacjentNazwa: string;
  data: string;  
  status: string; 
  powodAnulowania?: string;
}
