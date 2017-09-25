namespace AkkaPlayground.Messages
{
    /// <summary>
    /// Represents a simple text message
    /// </summary>
    public class SimpleMessage
    {
        /// <summary>
        /// The messages
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Initialises the object with the given message
        /// </summary>
        /// <param name="msg">The message</param>
        public SimpleMessage(string msg)
        {
            Message = msg;
        }
    }
}
