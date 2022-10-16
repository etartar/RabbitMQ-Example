using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Shared.Kernel.Constants;

namespace Producer
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
                channel.ExchangeDeclare(ExchangeNames.DIRECT_EXCHANGE_NAME, type: ExchangeType.Direct);
                
                for (int i = 0; i <= 100; i++)
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"sayı - {i}");

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    string routingKey = i % 2 == 0 ? RoutingKeys.DIRECT_EXCHANGE_KEY2 : RoutingKeys.DIRECT_EXCHANGE_KEY1;

                    channel.BasicPublish(exchange: ExchangeNames.DIRECT_EXCHANGE_NAME, routingKey: routingKey, basicProperties: properties, body: msg);
                }
            }
        }
    }
}