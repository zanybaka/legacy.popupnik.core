using System;

namespace Popupnik.Server.UnitTests.Base.Exceptions
{
    public sealed class UnitTestException : Exception
    {
        public UnitTestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}