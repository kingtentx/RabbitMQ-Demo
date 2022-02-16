using RabbitMQ_Producer.Direct;
using RabbitMQ_Producer.FanOut;
using RabbitMQ_Producer.Sample;
using RabbitMQ_Producer.Topic;
using System;

namespace RabbitMQ_Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            SamplePreducer.SendMessage();
            //FanoutProducer.SendMessage();
            //DirectProducer.SendMessage();
            //TopicProducer.SendMessage();
        }
    }
}
