export interface RejestracjaWizytyDTO {
  id?: number;
  pacjentId: number;
  lekarzId: number;
  recepcjonistkaId?: number | null;
  dataWizyty: string;
  opis?: string | null;
}