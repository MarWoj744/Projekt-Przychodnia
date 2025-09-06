import { Wizyta } from './wizyta.model';
import { Badanie } from './badanie.model';

export interface WykonaneBadania {
  id: number;
  data: string;
  wyniki: string;
  zalecenia?: string | null;
  wizytaId: number;
  badanieId: number;
  wizyta?: Wizyta | null;
  badanie?: Badanie | null;
  pacjentId: number;
}
