using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pagr.Controllers
{
    public static class pagrEncryption
    {
        public static string CreatePassWord(string username, string password)
        {
            //Inside Salt impossible to break the code
            // only exists c# can not be accessed via sql and etc
            string hiddenSalt = "#23MH";
            // our salt
            string usernameAndpassword = username + password + username;
            string EncryptedHasSaltpassword = generateSHA256Hash(hiddenSalt, usernameAndpassword);
            return EncryptedHasSaltpassword;
        }

        public static string generateSHA256Hash(string salt, string password)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(salt + password);
            System.Security.Cryptography.SHA256Managed sha256HashString =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            return System.Text.Encoding.Default.GetString(hash);
        }
    }
}