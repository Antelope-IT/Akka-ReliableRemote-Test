using AIT.ActorTest.Shared.Message;
using Akka.Actor;
using Akka.Event;

namespace AIT.ActorTest.Shared.Actors
{
    public class TestActor :ReceiveActor
    {
        private readonly ILoggingAdapter log = Context.GetLogger();

        public TestActor()
        {
            Receive<TestMessage>(message => { var sender = Sender; HandleTestMessage(message, sender); });
            Receive<ReplyMessage>(message => { var sender = Sender; HandleReplyMessage(message, sender); });
        }

        private void HandleTestMessage(TestMessage message, IActorRef sender)
        {
            log.Info("=> Received: {0} - with message {1} from {2}", message.Id, message.Message, sender.Path);
            sender.Tell(new ReplyMessage(message.Id, message.Message + ": "+ Sender.Path ));
        }
        private void HandleReplyMessage(ReplyMessage message, IActorRef sender)
        {
            log.Info("=> Received: {0} - with message {1} from {2}", message.Id, message.Message, sender.Path);
        }
    }
}
