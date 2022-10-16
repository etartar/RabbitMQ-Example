using RabbitMQ.Client;
using Shared.Kernel.Constants;
using System.Text;

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
                channel.ExchangeDeclare(ExchangeNames.TOPIC_EXCHANGE_NAME, type: ExchangeType.Topic);

                for (int i = 0; i <= 100; i++)
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"{i}. görev verildi");

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    string routingKey = "Asker.Subay.";
                    routingKey += i % 2 == 0 
                        ? RoutingKeys.TOPIC_EXCHANGE_YUZBASI
                        : (i % 11 == 0 ? RoutingKeys.TOPIC_EXCHANGE_BINBASI : RoutingKeys.TOPIC_EXCHANGE_TEGMEN);

                    channel.BasicPublish(exchange: ExchangeNames.TOPIC_EXCHANGE_NAME, routingKey: routingKey, basicProperties: properties, body: msg);
                }
            }
        }
    }
}