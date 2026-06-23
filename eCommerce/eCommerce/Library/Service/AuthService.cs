using Library.DTO;
using Library.Util;
using Microsoft.Extensions.Options;
using System.Net;

public class AuthService
{
    private static readonly TimeSpan SessionTtl = TimeSpan.FromHours(72);

    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;
    private readonly ISessionService _sessionService;
    private readonly CryptoOptions _cryptoOptions;

    public AuthService(IUserRepository userRepository, JwtTokenService jwtTokenService, ISessionService sessionService, IOptions<CryptoOptions> cryptoOptions)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _sessionService = sessionService;
        _cryptoOptions = cryptoOptions.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, RequestContext context)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null || AesPassphraseCipher.Decrypt(user.Pass, _cryptoOptions.Passphrase) != request.Password)
            throw new CustomException("Email ou senha inválidos", HttpStatusCode.Unauthorized);

        var jti = Guid.NewGuid().ToString();
        var response = _jwtTokenService.GenerateToken(user, context, jti);

        var session = new UserSession(user.Email, context.IpAddress, context.OperatingSystem, context.UserAgent, DateTime.UtcNow);
        await _sessionService.CreateAsync(jti, session, SessionTtl);

        return response;
    }

    public Task LogoutAsync(string jti)
    {
        return _sessionService.RemoveAsync(jti);
    }
}
