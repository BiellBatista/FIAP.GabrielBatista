using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace FIAP.Gabriel.Kafka
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Kafka!");

            var configProducer = new ProducerConfig()
            {
                BootstrapServers = "",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "",
                SaslPassword = ""
            };

            var configConsumer = new ConsumerConfig()
            {
                BootstrapServers = "",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "",
                SaslPassword = "",
                GroupId = "",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            string topic = "mus9pr80-fiap";

            //KafkaProducer(configProducer, topic).Wait();
            KafkaConsumer(configConsumer, topic);

            Console.ReadKey();
        }

        private static async Task KafkaProducer(ProducerConfig config, string topic)
        {
            string nome = "Gabriel Batista";

            var builder = new ProducerBuilder<string, string>(config);

            using (var producer = builder.Build())
            {
                for (int i = 0; i < 10; i++)
                {
                    var message = new Message<string, string>
                    {
                        Key = nome,
                        Value = "Mensagem de " + nome + " : " + i
                    };

                    await producer.ProduceAsync(topic, message);
                }
            }
        }

        private static void KafkaConsumer(ConsumerConfig config, string topic)
        {
            int count = 0;

            var builder = new ConsumerBuilder<string, string>(config);

            using (var consumer = builder.Build())
            {
                consumer.Subscribe(topic);

                while (true)
                {
                    var result = consumer.Consume(TimeSpan.FromSeconds(1));

                    if (result != null)
                    {
                        string texto = result.Message.Value;

                        int part = result.Partition.Value;

                        Console.WriteLine($"{count++}: recebido {texto} (Particao: {part})");
                    }
                }
            }
        }
    }
}