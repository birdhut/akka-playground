using Akka.Actor;
using AkkaPlayground.Messages;
using System;
using System.Linq;
using System.Management;

namespace AkkaPlayground.Actors
{
    /// <summary>
    /// Collects current memory usage information
    /// </summary>
    public class MemoryCollectionActor : ReceiveActor
    {
        private ActorSelection _output;

        /// <summary>
        /// Initialises the object to respond to <see cref="CollectMessage"/> of type <see cref="CollectionType.Memory"/>
        /// </summary>
        public MemoryCollectionActor()
        {

            Receive<CollectMessage>(m => CollectStats(m), m => m.Collect == CollectionType.Memory);

            _output = Program.FindOutput();
            _output.Tell(new SimpleMessage("MemoryCollectionActor created"));
        }


        /// <summary>
        /// Collects memory usage stats and forwards to the <see cref="CollectMessage.Receiver"/>
        /// </summary>
        /// <param name="message"><see cref="CollectMessage"/></param>
        private void CollectStats(CollectMessage message)
        {
            _output.Tell(new SimpleMessage($"MemoryCollectionActor received collect dated {message.CollectUtc:dd/MM/yyyy HH:mm:ss}"));

            var wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
            }).FirstOrDefault();

            if (memoryValues != null)
            {
                var used = (memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory);

                message.Receiver.Tell(new MemoryMessage(used));
            }
        }
    }
}
