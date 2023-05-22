using System;
using GenericNumberSystem;

namespace Samples
{
    public class Parsing
    {
        public void Parse()
        {
            var oct = NumberSystem.Octal;
            var number = oct.Parse("10");
            Console.WriteLine(number.Value); // 8
        }

        public void TryParse()
        {
            var bin = NumberSystem.Binary;
            if(bin.TryParse("10", out var number))
            {
                Console.WriteLine(number.Value); //2
            }
            else
            {
                Console.WriteLine("Could not parse input");
            }
        }
    }
}
