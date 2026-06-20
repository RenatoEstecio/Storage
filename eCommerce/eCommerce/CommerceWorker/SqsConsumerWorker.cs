using Library.DTO;
using Library.Interface;
using Library.Service;
using Microsoft.Extensions.Options;
using System.Text.Json;
namespace CommerceWorker
{
    public class SqsConsumerWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOrderService _ordertService;

       
        public SqsConsumerWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;          
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var receiver =
                    scope.ServiceProvider.GetRequiredService<SqsReceiverService>();

                var orderService =
                    scope.ServiceProvider.GetRequiredService<IOrderService>();

                var pedidos =
                    await receiver.ReceiveMessagesAsync<Order>();

                if (pedidos is not null)
                {
                    foreach (var mensagem in pedidos)
                    {
                        await orderService.Insert(mensagem);
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
