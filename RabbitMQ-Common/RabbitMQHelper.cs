using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Common
{
    public class RabbitMQHelper
    { 
        public static IConnection GetConnection()
        {
            var connectionFactory = new ConnectionFactory() { 
                HostName = "10.47.100.74",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
            return connectionFactory.CreateConnection();
        }
    }

}
