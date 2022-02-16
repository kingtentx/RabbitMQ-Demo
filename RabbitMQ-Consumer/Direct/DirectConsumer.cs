using RabbitMQ.Client.Events;
using RabbitMQ_Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Consumer.Direct
{
    public class DirectConsumer
    {
        public static void ConsumerMessage()
        {
            using var connection = RabbitMQHelper.GetConnection();
            using var channel = connection.CreateModel();

            //声明交换机，direct(路由)模式
            channel.ExchangeDeclare("direct_exchange", "direct", false, false, null);

            //声明队列
            channel.QueueDeclare("direct_queue1", false, false, false, null);
            channel.QueueDeclare("direct_queue2", false, false, false, null);
            channel.QueueDeclare("direct_queue3", false, false, false, null);

            //队列绑定交换机
            channel.QueueBind("direct_queue1", "direct_exchange", "info", null);
            channel.QueueBind("direct_queue2", "direct_exchange", "warn", null);
            channel.QueueBind("direct_queue3", "direct_exchange", "error", null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model,ea) => 
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var routingkey = ea.RoutingKey;
                Console.WriteLine($"message is {message},routingkey=> {routingkey}");
            };

            channel.BasicConsume("direct_queue1", true,"",false,false,null,consumer);
            channel.BasicConsume("direct_queue2", true,"",false,false,null,consumer);
            channel.BasicConsume("direct_queue3", true,"",false,false,null,consumer);
        }
    }
}
