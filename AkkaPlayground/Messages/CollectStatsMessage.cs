using Akka.Actor;
using System;

namespace AkkaPlayground.Messages
{
    /// <summary>
    /// A message that requests a statistic collection
    /// </summary>
    public class CollectStatsMessage
    {
        /// <summary>
        /// The output actor that results should be sent to
        /// </summary>
        public IActorRef ReceivingActor { get; private set; }

        /// <summary>
        /// The date that this message was created
        /// </summary>
        public DateTime DateUtc { get; private set; }


        /// <summary>
        /// Initialises the object using the given <see cref="IActorRef"/> that will receive output
        /// </summary>
        /// <param name="receiver">The output actor reference</param>
        public CollectStatsMessage(IActorRef receiver)
        {
            ReceivingActor = receiver;
            DateUtc = DateTime.UtcNow;
        }
    }
}
