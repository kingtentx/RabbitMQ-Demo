using RabbitMQ.Client.Events;
using RabbitMQ_Common;
using RabbitMQ_Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RabbitMQ_Consumer.Sample
{
    public class SampleComsumer
    {
        public static void ConsumerMessage()
        {
            //创建连接
            var connection = RabbitMQHelper.GetConnection();
            //创建信道
            var channel = connection.CreateModel();
            //创建队列，消费时防止未创建队列
            channel.QueueDeclare("Sample_queue",false,false,false,null);
            //消费信息
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model,ea) => 
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var data = JsonSerializer.Deserialize<MsgResult>(message);
                var routingKey = ea.RoutingKey;
                //Console.WriteLine($"MsgId:{data.MessageId},Data is:{data.Data},routingKey is:{routingKey}");

                if (data.MessageId < 100)
                {
                    Console.WriteLine($"MsgId:{data.MessageId},Data is:{data.Data},routingKey is:{routingKey}");
                    channel.BasicAck(ea.DeliveryTag, true);
                }               
            };

            //channel.BasicConsume("Sample_queue",true,"",false, false, null,consumer);
            channel.BasicConsume("Sample_queue", false, "", false, false, null, consumer);
            Console.ReadKey();
        }
    }
}
