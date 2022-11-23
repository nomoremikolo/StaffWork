using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using StaffWork.Server.Utils.Intarfaces;
using StaffWork.Server.Utils.Models;
using System.Security.Cryptography;

namespace StaffWork.Server.Utils
{
    public class HashHelper : IHashHelper
    {
        public HashWithSaltModel GenerateSaltedHash(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            var salt = Convert.ToBase64String(saltBytes);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32));

            return new HashWithSaltModel(hash, salt);
        }

        public bool VerifyHash(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: enteredPassword,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32));

            return hash == storedHash;
        }
    }
}
