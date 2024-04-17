using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("34.047374,-84.223918,Taco Bell Alpharetta...", -84.223918)]
        [InlineData("34.039588,-84.283254,Taco Bell Alpharetta...", -84.283254)]
        [InlineData("32.072974,-84.222921,Taco Bell Americu...", -84.222921)]

        // Parse Longtitude
        // Each "line" from your .csv file represents a TacoBell location
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            TacoParser tacoParserInstance = new TacoParser();

            //Act
            ITrackable actual = tacoParserInstance.Parse(line);

            //Assert
            Assert.Equal(expected, actual.Location.Longitude);
        }


        // Parse Longtitude
        // Each "line" from your .csv file represents a TacoBell location
        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        [InlineData("34.047374,-84.223918,Taco Bell Alpharetta...", 34.047374)]
        [InlineData("34.039588,-84.283254,Taco Bell Alpharetta...", 34.039588)]
        [InlineData("32.072974,-84.222921,Taco Bell Americu...", 32.072974)]

        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            TacoParser tacoParserInstance = new TacoParser();

            //Act
            ITrackable actual = tacoParserInstance.Parse(line);

            //Assert
            Assert.Equal(expected, actual.Location.Latitude);
        }

    }
}
