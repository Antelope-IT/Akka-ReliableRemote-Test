using Akka;
using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AIT.ActorTest.Shared.Actors
{
    public class MonitorActor : UntypedActor
    {
        private readonly ILoggingAdapter log = Context.GetLogger();
        private readonly IActorRef thingToWatch = ActorRefs.Nobody;
        public MonitorActor(IActorRef check)
        {
            thingToWatch = check;
            Context.Watch(check);
        }
        protected override void OnReceive(object message)
        {
            message.Match().With<Terminated>(termMsg => {
                log.Error("Actor {0} address is{1} terminated", termMsg.ActorRef, termMsg.AddressTerminated ? "" : " Not");
            });
        }

        protected override void PostStop()
        {
            Context.Unwatch(thingToWatch);
            base.PostStop();
        }
    }
}
