using System;
using System.Runtime.Serialization;

namespace GreenClean
{
    [Serializable]
    internal class FeatureNotSupportedException : Exception
    {
        public FeatureNotSupportedException()
        {
        }

        public FeatureNotSupportedException(string message) : base(message)
        {
        }

        public FeatureNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FeatureNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}