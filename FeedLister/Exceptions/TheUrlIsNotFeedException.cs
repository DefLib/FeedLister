using System;
using System.Runtime.Serialization;

namespace FeedLister.Exceptions
{
    [Serializable]
    internal class TheUrlIsNotFeedException : Exception
    {
        readonly string message = "This is NOT Feed";

        public TheUrlIsNotFeedException()
        {
        }

        public TheUrlIsNotFeedException(string message) : base(message)
        {
        }

        public TheUrlIsNotFeedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TheUrlIsNotFeedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}