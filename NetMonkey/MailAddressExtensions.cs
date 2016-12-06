using System;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;

namespace NetMonkey
{

    /// <summary>Extension methods for the <see cref="MailAddress" /> type.</summary>
    public static class MailAddressExtensions
    {

        /// <summary>Get the subscriber hash for the current mail address.</summary>
        /// <param name="address">The address.</param>
        /// <returns>The subscriber hash.</returns>
        public static string GetMailChimpSubscriberHash(this MailAddress address)
        {
            if (address==null)
                return null;

            using (var md5 = new MD5CryptoServiceProvider())
            {
                var hash=md5.ComputeHash(Encoding.ASCII.GetBytes(address.Address.ToLowerInvariant()));

                var chars = new char[hash.Length*2];
                for (int i = 0; i<hash.Length; i++)
                {
                    byte b = hash[i];
                    int ci = i*2;
                    chars[ci]=_GetHexValue(b/16);
                    chars[ci+1]=_GetHexValue(b%16);
                }

                return new string(chars, 0, chars.Length);
            }
        }

        private static char _GetHexValue(int i)
        {
            if (i<10)
                return (char)(i+'0');
            return (char)(i-10+'a');
        }
    }
}
