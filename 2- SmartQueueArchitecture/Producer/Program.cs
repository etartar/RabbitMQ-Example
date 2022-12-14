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
                channel.QueueDeclare(QueueNames.DefaultQueueName, durable: true, false, false, null);

                for (int i = 0; i <= 100; i++)
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"is - {i}");

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "", routingKey: QueueNames.DefaultQueueName, basicProperties: properties, body: msg);
                }
            }
        }
    }
}