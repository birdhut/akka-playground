
using Akka.Actor;
using AkkaPlayground.Messages;

namespace AkkaPlayground.Actors
{

    /// <summary>
    /// Actor to manage gathering stats from child actors
    /// </summary>
    public class StatActor : ReceiveActor
    {
        private IActorRef _memActor;
        private IActorRef _cpuActor;
        private ActorSelection _output;
        private IActorRef _dispatchActor;

        /// <summary>
        /// Initialises the object creating a <see cref="MemoryCollectionActor"/> and a <see cref="CpuCollectionActor"/>
        /// and responding to <see cref="CollectStatsMessage"/> messages
        /// </summary>
        public StatActor()
        {
            _memActor = Context.ActorOf<MemoryCollectionActor>("memory");
            _cpuActor = Context.ActorOf<CpuCollectionActor>("cpu");
            _dispatchActor = Context.ActorOf<UsageDispatcherActor>("dispatch");


            Receive<CollectStatsMessage>(m => RequestStats(m), m => m.ReceivingActor != null);
            _output = Program.FindOutput();
            _output.Tell(new SimpleMessage("StatActor created"));

        }

        /// <summary>
        /// Disseminates the collection of stats to child actors
        /// </summary>
        /// <param name="message"><see cref="CollectStatsMessage"/></param>
        private void RequestStats(CollectStatsMessage message)
        {
            _output.Tell(new SimpleMessage($"StatActor received collection dated {message.DateUtc:dd/MM/yyyy HH:mm:ss}"));
            IActorRef[] receivers = new IActorRef[]
            {
                _dispatchActor,
                message.ReceivingActor
            };

            _memActor.Tell(new CollectMessage(receivers, CollectionType.Memory));
            _cpuActor.Tell(new CollectMessage(receivers, CollectionType.CPU));
        }
    }
}
