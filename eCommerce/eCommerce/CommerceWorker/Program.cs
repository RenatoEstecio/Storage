using Amazon.SQS;
using Library.BLL;
using Library.Interface;
using Library.Service;
using Library.Util;

namespace CommerceWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);         
            builder.Services.AddAWSService<IAmazonSQS>();

            builder.Services.Configure<SqsOptions>(
                builder.Configuration.GetSection("Sqs"));

            builder.Services.AddScoped<DataBaseBLL>();
            builder.Services.AddScoped<SqsReceiverService>();
            builder.Services.AddHostedService<SqsConsumerWorker>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            var host = builder.Build();
            host.Run();
        }
    }
}
