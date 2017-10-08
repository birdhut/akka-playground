using AkkaPlayground.Messages;
using Microsoft.AspNet.SignalR;

namespace AkkaPlayground.Hubs
{
    /// <summary>
    /// SignalR Usage Hub
    /// </summary>
    public class UsageHub : Hub
    {
        /// <summary>
        /// Tracks a client returning a default <see cref="UsageMessage.Empty"/> value
        /// </summary>
        /// <returns><see cref="UsageMessage"/></returns>
        public UsageMessage Register()
        {
            return UsageMessage.Empty;
        }
    }
}
