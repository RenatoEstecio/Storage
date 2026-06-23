using Library.DTO;
using StackExchange.Redis;
using System.Text.Json;

public class RedisSessionService : ISessionService
{
    private const string KeyPrefix = "session:";

    private readonly IConnectionMultiplexer _redis;

    public RedisSessionService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public Task CreateAsync(string sessionId, UserSession session, TimeSpan ttl)
    {
        var db = _redis.GetDatabase();
        var value = JsonSerializer.Serialize(session);

        return db.StringSetAsync(KeyPrefix + sessionId, value, ttl);
    }

    public Task<bool> ExistsAsync(string sessionId)
    {
        var db = _redis.GetDatabase();

        return db.KeyExistsAsync(KeyPrefix + sessionId);
    }

    public Task RemoveAsync(string sessionId)
    {
        var db = _redis.GetDatabase();

        return db.KeyDeleteAsync(KeyPrefix + sessionId);
    }
}
