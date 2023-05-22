using System;
using System.Linq;
using System.Text;
using GenericNumberSystem.Abstractions;

namespace GenericNumberSystem
{
    public class NumberSystem : INumberSystem
    {
        public static readonly NumberSystem Octal = new NumberSystem("01234567");
        public static readonly NumberSystem Hex = new NumberSystem("0123456789abcdef");
        public static readonly NumberSystem Binary = new NumberSystem("01");

        public string AvailableNumbers { get; }
        private readonly string _minusSign;
        private readonly Position _minusSignPosition;

        public NumberSystem(string availableNumbers,
            string minusSign = "-",
            Position minusSignPosition = Position.Front)
        {
            AvailableNumbers = availableNumbers;
            _minusSign = minusSign;
            _minusSignPosition = minusSignPosition;

            foreach (var n in availableNumbers)
            {
                if (availableNumbers.Count(c => c == n) > 1)
                {
                    throw new ApplicationException($"The character '{n}' is contained more than once in the {nameof(availableNumbers)}, this is not supported.");
                }
            }
        }

        public string Convert(long input)
        {
            var charCount = AvailableNumbers.Length;
            var builder = new StringBuilder();

            var inputIsNegative = input < 0;

            if (inputIsNegative)
            {
                input = input * -1;
            }

            var temp = input;

            var position = 1;
            while (true)
            {
                var positionValue = (long)Math.Pow(charCount, position - 1);
                var maxNumber = (long)Math.Pow(charCount, position);

                var rest = temp % maxNumber;

                var index = 0L;
                if (rest > 0)
                {
                    index = rest / positionValue;
                }

                if (index >= AvailableNumbers.Length || index < 0)
                {
                    throw new ApplicationException($"Impossible Index of {index}");
                }

                builder.Insert(0, AvailableNumbers[(int)index]);

                temp -= index * positionValue;

                if (temp <= 0)
                {
                    break;
                }

                position++;
            }

            if (inputIsNegative)
            {
                if (_minusSignPosition == Position.Front)
                {
                    builder.Insert(0, _minusSign);
                }
                else
                {
                    builder.Append(_minusSign);
                }
            }

            return builder.ToString();
        }

        public Number Parse(string number)
        {
            var result = 0L;
            
            var isNegative = false;
            if (_minusSignPosition == Position.Front && number.StartsWith(_minusSign))
            {
                number = number.Substring(_minusSign.Length);
                isNegative = true;
            }
            else if (_minusSignPosition == Position.Back && number.EndsWith(_minusSign))
            {
                number = number.Substring(0, number.Length - _minusSign.Length);
                isNegative = true;
            }
            
            var charCount = AvailableNumbers.Length;
            var position = 1;
            for (int i = 0; i < number.Length; i++)
            {
                var positionValue = (long)Math.Pow(charCount, position - 1);
                var c = number[number.Length -1 - i];

                var count = AvailableNumbers.IndexOf(c);

                result += count * positionValue;
                position++;
            }

            if (isNegative)
            {
                result = result * -1;
            }

            return new Number(result, this);
        }

        public bool TryParse(string number, out Number result)
        {
            try
            {
                result = Parse(number);
                return true;
            }
            catch (Exception e)
            {
                result = null;
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is NumberSystem nsY)
            {
                return _minusSign == nsY._minusSign
                       && AvailableNumbers == nsY.AvailableNumbers
                       && _minusSignPosition == nsY._minusSignPosition;
            }

            return false;
        }
    }
}
