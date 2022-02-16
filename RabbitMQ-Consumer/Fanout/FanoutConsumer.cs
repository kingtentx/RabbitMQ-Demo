using RabbitMQ.Client.Events;
using RabbitMQ_Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Consumer.Fanout
{
    public class FanoutConsumer
    {
        public static void ConsumerMessage()
        {
            //创建连接
            var connection = RabbitMQHelper.GetConnection();
            //创建信道
            var channel = connection.CreateModel();
            //声明队列
            channel.QueueDeclare("fanout_queue1", false, false, false, null);
            channel.QueueDeclare("fanout_queue2", false, false, false, null);
            channel.QueueDeclare("fanout_queue3", false, false, false, null);

            //队列绑定交换机
            channel.QueueBind("fanout_queue1", "fanout_exchange", "", null);
            channel.QueueBind("fanout_queue2", "fanout_exchange", "", null);
            channel.QueueBind("fanout_queue3", "fanout_exchange", "", null);

            //消费信息
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var routingKey = ea.RoutingKey;
                Console.WriteLine($"Message is:{message},routingKey is:{routingKey}");
            };

            channel.BasicConsume("fanout_queue1", true, "", false, false, null, consumer);
            channel.BasicConsume("fanout_queue2", true, "", false, false, null, consumer);
            channel.BasicConsume("fanout_queue3", true, "", false, false, null, consumer);
            Console.ReadKey();
        }
    }
}
