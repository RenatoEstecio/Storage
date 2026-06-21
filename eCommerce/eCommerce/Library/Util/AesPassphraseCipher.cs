using System.Security.Cryptography;
using System.Text;

namespace Library.Util
{
    // Compatible with CryptoJS.AES.encrypt(text, passphrase): output is base64("Salted__" + 8-byte salt + ciphertext),
    // key/IV derived from the passphrase via OpenSSL's EVP_BytesToKey (MD5, 48 bytes: 32-byte key + 16-byte IV).
    public static class AesPassphraseCipher
    {
        public static string Decrypt(string cipherTextBase64, string passphrase)
        {
            var cipherBytes = Convert.FromBase64String(cipherTextBase64);

            var salt = cipherBytes.Skip(8).Take(8).ToArray();
            var ciphertext = cipherBytes.Skip(16).ToArray();

            var (key, iv) = DeriveKeyAndIv(passphrase, salt);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }

        private static (byte[] Key, byte[] Iv) DeriveKeyAndIv(string passphrase, byte[] salt)
        {
            var passphraseBytes = Encoding.UTF8.GetBytes(passphrase);
            var derived = new List<byte>();
            var lastHash = Array.Empty<byte>();

            while (derived.Count < 48)
            {
                lastHash = MD5.HashData(lastHash.Concat(passphraseBytes).Concat(salt).ToArray());
                derived.AddRange(lastHash);
            }

            return (derived.Take(32).ToArray(), derived.Skip(32).Take(16).ToArray());
        }
    }
}
