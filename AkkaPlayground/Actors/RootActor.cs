using Akka.Actor;
using AkkaPlayground.Messages;
using System;

namespace AkkaPlayground.Actors
{
    /// <summary>
    /// The root actor under the /user actor
    /// </summary>
    public class RootActor : UntypedActor
    {
        private IActorRef _statActor;
        private IActorRef _outputActor;

        /// <summary>
        /// Refresh interval (10 seconds)
        /// </summary>
        private const int REFRESH_INTERVAL_MS = 10000;
        
        /// <summary>
        /// Initialises the object, creating a <see cref="StatActor"/> and a <see cref="OutputWriterActor"/>
        /// </summary>
        public RootActor()
        {
            
            _statActor = Context.ActorOf<StatActor>("stats");
            _outputActor = Context.ActorOf<OutputWriterActor>("output");
            Context.System.Scheduler.ScheduleTellRepeatedly(REFRESH_INTERVAL_MS, REFRESH_INTERVAL_MS, Self, new RefreshMessage(), Self);

            _outputActor.Tell(new SimpleMessage("RootActor created"));
        }

        /// <summary>
        /// Receives a <see cref="RefreshMessage"/> to refresh the stats, or set the message as unhandled
        /// </summary>
        /// <param name="message">Type of message being received</param>
        protected override void OnReceive(object message)
        {
            if (message is RefreshMessage)
            {
                _outputActor.Tell(new SimpleMessage($"RootActor triggered refresh at {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}"));

                _statActor.Tell(new CollectStatsMessage(_outputActor));
            }
            Unhandled(message);
        }

        /// <summary>
        /// Message class to trigger a refresh
        /// </summary>
        private class RefreshMessage { }

    }
}
