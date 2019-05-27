using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {

                var config = new ProducerConfig { BootstrapServers = "" };

                // If serializers are not specified, default serializers from
                // `Confluent.Kafka.Serializers` will be automatically used where
                // available. Note: by default strings are encoded as UTF8.
                using (var p = new ProducerBuilder<Null, string>(config).Build())
                {
                    try
                    {
                        var dr = await p.ProduceAsync("test", new Message<Null, string> { Value = "test" });
                        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    }
                }

            }).GetAwaiter().GetResult();

        }

        //static void Main(string[] args)
        //{
        //    // The Kafka endpoint address
        //    string kafkaEndpoint = "wn0-accuvp.mc0fzsmhtqnujgr25pxx3ce5kh.jx.internal.cloudapp.net";

        //    // The Kafka topic we'll be using
        //    string kafkaTopic = "testtopic";

        //    // Create the producer configuration
        //    var producerConfig = new Dictionary<string, object> { { "bootstrap.servers", kafkaEndpoint } };

        //    // Create the producer
        //    using (var producer = new Producer<Null, string>(producerConfig, null, new StringSerializer(Encoding.UTF8)))
        //    {
        //        // Send 10 messages to the topic
        //        for (int i = 0; i < 10; i++)
        //        {
        //            var message = $"Event {i}";
        //            var result = producer.ProduceAsync(kafkaTopic, null, message).GetAwaiter().GetResult();
        //            Console.WriteLine($"Event {i} sent on Partition: {result.Partition} with Offset: {result.Offset}");
        //        }
        //    }
        //}
    }
}
