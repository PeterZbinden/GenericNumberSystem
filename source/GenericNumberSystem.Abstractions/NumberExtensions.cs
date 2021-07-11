namespace GenericNumberSystem.Abstractions
{
    public static class NumberExtensions
    {
        public static Number ToNumber(this long value, INumberSystem numberSystem)
        {
            return new Number(value, numberSystem);
        }

        public static Number ToNumber(this int value, INumberSystem numberSystem)
        {
            return new Number(value, numberSystem);
        }
    }
}