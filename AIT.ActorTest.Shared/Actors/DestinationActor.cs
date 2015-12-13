using AIT.ActorTest.Shared.Message;
using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.ActorTest.Shared.Actors
{
    public class DestinationActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        public DestinationActor()
        {
            Receive<ConfirmableMessage>(cm =>
            {
                _log.Info("=> Received: {0} - with message {1} from {2}", cm.Data.Id, cm.Data.Message, Sender.Path);
                Sender.Tell(new ConfirmationMessage(cm.DeliveryId, new ReplyMessage(cm.Data.Id, cm.Data.Message)));
            });

            Receive<UnhandledMessage>(um =>
            {
                _log.Warning("=> Received: {0} - for {1} from {2}", um.Message, um.Recipient, Sender.Path);
            });

            Receive<TestMessage>(message => { _log.Info("=> Received: {0} - with message {1} from {2}", message.Id, message.Message, Sender.Path); });
        }
    }
}
