using RabbitMQ.Client;
using RabbitMQ_Common;
using RabbitMQ_Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace RabbitMQ_Producer.Sample
{
    public class SamplePreducer
    {
        public static void SendMessage()
        {
            //创建连接
            using IConnection connection = RabbitMQHelper.GetConnection();
            //创建信道
            using var channel = connection.CreateModel();
            //创建队列
            channel.QueueDeclare("Sample_queue", false, false, false, null);

            for (int i = 0; i < 100; i++)
            {
                //string message = $"Sample_queue message:{i}";               
                //var body = Encoding.UTF8.GetBytes(message);
                //channel.BasicPublish("", "Sample_queue", false, null, body);
                //Console.WriteLine(message);

               
                var obj = new { id = i, msg = $"Sample_queue message:{i}" };
                var data = new MsgResult() { MessageId = i, Data = obj };
                //序列化消息对象，RabbitMQ并不支持复杂对象的序列化，所以对于自定义的类型需要自己序列化
                string message = JsonSerializer.Serialize(data);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "Sample_queue", false, null, body);
                Console.WriteLine(message);
            }
        }

   

    }
}
