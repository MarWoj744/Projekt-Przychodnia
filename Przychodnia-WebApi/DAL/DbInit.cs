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
                    Imie = "Adam",
                    Nazwisko = "Kowalski",
                    Adres = "Pacjentowa 1",
                    Email = "adam@example.com",
                    Telefon = "123456789",
                    Login = "adam",
                    Haslo = "$2a$11$VUsWRqKsyWP8FO6G5Fn6G.28T7vpktRw4jLYqYizpUnYeFYZ3FqwC",
                    Rola = Rola.Pacjent,
                    PESEL = "12345678901",
                    IsActive= true,
                    RefreshToken="CnXKb7n++Z+9w+2Wc6qQnQ9TaUx5aIw94Nx2qb5rVRI="
                },
                new Pacjent {
                    Imie = "Anna",
                    Nazwisko = "Nowak",
                    Adres = "kwiatowa 5",
                    Email = "anna@example.com",
                    Telefon = "123456789",
                    Login = "ania",
                    Haslo = "$2a$11$91RUzTQDdLSaIdJfK8grF.wM0dXpcjOZrsGKgHRtxTbpWCKxr9kIq",
                    Rola = Rola.Pacjent,
                    PESEL = "12345678902",
                    IsActive= true,
                    RefreshToken="xY1M/9wmF7DCN8fbQTI6FBFjgdmDGFg3q+WS91tX1MU="
                },
                new Lekarz {
                    Imie = "Jerzy",
                    Nazwisko = "Zieliński",
                    Adres = "Lekarska",
                    Email = "jerzy@exmaple.com",
                    Telefon = "123456789",
                    Login = "drjerzy",
                    Haslo = "$2a$11$S8NIAjUQNl8W.6C10XDIYeRzuWh.WbXQG9s/uhGtZqtSp7Y5GPg8a",
                    Rola = Rola.Lekarz,
                    Tytul = "dr",
                    Specjalizacja = "Kardiologia",
                    IsActive= true,
                    RefreshToken="tYoznL25WOGBbFTgWOlhrGrNZkDk9gHV7kmP/p5QDsY="
                },
                new Lekarz {
                    Imie = "Ewa",
                    Nazwisko = "Wysoka",
                    Adres = "Lekarska",
                    Email = "ewa@example.com",
                    Telefon = "123456789",
                    Login = "drewa",
                    Haslo = "$2a$11$Ol0pKHYDB1DPoV4pKBlftuAm2Yg9/4cp4Sw4Qsry427g8FcjCnSYO",
                    Rola = Rola.Lekarz,
                    Tytul = "dr",
                    Specjalizacja = "Radiologia",
                    IsActive= true,
                    RefreshToken="S2VodzT333l4q6L3gm5ANbg0bElGHe+RTMVqQX30dzM="
                },
                new Recepcjonistka {
                    Imie = "Jolanta",
                    Nazwisko = "Bryk",
                    Adres = "Recepcycjna",
                    Email = "jola@example.com",
                    Telefon = "123456789",
                    Login = "jolaR",
                    Haslo = "$2a$11$gBQyZK7Icp.LW9yZyCh5oe5aTAl1GWqFPMQMky82pKMyUYbwx88Uy",
                    Rola = Rola.Recepcjonistka,
                    IsActive= true,
                    RefreshToken="nqTJzd0nXHJO2QxOy0pKzLzaGSdMNKURCHGAt4h/bJs="
                }
            };
            context.Osoby.AddRange(osoby);
            context.SaveChanges();

            var pacjent1 = osoby.OfType<Pacjent>().FirstOrDefault(p => p.Login == "adam");
            var pacjent2 = osoby.OfType<Pacjent>().FirstOrDefault(p => p.Login == "ania");
            var lekarz1 = osoby.OfType<Lekarz>().FirstOrDefault(l => l.Login == "drjerzy");
            var lekarz2 = osoby.OfType<Lekarz>().FirstOrDefault(l => l.Login == "drewa");
            var recepcjonistka = osoby.OfType<Recepcjonistka>().FirstOrDefault(r => r.Login == "jolaR");

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
                    BadanieId = badania[0].Id,
                    Zalecenia = "ograniczyć zbędny wysiłek",
                    PacjentId=pacjent1.Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-8),
                    Wyniki = "Wyniki dobre",
                    WizytaId = wizyty[1].Id,
                    BadanieId = badania[1].Id,
                    Zalecenia = "cieszyć się życiem",
                    PacjentId=pacjent2.Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-7),
                    Wyniki = "Nieprawidłowości niewielkie",
                    WizytaId = wizyty[2].Id,
                    BadanieId = badania[1].Id,
                    Zalecenia = "wykonać dodatkowe prześwietlenie",
                    PacjentId=pacjent1.Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-3),
                    Wyniki = "Brak zmian patologicznych",
                    WizytaId = wizyty[3].Id,
                    BadanieId = badania[3].Id,
                    Zalecenia = "kontrolować co miesiąc przez rok na badaniu",
                    PacjentId=pacjent2.Id
                },
                new WykonaneBadania {
                    Data = DateTime.Now.AddDays(-1),
                    Wyniki = "Delikatne odchylenia",
                    WizytaId = wizyty[4].Id,
                    BadanieId = badania[4].Id,
                    Zalecenia = "paracetamol dwa razy dziennie",
                    PacjentId=pacjent1.Id
                }
            };
            context.WykonaneBadania.AddRange(wykonaneBadania);
            context.SaveChanges();
        }
    }
}
