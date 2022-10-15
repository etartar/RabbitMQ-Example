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
                channel.ExchangeDeclare("workingQueue", type: ExchangeType.Fanout);

                string queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "workingQueue", routingKey: "");

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queueName, false, consumer);
                consumer.Received += (sender, e) =>
                {
                    Thread.Sleep(int.Parse(args[0]));
                    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span) + " alındı.");
                    channel.BasicAck(e.DeliveryTag, false);
                };

                Console.Read();
            }
        }
    }
}