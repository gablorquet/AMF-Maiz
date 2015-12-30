using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AMF.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly SHA1 sha1 = SHA1.Create();

        public static string ToSHA1(this string input, string salt = "")
        {
            return sha1
                .ComputeHash(Encoding.Default.GetBytes(input + salt))
                .Aggregate("", (current, x) => current + x.ToString("x2"));
        }

    }
}