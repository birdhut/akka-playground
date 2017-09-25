namespace AkkaPlayground.Messages
{
    /// <summary>
    /// Represents a message containing Cpu usage 
    /// </summary>
    public class CpuMessage
    {
        /// <summary>
        /// The percentage of CPU currently in use
        /// </summary>
        public double CpuPercent { get; private set; }

        /// <summary>
        /// Initialises the object with the given percentage CPU usage
        /// </summary>
        /// <param name="percent">The percent of cpu usuge</param>
        public CpuMessage(double percent)
        {
            CpuPercent = percent;
        }
    }
}
