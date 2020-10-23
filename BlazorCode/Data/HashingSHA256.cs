using InventoryChecker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace InventoryChecker.Data
{
    public class HashingSHA256 : IHashing
    {
        public string GenerateHash(string input)
        {
            string finalHashString;
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                finalHashString = BytesToString(bytes);
            }
            return finalHashString;
        }
        private string BytesToString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
