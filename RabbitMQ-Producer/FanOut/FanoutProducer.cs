using RabbitMQ_Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Producer.FanOut
{
    public class FanoutProducer
    {
        public static void SendMessage()
        {
            using var connection = RabbitMQHelper.GetConnection();
            using var channel = connection.CreateModel();

            //声明交换机
            channel.ExchangeDeclare("fanout_exchange","fanout",false,false,null);

            //声明队列
            channel.QueueDeclare("fanout_queue1",false,false,false,null);
            channel.QueueDeclare("fanout_queue2",false,false,false,null);
            channel.QueueDeclare("fanout_queue3",false,false,false,null);

            //队列绑定交换机
            channel.QueueBind("fanout_queue1", "fanout_exchange","",null);
            channel.QueueBind("fanout_queue2", "fanout_exchange","",null);
            channel.QueueBind("fanout_queue3", "fanout_exchange","",null);

            for (int i=0;i<10;i++)
            {
                var message = $"Rabbit Fanout Message zhao{i+1}";
                var body = Encoding.UTF8.GetBytes(message);

                //如果routingkey名称和队列名称相同就是用默认交换机，前提是没有声明交换机
                channel.BasicPublish("fanout_exchange","",false,null,body);
                Console.WriteLine(message);
            }
        }
    }
}
