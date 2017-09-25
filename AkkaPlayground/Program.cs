using System;
using Akka.Actor;
using AkkaPlayground.Actors;


namespace AkkaPlayground
{
    /// <summary>
    /// Represents the program
    /// </summary>
    internal class Program
    {
        private static ActorSystem actorSystem;
        private static IActorRef _root;

        /// <summary>
        /// Entry point for application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            actorSystem = ActorSystem.Create("akka-playground");

            _root = actorSystem.ActorOf(Props.Create<RootActor>(), "root");

            Console.ReadLine();


        }

        /// <summary>
        /// Gets the Output Actor from the Syste,
        /// </summary>
        /// <returns><see cref="ActorSelection"/></returns>
        internal static ActorSelection FindOutput()
        {
            return actorSystem.ActorSelection("/user/root/output");
        }
    }
}
