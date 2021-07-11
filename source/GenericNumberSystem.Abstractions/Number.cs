using System;

namespace GenericNumberSystem.Abstractions
{
    public class Number
    {
        public long DecimalNumber { get; }
        public INumberSystem NumberSystem { get; }

        public Number(long number, INumberSystem numberSystem)
        {
            DecimalNumber = number;
            NumberSystem = numberSystem;
        }

        public override string ToString()
        {
            return NumberSystem.ConvertTo(DecimalNumber);
        }

        public static Number operator +(Number a, long b)
        {
            return new Number(a.DecimalNumber + b, a.NumberSystem);
        }

        public static Number operator -(Number a, long b)
        {
            return new Number(a.DecimalNumber - b, a.NumberSystem);
        }

        public static Number operator *(Number a, long b)
        {
            return new Number(a.DecimalNumber * b, a.NumberSystem);
        }

        public static Number operator /(Number a, long b)
        {
            return new Number(a.DecimalNumber / b, a.NumberSystem);
        }

        public static Number operator ^(Number a, long b)
        {
            return new Number(a.DecimalNumber ^ b, a.NumberSystem);
        }

        public static bool operator ==(Number a, long b)
        {
            return a.DecimalNumber == b;
        }

        public static bool operator !=(Number a, long b)
        {
            return a.DecimalNumber != b;
        }

        public static bool operator <(Number a, long b)
        {
            return a.DecimalNumber < b;
        }

        public static bool operator >(Number a, long b)
        {
            return a.DecimalNumber > b;
        }

        public static bool operator <=(Number a, long b)
        {
            return a.DecimalNumber <= b;
        }

        public static bool operator >=(Number a, long b)
        {
            return a.DecimalNumber >= b;
        }

        public static Number operator +(Number a, Number b)
        {
            return Calculate(a, b, (x, y) => x + y);
        }

        public static Number operator -(Number a, Number b)
        {
            return Calculate(a, b, (x, y) => x - y);
        }

        public static Number operator *(Number a, Number b)
        {
            return Calculate(a, b, (x, y) => x * y);
        }

        public static Number operator /(Number a, Number b)
        {
            return Calculate(a, b, (x, y) => x / y);
        }

        public static Number operator ^(Number a, Number b)
        {
            return Calculate(a, b, (x, y) => x ^ y);
        }

        public static bool operator ==(Number a, Number b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Number a, Number b)
        {
            return !a.Equals(b);
        }

        public static bool operator <(Number a, Number b)
        {
            return a.DecimalNumber < b.DecimalNumber;
        }

        public static bool operator >(Number a, Number b)
        {
            return a.DecimalNumber > b.DecimalNumber;
        }

        public static bool operator <=(Number a, Number b)
        {
            return a.DecimalNumber <= b.DecimalNumber;
        }

        public static bool operator >=(Number a, Number b)
        {
            return a.DecimalNumber >= b.DecimalNumber;
        }

        private static Number Calculate(Number a, Number b, Func<long, long, long> operation)
        {
            ThrowIfNumbersystemsAreDifferent(a, b);
            return new Number(operation(a.DecimalNumber, b.DecimalNumber), a.NumberSystem);
        }

        private static void ThrowIfNumbersystemsAreDifferent(Number a, Number b)
        {
            if (!a.NumberSystem.Equals(b.NumberSystem))
            {
                throw new NotSameNumberSystemUsedException();
            }
        }
    }
}