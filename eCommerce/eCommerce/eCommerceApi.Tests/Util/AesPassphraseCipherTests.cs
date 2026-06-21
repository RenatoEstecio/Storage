using Library.Util;
using Xunit;

public class AesPassphraseCipherTests
{
    [Fact]
    public void Decrypt_DeveRetornarSenhaOriginal_QuandoCifradoComCryptoJs()
    {
        // Valor gerado com CryptoJS.AES.encrypt("storage", "0fc3d2b6-cad2-4dd2-8707-42d2945e9e33")
        var result = AesPassphraseCipher.Decrypt(
            "U2FsdGVkX1/nYFixJLvl+QloUfLdv0azpLRPw8EQVu4=",
            "0fc3d2b6-cad2-4dd2-8707-42d2945e9e33");

        Assert.Equal("storage", result);
    }
}
