using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.ActorTest.Shared.Message
{
    public class ConfirmableMessage
    {
        public long DeliveryId { get; }

        public TestMessage Data { get; }

        public ConfirmableMessage(long deliveryId, TestMessage message)
        {
            DeliveryId = deliveryId;
            Data = message;
        }
    }
}
