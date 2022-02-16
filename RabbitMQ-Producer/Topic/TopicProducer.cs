using RabbitMQ_Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Producer.Topic
{
    public class TopicProducer
    {
        public static void SendMessage()
        {
            using var connection = RabbitMQHelper.GetConnection();
            using var channel = connection.CreateModel();

            //声明交换机，direct(路由)模式
            channel.ExchangeDeclare("topic_exchange", "topic", false, false, null);

            //声明队列
            channel.QueueDeclare("topic_queue1", false, false, false, null);
            channel.QueueDeclare("topic_queue2", false, false, false, null);
            channel.QueueDeclare("topic_queue3", false, false, false, null);

            //队列绑定交换机
            channel.QueueBind("topic_queue1", "topic_exchange", "user.insert", null);
            channel.QueueBind("topic_queue2", "topic_exchange", "user.update", null);
            channel.QueueBind("topic_queue3", "topic_exchange", "user.*", null);

            for (var i=0;i<10;i++)
            {
                string message = $"Rabbit topic Message{i + 1}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("topic_exchange", "user.insert", false,null,body);
            }
        }
    }
}
