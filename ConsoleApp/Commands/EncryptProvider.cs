using System.Numerics;

namespace ConsoleApp.Commands
{
    internal interface IEncryptProvider
    {
        /// <summary>
        /// Encrypt byte array by given public key
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        byte[] Encrypt(BigInteger e, BigInteger n, byte[] m);
    }

    public class EncryptProvider : IEncryptProvider
    {
        /// <inheritdoc />
        public byte[] Encrypt(BigInteger e, BigInteger n, byte[] m) =>
            BigInteger.ModPow(new BigInteger(m), e, n).ToByteArray();
    }
}