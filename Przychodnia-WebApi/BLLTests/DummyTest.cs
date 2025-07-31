using IBLL;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLLTests
{
    public class DummyTest
    {
        [Fact]
        public void DummyObject_ShouldNotBeUsed()
        {
            var dummyPacjent = new Pacjent();
            dummyPacjent.Imie = "Test";
            dummyPacjent.Nazwisko = "Dummy";
            dummyPacjent.PESEL = "00000000000";

            var pacjentService = new Mock<IPacjentService>();
            pacjentService.Setup(x => x.IsValidPesel(It.IsAny<string>())).Returns(true);

            string testPesel = "12345678901";

            bool result = pacjentService.Object.IsValidPesel(testPesel);

            Assert.True(result);
            Assert.NotNull(testPesel);
            Assert.Equal(11, testPesel.Length);
        }
    }
}
