using RabbitMQ.Client.Events;
using RabbitMQ.Client;
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
                channel.ExchangeDeclare("directExchange", type: ExchangeType.Direct);

                string queueName = channel.QueueDeclare().QueueName;
                string routingKey = int.Parse(args[0]) == 1 ? "teksayilar" : "ciftsayilar";

                Console.WriteLine($"queueName : {routingKey}");

                channel.QueueBind(queue: queueName, exchange: "directExchange", routingKey: routingKey);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queueName, false, consumer);
                consumer.Received += (sender, e) =>
                {
                    Thread.Sleep(int.Parse(args[0]));
                    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span) + " sayısı alındı.");
                    channel.BasicAck(e.DeliveryTag, false);
                };

                Console.Read();
            }
        }
    }
}