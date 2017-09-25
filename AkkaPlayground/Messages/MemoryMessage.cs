

namespace AkkaPlayground.Messages
{
    /// <summary>
    /// A message containing Memory usage information
    /// </summary>
    public class MemoryMessage
    {
        /// <summary>
        /// The number of bytes of memory currently used
        /// </summary>
        public double BytesUsed { get; private set; }

        /// <summary>
        /// Initialises the object with the given number of bytes currently in use
        /// </summary>
        /// <param name="bytes">The number of memory bytes currently in use</param>
        public MemoryMessage(double bytes)
        {
            BytesUsed = bytes;
        }
    }
}
