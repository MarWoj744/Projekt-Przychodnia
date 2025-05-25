using System;
using System.Linq;
using Models;

namespace Models
{
    public static class DbInitializer
    {
        public static void Seed(DbPrzychodnia context)
        {
            if (context.Pacjenci.Any()) return;

            var pacjenci = new[]
            {
                new Pacjent { Imie = "Anna", Nazwisko = "Kowalska", PESEL = "12345678901" },
                new Pacjent { Imie = "Jan", Nazwisko = "Nowak", PESEL = "12345678902" },
                new Pacjent { Imie = "Maria", Nazwisko = "Wiśniewska", PESEL = "12345678903" },
                new Pacjent { Imie = "Tomasz", Nazwisko = "Mazur", PESEL = "12345678904" },
                new Pacjent { Imie = "Ewa", Nazwisko = "Kaczmarek", PESEL = "12345678905" }
            };

            context.Pacjenci.AddRange(pacjenci);

            var lekarze = new[]
            {
                new Lekarz { Imie = "Adam", Nazwisko = "Zieliński", Specjalizacja = "Kardiolog" },
                new Lekarz { Imie = "Monika", Nazwisko = "Bąk", Specjalizacja = "Dermatolog" },
                new Lekarz { Imie = "Krzysztof", Nazwisko = "Duda", Specjalizacja = "Pediatra" },
                new Lekarz { Imie = "Agnieszka", Nazwisko = "Wójcik", Specjalizacja = "Neurolog" },
                new Lekarz { Imie = "Michał", Nazwisko = "Kowal", Specjalizacja = "Ortopeda" }
            };

            context.Lekarze.AddRange(lekarze);

            var recepcjonistki = new[]
            {
                new Recepcjonistka { Imie = "Kasia", Nazwisko = "Nowicka" },
                new Recepcjonistka { Imie = "Julia", Nazwisko = "Lis" },
                new Recepcjonistka { Imie = "Paulina", Nazwisko = "Wrona" },
                new Recepcjonistka { Imie = "Natalia", Nazwisko = "Król" },
                new Recepcjonistka { Imie = "Alicja", Nazwisko = "Jankowska" }
            };

            context.Recepcjonistki.AddRange(recepcjonistki);

            var badania = new[]
            {
                new Badanie { Nazwa = "Morfologia", Cennik = 50, Specjalizacja = "Diagnostyka" },
                new Badanie { Nazwa = "RTG", Cennik = 120, Specjalizacja = "Radiologia" },
                new Badanie { Nazwa = "EKG", Cennik = 80, Specjalizacja = "Kardiologia" },
                new Badanie { Nazwa = "USG", Cennik = 150, Specjalizacja = "Diagnostyka obrazowa" },
                new Badanie { Nazwa = "Tomografia", Cennik = 500, Specjalizacja = "Radiologia" }
            };

            context.Badania.AddRange(badania);

            context.SaveChanges();

            var wizyta1 = new Wizyta
            {
                Data = DateTime.Now,
                Opis = "Kontrola",
                PacjentId = pacjenci[0].Id,
                LekarzId = lekarze[0].Id,
                RecepcjonistkaId = recepcjonistki[0].Id
            };

            var wizyta2 = new Wizyta
            {
                Data = DateTime.Now.AddDays(1),
                Opis = "Badania okresowe",
                PacjentId = pacjenci[1].Id,
                LekarzId = lekarze[1].Id,
                RecepcjonistkaId = recepcjonistki[1].Id
            };

            context.Wizyty.AddRange(wizyta1, wizyta2);
            context.SaveChanges();

            context.WykonaneBadania.AddRange(
                new WykonaneBadania { Data = DateTime.Now, WizytaId = wizyta1.Id, BadanieId = badania[0].Id, Wyniki = "W normie" },
                new WykonaneBadania { Data = DateTime.Now, WizytaId = wizyta2.Id, BadanieId = badania[1].Id, Wyniki = "Nieprawidłowości" }
            );

            context.SaveChanges();
        }
    }
}
