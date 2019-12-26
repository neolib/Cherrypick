using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MyHotKeys.Library
{
    public class Secret
    {
        AesCng aes = new AesCng();
        ICryptoTransform enc;
        ICryptoTransform dec;

        public Secret(byte[] key, byte[] IV)
        {
            aes.Key = key;
            aes.IV = IV;
            enc = aes.CreateEncryptor();
            dec = aes.CreateDecryptor();
        }

        public byte[] Cloak(string cleartext)
        {
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
            {
                using (var w = new StreamWriter(cs))
                {
                    w.Write(cleartext);
                }
                return ms.ToArray();
            }
        }

        public string Reveal(byte[] secret)
        {
            using (var ms = new MemoryStream(secret))
            using (var cs = new CryptoStream(ms, dec, CryptoStreamMode.Read))
            {
                using (var r = new StreamReader(cs))
                {
                    return r.ReadToEnd();
                }
            }
        }
    }
}
