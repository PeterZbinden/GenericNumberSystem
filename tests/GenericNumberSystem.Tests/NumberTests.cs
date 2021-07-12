using System;
using FluentAssertions;
using GenericNumberSystem.Abstractions;
using Xunit;

namespace GenericNumberSystem.Tests
{
    public class NumberTests
    {
        [Fact]
        public void When_AddingNumbersWithDifferentNumberSystems_Expected_ExceptionThrown()
        {
            // Arrange
            var numberSystemA = new NumberSystem("01");
            var numberSystemB = new NumberSystem("012");
            var a = new Number(1, numberSystemA);
            var b = new Number(1, numberSystemB);

            // Act
            Action call = () =>
            {
                var x = a + b;
            };

            // Assert
            call.Should().Throw<NotSameNumberSystemUsedException>();
        }

        [Fact]
        public void When_AddingNumbersWithSameNumberSystems_Expected_CorrectResult()
        {
            // Arrange
            var numberSystem = new NumberSystem("01");
            var a = new Number(1, numberSystem);
            var b = new Number(1, numberSystem);

            // Act
            var x = a + b;

            // Assert
            x.Value.Should().Be(2);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 2, 3)]
        [InlineData(1000, 1000, 2000)]
        [InlineData(-1000, -1000, -2000)]
        public void When_AddingTwoNumbersOfSameNumberSystem_Expected_CorrectResult(int a, int b, int expectedResult)
        {
            // Arrange
            var numberA = a.ToBinary();
            var numberB = b.ToBinary();

            // Act
            var result = numberA + numberB;

            // Assert
            result.Value.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 2, -1)]
        [InlineData(1000, 1000, 0)]
        [InlineData(-1000, -1000, 0)]
        public void When_SubtractingTwoNumbersOfSameNumberSystem_Expected_CorrectResult(int a, int b, int expectedResult)
        {
            // Arrange
            var numberA = a.ToBinary();
            var numberB = b.ToBinary();

            // Act
            var result = numberA - numberB;

            // Assert
            result.Value.Should().Be(expectedResult);
        }
    }
}