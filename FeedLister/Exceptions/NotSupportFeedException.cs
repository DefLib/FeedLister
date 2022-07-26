using System;
using System.Runtime.Serialization;

namespace FeedLister.Exceptions
{
    [Serializable]
    internal class NotSupportFeedException : Exception
    {
        string message = "I'm Sorry. This Feed is Not Support.\n" +
            " If You Throw Issues This Repository I Create This Feed's Extractor!";

        public NotSupportFeedException()
        {
        }

        public NotSupportFeedException(string message) : base(message)
        {
        }

        public NotSupportFeedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSupportFeedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}