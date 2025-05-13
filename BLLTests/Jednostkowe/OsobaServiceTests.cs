using BLL;
using IDAL_;
using Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BLLTests.Jednostkowe
{
    public class OsobaServiceTests
    {
        private readonly Mock<IOsobaRepository> _mockRepo;
        private readonly OsobaService _service;

        public OsobaServiceTests()
        {
            _mockRepo = new Mock<IOsobaRepository>();
            _service = new OsobaService(_mockRepo.Object);
        }

        private Osoba GetValidOsoba()
        {
            return new Pacjent
            {
                Login = "testuser",
                Haslo = "haslo123",
                Email = "test@example.com",
                Telefon = "123456789"
            };
        }

        [Fact]
        public void ValidateData_NullOsoba_ZwracaKomunikat()
        {
            var result = _service.ValidateData(null);
            Assert.Equal("Osoba jest nullem.", result);
        }

        [Fact]
        public void ValidateData_BrakLoginuLubHasla_ZwracaKomunikat()
        {
            var osoba = new Pacjent { Login = "", Haslo = null };
            var result = _service.ValidateData(osoba);
            Assert.Equal("Login lub hasło jest nullem.", result);
        }

        [Fact]
        public void ValidateData_LoginZajety_ZwracaKomunikat()
        {
            var osoba = GetValidOsoba();
            _mockRepo.Setup(r => r.GetOsobaByLogin(osoba.Login)).Returns(new Pacjent());

            var result = _service.ValidateData(osoba);
            Assert.Equal("Login zajęty.", result);
        }

        [Fact]
        public void ValidateData_EmailZajety_ZwracaKomunikat()
        {
            var osoba = GetValidOsoba();
            _mockRepo.Setup(r => r.GetOsobaByEmail(osoba.Email)).Returns(new Pacjent());

            var result = _service.ValidateData(osoba);
            Assert.Equal("Email jest zajęty.", result);
        }

        [Theory]
        [InlineData("12345", false)]
        [InlineData("123456789", true)]
        [InlineData("abcdefghi", false)]
        public void IsValidPhoneNumber_SprawdzaPoprawnoscNumeru(string number, bool expected)
        {
            var result = _service.IsValidPhoneNumber(number);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("user.name@domain.co", true)]
        [InlineData("invalid_email", false)]
        [InlineData("", false)]
        public void IsValidEmail_SprawdzaPoprawnoscAdresu(string email, bool expected)
        {
            var result = _service.IsValidEmail(email);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValidateData_NieprawidlowyTelefon_ZwracaKomunikat()
        {
            var osoba = GetValidOsoba();
            osoba.Telefon = "123";
            var result = _service.ValidateData(osoba);
            Assert.Equal("Numer telefonu nieprawidłowy.", result);
        }

        [Fact]
        public void ValidateData_NieprawidlowyEmail_ZwracaKomunikat()
        {
            var osoba = GetValidOsoba();
            osoba.Email = "invalid_email";
            var result = _service.ValidateData(osoba);
            Assert.Equal("Email jest nieprawidłowy.", result);
        }

        [Fact]
        public void ValidateData_TelefonZajety_ZwracaKomunikat()
        {
            var osoba = GetValidOsoba();
            _mockRepo.Setup(r => r.GetOsobaByPhoneNumber(osoba.Telefon)).Returns(new Pacjent());

            var result = _service.ValidateData(osoba);
            Assert.Equal("Numer telefonu zajęty.", result);
        }

        [Fact]
        public void ValidateData_PoprawneDane_ZwracaSukces()
        {
            var osoba = GetValidOsoba();

            _mockRepo.Setup(r => r.GetOsobaByLogin(osoba.Login)).Returns((Pacjent)null);
            _mockRepo.Setup(r => r.GetOsobaByEmail(osoba.Email)).Returns((Pacjent)null);
            _mockRepo.Setup(r => r.GetOsobaByPhoneNumber(osoba.Telefon)).Returns((Pacjent)null);

            var result = _service.ValidateData(osoba);
            Assert.Equal("Walidacja zakończona sukcesem.", result);
        }

        [Fact]
        public void PobierzWszystkie_ZwracaListeOsob()
        {
            var osoby = new List<Osoba> { GetValidOsoba() }.AsQueryable();
            _mockRepo.Setup(r => r.PobierzWszystkie()).Returns(osoby);

            var result = _service.PobierzWszystkie().ToList();

            Assert.Single(result);
        }

        [Fact]
        public void GetOsobaById_ZwracaPoprawnaOsobe()
        {
            var osoba = GetValidOsoba();
            _mockRepo.Setup(r => r.GetOsobaById(1)).Returns(osoba);

            var result = _service.GetOsobaById(1);

            Assert.Equal(osoba.Login, result.Login);
        }

        [Fact]
        public void Dodaj_WywolujeDodajWRepozytorium()
        {
            var osoba = GetValidOsoba();

            _service.Dodaj(osoba);

            _mockRepo.Verify(r => r.Dodaj(osoba), Times.Once);
        }

        [Fact]
        public void Delete_WywolujeDeleteWRepozytorium()
        {
            _service.Delete(1);

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public void Update_WywolujeUpdateWRepozytorium()
        {
            var osoba = GetValidOsoba();

            _service.Update(osoba);

            _mockRepo.Verify(r => r.Update(osoba), Times.Once);
        }

        [Fact]
        public void Save_WywolujeSaveWRepozytorium()
        {
            _service.Save();

            _mockRepo.Verify(r => r.save(), Times.Once);
        }
    }
}
