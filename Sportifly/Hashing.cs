using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sportifly
{
    internal class Hashing
    {
        public string Hash(string input)
        {
            using (var sha128 = SHA1.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha128.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hash);
            }

        }
    }
}
