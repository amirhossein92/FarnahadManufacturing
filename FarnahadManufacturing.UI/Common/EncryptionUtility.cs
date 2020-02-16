using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.UI.Common
{
    public static class EncryptionUtility
    {
        public static string EncryptPassword(string password, string passwordSalt)
        {
            var sha = SHA256.Create();
            var value = Encoding.UTF8.GetBytes(password + passwordSalt);
            var encryptedBytes = sha.ComputeHash(value);
            return Convert.ToBase64String(encryptedBytes) + passwordSalt;
        }
    }
}
