using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Kernel.Constants;
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
                channel.ExchangeDeclare(ExchangeNames.TOPIC_EXCHANGE_NAME, type: ExchangeType.Topic);

                string queueName = channel.QueueDeclare().QueueName;
                string routingKey = "";

                routingKey = args[0] switch
                {
                    "1" => $"*.*.Tegmen",
                    "2" => $"*.#.Yuzbasi",
                    "3" => $"#.Binbasi.#",
                    "4" => $"Asker.Subay.Tegmen"
                };

                channel.QueueBind(queue: queueName, exchange: ExchangeNames.TOPIC_EXCHANGE_NAME, routingKey: routingKey);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queueName, false, consumer);
                consumer.Received += (sender, e) =>
                {
                    Console.WriteLine($"{routingKey} {Encoding.UTF8.GetString(e.Body.Span)} görevi alındı.");
                    channel.BasicAck(e.DeliveryTag, false);
                };

                Console.Read();
            }
        }
    }
}