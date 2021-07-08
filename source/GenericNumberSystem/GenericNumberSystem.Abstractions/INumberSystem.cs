namespace GenericNumberSystem.Abstractions
{
    public interface INumberSystem
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
}