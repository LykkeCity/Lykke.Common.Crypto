using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lykke.Common.Crypto
{
    public static class StringCryptoExt
    {
        public static string AesEncript(this string str, string passPhrase)
        {
            var myAes = GetAes(passPhrase);
            var bytesToBeEncrypted = Encoding.Default.GetBytes(str);
            using (MemoryStream ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, myAes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }
                var encryptedBytes = ms.ToArray();

                return BitConverter.ToString(encryptedBytes).Replace("-", "");
            }
        }

        public static string AesDecript(this string str, string passPhrase)
        {
            var myAes = GetAes(passPhrase);
            var bytesToBeDecript = StringToByteArray(str);

            using (MemoryStream ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, myAes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecript, 0, bytesToBeDecript.Length);
                    cs.Close();
                }
                var encryptedBytes = ms.ToArray();

                return Encoding.Default.GetString(encryptedBytes).TrimEnd("\0".ToCharArray());
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();

        }

        private static AesManaged GetAes(string passPhrase)
        {
            var preparedKey = PrepareKey(passPhrase);
            var aes = new AesManaged
            {
                Mode = CipherMode.ECB,
                IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                Key = preparedKey,
                Padding = PaddingMode.Zeros
            };

            //aes.Key = preparedKey;
            return aes;


        }

        private static byte[] PrepareKey(string passPhrase)
        {
            var result = Encoding.ASCII.GetBytes(passPhrase);


            var leadZeroResult = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                leadZeroResult[i] = i < result.Length ? result[i] : (byte)0;
            }
            return leadZeroResult;

        }



    }
}
