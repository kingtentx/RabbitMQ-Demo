using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Common.Model
{
    public class MsgResult
    {
        public int MessageId { get; set; }

        public object Data { get; set; }
    }
}
