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
            x.DecimalNumber.Should().Be(2);
        }
    }
}