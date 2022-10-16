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
                channel.ExchangeDeclare(ExchangeNames.HEADER_EXCHANGE_NAME, type: ExchangeType.Headers);

                for (int i = 0; i <= 100; i++)
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"{i}. mesaj");

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.Headers = new Dictionary<string, object>()
                    {
                        ["no"] = args[0] == "1" ? "123456" : "654321"
                    };

                    channel.BasicPublish(exchange: ExchangeNames.HEADER_EXCHANGE_NAME, routingKey: string.Empty, basicProperties: properties, body: msg);
                }
            }
        }
    }
}