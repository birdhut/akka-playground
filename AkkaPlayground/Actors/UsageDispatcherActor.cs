using Akka.Actor;
using AkkaPlayground.Hubs;
using AkkaPlayground.Messages;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AkkaPlayground.Actors
{
    /// <summary>
    /// Actor that dispatches usage information via SignalR
    /// </summary>
    public class UsageDispatcherActor : ReceiveActor
    {
        /// <summary>
        /// Constant CPU key
        /// </summary>
        private const string CPU = "CPU";

        /// <summary>
        /// Constant Memory Key
        /// </summary>
        private const string MEM = "MEM";

        IHubConnectionContext<dynamic> _clients;
        ActorSelection _output;
        private ImmutableDictionary<string, double> _stats;

        /// <summary>
        /// Initialises the object
        /// </summary>
        public UsageDispatcherActor()
        {
            _stats = new Dictionary<string, double>() { { CPU, 0 }, { MEM, 1 } }.ToImmutableDictionary();

            _clients = GlobalHost.ConnectionManager.GetHubContext<UsageHub>().Clients;
            Receive<CpuMessage>(m => UpdateUsage(m));
            Receive<MemoryMessage>(m => UpdateUsage(m));

            _output = Program.FindOutput();
            _output.Tell(new SimpleMessage("UsageDispatcherActor created"));
        }

        /// <summary>
        /// Updates the usage statistics based on the passed message
        /// </summary>
        /// <typeparam name="T">The type (supports <see cref="CpuMessage"/> and <see cref="MemoryMessage"/>)</typeparam>
        /// <param name="msg">The message to be processed</param>
        private void UpdateUsage<T>(T msg) where T : class
        {
            double currentVal, newval;
            string updateKey;
            if (msg is CpuMessage)
            {
                currentVal = _stats[CPU];
                newval = (msg as CpuMessage).CpuPercent;
                updateKey = CPU;
            }
            else if (msg is MemoryMessage)
            {
                currentVal = _stats[MEM];
                newval = (msg as MemoryMessage).BytesUsed;
                updateKey = MEM;
            }
            else
            {
                _output.Tell(new SimpleMessage($"UsageDispatcherActor received unknown message type: {msg.GetType().Name}"));
                return;
            }

            if (currentVal != newval)
            {
                // Update the immutable dictionary
                _stats = _stats.Remove(updateKey);
                _stats = _stats.Add(updateKey, newval);

                // Create a new message to broadcast
                BroadcastUsage(new UsageMessage(_stats[CPU], _stats[MEM]));
            }
        }

        /// <summary>
        /// Distributes the message via SignalR
        /// </summary>
        /// <param name="usage">The usage stats to send</param>
        private void BroadcastUsage(UsageMessage usage)
        {
            _clients.All.updateUsage(usage);
            _output.Tell(new SimpleMessage($"UsageDispatchActor created new broadcast with Cpu={usage.CpuPercent} and Memory={usage.BytesUsed}"));
        }
    }
}
