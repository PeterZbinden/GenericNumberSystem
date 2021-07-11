using GenericNumberSystem.Abstractions;

namespace GenericNumberSystem
{
    public static class NumberExtensions
    {
        public static Number ToBinary(this int value)
        {
            var numberSystem = new NumberSystem("01");
            return new Number(value, numberSystem);
        }

        public static Number ToHex(this int value)
        {
            var numberSystem = new NumberSystem("0123456789ABCDEF");
            return new Number(value, numberSystem);
        }

        public static Number ToNumberSystem(this int value,
            string availableNumbers,
            string minusSign = "-",
            Position minusSignPosition = Position.Front)
        {
            var numberSystem = new NumberSystem(availableNumbers, minusSign, minusSignPosition);
            return new Number(value, numberSystem);
        }
    }
}