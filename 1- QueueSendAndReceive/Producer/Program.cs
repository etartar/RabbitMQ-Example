using RabbitMQ.Client;
using Shared.Kernel.Constants;
using System.Text;

namespace Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName= "localhost",
            };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                /// durable: Normal şartlarda kuyruktaki mesajların hepsi bellek üzerinde dizilirler. 
                /// Hal böyleyken RabbitMQ sunucuları bir sebepten dolayı restart atarlarsa tüm veriler kaybolabilir. 
                /// durable parametresine true değerini verirsek eğer verilerimiz güvenli bir şekilde sağlamlaştırılacak yani fiziksel hale getirilecektir.
                /// exclusive: Oluşturulacak bu kuyruğa birden fazla kanalın bağlanıp bağlanmayacağını belirtir.
                /// autoDelete: True değerine karşılık tüm mesajlar bitince kuyruğu otomaik imha eder.
                channel.QueueDeclare(QueueNames.DefaultQueueName, false, false, false);
                byte[] msg = Encoding.UTF8.GetBytes("Havagi.");
                /// exchange : Eğer exchange kullanmıyorsanız boş bırakınız. Böylece default exchange devreye girecek ve kullanılmış olacaktır.
                /// routingKey : Eğer ki default exchange kullanıyorsanız routingKey olarak oluşturduğunuz kuyruğa verdiğiniz ismin birebir aynısını veriniz.
                /// body : Gönderilecek mesajın ta kendisidir.
                channel.BasicPublish(exchange: "", routingKey: QueueNames.DefaultQueueName, body: msg);
            }
        }
    }
}