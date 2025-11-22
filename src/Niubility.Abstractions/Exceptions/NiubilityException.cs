using System;

namespace Niubility.Core
{
    public abstract class NiubilityException : Exception
    {
        protected string _Message;

        public abstract string ErrorCode { get; }
        public abstract int HttpStatusCode { get; }
        public sealed override string Message { get => _Message; }

        internal NiubilityException(string message, Exception innerException = null)
            : base(null, innerException)
        {
            _Message = message;
        }
    }
}