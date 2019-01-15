using System.Numerics;
using System.Security.Cryptography;

namespace ConsoleApp.Commands 
{
    internal static class PrimeGenerator
    {
        /// <summary>
        /// Generate BigInteger prime with particular length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static BigInteger Generate(int length = 50)
        {
            while (true)
            {
                var rng = RandomNumberGenerator.Create();
                var bytes = new byte[length];

                rng.GetBytes(bytes);
                var i = new BigInteger(bytes);

                if (i.IsProbablePrime(400))
                {
                    return i;
                }
            }
        }
    }
}