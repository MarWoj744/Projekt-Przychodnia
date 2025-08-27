using DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Mapper
{
    public class Mapper
    {
        public OsobaDTO OsobaToDTO(Osoba osoba)
        {
            if (osoba == null) return null;

            return new OsobaDTO
            {
                Id = osoba.Id,
                Adres = osoba.Adres,
                Rola = osoba.Rola,
                Nazwisko = osoba.Nazwisko,
                Imie = osoba.Imie,
                Telefon = osoba.Telefon,
                Email = osoba.Email,
                Haslo = osoba.Haslo,
                Login = osoba.Login
            };
        }

        public Osoba osobaToEntity(OsobaDTO osobadto)
        {
            if (osobadto == null) return null;

            Osoba osoba = new Pacjent
            {
                Id = osobadto.Id,
                Adres = osobadto.Adres,
                Rola = osobadto.Rola,
                Nazwisko = osobadto.Nazwisko,
                Imie = osobadto.Imie,
                Telefon = osobadto.Telefon,
                Email = osobadto.Email,
                Haslo = osobadto.Haslo,
                Login = osobadto.Login
            };
            return osoba;
        }

        public PacjentDTO PacjentToDTO(Pacjent pacjent)
        {
            if (pacjent == null) return null;

            var pacjentDTO = new PacjentDTO
            {
                Id = pacjent.Id,
                Adres = pacjent.Adres,
                Rola = pacjent.Rola,
                Nazwisko = pacjent.Nazwisko,
                Imie = pacjent.Imie,
                Telefon = pacjent.Telefon,
                Email = pacjent.Email,
                Haslo = pacjent.Haslo,
                Login = pacjent.Login
            };

            var wykonaneBadania = new List<RejestracjaWizytyDTO>();
            if (pacjent.Wizyty != null)
            {
                foreach (var badanie in pacjent.Wizyty)
                {
                    var dto = WizytaToDTO(badanie);
                    if (dto != null) wykonaneBadania.Add(dto);
                }
            }
            pacjentDTO.Wizyty = wykonaneBadania;
            return pacjentDTO;
        }

        public Pacjent pacjentToEntity(PacjentDTO pacjentdto)
        {
            if (pacjentdto == null) return null;

            var pacjent = new Pacjent
            {
                Id = pacjentdto.Id,
                Adres = pacjentdto.Adres,
                Rola = pacjentdto.Rola,
                Nazwisko = pacjentdto.Nazwisko,
                Imie = pacjentdto.Imie,
                Telefon = pacjentdto.Telefon,
                Email = pacjentdto.Email,
                Haslo = pacjentdto.Haslo,
                Login = pacjentdto.Login,
                PESEL = pacjentdto.PESEL
            };

            var wykonaneBadania = new List<Wizyta>();
            if (pacjentdto.Wizyty != null)
            {
                foreach (var badanie in pacjentdto.Wizyty)
                {
                    var entity = WizytaToEntity(badanie);
                    if (entity != null) wykonaneBadania.Add(entity);
                }
            }
            pacjent.Wizyty = wykonaneBadania;
            return pacjent;
        }

        public LekarzDTO LekarzToDTO(Lekarz lekarz)
        {
            if (lekarz == null) return null;

            var dto = new LekarzDTO
            {
                Id = lekarz.Id,
                Adres = lekarz.Adres,
                Rola = lekarz.Rola,
                Nazwisko = lekarz.Nazwisko,
                Imie = lekarz.Imie,
                Telefon = lekarz.Telefon,
                Email = lekarz.Email,
                Haslo = lekarz.Haslo,
                Login = lekarz.Login,
                Tytul = lekarz.Tytul,
                Specjalizacja = lekarz.Specjalizacja
            };

            var wykonaneBadania = new List<RejestracjaWizytyDTO>();
            if (lekarz.Wizyty != null)
            {
                foreach (var badanie in lekarz.Wizyty)
                {
                    var b = WizytaToDTO(badanie);
                    if (b != null) wykonaneBadania.Add(b);
                }
            }
            dto.Wizyty = wykonaneBadania;
            return dto;
        }

        public Lekarz LekarzToEntity(LekarzDTO dto)
        {
            if (dto == null) return null;

            var lekarz = new Lekarz
            {
                Id = dto.Id,
                Adres = dto.Adres,
                Rola = dto.Rola,
                Nazwisko = dto.Nazwisko,
                Imie = dto.Imie,
                Telefon = dto.Telefon,
                Email = dto.Email,
                Haslo = dto.Haslo,
                Login = dto.Login,
                Tytul = dto.Tytul,
                Specjalizacja = dto.Specjalizacja
            };

            var wykonaneBadania = new List<Wizyta>();
            if (dto.Wizyty != null)
            {
                foreach (var badanie in dto.Wizyty)
                {
                    var w = WizytaToEntity(badanie);
                    if (w != null) wykonaneBadania.Add(w);
                }
            }
            lekarz.Wizyty = wykonaneBadania;
            return lekarz;
        }

        public RecepcjonistkaDTO RecepcjonistkaToDTO(Recepcjonistka rec)
        {
            if (rec == null) return null;

            var dto = new RecepcjonistkaDTO
            {
                Id = rec.Id,
                Adres = rec.Adres,
                Rola = rec.Rola,
                Nazwisko = rec.Nazwisko,
                Imie = rec.Imie,
                Telefon = rec.Telefon,
                Email = rec.Email,
                Haslo = rec.Haslo,
                Login = rec.Login
            };

            var wizyty = new List<RejestracjaWizytyDTO>();
            if (rec.WizytyZarejestrowane != null)
            {
                foreach (var badanie in rec.WizytyZarejestrowane)
                {
                    var b = WizytaToDTO(badanie);
                    if (b != null) wizyty.Add(b);
                }
            }
            dto.WizytyZarejestrowane = wizyty;
            return dto;
        }

        public Recepcjonistka RecepcjonistkaToEntity(RecepcjonistkaDTO dto)
        {
            if (dto == null) return null;

            var rec = new Recepcjonistka
            {
                Id = dto.Id,
                Adres = dto.Adres,
                Rola = dto.Rola,
                Nazwisko = dto.Nazwisko,
                Imie = dto.Imie,
                Telefon = dto.Telefon,
                Email = dto.Email,
                Haslo = dto.Haslo,
                Login = dto.Login
            };

            var wizyty = new List<Wizyta>();
            if (dto.WizytyZarejestrowane != null)
            {
                foreach (var badanie in dto.WizytyZarejestrowane)
                {
                    var b = WizytaToEntity(badanie);
                    if (b != null) wizyty.Add(b);
                }
            }
            rec.WizytyZarejestrowane = wizyty;
            return rec;
        }

        public RejestracjaWizytyDTO WizytaToDTO(Wizyta wizyta)
        {
            if (wizyta == null) return null;

            return new RejestracjaWizytyDTO
            {
                Id = wizyta.Id,
                DataWizyty = wizyta.Data,
                Opis = wizyta.Opis,
                PacjentId = wizyta.PacjentId,
                LekarzId = wizyta.LekarzId,
                RecepcjonistkaId = wizyta.RecepcjonistkaId
            };
        }

        public Wizyta WizytaToEntity(RejestracjaWizytyDTO dto)
        {
            if (dto == null) return null;

            return new Wizyta
            {
                Id = dto.Id,
                Data = dto.DataWizyty,
                Opis = dto.Opis,
                PacjentId = dto.PacjentId,
                LekarzId = dto.LekarzId,
                RecepcjonistkaId = dto.RecepcjonistkaId
            };
        }

        public WykonaneBadaniaDTO WykonaneBadaniaToDTO(WykonaneBadania badanie)
        {
            if (badanie == null) return null;

            return new WykonaneBadaniaDTO
            {
                WizytaId = badanie.WizytaId,
                BadanieId = badanie.BadanieId,
                Data = badanie.Data,
                Wyniki = badanie.Wyniki
            };
        }

        public WykonaneBadania WykonaneBadaniaToEntity(WykonaneBadaniaDTO dto)
        {
            if (dto == null) return null;

            return new WykonaneBadania
            {
                WizytaId = dto.WizytaId,
                BadanieId = dto.BadanieId,
                Data = dto.Data,
                Wyniki = dto.Wyniki
            };
        }

        public BadanieDTO BadanieToDTO(Badanie badanie)
        {
            if (badanie == null) return null;

            var wykonaneBadaniadto = new List<WykonaneBadaniaDTO>();
            if (badanie.Wykonane != null)
            {
                foreach (var dto in badanie.Wykonane)
                {
                    var b = WykonaneBadaniaToDTO(dto);
                    if (b != null) wykonaneBadaniadto.Add(b);
                }
            }

            return new BadanieDTO
            {
               
                Nazwa = badanie.Nazwa,
                Cennik = badanie.Cennik,
                Specjalizacja = badanie.Specjalizacja,
                Wykonane = wykonaneBadaniadto
            };
        }

        public Badanie BadanieToEntity(BadanieDTO dto)
        {
            if (dto == null) return null;

            var wykonaneBadania = new List<WykonaneBadania>();
            if (dto.Wykonane != null)
            {
                foreach (var b in dto.Wykonane)
                {
                    var entity = WykonaneBadaniaToEntity(b);
                    if (entity != null) wykonaneBadania.Add(entity);
                }
            }

            return new Badanie
            {
                Id = dto.Id,
                Nazwa = dto.Nazwa,
                Cennik = dto.Cennik,
                Specjalizacja = dto.Specjalizacja,
                Wykonane = wykonaneBadania
            };
        }

        public HarmonogramDTO HarmonogramToDTO(Harmonogram harmonogram)
        {
            if (harmonogram == null) return null;

            return new HarmonogramDTO
            {
                Id = harmonogram.Id,
                LekarzId = harmonogram.LekarzId,
                DataOd = harmonogram.DataOd,
                DataDo = harmonogram.DataDo,
                Opis = harmonogram.Opis
            };
        }

        public Harmonogram HarmonogramToEntity(HarmonogramDTO dto)
        {
            if (dto == null) return null;

            return new Harmonogram
            {
                Id = dto.Id,
                LekarzId = dto.LekarzId,
                DataOd = dto.DataOd,
                DataDo = dto.DataDo,
                Opis = dto.Opis
            };
        }
        public WizytaWidokDTO WizytaToWidokDTO(Wizyta wizyta)
        {
            if (wizyta == null) return null;

            return new WizytaWidokDTO
            {
                Id = wizyta.Id,
                Pacjent = wizyta.Pacjent != null ? $"{wizyta.Pacjent.Imie} {wizyta.Pacjent.Nazwisko}" : "-",
                Lekarz = wizyta.Lekarz != null ? $"{wizyta.Lekarz.Imie} {wizyta.Lekarz.Nazwisko}" : "-",
                Badanie = wizyta.Badania != null && wizyta.Badania.Any()
                    ? string.Join(", ", wizyta.Badania.Select(b => b.Badanie?.Nazwa))
                    : "-",
                Data = wizyta.Data,
                Status = wizyta.Status.ToString()
            };
        }


    }
}
