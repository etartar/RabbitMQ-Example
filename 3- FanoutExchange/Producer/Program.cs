using RabbitMQ.Client;
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
                channel.ExchangeDeclare("workingQueue", type: ExchangeType.Fanout);
                for (int i = 0; i <= 100; i++)
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"is - {i}");

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "workingQueue", routingKey: "", basicProperties: properties, body: msg);
                }
            }
        }
    }
}