using System;
using System.Linq;
using Models;

namespace DAL
{
    public static class DbInit
    {
        public static void Seed(DbPrzychodnia context)
        {
            if (context.Osoby.Any())
                return;

            var badania = new Badanie[]
            {
                new Badanie { Nazwa = "Morfologia krwi", Cennik = 50, Specjalizacja = "Hematologia" },
                new Badanie { Nazwa = "EKG", Cennik = 120, Specjalizacja = "Kardiologia" },
                new Badanie { Nazwa = "RTG klatki piersiowej", Cennik = 150, Specjalizacja = "Radiologia" },
                new Badanie { Nazwa = "USG jamy brzusznej", Cennik = 200, Specjalizacja = "Radiologia" },
                new Badanie { Nazwa = "Badanie moczu", Cennik = 40, Specjalizacja = "Urologia" }
            };
            context.Badania.AddRange(badania);
            context.SaveChanges();

            var osoby = new Osoba[]
            {
                new Pacjent {
                    Imie = "Jan",
                    Nazwisko = "Kowalski",
                    Adres = "ul. Kwiatowa 10",
                    Email = "jan.kowalski@example.com",
                    Telefon = "123456789",
                    Login = "janek",
                    Haslo = "haslo123",
                    Rola = Rola.Pacjent,
                    PESEL = "90010112345"
                },
                new Pacjent {
                    Imie = "Anna",
                    Nazwisko = "Nowak",
                    Adres = "ul. Lipowa 5",
                    Email = "anna.nowak@example.com",
                    Telefon = "987654321",
                    Login = "annaN",
                    Haslo = "haslo456",
                    Rola = Rola.Pacjent,
                    PESEL = "89020254321"
                },
                new Lekarz {
                    Imie = "Piotr",
                    Nazwisko = "Zieliński",
                    Adres = "ul. Szpitalna 3",
                    Email = "piotr.zielinski@example.com",
                    Telefon = "555666777",
                    Login = "drpiotr",
                    Haslo = "lek123",
                    Rola = Rola.Lekarz,
                    Tytul = "dr",
                    Specjalizacja = "Kardiologia"
                },
                new Lekarz {
                    Imie = "Marta",
                    Nazwisko = "Wiśniewska",
                    Adres = "ul. Zdrowa 7",
                    Email = "marta.wisniewska@example.com",
                    Telefon = "444555666",
                    Login = "drmarta",
                    Haslo = "lek456",
                    Rola = Rola.Lekarz,
                    Tytul = "dr",
                    Specjalizacja = "Radiologia"
                },
                new Recepcjonistka {
                    Imie = "Ewa",
                    Nazwisko = "Kaczmarek",
                    Adres = "ul. Recepcji 1",
                    Email = "ewa.kaczmarek@example.com",
                    Telefon = "333444555",
                    Login = "ewaR",
                    Haslo = "recep123",
                    Rola = Rola.Recepcjonistka
                }
            };
            context.Osoby.AddRange(osoby);
            context.SaveChanges();

            var pacjent1 = osoby.OfType<Pacjent>().FirstOrDefault(p => p.Login == "janek");
            var pacjent2 = osoby.OfType<Pacjent>().FirstOrDefault(p => p.Login == "annaN");
            var lekarz1 = osoby.OfType<Lekarz>().FirstOrDefault(l => l.Login == "drpiotr");
            var lekarz2 = osoby.OfType<Lekarz>().FirstOrDefault(l => l.Login == "drmarta");
            var recepcjonistka = osoby.OfType<Recepcjonistka>().FirstOrDefault(r => r.Login == "ewaR");

            var wizyty = new Wizyta[]
            {
                new Wizyta {
                    Data = DateTime.Now.AddDays(-10),
                    Opis = "Kontrola pooperacyjna",
                    PacjentId = pacjent1.Id,
                    LekarzId = lekarz1.Id,
                    RecepcjonistkaId = recepcjonistka.Id
                },
                new Wizyta {
                    Data = DateTime.Now.AddDays(-8),
                    Opis = "Badanie kontrolne",
                    PacjentId = pacjent2.Id,
                    LekarzId = lekarz2.Id,
                    RecepcjonistkaId = recepcjonistka.Id
                },
                new Wizyta {
                    Data = DateTime.Now.AddDays(-7),
                    Opis = "Konsultacja kardiologiczna",
                    PacjentId = pacjent1.Id,
                    LekarzId = lekarz1.Id,
                    RecepcjonistkaId = recepcjonistka.Id
                },
                new Wizyta {
                    Data = DateTime.Now.AddDays(-3),
                    Opis = "USG jamy brzusznej",
                    PacjentId = pacjent2.Id,
                    LekarzId = lekarz2.Id,
                    RecepcjonistkaId = recepcjonistka.Id
                },
                new Wizyta {
                    Data = DateTime.Now.AddDays(-1),
                    Opis = "Konsultacja urologiczna",
                    PacjentId = pacjent1.Id,
                    LekarzId = lekarz2.Id,
                    RecepcjonistkaId = recepcjonistka.Id
                }
            };
            context.Wizyty.AddRange(wizyty);
            context.SaveChanges();

            var wykonaneBadania = new WykonaneBadania[]
            {
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-10),
                    Wyniki = "Wyniki w normie",
                    WizytaId = wizyty[0].Id,
                    BadanieId = badania[0].Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-8),
                    Wyniki = "Wyniki dobre",
                    WizytaId = wizyty[1].Id,
                    BadanieId = badania[1].Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-7),
                    Wyniki = "Nieprawidłowości niewielkie",
                    WizytaId = wizyty[2].Id,
                    BadanieId = badania[1].Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-3),
                    Wyniki = "Brak zmian patologicznych",
                    WizytaId = wizyty[3].Id,
                    BadanieId = badania[3].Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-1),
                    Wyniki = "Delikatne odchylenia",
                    WizytaId = wizyty[4].Id,
                    BadanieId = badania[4].Id
                }
            };
            context.WykonaneBadania.AddRange(wykonaneBadania);
            context.SaveChanges();
        }
    }
}
