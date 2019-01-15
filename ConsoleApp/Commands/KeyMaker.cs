using System;
using System.Numerics;

namespace ConsoleApp.Commands
{
    internal interface IKeyMaker
    {
        /// <summary>
        /// Generate private and public keys for RSA
        /// </summary>
        /// <returns>
        /// public key is (e, n)
        /// private key is (d, n)
        /// </returns>
        (BigInteger e, BigInteger d, BigInteger n) Make();
    }

    /// <summary>
    /// RSA key generator
    /// </summary>
    public class KeyMaker : IKeyMaker
    {
        /// <inheritdoc />
        public (BigInteger e, BigInteger d, BigInteger n) Make()
        {
            var p = PrimeGenerator.Generate();
            var q = PrimeGenerator.Generate();

            while (p == q)
            {
                Console.WriteLine("hmm, as i see p == q, let's try again");
                q = PrimeGenerator.Generate();
            }

            var n = p * q; // mod
            var fi = (p - 1) * (q - 1); // Euler's 

            var d = D(fi);
            var e = E(d, fi);

            if (e == d)
            {
                Console.WriteLine("bad day");
            }

            return (e, d, n);
        }

        /// <summary>
        /// Euclidean algorithm
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        private static BigInteger D(BigInteger fi)
        {
            var d = PrimeGenerator.Generate();

            while (true)
            {
                if (BigInteger.GreatestCommonDivisor(fi, d) == 1)
                {
                    return d;
                }

                d--;
            }
        }

        /// <summary>
        /// Extended Euclidean algorithm
        /// </summary>
        /// <param name="d"></param>
        /// <param name="fi"></param>
        /// <returns></returns>
        private static BigInteger E(BigInteger d, BigInteger fi)
        {
            BigInteger inv, u1, u3, v1, v3, t1, t3, q;
            BigInteger iter;

            /* Step X1. Initialise */
            u1 = 1;
            u3 = d;
            v1 = 0;
            v3 = fi;

            /* Remember odd/even iterations */
            iter = 1;

            /* Step X2. Loop while v3 != 0 */
            while (v3 != 0)
            {
                /* Step X3. Divide and "Subtract" */
                q = u3 / v3;
                t3 = u3 % v3;
                t1 = u1 + q * v1;

                /* Swap */
                u1 = v1;
                v1 = t1;
                u3 = v3;
                v3 = t3;
                iter = -iter;
            }

            /* Make sure u3 = gcd(u,v) == 1 */
            if (u3 != 1)
            {
                return 0; /* Error: No inverse exists */
            }

            /* Ensure a positive result */
            if (iter < 0)
            {
                inv = fi - u1;
            }
            else
            {
                inv = u1;
            }

            return inv;
        }
    }
}