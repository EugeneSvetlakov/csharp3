using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Encryptor
{
    public static class StringEncryptor
    {
        public static string Encrypt(string str, int key = 1) => 
            new string(str.Select(c => (char)(c + key)).ToArray());

        public static string Decrypt(string str, int key = 1) => 
            new string(str.Select(c => (char)(c - key)).ToArray());
    }
}
