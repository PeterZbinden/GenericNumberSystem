﻿using System;

namespace GenericNumberSystem.Abstractions
{
    public class Number
    {
        /// <summary>
        /// The decimal representation of the value
        /// </summary>
        public long Value { get; }
        /// <summary>
        /// The number system that was used to create this number
        /// </summary>
        public INumberSystem NumberSystem { get; }

        /// <summary>
        /// Creates a new Number using a decimal value and a number system
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberSystem"></param>
        public Number(long value, INumberSystem numberSystem)
        {
            Value = value;
            NumberSystem = numberSystem;
        }

        public string GetNumberSystemRepresentation()
        {
            return NumberSystem.Convert(Value);
        }

        public override string ToString()
        {
            return GetNumberSystemRepresentation();
        }

        public static Number operator +(Number a, long b)
        {
            return new Number(a.Value + b, a.NumberSystem);
        }

        public static Number operator +(long a, Number b)
        {
            return new Number(a + b.Value, b.NumberSystem);
        }

        public static Number operator -(Number a, long b)
        {
            return new Number(a.Value - b, a.NumberSystem);
        }

        public static Number operator -(long a, Number b)
        {
            return new Number(a - b.Value, b.NumberSystem);
        }

        public static Number operator *(Number a, long b)
        {
            return new Number(a.Value * b, a.NumberSystem);
        }

        public static Number operator *(long a, Number b)
        {
            return new Number(a * b.Value, b.NumberSystem);
        }

        public static Number operator /(Number a, long b)
        {
            return new Number(a.Value / b, a.NumberSystem);
        }

        public static Number operator /(long a, Number b)
        {
            return new Number(a / b.Value, b.NumberSystem);
        }

        public static Number operator ^(Number a, long b)
        {
            return new Number(a.Value ^ b, a.NumberSystem);
        }

        public static Number operator ^(long a, Number b)
        {
            return new Number(a ^ b.Value, b.NumberSystem);
        }

        public static bool operator ==(Number a, long b)
        {
            return a.Value == b;
        }

        public static bool operator ==(long a, Number b)
        {
            return a == b.Value;
        }

        public static bool operator !=(Number a, long b)
        {
            return a.Value != b;
        }

        public static bool operator !=(long a, Number b)
        {
            return a != b.Value;
        }

        public static bool operator <(Number a, long b)
        {
            return a.Value < b;
        }

        public static bool operator <(long a, Number b)
        {
            return a < b.Value;
        }

        public static bool operator >(Number a, long b)
        {
            return a.Value > b;
        }

        public static bool operator >(long a, Number b)
        {
            return a > b.Value;
        }

        public static bool operator <=(Number a, long b)
        {
            return a.Value <= b;
        }

        public static bool operator <=(long a, Number b)
        {
            return a <= b.Value;
        }

        public static bool operator >=(Number a, long b)
        {
            return a.Value >= b;
        }

        public static bool operator >=(long a, Number b)
        {
            return a >= b.Value;
        }

        public static Number operator +(Number a, Number b)
        {
            return GetResult(a, b, (x, y) => x + y);
        }

        public static Number operator -(Number a, Number b)
        {
            return GetResult(a, b, (x, y) => x - y);
        }

        public static Number operator *(Number a, Number b)
        {
            return GetResult(a, b, (x, y) => x * y);
        }

        public static Number operator /(Number a, Number b)
        {
            return GetResult(a, b, (x, y) => x / y);
        }

        public static Number operator ^(Number a, Number b)
        {
            return GetResult(a, b, (x, y) => x ^ y);
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
            return a.Value < b.Value;
        }

        public static bool operator >(Number a, Number b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <=(Number a, Number b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(Number a, Number b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Number n)
            {
                return Value == n.Value;
            }
            if (obj is long l)
            {
                return Value == l;
            }
            if (obj is int i)
            {
                return Value == i;
            }
            if (obj is byte b)
            {
                return Value == b;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        private static Number GetResult(Number a, Number b, Func<long, long, long> operation)
        {
            return new Number(operation(a.Value, b.Value), a.NumberSystem);
        }
    }
}