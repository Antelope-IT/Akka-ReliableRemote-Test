using AIT.ActorTest.Shared.Actors;
using AIT.ActorTest.Shared.Message;
using Akka.Actor;
using Akka.Routing;
using System;

namespace AIT.ActorTest.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ActorSystem clientSystem = ActorSystem.Create("ClientSystem"))
            {
                // This will work
                //StandardRemoteDeploymentWithRouters(clientSystem);

                // This will work
                //ReliableLocalDeploymentWithDestinationRouters(clientSystem);

                // This doesn't work - messages are sent to dead letter
                ReliableRemoteDeployment(clientSystem);
            }
        }


        private static void StandardRemoteDeploymentWithRouters(ActorSystem clientSystem)
        {
            int messageCount = 0;

            var remote = clientSystem.ActorOf(Props.Create(() => new TestActor()).WithRouter(FromConfig.Instance), "remoteactor");
            var local = clientSystem.ActorOf(Props.Create(() => new TestActor()).WithRouter(FromConfig.Instance), "localactor");
            Console.WriteLine("Click Return to send message to remote server...");
            Console.ReadLine();

            do
            {
                var actorToUse = clientSystem.ActorSelection(local.Path).ResolveOne(TimeSpan.FromSeconds(5)).ContinueWith(result =>
                {
                    remote.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++), result.Result);
                    remote.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++), result.Result);
                    remote.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++), result.Result);
                    remote.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++), result.Result);
                    remote.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++), result.Result);
                    remote.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++), result.Result);
                });
                Console.WriteLine("Click Return to send message to remote server...");
                Console.ReadLine();
            } while (true);
        }

        private static void ReliableLocalDeploymentWithDestinationRouters(ActorSystem clientSystem)
        {
            int messageCount = 0;

            var remote = clientSystem.ActorOf(Props.Create(() => new DestinationActor()).WithRouter(FromConfig.Instance), "localdestination");
            var local = clientSystem.ActorOf(Props.Create(() => new GateWayActor(remote.Path)), "localgateway");

            Console.WriteLine("Click Return to send message reliable messages locally...");
            Console.ReadLine();


            do
            {
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));

                Console.WriteLine("Click Return to send message reliable messages locally...");
                Console.ReadLine();

            } while (true);

        }

        private static void ReliableRemoteDeployment(ActorSystem clientSystem)
        {
            int messageCount = 0;

            var remote = clientSystem.ActorOf(Props.Create(() => new DestinationActor()), "remotedestination");
            var local = clientSystem.ActorOf(Props.Create(() => new GateWayActor(remote.Path)), "gateway");

            Console.WriteLine("Click Return to send message reliable messages remotely...");
            Console.ReadLine();


            // This succeeds - the remote actor exists
            var actorToUse = clientSystem.ActorSelection(local.Path).ResolveOne(TimeSpan.FromSeconds(5)).ContinueWith(result =>
            {
                remote.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++), result.Result);
            });

            do
            {
                // This will fail
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));
                local.Tell(new TestMessage(Guid.NewGuid(), "This is message: " + messageCount++));

                Console.WriteLine("Click Return to send message reliable messages remotely...");
                Console.ReadLine();

            } while (true);
        }
    }
}
