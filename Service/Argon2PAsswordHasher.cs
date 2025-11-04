using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using Backend.Service.Interface;


namespace Backend.Service
{
    public class Argon2PAsswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 64
            };

            var hash = argon2.GetBytes(32);
            return Convert.ToBase64String(salt.Concat(hash).ToArray());
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var bytes = Convert.FromBase64String(hashedPassword);
            var salt = bytes.Take(16).ToArray();
            var hashToCompare = bytes.Skip(16).ToArray();

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 64
            };

            var computedHash = argon2.GetBytes(32);
            return computedHash.SequenceEqual(hashToCompare);
        }
    }
}