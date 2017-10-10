namespace AkkaPlayground.Messages
{
    /// <summary>
    /// A Message representing current CPU and Memory usage
    /// </summary>
    public class UsageMessage
    {
        /// <summary>
        /// A Default empty instance
        /// </summary>
        public static readonly UsageMessage Empty = new UsageMessage(0, 0);

        /// <summary>
        /// The percentage of CPU currently in use
        /// </summary>
        public double CpuPercent { get; private set; }

        /// <summary>
        /// The number of bytes of memory currently used
        /// </summary>
        public double BytesUsed { get; private set; }

        /// <summary>
        /// Initialises the object using the given values
        /// </summary>
        /// <param name="cpu">CPU Usage in Percent</param>
        /// <param name="memory">Memory usage in bytes</param>
        public UsageMessage(double cpu, double memory)
        {
            CpuPercent = cpu;
            BytesUsed = memory;
        }
    }
}
