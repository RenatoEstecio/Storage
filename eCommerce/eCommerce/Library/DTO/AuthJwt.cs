namespace Library.DTO
{
    public sealed record LoginResponse(string Token, DateTime ExpiresAt);

    public sealed record RequestContext(string IpAddress, string OperatingSystem, string UserAgent);
}
