using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare("mesajkuyrugu", false, false, false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, e) =>
                {
                    var msg = Encoding.UTF8.GetString(e.Body.Span);
                    Console.WriteLine("Mesaj alındı: " + msg);
                };

                consumer.Registered += (sender, e) =>
                {
                    Console.WriteLine("Registered.");
                };

                /// autoAck : Kuruktan alınan mesajın silinip silinmemesini sağlıyor. 
                /// Bazen kuyruktan alınan mesaj işlenirken beklenmeyen hatalarla karşılaşılabiliyor. 
                /// O yüzden mesajı başarılı bir şekilde işlemeksizin kuyruktan silinmesini pek önermeyiz.
                channel.BasicConsume("mesajkuyrugu", false, consumer);
            }

            Console.ReadKey();
        }
    }
}