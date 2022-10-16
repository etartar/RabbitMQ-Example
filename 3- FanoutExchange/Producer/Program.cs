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
                channel.ExchangeDeclare(ExchangeNames.FANOUT_EXCHANGE_NAME, type: ExchangeType.Fanout);
                for (int i = 0; i <= 100; i++)
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"is - {i}");

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: ExchangeNames.FANOUT_EXCHANGE_NAME, routingKey: "", basicProperties: properties, body: msg);
                }
            }
        }
    }
}