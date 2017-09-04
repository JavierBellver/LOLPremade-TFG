using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;

namespace lolpremade.Utils
{
    public static class CryptoUtils
    {
        public static string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var saltGenerator = RandomNumberGenerator.Create())
            {
                saltGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public static string HashWithSHA256(string input,string salt)
        {
            using (SHA256 hashService = SHA256.Create())
            {
                var hashedBytes = hashService.ComputeHash(Encoding.UTF8.GetBytes(input));
                return salt+BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static string GetPseudoRandomString(int numberOfCharacters)
        {
            using (RandomNumberGenerator randomService = RandomNumberGenerator.Create())
            {
                string _Chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ123456790";
                byte[] randomBytes = new byte[numberOfCharacters];
                randomService.GetBytes(randomBytes);
                char[] chars = new char[numberOfCharacters];
                int Count = _Chars.Length;

                for(int i=0;i<numberOfCharacters;i++)
                {
                    chars[i] = _Chars[(int)randomBytes[i] % Count];
                }
                return new string(chars);
            }
        }
    }
}
