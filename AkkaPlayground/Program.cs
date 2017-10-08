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
        private const string OWIN_URL = "http://localhost:8080";
        private static ActorSystem actorSystem;
        private static IActorRef _root;
        private static IDisposable _webApp;

        /// <summary>
        /// Entry point for application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            StartAkka();
            StartWebApp();

            Console.ReadLine();

            if (_webApp != null)
            {
                _webApp.Dispose();
            }
        }

        /// <summary>
        /// Configures the Akka Actor Syste,
        /// </summary>
        private static void StartAkka()
        {
            actorSystem = ActorSystem.Create("akka-playground");
            _root = actorSystem.ActorOf(Props.Create<RootActor>(), "root");
        }

        /// <summary>
        /// Configures the Owin SignalR Web App
        /// </summary>
        private static void StartWebApp()
        {
            _webApp = Microsoft.Owin.Hosting.WebApp.Start<Startup>(OWIN_URL);
            Console.WriteLine($"Web Server started at {OWIN_URL}");
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
