using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.ActorTest.Shared.Message
{
    public class ConfirmationMessage
    {
        public long DeliveryId { get; }

        public ReplyMessage Data { get; }

        public ConfirmationMessage(long deliveryId, ReplyMessage message)
        {
            DeliveryId = deliveryId;
            Data = message;
        }
        
    }
}
