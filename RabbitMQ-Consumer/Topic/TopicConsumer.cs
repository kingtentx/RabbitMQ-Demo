using RabbitMQ.Client.Events;
using RabbitMQ_Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Consumer.Topic
{
    public class TopicConsumer
    {
        public static void ConsumerMessage()
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var routingkey = ea.RoutingKey;
                Console.WriteLine($"message is {message},routingkey=> {routingkey}");
            };
            channel.BasicConsume("topic_queue1", true,"",false,false,null, consumer);
            channel.BasicConsume("topic_queue3", true,"",false,false,null, consumer);
        }
    }
}
