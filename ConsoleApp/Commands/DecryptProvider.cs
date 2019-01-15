using System.Numerics;

namespace ConsoleApp.Commands
{
    internal interface IDecryptProvider
    {
        /// <summary>
        /// Decrypt byte array by given private key
        /// </summary>
        /// <param name="d"></param>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        byte[] Decrypt(BigInteger d, BigInteger n, byte[] m);
    }

    public class DecryptProvider : IDecryptProvider
    {
        /// <inheritdoc />
        public byte[] Decrypt(BigInteger d, BigInteger n, byte[] m) =>
            BigInteger.ModPow(new BigInteger(m), d, n).ToByteArray();
    }
}