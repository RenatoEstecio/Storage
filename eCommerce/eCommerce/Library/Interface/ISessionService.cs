using Library.DTO;

public interface ISessionService
{
    Task CreateAsync(string sessionId, UserSession session, TimeSpan ttl);
    Task<bool> ExistsAsync(string sessionId);
    Task RemoveAsync(string sessionId);
}
