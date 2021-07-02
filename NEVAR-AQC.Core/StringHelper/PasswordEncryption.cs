#region	License
//------------------------------------------------------------------------------------------------
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Core </Project>
//     <File>
//         <Name> PasswordEncryption.cs </Name>
//         <Created> 27/2/2019 - 22:28:23 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         PasswordEncryption.cs
//     </Summary>
// <License>
//------------------------------------------------------------------------------------------------
#endregion License

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NEVAR_AQC.Core.StringHelper
{
    public class PasswordEncryption
    {
        private static string MD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public static string EncryptionPasswordWithKey(string password, string key)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(key))
            {
                return null;
            }
            return MD5Hash(MD5Hash(password) + key);
        }

        public static string GeneratePasswordKey()
        {
            Random rd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[rd.Next(s.Length)]).ToArray());
        }
    }
}