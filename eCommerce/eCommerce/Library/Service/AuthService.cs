using Library.DTO;
using Library.Util;
using Microsoft.Extensions.Options;
using System.Net;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;
    private readonly CryptoOptions _cryptoOptions;

    public AuthService(IUserRepository userRepository, JwtTokenService jwtTokenService, IOptions<CryptoOptions> cryptoOptions)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _cryptoOptions = cryptoOptions.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, RequestContext context)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null || AesPassphraseCipher.Decrypt(user.Pass, _cryptoOptions.Passphrase) != request.Password)
            throw new CustomException("Email ou senha inválidos", HttpStatusCode.Unauthorized);

        return _jwtTokenService.GenerateToken(user, context);
    }
}
