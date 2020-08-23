using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Models.Logic.Hash
{
    public static class Hasher
    {
        public static string GetShortHash(string url)
        {
            if (url == null || string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            string hash = ComputeSha256Hash(url);

            return hash.Substring(0, 5);
        }

        private static string ComputeSha256Hash(string url)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
