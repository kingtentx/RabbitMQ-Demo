using RabbitMQ_Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Producer.Direct
{
    public class DirectProducer
    {
        public static void SendMessage()
        {
            using var connection = RabbitMQHelper.GetConnection();
            using var channel = connection.CreateModel();

            //声明交换机，direct(路由)模式
            channel.ExchangeDeclare("direct_exchange", "direct",false,false,null);

            //声明队列
            channel.QueueDeclare("direct_queue1", false,false,false,null);
            channel.QueueDeclare("direct_queue2", false,false,false,null);
            channel.QueueDeclare("direct_queue3", false,false,false,null);

            //队列绑定交换机
            channel.QueueBind("direct_queue1", "direct_exchange", "info",null);
            channel.QueueBind("direct_queue2", "direct_exchange", "warn",null);
            channel.QueueBind("direct_queue3", "direct_exchange", "error",null);

            for (var i=0;i<10;i++)
            {
                string message = $"Rabbit Direct info Message{i+1}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("direct_exchange", "info", false,null, body);
                channel.BasicPublish("direct_exchange", "warn", false, null, body);
            }

        }
    }
}
