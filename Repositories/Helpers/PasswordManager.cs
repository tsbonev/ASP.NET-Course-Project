using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Helpers
{
    public class PasswordManager
    {
        #region Public methods
        public string GeneratePasswordHash(string password, out string salt)
        {
            salt = GenerateSalt();
            string passAndSalt = password + salt;

            string hash = GetStringHash(passAndSalt);
            return hash;
        }

        public bool IsPasswordMatch(string password, string hash, string salt)
        {
            string passAndSalt = password + salt;
            string newPassHash = GetStringHash(passAndSalt);

            bool areSame = newPassHash == hash;
            return areSame;
        }
        #endregion

        #region Private methods
        private string GenerateSalt()
        {
            string salt = Guid.NewGuid().ToString();
            return salt;
        }

        private string GetStringHash(string text)
        {

            SHA256 sha256 = SHA256CryptoServiceProvider.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            byte[] hash = sha256.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}
