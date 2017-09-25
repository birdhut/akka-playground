using Akka.Actor;
using AkkaPlayground.Messages;
using System;

namespace AkkaPlayground.Actors
{
    /// <summary>
    /// Outputs messages to the console
    /// </summary>
    public class OutputWriterActor : ReceiveActor
    {
        /// <summary>
        /// Initialises the object to respond to <see cref="MemoryMessage"/>, <see cref="CpuMessage"/> and
        /// <see cref="SimpleMessage"/> messages
        /// </summary>
        public OutputWriterActor()
        {
            Receive<MemoryMessage>(m => OutputMessage($"Memory: {m.BytesUsed} bytes"));
            Receive<CpuMessage>(m => OutputMessage($"CPU: {m.CpuPercent:P}"));
            Receive<SimpleMessage>(m => OutputMessage($"Simple Message: {m.Message}"));
        }

        /// <summary>
        /// Outputs a message to the console
        /// </summary>
        /// <param name="message">The message to output</param>
        private void OutputMessage(string message)
        {
            Console.WriteLine($"OutputWriter Received at {DateTime.UtcNow:dd/MM/yyyy HH:mm:sss} - {message}");
        }
    }
}
