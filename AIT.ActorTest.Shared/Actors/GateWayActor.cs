using AIT.ActorTest.Shared.Message;
using Akka;
using Akka.Actor;
using Akka.Event;
using Akka.Persistence;

namespace AIT.ActorTest.Shared.Actors
{
    public class GateWayActor : AtLeastOnceDeliveryReceiveActor
    {

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public override string PersistenceId
        {
            get
            {
                return "ait-gateway";
            }
        }

        public GateWayActor(ActorPath destination)
        {
            Command<TestMessage>(test => {
                Persist(test, tm =>  Deliver(destination, deliveryId => new ConfirmableMessage(deliveryId, tm)));
            });

            Command<ConfirmationMessage>(ack =>
            {
                if (ConfirmDelivery(ack.DeliveryId))
                {
                    Persist(ack, am => ConfirmDelivery(am.DeliveryId));
                }
            });

            Command<UnconfirmedWarning>(unconfirmedWarn =>
            {
                _log.Warning("Sender got unconfirmed warning: unconfirmed deliveries count {0}",
                    unconfirmedWarn.UnconfirmedDeliveries.Length);
            });
        }


    }
}
