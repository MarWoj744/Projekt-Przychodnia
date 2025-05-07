using IBLL;
using Models;
using Moq;
using System;
using Xunit;

namespace BLLTests
{
    public class StubTest
    {
        [Fact]
        public void Stub_ReturnsFixedValue()
        {
            var mock = new Mock<IOsobaService>();
            mock.Setup(s => s.ValidateData(It.IsAny<Osoba>())).Returns("Dane poprawne");

            var osoba = new Pacjent
            {
                Imie = "Anna",
                Nazwisko = "Nowak",
                Adres = "ul. Kwiatowa 1",
                Email = "anna@nowak.pl",
                Telefon = "123456789",
                Login = "anowak",
                Haslo = "tajne123",
                Rola = Rola.Pacjent,
                PhoneNumber = "123456789",
                PESEL = "99010212345"
            };

            var result = mock.Object.ValidateData(osoba);

            Assert.Equal("Dane poprawne", result);
        }
    }
}