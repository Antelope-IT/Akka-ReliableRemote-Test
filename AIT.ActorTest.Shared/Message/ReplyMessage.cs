using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.ActorTest.Shared.Message
{
    public class ReplyMessage
    {
        public Guid Id { get; private set; }
        public string Message { get; set; }

        public ReplyMessage(Guid id, string message)
        {
            Id = id;
            Message = message;
        }
    }
}
