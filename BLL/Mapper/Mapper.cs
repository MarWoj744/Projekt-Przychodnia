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
            OsobaDTO osobaDTO = new OsobaDTO();
            osobaDTO.Id = osoba.Id;
            osobaDTO.Adres = osoba.Adres;
            osobaDTO.Rola = osoba.Rola;
            osobaDTO.Nazwisko = osoba.Nazwisko;
            osobaDTO.Imie = osoba.Imie;
            osobaDTO.Telefon = osoba.Telefon;
            osobaDTO.Email = osoba.Email;
            osobaDTO.Haslo = osoba.Haslo;
            osobaDTO.Login = osoba.Login;

            return osobaDTO;
        }

        public Osoba osobaToEntity(OsobaDTO osobadto)
        {
            Osoba osoba = new Pacjent();
            osoba.Id = osobadto.Id;
            osoba.Adres = osobadto.Adres;
            osoba.Rola = osobadto.Rola;
            osoba.Nazwisko = osobadto.Nazwisko;
            osoba.Imie = osobadto.Imie;
            osoba.Telefon = osobadto.Telefon;
            osoba.Email = osobadto.Email;
            osoba.Haslo = osobadto.Haslo;
            osoba.Login = osobadto.Login;
            return osoba;
        }

        public PacjentDTO PacjentToDTO(Pacjent pacjent)
        {
            PacjentDTO pacjentDTO = new PacjentDTO();
            pacjentDTO.Id = pacjent.Id;
            pacjentDTO.Adres = pacjent.Adres;
            pacjentDTO.Rola = pacjent.Rola;
            pacjentDTO.Nazwisko = pacjent.Nazwisko;
            pacjentDTO.Imie = pacjent.Imie;
            pacjentDTO.Telefon = pacjent.Telefon;
            pacjentDTO.Email = pacjent.Email;
            pacjentDTO.Haslo = pacjent.Haslo;
            pacjentDTO.Login = pacjent.Login;


            return pacjentDTO;
        }


        public Pacjent pacjentToEntity(PacjentDTO pacjentdto)
        {
            Pacjent pacjent = new Pacjent();
            pacjent.Id = pacjentdto.Id;
            pacjent.Adres = pacjentdto.Adres;
            pacjent.Rola = pacjentdto.Rola;
            pacjent.Nazwisko = pacjentdto.Nazwisko;
            pacjent.Imie = pacjentdto.Imie;
            pacjent.Telefon = pacjentdto.Telefon;
            pacjent.Email = pacjentdto.Email;
            pacjent.Haslo = pacjentdto.Haslo;
            pacjent.Login = pacjentdto.Login;
            pacjent.PESEL = pacjentdto.PESEL;
            foreach (Wizyta wiz in pacjentdto.Wizyty)
            {

            }
            pacjent.Wizyty.Add() = pacjentdto.Wizyty;
            return pacjent;
        }

        public LekarzDTO LekarzToDTO(Lekarz lekarz)
        {
            LekarzDTO lekarzDTO = new LekarzDTO();
            lekarzDTO.Id = lekarz.Id;
            lekarzDTO.Adres = lekarz.Adres;
            lekarzDTO.Rola = lekarz.Rola;
            lekarzDTO.Nazwisko = lekarz.Nazwisko;
            lekarzDTO.Imie = lekarz.Imie;
            lekarzDTO.Telefon = lekarz.Telefon;
            lekarzDTO.Email = lekarz.Email;
            lekarzDTO.Haslo = lekarz.Haslo;
            lekarzDTO.Login = lekarz.Login;
            lekarzDTO.Tytul = lekarz.Tytul;
            lekarzDTO.Specjalizacja = lekarz.Specjalizacja;
            return lekarzDTO;
        }

        public Lekarz LekarzToEntity(LekarzDTO dto)
        {
            Lekarz lekarz = new Lekarz();
            lekarz.Id = dto.Id;
            lekarz.Adres = dto.Adres;
            lekarz.Rola = dto.Rola;
            lekarz.Nazwisko = dto.Nazwisko;
            lekarz.Imie = dto.Imie;
            lekarz.Telefon = dto.Telefon;
            lekarz.Email = dto.Email;
            lekarz.Haslo = dto.Haslo;
            lekarz.Login = dto.Login;
            lekarz.Tytul = dto.Tytul;
            lekarz.Specjalizacja = dto.Specjalizacja;
            return lekarz;
        }

        public RecepcjonistkaDTO RecepcjonistkaToDTO(Recepcjonistka rec)
        {
            RecepcjonistkaDTO dto = new RecepcjonistkaDTO();
            dto.Id = rec.Id;
            dto.Adres = rec.Adres;
            dto.Rola = rec.Rola;
            dto.Nazwisko = rec.Nazwisko;
            dto.Imie = rec.Imie;
            dto.Telefon = rec.Telefon;
            dto.Email = rec.Email;
            dto.Haslo = rec.Haslo;
            dto.Login = rec.Login;
            return dto;
        }

        public Recepcjonistka RecepcjonistkaToEntity(RecepcjonistkaDTO dto)
        {
            Recepcjonistka rec = new Recepcjonistka();
            rec.Id = dto.Id;
            rec.Adres = dto.Adres;
            rec.Rola = dto.Rola;
            rec.Nazwisko = dto.Nazwisko;
            rec.Imie = dto.Imie;
            rec.Telefon = dto.Telefon;
            rec.Email = dto.Email;
            rec.Haslo = dto.Haslo;
            rec.Login = dto.Login;
            return rec;
        }

        public RejestracjaWizytyDTO WizytaToDTO(Wizyta wizyta)
        {
            return new RejestracjaWizytyDTO
            {
                Id = wizyta.Id,
                DataWizyty = wizyta.Data,
                Opis = wizyta.Opis,
                PacjentId = wizyta.PacjentId,
                LekarzId = wizyta.LekarzId,
                RecepcjonistkaId = wizyta.RecepcjonistkaId,
            };
        }

        public Wizyta WizytaToEntity(RejestracjaWizytyDTO wizytaDTO)
        {
            return new Wizyta
            {
                Id = wizytaDTO.Id,
                Data = wizytaDTO.DataWizyty,
                Opis = wizytaDTO.Opis,
                PacjentId = wizytaDTO.PacjentId,
                LekarzId = wizytaDTO.LekarzId,
                RecepcjonistkaId = wizytaDTO.RecepcjonistkaId,
            };
        }

        public WykonaneBadaniaDTO WykonaneBadaniaToDTO(WykonaneBadania badanie)
        {
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
            ICollection<WykonaneBadaniaDTO> wykonaneBadaniadto = new List<WykonaneBadaniaDTO>();
            foreach (WykonaneBadania dto in badanie.Wykonane)
            {
                wykonaneBadaniadto.Add(this.WykonaneBadaniaToDTO(dto));
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
            ICollection<WykonaneBadania> wykonaneBadania = new List<WykonaneBadania>();
            foreach (WykonaneBadaniaDTO badanie in dto.Wykonane)
            {
                wykonaneBadania.Add(this.WykonaneBadaniaToEntity(badanie));
            }
            return new Badanie
            {
                Nazwa = dto.Nazwa,
                Cennik = dto.Cennik,
                Specjalizacja = dto.Specjalizacja,
                Wykonane = wykonaneBadania
            };
        }


    }
}
