using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL
{
    public static class FileBLL
    {
        public static async Task<string> StreamToBase64Async(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (stream.CanSeek)
                stream.Position = 0; // importante!

            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        public static Stream Base64ToStream(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentException("Base64 inválido");

            // Remove prefixo se existir (data:image/...;base64,)
            var commaIndex = base64.IndexOf(',');
            if (commaIndex >= 0)
                base64 = base64[(commaIndex + 1)..];

            byte[] bytes = Convert.FromBase64String(base64);
            return new MemoryStream(bytes);
        }
    }
}
