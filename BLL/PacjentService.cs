using IBLL;
using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace BLL
{
    public class PacjentService : IPacjentService
    {

        private readonly IPacjentRepository _pacjentRepo;
        public PacjentService(IPacjentRepository pacjentRepo)
        {
            _pacjentRepo = pacjentRepo;
        }
        public IQueryable<Pacjent> PobierzWszystkie()
        {
            return _pacjentRepo.PobierzWszystkie();
        }
        public Pacjent GetPacjentById(int id)
        {
            return _pacjentRepo.GetPacjentById(id);
        }
        public void Dodaj(Pacjent pacjent)
        {
            pacjent.Haslo = PasswordHasher.HashPassword(pacjent.Haslo);
            _pacjentRepo.Dodaj(pacjent);
        }
        public void Delete(int id)
        {
            _pacjentRepo.Delete(id);
        }
        public void Update(Pacjent pacjent)
        {
            _pacjentRepo.Update(pacjent);
        }
        public void save()
        {
            _pacjentRepo.save();
        }

        public string ValidatePesel(Pacjent pacjent)
        {
            if (pacjent == null || string.IsNullOrWhiteSpace(pacjent.PESEL))
            {
                return "PESEL nie może być pusty.";
            }

            if (!IsValidPesel(pacjent.PESEL))
            {
                return "Nieprawidłowy numer PESEL.";
            }

            return "PESEL jest poprawny.";
        }

        public bool IsValidPesel(string pesel)
        {
            if (!Regex.IsMatch(pesel, @"^\d{11}$")) { return false; }


            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (pesel[i] - '0');
            }

            int checksum = (10 - (sum % 10)) % 10;
            int lastDigit = pesel[10] - '0';

            return checksum == lastDigit;
        }

        public byte[] GenerujHistorieWizytPdf(int pacjentId)
        {
            var pacjent = _pacjentRepo.GetPacjentById(pacjentId);

            if (pacjent == null)
                throw new Exception("Nie znaleziono pacjenta.");

            var wizyty = pacjent.Wizyty.OrderByDescending(w => w.Data).ToList();

            var document = new PdfDocument();
            document.Info.Title = $"Historia wizyt - {pacjent.Imie} {pacjent.Nazwisko}";

            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12);

            double yPoint = 40;

            gfx.DrawString($"Historia wizyt pacjenta: {pacjent.Imie} {pacjent.Nazwisko}", font, XBrushes.Black,
                new XRect(0, yPoint, page.Width, page.Height), XStringFormats.TopCenter);
            yPoint += 40;

            foreach (var wizyta in wizyty)
            {
                string lekarz = $"{wizyta.Lekarz?.Tytul} {wizyta.Lekarz?.Imie} {wizyta.Lekarz?.Nazwisko} ({wizyta.Lekarz?.Specjalizacja})";
                string opis = string.IsNullOrEmpty(wizyta.Opis) ? "Brak opisu" : wizyta.Opis;

                gfx.DrawString($"Data: {wizyta.Data:yyyy-MM-dd HH:mm}", font, XBrushes.Black, 40, yPoint);
                yPoint += 20;
                gfx.DrawString($"Lekarz: {lekarz}", font, XBrushes.Black, 40, yPoint);
                yPoint += 20;
                gfx.DrawString($"Opis: {opis}", font, XBrushes.Black, 40, yPoint);
                yPoint += 40;

                if (yPoint > page.Height - 100)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPoint = 40;
                }
            }

            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                return stream.ToArray();
            }
        }
    }
}
