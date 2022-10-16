using RabbitMQ.Client.Events;
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
                channel.ExchangeDeclare("directExchange", type: ExchangeType.Direct);
                
                for (int i = 0; i <= 100; i++)
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"sayı - {i}");

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    string routingKey = i % 2 == 0 ? "ciftsayilar" : "teksayilar";

                    channel.BasicPublish(exchange: "directExchange", routingKey: routingKey, basicProperties: properties, body: msg);
                }
            }
        }
    }
}