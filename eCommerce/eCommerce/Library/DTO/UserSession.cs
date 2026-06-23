namespace Library.DTO
{
    public sealed record UserSession(string Email, string IpAddress, string OperatingSystem, string UserAgent, DateTime LoginAt);
}
