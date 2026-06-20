using Amazon.SQS;
using Amazon.SQS.Model;
using Library.Util;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Library.Service
{
    public class SqsReceiverService
    {
        private readonly IAmazonSQS _sqs;
        private readonly SqsOptions _options;

        public SqsReceiverService(
            IAmazonSQS sqs,
            IOptions<SqsOptions> options)
        {
            _sqs = sqs;
            _options = options.Value;
        }

        public async Task<List<T>>? ReceiveMessagesAsync<T>()
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _options.QueueUrl,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 20 // Long Polling
            };

            var response = await _sqs.ReceiveMessageAsync(request);

            if (response.Messages is null)
                return null;
            else
            {
                var messages = new List<T>();

                try
                {
                    foreach (var message in response.Messages)
                    {
                        var obj = JsonSerializer.Deserialize<T>(message.Body);

                        if (obj != null)
                        {
                            messages.Add(obj);
                        }

                        // Remove a mensagem da fila após processamento
                        try
                        {
                            await _sqs.DeleteMessageAsync(
                            _options.QueueUrl,
                            message.ReceiptHandle);
                        }
                        catch { }
                    }
                }
                catch { }

                return messages;
            }
        }
    }
}
