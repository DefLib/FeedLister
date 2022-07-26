using System;
using System.Runtime.Serialization;

namespace FeedLister.Exceptions
{
    [Serializable]
    internal class URLisNotFoundException : Exception
    {
        public URLisNotFoundException()
        {
        }

        public URLisNotFoundException(string message) : base(message)
        {
        }

        public URLisNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected URLisNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}