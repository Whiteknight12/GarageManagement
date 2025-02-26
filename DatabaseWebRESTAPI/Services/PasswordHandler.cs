using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DatabaseWebRESTAPI.Services
{
    public class PasswordHandler
    {
        private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();
        private const int _iterationCount = 10000;
        private const int _saltSize = 128 / 8; 
        private const int _keySize = 256 / 8;  

        public static string HashPassword(string password)
        {
            var salt = new byte[_saltSize];
            _randomNumberGenerator.GetBytes(salt);

            var subkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, _iterationCount, _keySize);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // Version marker

            WriteNetworkByteOrder(outputBytes, 1, (uint)KeyDerivationPrf.HMACSHA512);
            WriteNetworkByteOrder(outputBytes, 5, (uint)_iterationCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)_saltSize);

            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + _saltSize, subkey.Length);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                var hashBytes = Convert.FromBase64String(hashedPassword);
                var keyDerivationPrf = (KeyDerivationPrf)ReadNetworkByteOrder(hashBytes, 1);
                var iterationCount = (int)ReadNetworkByteOrder(hashBytes, 5);
                var saltLength = (int)ReadNetworkByteOrder(hashBytes, 9);

                if (saltLength < _saltSize) return false;

                var salt = new byte[saltLength];
                Buffer.BlockCopy(hashBytes, 13, salt, 0, saltLength);

                var expectedSubkey = new byte[hashBytes.Length - 13 - saltLength];
                Buffer.BlockCopy(hashBytes, 13 + saltLength, expectedSubkey, 0, expectedSubkey.Length);

                var actualSubkey = KeyDerivation.Pbkdf2(password, salt, keyDerivationPrf, iterationCount, expectedSubkey.Length);

                return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
            }
            catch
            {
                return false;
            }
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)value;
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)buffer[offset] << 24)
                 | ((uint)buffer[offset + 1] << 16)
                 | ((uint)buffer[offset + 2] << 8)
                 | buffer[offset + 3];
        }
    }
}
