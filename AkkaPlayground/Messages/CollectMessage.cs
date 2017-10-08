using System.Collections.Generic;
using Akka.Actor;
using System;
namespace AkkaPlayground.Messages
{
    
    /// <summary>
    /// A message indicating a particular collection of stats should be performs
    /// </summary>
    public class CollectMessage
    {
        /// <summary>
        /// The output actors to forward results to
        /// </summary>
        public IEnumerable<IActorRef> Receivers { get; private set; }

        /// <summary>
        /// The type of collection that this is for
        /// </summary>
        public CollectionType Collect { get; private set; }

        /// <summary>
        /// The time that this message was created
        /// </summary>
        public DateTime CollectUtc { get; private set; }


        /// <summary>
        /// Initialises the object using the given <see cref="IActorRef"/> receiver and <see cref="CollectionType"/>
        /// </summary>
        /// <param name="receivers">The receivers that will output result</param>
        /// <param name="type">The type of stats to collect</param>
        public CollectMessage(IEnumerable<IActorRef> receivers, CollectionType type)
        {
            Receivers = receivers;
            Collect = type;
            CollectUtc = DateTime.UtcNow;
        }
    }
}
