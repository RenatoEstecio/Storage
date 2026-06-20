public interface ISqsSendService
{
    Task SendMessageAsync<T>(T message);
}