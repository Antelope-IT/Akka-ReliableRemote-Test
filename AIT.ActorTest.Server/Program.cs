using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.ActorTest.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ActorSystem serverSystem = ActorSystem.Create("ServerSystem"))
            {
                Console.WriteLine("Waiting for messages..");
                Console.ReadLine();
            }
        }
    }
}
