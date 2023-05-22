using System;
using GenericNumberSystem;

namespace Samples
{
    public class Math
    {
        public void Add()
        {
            var hex = NumberSystem.Hex;
            var a = hex.Parse("f"); // 15
            var b = hex.Parse("e"); // 14

            Console.WriteLine(a + b); // 1d in Hex --> 29 in Dec
        }

        public void Substract()
        {
            var hex = NumberSystem.Hex;
            var a = hex.Parse("f"); // 15
            var b = hex.Parse("e"); // 14

            Console.WriteLine(a - b); // 1 in Hex --> 1 in Dec
        }

        public void Multiply()
        {
            var hex = NumberSystem.Hex;
            var a = hex.Parse("f"); // 15
            var b = hex.Parse("a"); // 10

            Console.WriteLine(a * b); // 96 in Hex --> 150 in Dec
        }

        public void Divide()
        {
            var hex = NumberSystem.Hex;
            var a = hex.Parse("28"); // 40 in Dec
            var b = hex.Parse("a"); // 10

            Console.WriteLine(a / b); // 4 in Hex --> 4 in Dec
        }
    }
}