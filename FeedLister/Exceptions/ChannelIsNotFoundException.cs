using System;
using System.Runtime.Serialization;

namespace FeedLister.Exceptions
{
    [Serializable]
    internal class ChannelIsNotFoundException : Exception
    {
        public ChannelIsNotFoundException()
        {
        }

        public ChannelIsNotFoundException(string message) : base(message)
        {
        }

        public ChannelIsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChannelIsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}