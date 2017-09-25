using Akka.Actor;
using AkkaPlayground.Messages;
using System.Linq;
using System.Management;

namespace AkkaPlayground.Actors
{
    /// <summary>
    /// Collects current cpu usage information
    /// </summary>
    public class CpuCollectionActor : ReceiveActor
    {
        ActorSelection _output;

        /// <summary>
        /// Initialises the object to respond to <see cref="CollectMessage"/> of type <see cref="CollectionType.CPU"/>
        /// </summary>
        public CpuCollectionActor()
        {
            Receive<CollectMessage>(m => CollectStats(m), m => m.Collect == CollectionType.CPU);
            _output = Program.FindOutput();
            _output.Tell(new SimpleMessage("CpuCollectionActor created"));
        }

        /// <summary>
        /// Collects cpu usage stats and forwards to the <see cref="CollectMessage.Receiver"/>
        /// </summary>
        /// <param name="message"><see cref="CollectMessage"/></param>
        private void CollectStats(CollectMessage message)
        {
            _output.Tell(new SimpleMessage($"MemoryCollectionActor received collect dated {message.CollectUtc:dd/MM/yyyy HH:mm:ss}"));

            var processorSearcher = new ManagementObjectSearcher(@"root\CIMV2",
                "SELECT * FROM Win32_PerfFormattedData_Counters_ProcessorInformation");

            var obj = processorSearcher.Get().Cast<ManagementObject>().FirstOrDefault();
            
            if (obj != null)
            { 
                var percent = double.Parse(obj["PercentProcessorTime"].ToString());
                message.Receiver.Tell(new CpuMessage(percent/100));
            }
        }
    }
}
