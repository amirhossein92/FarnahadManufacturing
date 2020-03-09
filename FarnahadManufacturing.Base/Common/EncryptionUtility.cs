using System;
using System.Security.Cryptography;
using System.Text;

// CHECK
namespace FarnahadManufacturing.Base.Common
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
