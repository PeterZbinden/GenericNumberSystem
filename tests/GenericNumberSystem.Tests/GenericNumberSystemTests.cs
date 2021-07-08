using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using GenericNumberSystem.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace GenericNumberSystem.Tests
{
    public class GenericNumberSystemTests
    {
        private readonly ITestOutputHelper _output;

        public GenericNumberSystemTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(99)]
        [InlineData(100)]
        [InlineData(-1)]
        [InlineData(-99)]
        public void When_ConvertingToDecimal_Expected_SameOutputAsInput(int input)
        {
            // Arrange
            var sut = new NumberSystem("0123456789");

            // Act
            var result = sut.ConvertTo(input);

            // Assert
            result.Should().Be(input.ToString(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(1, "1")]
        [InlineData(2, "10")]
        [InlineData(3, "11")]
        [InlineData(4, "100")]
        [InlineData(5, "101")]
        [InlineData(6, "110")]
        [InlineData(128, "10000000")]
        [InlineData(1000, "1111101000")]
        [InlineData(9999, "10011100001111")]
        public void When_ConvertingToBinary_Expected_CorrectResult(int input, string expectedResult)
        {
            // Arrange
            var sut = new NumberSystem("01");

            // Act
            var result = sut.ConvertTo(input);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "10")]
        [InlineData(4, "11")]
        [InlineData(5, "12")]
        [InlineData(6, "20")]
        [InlineData(7, "21")]
        [InlineData(8, "22")]
        [InlineData(9, "100")]
        [InlineData(10, "101")]
        public void When_ConvertingToBase3_Expected_CorrectResult(int input, string expectedResult)
        {
            // Arrange
            var sut = new NumberSystem("012");

            // Act
            var result = sut.ConvertTo(input);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-9999)]
        [InlineData(int.MinValue)]
        public void When_ProvidingNegativeNumber_Expected_NegativeSignIsAdded(int negativeNumber)
        {
            // Arrange
            var minusSign = "-";
            var sut = new NumberSystem("01", minusSign: minusSign, Position.Front);

            // Act
            var result = sut.ConvertTo(negativeNumber);

            // Assert
            result.Should().StartWith(minusSign);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-9999)]
        [InlineData(int.MinValue)]
        public void When_DefiningCustomMinusSign_Expected_CorrectNegativeSignPositionIsUsed(int negativeNumber)
        {
            // Arrange
            var minusSign = "x";
            var sut = new NumberSystem("01", minusSign: minusSign, Position.Back);

            // Act
            var result = sut.ConvertTo(negativeNumber);

            // Assert
            result.Should().EndWith(minusSign);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(100)]
        [InlineData(-1)]
        [InlineData(-9)]
        public void When_ConvertingFromDecimal_Expected_SameOutputAsInput(int input)
        {
            // Arrange
            var minusSign = "-";
            var sut = new NumberSystem("0123456789", minusSign: minusSign, Position.Front);

            // Act
            var result = sut.Parse(input.ToString(CultureInfo.InvariantCulture));

            // Assert
            result.Should().Be(input);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("00", 0)]
        [InlineData("1", 1)]
        [InlineData("10", 2)]
        [InlineData("11", 3)]
        [InlineData("100", 4)]
        [InlineData("101", 5)]
        [InlineData("110", 6)]
        [InlineData("111", 7)]
        [InlineData("1000", 8)]
        [InlineData("1001", 9)]
        [InlineData("1010", 10)]
        [InlineData("-0", 0)]
        [InlineData("-1", -1)]
        [InlineData("-1010", -10)]
        public void When_ConvertingFromBinary_Expected_CorrectResult(string binaryInput, int expectedResult)
        {
            // Arrange
            var minusSign = "-";
            var sut = new NumberSystem("01", minusSign: minusSign, Position.Front);

            // Act
            var result = sut.Parse(binaryInput);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
