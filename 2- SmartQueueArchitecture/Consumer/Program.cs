using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
                channel.QueueDeclare("iskuyrugu", durable: true, false, false, null);
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume("iskuyrugu", false, consumer);

                consumer.Received += (sender, e) =>
                {
                    Thread.Sleep(int.Parse(args[0]));
                    Console.WriteLine($"{Encoding.UTF8.GetString(e.Body.Span)} alındı.");
                    channel.BasicAck(e.DeliveryTag, false);
                };

                Console.Read();
            }
        }
    }
}