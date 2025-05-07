using DTOs;
using IBLL;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLLTests
{
    public class MockTest
    {
        [Fact]
        public async Task Mock_VerifyMethodWasCalled()
        {
            var mock = new Mock<IWizytaService>();
            var dto = new RejestracjaWizytyDTO
            {
                PacjentId = 1,
                DataWizyty = DateTime.Now.AddDays(1),
                LekarzId = 101,
                Opis = "Konsultacja"
            };

            // Sprawdzamy, czy mock poprawnie ustawi metodę ZarejestrujWizyteAsync
            mock.Setup(m => m.ZarejestrujWizyteAsync(It.IsAny<RejestracjaWizytyDTO>()))
                .ReturnsAsync(true);

            var result = await mock.Object.ZarejestrujWizyteAsync(dto);

            Assert.True(result);
            Assert.Equal(1, dto.PacjentId);
            Assert.Equal("Konsultacja", dto.Opis);
            mock.Verify(m => m.ZarejestrujWizyteAsync(dto), Times.Once);

            var anotherResult = await mock.Object.ZarejestrujWizyteAsync(new RejestracjaWizytyDTO());
            Assert.True(anotherResult);
            mock.Verify(m => m.ZarejestrujWizyteAsync(It.IsAny<RejestracjaWizytyDTO>()), Times.Exactly(2));
        }
    }
}
