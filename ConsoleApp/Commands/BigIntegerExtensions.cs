using System.Numerics;
using System.Security.Cryptography;

namespace ConsoleApp.Commands
{
    public static class BigIntegerExtensions
    {
        /// <summary>
        /// Miller-Rabin implementation 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="certainty"></param>
        /// <returns></returns>
        public static bool IsProbablePrime(this BigInteger source, int certainty)
        {
            if (source == 2 || source == 3)
            {
                return true;
            }

            if (source < 2 || source % 2 == 0)
            {
                return false;
            }

            var d = source - 1;
            var s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[source.ToByteArray().LongLength];

            for (var i = 0; i < certainty; i++)
            {
                BigInteger a;

                do
                {
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                } while (a < 2 || a >= source - 2);

                var x = BigInteger.ModPow(a, d, source);

                if (x == 1 || x == source - 1)
                {
                    continue;
                }

                for (var r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}