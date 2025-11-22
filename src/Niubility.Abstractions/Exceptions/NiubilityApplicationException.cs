using System;

namespace Niubility.Core
{
    public class NiubilityApplicationException : NiubilityException
    {
        public sealed override string ErrorCode { get; } = SystemErrorCodes.SystemError;
        public sealed override int HttpStatusCode { get; } = 500;

        public NiubilityApplicationException(string message, Exception innerException)
            : base(message, innerException)
        { }
        public NiubilityApplicationException(int status,
            string errerCode, string message,
            Exception innerException)
            : this(message, innerException)
        {
            HttpStatusCode = status;
            ErrorCode = errerCode;
        }

        public NiubilityApplicationException(Exception innerException)
#if DEBUG
            : base(string.Concat(innerException.Message, "\r\n\r\n", innerException.StackTrace), innerException)
#else
            : base("Application Error", innerException)
#endif
        { }
    }
}