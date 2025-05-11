/*using DTOs;
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
    public class SpyTest
    {
        [Fact]
        public async Task Spy_CapturePassedArgument()
        {
            var mock = new Mock<IWykonaneBadanieService>();
            WykonaneBadaniaDTO capturedDto = null;

            // Ustawienie callbacku w celu przechwycenia przekazywanego argumentu
            mock.Setup(s => s.DodajWykonaneBadanieAsync(It.IsAny<WykonaneBadaniaDTO>()))
                .Callback<WykonaneBadaniaDTO>(dto => capturedDto = dto)
                .Returns(Task.CompletedTask);

            // Tworzenie obiektu DTO, który będzie przekazywany do metody
            var dtoToSend = new WykonaneBadaniaDTO { BadanieId = 1, Data = DateTime.Now, Wyniki = "Badanie kontrolne" };

            await mock.Object.DodajWykonaneBadanieAsync(dtoToSend);
            Assert.NotNull(capturedDto); 
            // Sprawdzamy, czy dto zostało przechwycone
            Assert.Equal(1, capturedDto.BadanieId); 
            // Sprawdzamy, czy id badania jest poprawne
            Assert.Equal("Badanie kontrolne", capturedDto.Wyniki); 
            // Sprawdzamy poprawność opisu

            // Dodatkowy test: sprawdzenie, czy metoda została wywołana tylko raz
            mock.Verify(s => s.DodajWykonaneBadanieAsync(It.IsAny<WykonaneBadaniaDTO>()), Times.Once);

            // Dodatkowy test: sprawdzanie, czy data badania została ustawiona poprawnie
            Assert.True(capturedDto.Data.Date == DateTime.Now.Date);

            // Test z innym obiektem DTO
            var anotherDto = new WykonaneBadaniaDTO { BadanieId = 2, Wyniki = "Badanie diagnostyczne" };
            await mock.Object.DodajWykonaneBadanieAsync(anotherDto);

            // Sprawdzamy, czy nowy obiekt DTO również został przechwycony
            Assert.Equal(2, capturedDto.BadanieId);
            Assert.Equal("Badanie diagnostyczne", capturedDto.Wyniki);
        }
    }
}
*/