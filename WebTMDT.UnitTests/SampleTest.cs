using WebTMDTLibrary.Helper;
using Xunit;

namespace WebTMDT.UnitTests
{
    public class SampleTest
    {
        public SampleTest()
        {

        }

        [Fact]
        public void AddFunction_ValidNumbers_ReturnCorrectResult()
        {
            // Arrange
            var firstNumber = 1;
            var secondNumber = 2;

            // Act
            var result = firstNumber + secondNumber;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result);
        }

        [Fact]
        public void UtilityRandomString_ValidLength_ReturnStringWithCorrectLength()
        {
            var length = 5;

            var result = Utility.RandomString(length);

            Assert.NotNull(result);
            Assert.Equal(result.Length, 5);
        }
    }
}