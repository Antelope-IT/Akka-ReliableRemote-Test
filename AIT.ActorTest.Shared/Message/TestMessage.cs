using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.ActorTest.Shared.Message
{
    public class TestMessage
    {
        public Guid Id { get; private set; }
        public string Message { get; set; }

        public TestMessage(Guid id, string message)
        {
            Id = id;
            Message = message;
        }
    }
}
