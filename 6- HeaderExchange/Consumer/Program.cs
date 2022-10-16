using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Kernel.Constants;
using System.Globalization;
using System.Text;

namespace Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(ExchangeNames.HEADER_EXCHANGE_NAME, type: ExchangeType.Headers);

                channel.QueueDeclare($"kuyruk-{args[0]}", false, false, false, null);

                channel.QueueBind(queue: $"kuyruk-{args[0]}", exchange: ExchangeNames.HEADER_EXCHANGE_NAME, routingKey: string.Empty, new Dictionary<string, object>
                {
                    ["x-match"] = "all",
                    ["no"] = args[0] == "1" ? "123456" : "654321",
                });

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume($"kuyruk-{args[0]}", false, consumer);
                consumer.Received += (sender, e) =>
                {
                    Console.WriteLine($"{Encoding.UTF8.GetString(e.Body.Span)}. mesaj");
                    channel.BasicAck(e.DeliveryTag, false);
                };
                Console.Read();
            }
        }
    }
}