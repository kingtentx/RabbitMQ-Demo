using RabbitMQ_Consumer.Direct;
using RabbitMQ_Consumer.Fanout;
using RabbitMQ_Consumer.Sample;
using RabbitMQ_Consumer.Topic;
using System;

namespace RabbitMQ_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleComsumer.ConsumerMessage();
            //FanoutConsumer.ConsumerMessage();
            //DirectConsumer.ConsumerMessage();
            //TopicConsumer.ConsumerMessage();
        }
    }
}
