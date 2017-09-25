using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaPlayground.Messages
{
    /// <summary>
    /// Represents a type of statistic collection
    /// </summary>
    public enum CollectionType
    {
        /// <summary>
        /// The memory bytes in use
        /// </summary>
        Memory = 0,

        /// <summary>
        /// The Cpu percent in use
        /// </summary>
        CPU = 1
    }
}
