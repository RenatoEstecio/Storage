using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.Model;
using Library.Util;
using Microsoft.Extensions.Options;
using System.Text.Json;

public class SqsSendService : ISqsSendService
{
    private readonly IAmazonSQS _sqs;
    private readonly SqsOptions _options;

    public SqsSendService(
        IAmazonSQS sqs,
        IOptions<SqsOptions> options)
    {
        _sqs = sqs;
        _options = options.Value;
    }

    public async Task SendMessageAsync<T>(T message)
    {
        var body = JsonSerializer.Serialize(message);

        var request = new SendMessageRequest
        {
            QueueUrl = _options.QueueUrl,
            MessageGroupId = "orders",
            MessageBody = body,
            MessageDeduplicationId = Guid.NewGuid().ToString()
        };
        try
        {
            await _sqs.SendMessageAsync(request);
        }
        catch(Exception e)
        { }
    }
}