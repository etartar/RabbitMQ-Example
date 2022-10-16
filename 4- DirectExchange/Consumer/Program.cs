using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Shared.Kernel.Constants;

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
                channel.ExchangeDeclare(ExchangeNames.DIRECT_EXCHANGE_NAME, type: ExchangeType.Direct);

                string queueName = channel.QueueDeclare().QueueName;
                string routingKey = int.Parse(args[0]) == 1 ? RoutingKeys.DIRECT_EXCHANGE_KEY1 : RoutingKeys.DIRECT_EXCHANGE_KEY2;

                Console.WriteLine($"queueName : {routingKey}");

                channel.QueueBind(queue: queueName, exchange: ExchangeNames.DIRECT_EXCHANGE_NAME, routingKey: routingKey);

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