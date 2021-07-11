using System.Collections;

namespace GenericNumberSystem.Abstractions
{
    public interface INumberSystem : IEqualityComparer
    {
        /// <summary>
        /// Converts a given decimal number into the defined NumberSystem
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        string ConvertTo(long number);
        /// <summary>
        /// Converts a number within the defined NumberSystem back into a useable decimal format.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        long Parse(string number);
    }

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

        public static Number operator +(Number a, Number b)
        {
            if (!a.NumberSystem.Equals(b.NumberSystem))
            {
                throw new NotSameNumberSystemUsedException();
            }

            var decResult = a.DecimalNumber + b.DecimalNumber;

            return new Number(decResult, a.NumberSystem);
        }
    }
}