using System;
using System.Text;
using GenericNumberSystem.Abstractions;

namespace GenericNumberSystem
{
    public class NumberSystem : INumberSystem
    {
        private readonly string _availableNumbers;
        private readonly string _minusSign;
        private readonly Position _minusSignPosition;

        public NumberSystem(string availableNumbers,
            string minusSign = "-",
            Position minusSignPosition = Position.Front)
        {
            _availableNumbers = availableNumbers;
            _minusSign = minusSign;
            _minusSignPosition = minusSignPosition;
        }

        public string ConvertTo(long input)
        {
            var charCount = _availableNumbers.Length;
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

                if (index >= _availableNumbers.Length || index < 0)
                {
                    throw new ApplicationException($"Impossible Index of {index}");
                }

                builder.Insert(0, _availableNumbers[(int)index]);

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

        public long Parse(string number)
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
            
            var charCount = _availableNumbers.Length;
            var position = 1;
            for (int i = 0; i < number.Length; i++)
            {
                var positionValue = (long)Math.Pow(charCount, position - 1);
                var c = number[number.Length -1 - i];

                var count = _availableNumbers.IndexOf(c);

                result += count * positionValue;
                position++;
            }

            if (isNegative)
            {
                result = result * -1;
            }

            return result;
        }
    }
}
